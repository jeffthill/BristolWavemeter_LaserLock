namespace LINQS.BristolWavemeter
{
    partial class BristolWavemeterApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.buttonShowData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelWavelength = new System.Windows.Forms.Label();
            this.bgwGetWL = new System.ComponentModel.BackgroundWorker();
            this.inputVoltage = new System.Windows.Forms.GroupBox();
            this.labelPiezoVoltage = new System.Windows.Forms.Label();
            this.buttonSmoothSet0V = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numericUpDownPIDIntegral = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numericUpDownPIDDerivative = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numericUpDownPIDProportional = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownPIDGain = new System.Windows.Forms.NumericUpDown();
            this.labelWavelengthRMS = new System.Windows.Forms.Label();
            this.buttonToggleLaserLock = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownTargetWavelength = new System.Windows.Forms.NumericUpDown();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.inputVoltage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDIntegral)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDDerivative)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDProportional)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDGain)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetWavelength)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonShowData
            // 
            this.buttonShowData.Location = new System.Drawing.Point(284, 74);
            this.buttonShowData.Name = "buttonShowData";
            this.buttonShowData.Size = new System.Drawing.Size(115, 26);
            this.buttonShowData.TabIndex = 1;
            this.buttonShowData.Text = "Show Data";
            this.buttonShowData.UseVisualStyleBackColor = true;
            this.buttonShowData.Click += new System.EventHandler(this.buttonShowData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelWavelength);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wavelength (nm)";
            // 
            // labelWavelength
            // 
            this.labelWavelength.AutoSize = true;
            this.labelWavelength.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWavelength.Location = new System.Drawing.Point(22, 18);
            this.labelWavelength.Name = "labelWavelength";
            this.labelWavelength.Size = new System.Drawing.Size(208, 54);
            this.labelWavelength.TabIndex = 0;
            this.labelWavelength.Text = "1550.0000";
            // 
            // bgwGetWL
            // 
            this.bgwGetWL.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGetWL_DoWork);
            this.bgwGetWL.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwGetWL_ProgressChanged);
            this.bgwGetWL.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwGetWL_RunWorkerCompleted);
            // 
            // inputVoltage
            // 
            this.inputVoltage.Controls.Add(this.labelPiezoVoltage);
            this.inputVoltage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputVoltage.Location = new System.Drawing.Point(19, 216);
            this.inputVoltage.Name = "inputVoltage";
            this.inputVoltage.Size = new System.Drawing.Size(240, 80);
            this.inputVoltage.TabIndex = 3;
            this.inputVoltage.TabStop = false;
            this.inputVoltage.Text = "Input Voltage (V)";
            // 
            // labelPiezoVoltage
            // 
            this.labelPiezoVoltage.AutoSize = true;
            this.labelPiezoVoltage.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPiezoVoltage.Location = new System.Drawing.Point(51, 18);
            this.labelPiezoVoltage.Name = "labelPiezoVoltage";
            this.labelPiezoVoltage.Size = new System.Drawing.Size(120, 54);
            this.labelPiezoVoltage.TabIndex = 1;
            this.labelPiezoVoltage.Text = "0.000";
            // 
            // buttonSmoothSet0V
            // 
            this.buttonSmoothSet0V.Location = new System.Drawing.Point(284, 247);
            this.buttonSmoothSet0V.Name = "buttonSmoothSet0V";
            this.buttonSmoothSet0V.Size = new System.Drawing.Size(111, 23);
            this.buttonSmoothSet0V.TabIndex = 4;
            this.buttonSmoothSet0V.Text = "Set 0 V";
            this.buttonSmoothSet0V.UseVisualStyleBackColor = true;
            this.buttonSmoothSet0V.Click += new System.EventHandler(this.buttonSmoothSet0V_Click);
            // 
            // chart1
            // 
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea7.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea7);
            this.chart1.Location = new System.Drawing.Point(414, 42);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(434, 170);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Location = new System.Drawing.Point(1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 450);
            this.tabControl1.TabIndex = 6;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.groupBox4);
            this.tabMain.Controls.Add(this.labelWavelengthRMS);
            this.tabMain.Controls.Add(this.buttonToggleLaserLock);
            this.tabMain.Controls.Add(this.groupBox2);
            this.tabMain.Controls.Add(this.buttonShowData);
            this.tabMain.Controls.Add(this.chart1);
            this.tabMain.Controls.Add(this.groupBox1);
            this.tabMain.Controls.Add(this.buttonSmoothSet0V);
            this.tabMain.Controls.Add(this.inputVoltage);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(858, 424);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Location = new System.Drawing.Point(414, 249);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(346, 127);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PID Parameters";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.numericUpDownPIDIntegral);
            this.groupBox7.Location = new System.Drawing.Point(175, 74);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(150, 49);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Ki (Hz)";
            // 
            // numericUpDownPIDIntegral
            // 
            this.numericUpDownPIDIntegral.DecimalPlaces = 3;
            this.numericUpDownPIDIntegral.Location = new System.Drawing.Point(14, 20);
            this.numericUpDownPIDIntegral.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownPIDIntegral.Name = "numericUpDownPIDIntegral";
            this.numericUpDownPIDIntegral.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownPIDIntegral.TabIndex = 0;
            this.numericUpDownPIDIntegral.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPIDIntegral.ValueChanged += new System.EventHandler(this.numericUpDownPIDIntegral_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numericUpDownPIDDerivative);
            this.groupBox6.Location = new System.Drawing.Point(19, 74);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(150, 49);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Kd (ms)";
            // 
            // numericUpDownPIDDerivative
            // 
            this.numericUpDownPIDDerivative.DecimalPlaces = 1;
            this.numericUpDownPIDDerivative.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownPIDDerivative.Location = new System.Drawing.Point(14, 20);
            this.numericUpDownPIDDerivative.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownPIDDerivative.Name = "numericUpDownPIDDerivative";
            this.numericUpDownPIDDerivative.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownPIDDerivative.TabIndex = 0;
            this.numericUpDownPIDDerivative.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPIDDerivative.ValueChanged += new System.EventHandler(this.numericUpDownPIDDerivative_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numericUpDownPIDProportional);
            this.groupBox5.Location = new System.Drawing.Point(175, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(150, 49);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Kp";
            // 
            // numericUpDownPIDProportional
            // 
            this.numericUpDownPIDProportional.DecimalPlaces = 2;
            this.numericUpDownPIDProportional.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownPIDProportional.Location = new System.Drawing.Point(14, 20);
            this.numericUpDownPIDProportional.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownPIDProportional.Name = "numericUpDownPIDProportional";
            this.numericUpDownPIDProportional.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownPIDProportional.TabIndex = 0;
            this.numericUpDownPIDProportional.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numericUpDownPIDProportional.ValueChanged += new System.EventHandler(this.numericUpDownPIDProportional_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownPIDGain);
            this.groupBox3.Location = new System.Drawing.Point(19, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 49);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gain (V/nm)";
            // 
            // numericUpDownPIDGain
            // 
            this.numericUpDownPIDGain.Location = new System.Drawing.Point(14, 20);
            this.numericUpDownPIDGain.Name = "numericUpDownPIDGain";
            this.numericUpDownPIDGain.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownPIDGain.TabIndex = 0;
            this.numericUpDownPIDGain.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownPIDGain.ValueChanged += new System.EventHandler(this.numericUpDownPIDGain_ValueChanged);
            // 
            // labelWavelengthRMS
            // 
            this.labelWavelengthRMS.AutoSize = true;
            this.labelWavelengthRMS.Location = new System.Drawing.Point(568, 233);
            this.labelWavelengthRMS.Name = "labelWavelengthRMS";
            this.labelWavelengthRMS.Size = new System.Drawing.Size(33, 13);
            this.labelWavelengthRMS.TabIndex = 8;
            this.labelWavelengthRMS.Text = "-.- pm";
            // 
            // buttonToggleLaserLock
            // 
            this.buttonToggleLaserLock.BackColor = System.Drawing.Color.Transparent;
            this.buttonToggleLaserLock.Location = new System.Drawing.Point(290, 146);
            this.buttonToggleLaserLock.Name = "buttonToggleLaserLock";
            this.buttonToggleLaserLock.Size = new System.Drawing.Size(75, 23);
            this.buttonToggleLaserLock.TabIndex = 7;
            this.buttonToggleLaserLock.Text = "Lock";
            this.buttonToggleLaserLock.UseVisualStyleBackColor = false;
            this.buttonToggleLaserLock.Click += new System.EventHandler(this.buttonToggleLaserLock_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownTargetWavelength);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 80);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Wavelength (nm)";
            // 
            // numericUpDownTargetWavelength
            // 
            this.numericUpDownTargetWavelength.DecimalPlaces = 4;
            this.numericUpDownTargetWavelength.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownTargetWavelength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownTargetWavelength.Location = new System.Drawing.Point(15, 14);
            this.numericUpDownTargetWavelength.Maximum = new decimal(new int[] {
            1570,
            0,
            0,
            0});
            this.numericUpDownTargetWavelength.Minimum = new decimal(new int[] {
            1520,
            0,
            0,
            0});
            this.numericUpDownTargetWavelength.Name = "numericUpDownTargetWavelength";
            this.numericUpDownTargetWavelength.Size = new System.Drawing.Size(211, 61);
            this.numericUpDownTargetWavelength.TabIndex = 9;
            this.numericUpDownTargetWavelength.Value = new decimal(new int[] {
            15500000,
            0,
            0,
            262144});
            this.numericUpDownTargetWavelength.ValueChanged += new System.EventHandler(this.numericUpDownTargetWavelength_ValueChanged);
            // 
            // tabSettings
            // 
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(858, 424);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // BristolWavemeterApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "BristolWavemeterApplication";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.inputVoltage.ResumeLayout(false);
            this.inputVoltage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDIntegral)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDDerivative)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDProportional)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPIDGain)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetWavelength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShowData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelWavelength;
        private System.ComponentModel.BackgroundWorker bgwGetWL;
        private System.Windows.Forms.GroupBox inputVoltage;
        private System.Windows.Forms.Label labelPiezoVoltage;
        private System.Windows.Forms.Button buttonSmoothSet0V;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonToggleLaserLock;
        private System.Windows.Forms.NumericUpDown numericUpDownTargetWavelength;
        private System.Windows.Forms.Label labelWavelengthRMS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownPIDGain;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown numericUpDownPIDIntegral;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numericUpDownPIDDerivative;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericUpDownPIDProportional;
    }
}

