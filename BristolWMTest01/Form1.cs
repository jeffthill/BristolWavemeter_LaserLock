using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.Runtime.InteropServices;   // a way of using unmanaged code in C# (PInvoke)
using NationalInstruments;
using NationalInstruments.DAQmx;        // to use the NI DAQ
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.ServiceModel;              // For the ServiceHost and MATLAB Interface/Service stuff

namespace LINQS.BristolWavemeter
{
    public partial class BristolWavemeterApplication : Form
    {
        // Calls to the unmanaged Bristol Wavemeter DLL
        [DllImport("CLDevIFace.dll")]
        public static extern int CLGetDllVersion(); // call to unmanaged c++ dll

        [DllImport("CLDevIFace.dll")]
        public static extern int CLOpenUSBSerialDevice(int ComNumber);

        [DllImport("CLDevIFace.dll")]
        public static extern double CLGetLambdaReading(int devHandle);

        public static BristolWavemeterApplication MainThread;                       

        Task TaskAI;
        AnalogSingleChannelReader ReaderAI;
        Task TaskAO;
        AnalogSingleChannelWriter WriterAO;

        // My variables
        private int HandleBristolWavemeter;        
        private bool IsShowData = false;
        public double Wavelength;
        public double PiezoVoltage;

        private bool _IsLockEnabled = false;
        public bool IsLockEnabled
        {
            get { return _IsLockEnabled; }
            set
            {
                if (value != IsLockEnabled)
                {
                    _IsLockEnabled = value;
                    if (!_IsLockEnabled)
                    {
                        IsLocked = false;
                        labelWavelength.ForeColor = Color.Black;
                        labelPiezoVoltage.ForeColor = Color.Black;                        
                    }
                    else
                    {
                        labelWavelength.ForeColor = IsLocked ? Color.Green : Color.Red;
                    }
                }
            }
        }
        
        private bool _IsLocked = false;
        public bool IsLocked
        {
            get { return _IsLocked; }
            set
            {
                if (value != IsLocked)
                {
                    _IsLocked = value;
                    if (IsLockEnabled)
                    {
                        labelWavelength.ForeColor = _IsLocked ? Color.Green : Color.Red;
                    }
                }
            }
        }

        private Series series1;
        private WavemeterPID LaserLockPID;
                    
        public BristolWavemeterApplication()
        {
            InitializeComponent();
            MainThread = this;
            //int dllVersion = CLGetDllVersion(); // had to change the target system to x64 (from x86) to make this work, weird (?)
            HandleBristolWavemeter = CLOpenUSBSerialDevice(Properties.Settings.Default.WavemeterCOMPort);   // returns a handle to the Bristol Wavemeter (CL device)
            bgwGetWL.WorkerSupportsCancellation = true;
            bgwGetWL.WorkerReportsProgress = true;

            TaskAI = new Task();
            TaskAI.AIChannels.CreateVoltageChannel(string.Format("dev1/ai{0}", Properties.Settings.Default.AnalogInputPiezo), "PiezoVoltageAI", AITerminalConfiguration.Differential, -3, 3, AIVoltageUnits.Volts);
            ReaderAI = new AnalogSingleChannelReader(TaskAI.Stream);

            TaskAO = new Task();
            TaskAO.AOChannels.CreateVoltageChannel(string.Format("dev1/ao{0}", Properties.Settings.Default.AnalogOutputPiezo), "PiezoVoltageAO", -3, 3, AOVoltageUnits.Volts);
            //TaskAO.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, numberOfSamples);    // I'm asking for 100 Hz, it is 1000 Hz                
            WriterAO = new AnalogSingleChannelWriter(TaskAO.Stream);

            // remoting server
            ServiceHost Host;
            try
            {
                Host = new ServiceHost(typeof(WavemeterMATLABObject), new Uri[] { new Uri(String.Format("net.tcp://localhost:{0}", Properties.Settings.Default.ServerPort)) });
                Host.AddServiceEndpoint(typeof(IWavemeterMATLABObject), new NetTcpBinding(SecurityMode.None), Properties.Settings.Default.ServerName);
                Host.Open();
            }
            catch (Exception e) { MessageBox.Show(this, e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            

            //Data series for the chart
            series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Teal,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline                
            };
            chart1.Series.Add(series1);                       
            chart1.ChartAreas[0].AxisY.Maximum = 1570;
            chart1.ChartAreas[0].AxisY.Minimum = 1520;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "###0.0000";

            //Setup the PID controller and interface
            LaserLockPID = new WavemeterPID();
            LaserLockPID.G = Properties.Settings.Default.G;
            numericUpDownPIDGain.Value = Convert.ToDecimal(Properties.Settings.Default.G);
            LaserLockPID.Kp = Properties.Settings.Default.Kp;
            numericUpDownPIDProportional.Value = Convert.ToDecimal(Properties.Settings.Default.Kp);
            LaserLockPID.Kd = Properties.Settings.Default.Kd;
            numericUpDownPIDDerivative.Value = Convert.ToDecimal(Properties.Settings.Default.Kd);
            LaserLockPID.Ki = Properties.Settings.Default.Ki;
            numericUpDownPIDIntegral.Value = Convert.ToDecimal(Properties.Settings.Default.Ki);            

            //System.Media.SoundPlayer mySoundPlayer = new System.Media.SoundPlayer(LINQS.BristolWavemeter.Properties.Resources.LINQStartupSound);
            //mySoundPlayer.Play();
        }
        
        private void buttonShowData_Click(object sender, EventArgs e)
        {                       
            if (!IsShowData)
            {
                IsShowData = true;
                buttonShowData.Text = "Stop Showing Data";
                UpdateBackGroundWorkerState();                               
            }
            else
            {
                IsShowData = false;
                buttonShowData.Text = "Show data";
                UpdateBackGroundWorkerState();
            }
           
        }

        private void UpdateBackGroundWorkerState()
        {
            bool isRunning = IsShowData || IsLockEnabled;
            if (isRunning && !bgwGetWL.IsBusy)      //EITHER should be showing data OR locked, but haven't started collecting data, so start
            {
                StartBackGroundWorker();
            }
            else if (!isRunning && bgwGetWL.IsBusy) //NOT showing data and NOT locked, but still collecting data, so stop
            {
                StopBackGroundWorker();
            }
            
        }

        private void StartBackGroundWorker()
        {
            if (bgwGetWL.IsBusy == false)
            {
                bgwGetWL.RunWorkerAsync();
            }
        }

        private void StopBackGroundWorker()
        {
            if (bgwGetWL.WorkerSupportsCancellation == true)
            {
                bgwGetWL.CancelAsync();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Maybe run shutdown code here. (i.e. release resources)                        
            bgwGetWL.CancelAsync();            
            int numberOfSamples = 1000;                
            double currentPiezo = ReaderAI.ReadSingleSample();
            double[] outputSteps = Enumerable.Range(0, numberOfSamples).Select(x => 0 + (currentPiezo - 0) * ((double)x / (numberOfSamples - 1))).Reverse().ToArray();
            WriterAO.WriteMultiSample(true, outputSteps);
            Thread.Sleep(1000); //need to wait for the sweep to finish
            Console.WriteLine("I'm out of here.");
        }

        private void bgwGetWL_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // Do the work (i.e. get the wavelength)
            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Wavelength = CLGetLambdaReading(HandleBristolWavemeter);
                    PiezoVoltage = ReaderAI.ReadSingleSample();                                            
                    if (IsLockEnabled)
                    {
                        double newPiezoVoltage = LaserLockPID.AddDataPoint(Wavelength, BristolWavemeter.Properties.Settings.Default.AcquisitionPeriod, PiezoVoltage);                                                
                        using (Task myTask = new Task())
                        {
                            myTask.AOChannels.CreateVoltageChannel(string.Format("dev1/ao{0}", Properties.Settings.Default.AnalogOutputPiezo), "PiezoVoltageAO", -3, 3, AOVoltageUnits.Volts);
                            AnalogSingleChannelWriter myWriter = new AnalogSingleChannelWriter(myTask.Stream);
                            myWriter.WriteSingleSample(true, Convert.ToDouble(newPiezoVoltage));                       
                        }
                        IsLocked = (Math.Abs(LaserLockPID.LastWavelengthError) < Properties.Settings.Default.LockThreshold);                        
                        labelPiezoVoltage.ForeColor = (Math.Abs(newPiezoVoltage) >= 2.9) ? Color.Red : Color.Black;            
                    }
                    worker.ReportProgress(5);   // report 5% progress                    
                    Thread.Sleep(BristolWavemeter.Properties.Settings.Default.AcquisitionPeriod);
                }
            }
        }

        private void bgwGetWL_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelWavelength.Text = Wavelength.ToString("#.0000");
            labelPiezoVoltage.Text = PiezoVoltage.ToString("0.0000");
            labelWavelengthRMS.Text = IsLocked ? LaserLockPID.GetRMSError().ToString("0.00") + " pm" : "-.- pm";
            if (IsShowData)
            {
                if (series1.Points.Count > BristolWavemeter.Properties.Settings.Default.WavelengthHistoryLength - 1) //Keep erroring here - some object doesn't exist on exit
                {
                    series1.Points.RemoveAt(0);
                }
                series1.Points.Add(Wavelength);
                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(series1.Points.FindMaxByValue().YValues[0]) + 0.0005;
                chart1.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(series1.Points.FindMinByValue().YValues[0]) - 0.0005;                
            }
        }

        private void bgwGetWL_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // Cancelled!
            }
            else if (e.Error != null)
            {
                // Error!
            }
            else
            {
                // Done!
            }

        }

        private void buttonSmoothSet0V_Click(object sender, EventArgs e)
        {            
            IsLockEnabled = false;
            buttonToggleLaserLock.Text = "Lock";            
            LaserLockPID.ClearHistory();
            //BeginInvoke((Action)(() =>
            //{
            double currentPiezo = ReaderAI.ReadSingleSample();
            int numberOfSamples = 1000;                             
            double[] outputSteps = Enumerable.Range(0, numberOfSamples).Select(x => 0 + (currentPiezo - 0) * ((double)x / (numberOfSamples - 1))).Reverse().ToArray();           
            WriterAO.BeginWriteMultiSample(true, outputSteps, null, null);                
            //}));
        }

        public void LaserLockState(bool isLockEnabled)
        {
            this.IsLockEnabled = isLockEnabled;
            buttonToggleLaserLock.Text = isLockEnabled ? "Unlock" : "Lock";
            UpdateBackGroundWorkerState();
        }

        private void buttonToggleLaserLock_Click(object sender, EventArgs e)
        {                        
            if (IsLockEnabled)
            {
                buttonToggleLaserLock.Text = "Lock";                
                IsLockEnabled = false;
                UpdateBackGroundWorkerState();
            }
            else
            {
                buttonToggleLaserLock.Text = "Unlock";                                
                IsLockEnabled = true;
                UpdateBackGroundWorkerState();
            }
        }

        public void SetTargetWavelength(double targetWavelength)
        {
            LaserLockPID.WavelenthSetPoint = targetWavelength;
            numericUpDownTargetWavelength.Value = Convert.ToDecimal(targetWavelength);
            chart1.ChartAreas[0].AxisY.StripLines.Clear();
            chart1.ChartAreas[0].AxisY.StripLines.Add(new StripLine()
            {
                StripWidth = 0,
                Interval = 0,
                IntervalOffset = targetWavelength,
                BorderWidth = 1,
                BorderDashStyle = ChartDashStyle.Dash,
                BorderColor = Color.Red
            });
        }

        public double GetTargetWavelength()
        {
            double targetWavelength = 0;
            if (IsLockEnabled)
            {
                targetWavelength = LaserLockPID.WavelenthSetPoint;
            }
            return targetWavelength;
        }

        public double GetWavelength()
        {
            if (!IsLockEnabled)
            {
                Wavelength = CLGetLambdaReading(HandleBristolWavemeter);
                labelWavelength.Text = Wavelength.ToString("#.0000");
            }
            return Wavelength;
        }

        private void numericUpDownTargetWavelength_ValueChanged(object sender, EventArgs e)
        {
            double targetWavelength = Convert.ToDouble(numericUpDownTargetWavelength.Value);
            SetTargetWavelength(targetWavelength);
        }

        private void numericUpDownPIDGain_ValueChanged(object sender, EventArgs e)
        {
            LaserLockPID.G = Convert.ToDouble(numericUpDownPIDGain.Value);
        }

        private void numericUpDownPIDProportional_ValueChanged(object sender, EventArgs e)
        {
            LaserLockPID.Kp = Convert.ToDouble(numericUpDownPIDProportional.Value);
        }

        private void numericUpDownPIDDerivative_ValueChanged(object sender, EventArgs e)
        {
            LaserLockPID.Kd = Convert.ToDouble(numericUpDownPIDDerivative.Value);
        }

        private void numericUpDownPIDIntegral_ValueChanged(object sender, EventArgs e)
        {
            LaserLockPID.Ki = Convert.ToDouble(numericUpDownPIDIntegral.Value)/1000;
        }
      
    }

    public class WavemeterPID
    {
        public double Kp = 0.2;
        public double Kd = 1;   // milliseconds
        public double Ki = 0.001;   // 1/milliseconds (needs to be < 0.001 it seems)
        public double G = 25;   // V/nm (need to verify this number)
        public List<double> ErrorList = new List<double>();        
        public double WavelenthSetPoint;
        public double WavelengthError = 0;
        public double LastWavelengthError = 0;
        public double IntegratedError = 0;
        private double MaxPiezoVoltage = 2.9;        
        private double MaxPiezoStep = 0.01;
        

        public WavemeterPID()
        {
        }

        public double AddDataPoint(double currentWavelength, double Dt, double PiezoVoltage)
        {            
            WavelengthError = WavelenthSetPoint - currentWavelength;    //nn
            if ((ErrorList.Count * Dt) > 20)
            {
                ErrorList.RemoveAt(0);
            }
            ErrorList.Add(WavelengthError);
            IntegratedError = ErrorList.Sum() * Dt;    //nm*ms
            //IntegratedError += WavelengthError * Dt;    //nm*ms
            double derivativeError = (WavelengthError - LastWavelengthError) / Dt;  //nm/ms
            double piezoStep = G * (Kp * WavelengthError + Ki * IntegratedError + Kd * derivativeError);
            piezoStep = Math.Sign(piezoStep) * Math.Min(MaxPiezoStep, Math.Abs(piezoStep));
            double newVoltage = PiezoVoltage + piezoStep;
            newVoltage = Math.Sign(newVoltage) * Math.Min(MaxPiezoVoltage, Math.Abs(newVoltage));
            LastWavelengthError = WavelengthError;                                    
            return newVoltage;
        }

        public double GetRMSError()
        {
            double sumOfSquares = 0;
            ErrorList.ForEach(x => sumOfSquares += Math.Pow(x,2));
            double meanOfSquares = sumOfSquares / ErrorList.Count;
            double rms_pm = Math.Sqrt(meanOfSquares) * 1000;    //rms in picometers
            return rms_pm;
        }


        public void ClearHistory()
        {
            ErrorList.Clear();
            WavelengthError = 0;
            LastWavelengthError = 0;
            IntegratedError = 0;
        }
    }

    [ServiceContract]
    interface IWavemeterMATLABObject
    {
        [OperationContract]
        string ServiceTest();

        [OperationContract]
        double GetWavelength();

        [OperationContract]
        void LaserLockSetTargetWavelength(double targetWavelength);

        [OperationContract]
        double LaserLockGetTargetWavelength();        

        [OperationContract]
        void LaserLockEnabled(bool isLockEnabled);

        [OperationContract]
        bool IsLaserLocked();

        [OperationContract]
        bool IsLockEnabled();
    }

    public class WavemeterMATLABObject : IWavemeterMATLABObject
    {
        public string ServiceTest()
        {
            return "Connected to the LINQS Bristol Wavemeter Application.";
        }

        public double GetWavelength()
        {
            return BristolWavemeterApplication.MainThread.GetWavelength();
        }

        public void LaserLockSetTargetWavelength(double targetWavelength)
        {
            BristolWavemeterApplication.MainThread.SetTargetWavelength(targetWavelength);
        }

        public double LaserLockGetTargetWavelength()
        {
            return BristolWavemeterApplication.MainThread.GetTargetWavelength();
        }

        public void LaserLockEnabled(bool isLockEnabled)
        {
            BristolWavemeterApplication.MainThread.LaserLockState(isLockEnabled);
        }

        public bool IsLaserLocked()
        {
            return BristolWavemeterApplication.MainThread.IsLocked;
        }

        public bool IsLockEnabled()
        {
            return BristolWavemeterApplication.MainThread.IsLockEnabled;
        }
    }
}
