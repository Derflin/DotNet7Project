
namespace SensorDataGen
{
    partial class SensorDataGenForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startBt = new System.Windows.Forms.Button();
            this.stopBt = new System.Windows.Forms.Button();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.sensorsGB = new System.Windows.Forms.GroupBox();
            this.sensorsLB = new System.Windows.Forms.ListView();
            this.addSensorGB = new System.Windows.Forms.GroupBox();
            this.sensorsNumberNUD = new System.Windows.Forms.NumericUpDown();
            this.sensorsNumberLabel = new System.Windows.Forms.Label();
            this.addSensorAddBt = new System.Windows.Forms.Button();
            this.addSensorAdvancePanel = new System.Windows.Forms.Panel();
            this.advanceMaxNUD = new System.Windows.Forms.NumericUpDown();
            this.advanceMinNUD = new System.Windows.Forms.NumericUpDown();
            this.advanceDataPerSecNUD = new System.Windows.Forms.NumericUpDown();
            this.advanceDataPerSecLabel = new System.Windows.Forms.Label();
            this.advanceMinLabel = new System.Windows.Forms.Label();
            this.advanceMaxLabel = new System.Windows.Forms.Label();
            this.addSensorAdvanceCheck = new System.Windows.Forms.CheckBox();
            this.addSensorTypeCB = new System.Windows.Forms.ComboBox();
            this.addSensorTypeLabel = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.sensorsGB.SuspendLayout();
            this.addSensorGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensorsNumberNUD)).BeginInit();
            this.addSensorAdvancePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advanceMaxNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advanceMinNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advanceDataPerSecNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // startBt
            // 
            this.startBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startBt.Location = new System.Drawing.Point(594, 14);
            this.startBt.Name = "startBt";
            this.startBt.Size = new System.Drawing.Size(94, 29);
            this.startBt.TabIndex = 1;
            this.startBt.Text = "Start";
            this.startBt.UseVisualStyleBackColor = true;
            this.startBt.Click += new System.EventHandler(this.startBt_Click);
            // 
            // stopBt
            // 
            this.stopBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopBt.Enabled = false;
            this.stopBt.Location = new System.Drawing.Point(694, 14);
            this.stopBt.Name = "stopBt";
            this.stopBt.Size = new System.Drawing.Size(94, 29);
            this.stopBt.TabIndex = 2;
            this.stopBt.Text = "Stop";
            this.stopBt.UseVisualStyleBackColor = true;
            this.stopBt.Click += new System.EventHandler(this.stopBt_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.errorLabel);
            this.bottomPanel.Controls.Add(this.startBt);
            this.bottomPanel.Controls.Add(this.stopBt);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 395);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(800, 55);
            this.bottomPanel.TabIndex = 3;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(12, 18);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(38, 25);
            this.errorLabel.TabIndex = 3;
            this.errorLabel.Text = "Error";
            this.errorLabel.UseCompatibleTextRendering = true;
            this.errorLabel.Visible = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.sensorsGB);
            this.mainPanel.Controls.Add(this.addSensorGB);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(800, 395);
            this.mainPanel.TabIndex = 4;
            // 
            // sensorsGB
            // 
            this.sensorsGB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sensorsGB.Controls.Add(this.sensorsLB);
            this.sensorsGB.Location = new System.Drawing.Point(345, 12);
            this.sensorsGB.Name = "sensorsGB";
            this.sensorsGB.Size = new System.Drawing.Size(443, 377);
            this.sensorsGB.TabIndex = 1;
            this.sensorsGB.TabStop = false;
            this.sensorsGB.Text = "Sensory";
            // 
            // sensorsLB
            // 
            this.sensorsLB.HideSelection = false;
            this.sensorsLB.Location = new System.Drawing.Point(6, 20);
            this.sensorsLB.Margin = new System.Windows.Forms.Padding(0);
            this.sensorsLB.MultiSelect = false;
            this.sensorsLB.Name = "sensorsLB";
            this.sensorsLB.Size = new System.Drawing.Size(431, 351);
            this.sensorsLB.TabIndex = 0;
            this.sensorsLB.UseCompatibleStateImageBehavior = false;
            this.sensorsLB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.sensorLB_MouseDoubleClick);
            // 
            // addSensorGB
            // 
            this.addSensorGB.Controls.Add(this.sensorsNumberNUD);
            this.addSensorGB.Controls.Add(this.sensorsNumberLabel);
            this.addSensorGB.Controls.Add(this.addSensorAddBt);
            this.addSensorGB.Controls.Add(this.addSensorAdvancePanel);
            this.addSensorGB.Controls.Add(this.addSensorAdvanceCheck);
            this.addSensorGB.Controls.Add(this.addSensorTypeCB);
            this.addSensorGB.Controls.Add(this.addSensorTypeLabel);
            this.addSensorGB.Location = new System.Drawing.Point(12, 12);
            this.addSensorGB.Name = "addSensorGB";
            this.addSensorGB.Size = new System.Drawing.Size(327, 377);
            this.addSensorGB.TabIndex = 0;
            this.addSensorGB.TabStop = false;
            this.addSensorGB.Text = "Dodaj sensor";
            // 
            // sensorsNumberNUD
            // 
            this.sensorsNumberNUD.Location = new System.Drawing.Point(147, 62);
            this.sensorsNumberNUD.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.sensorsNumberNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sensorsNumberNUD.Name = "sensorsNumberNUD";
            this.sensorsNumberNUD.Size = new System.Drawing.Size(132, 27);
            this.sensorsNumberNUD.TabIndex = 5;
            this.sensorsNumberNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // sensorsNumberLabel
            // 
            this.sensorsNumberLabel.AutoSize = true;
            this.sensorsNumberLabel.Location = new System.Drawing.Point(6, 64);
            this.sensorsNumberLabel.Name = "sensorsNumberLabel";
            this.sensorsNumberLabel.Size = new System.Drawing.Size(120, 20);
            this.sensorsNumberLabel.TabIndex = 4;
            this.sensorsNumberLabel.Text = "Liczba czujników";
            // 
            // addSensorAddBt
            // 
            this.addSensorAddBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addSensorAddBt.Location = new System.Drawing.Point(227, 342);
            this.addSensorAddBt.Name = "addSensorAddBt";
            this.addSensorAddBt.Size = new System.Drawing.Size(94, 29);
            this.addSensorAddBt.TabIndex = 3;
            this.addSensorAddBt.Text = "Dodaj";
            this.addSensorAddBt.UseVisualStyleBackColor = true;
            this.addSensorAddBt.Click += new System.EventHandler(this.addSensorAddBt_Click);
            // 
            // addSensorAdvancePanel
            // 
            this.addSensorAdvancePanel.Controls.Add(this.advanceMaxNUD);
            this.addSensorAdvancePanel.Controls.Add(this.advanceMinNUD);
            this.addSensorAdvancePanel.Controls.Add(this.advanceDataPerSecNUD);
            this.addSensorAdvancePanel.Controls.Add(this.advanceDataPerSecLabel);
            this.addSensorAdvancePanel.Controls.Add(this.advanceMinLabel);
            this.addSensorAdvancePanel.Controls.Add(this.advanceMaxLabel);
            this.addSensorAdvancePanel.Enabled = false;
            this.addSensorAdvancePanel.Location = new System.Drawing.Point(30, 131);
            this.addSensorAdvancePanel.Name = "addSensorAdvancePanel";
            this.addSensorAdvancePanel.Size = new System.Drawing.Size(270, 139);
            this.addSensorAdvancePanel.TabIndex = 1;
            // 
            // advanceMaxNUD
            // 
            this.advanceMaxNUD.DecimalPlaces = 2;
            this.advanceMaxNUD.Location = new System.Drawing.Point(138, 48);
            this.advanceMaxNUD.Maximum = new decimal(new int[] {
            1100,
            0,
            0,
            0});
            this.advanceMaxNUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.advanceMaxNUD.Name = "advanceMaxNUD";
            this.advanceMaxNUD.Size = new System.Drawing.Size(102, 27);
            this.advanceMaxNUD.TabIndex = 7;
            // 
            // advanceMinNUD
            // 
            this.advanceMinNUD.DecimalPlaces = 2;
            this.advanceMinNUD.Location = new System.Drawing.Point(138, 8);
            this.advanceMinNUD.Maximum = new decimal(new int[] {
            1100,
            0,
            0,
            0});
            this.advanceMinNUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.advanceMinNUD.Name = "advanceMinNUD";
            this.advanceMinNUD.Size = new System.Drawing.Size(102, 27);
            this.advanceMinNUD.TabIndex = 6;
            // 
            // advanceDataPerSecNUD
            // 
            this.advanceDataPerSecNUD.Location = new System.Drawing.Point(138, 88);
            this.advanceDataPerSecNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.advanceDataPerSecNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.advanceDataPerSecNUD.Name = "advanceDataPerSecNUD";
            this.advanceDataPerSecNUD.Size = new System.Drawing.Size(102, 27);
            this.advanceDataPerSecNUD.TabIndex = 4;
            this.advanceDataPerSecNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // advanceDataPerSecLabel
            // 
            this.advanceDataPerSecLabel.AutoSize = true;
            this.advanceDataPerSecLabel.Location = new System.Drawing.Point(10, 90);
            this.advanceDataPerSecLabel.Name = "advanceDataPerSecLabel";
            this.advanceDataPerSecLabel.Size = new System.Drawing.Size(122, 20);
            this.advanceDataPerSecLabel.TabIndex = 5;
            this.advanceDataPerSecLabel.Text = "Dane na sekundę";
            // 
            // advanceMinLabel
            // 
            this.advanceMinLabel.AutoSize = true;
            this.advanceMinLabel.Location = new System.Drawing.Point(10, 10);
            this.advanceMinLabel.Name = "advanceMinLabel";
            this.advanceMinLabel.Size = new System.Drawing.Size(72, 20);
            this.advanceMinLabel.TabIndex = 3;
            this.advanceMinLabel.Text = "Minimum";
            // 
            // advanceMaxLabel
            // 
            this.advanceMaxLabel.AutoSize = true;
            this.advanceMaxLabel.Location = new System.Drawing.Point(10, 50);
            this.advanceMaxLabel.Name = "advanceMaxLabel";
            this.advanceMaxLabel.Size = new System.Drawing.Size(81, 20);
            this.advanceMaxLabel.TabIndex = 4;
            this.advanceMaxLabel.Text = "Maksimum";
            // 
            // addSensorAdvanceCheck
            // 
            this.addSensorAdvanceCheck.AutoSize = true;
            this.addSensorAdvanceCheck.Location = new System.Drawing.Point(6, 101);
            this.addSensorAdvanceCheck.Name = "addSensorAdvanceCheck";
            this.addSensorAdvanceCheck.Size = new System.Drawing.Size(149, 24);
            this.addSensorAdvanceCheck.TabIndex = 2;
            this.addSensorAdvanceCheck.Text = "Opcje dodatkowe";
            this.addSensorAdvanceCheck.UseVisualStyleBackColor = true;
            this.addSensorAdvanceCheck.CheckedChanged += new System.EventHandler(this.addSensorAdvanceCheck_CheckedChanged);
            // 
            // addSensorTypeCB
            // 
            this.addSensorTypeCB.FormattingEnabled = true;
            this.addSensorTypeCB.Location = new System.Drawing.Point(147, 20);
            this.addSensorTypeCB.Name = "addSensorTypeCB";
            this.addSensorTypeCB.Size = new System.Drawing.Size(133, 28);
            this.addSensorTypeCB.TabIndex = 1;
            // 
            // addSensorTypeLabel
            // 
            this.addSensorTypeLabel.AutoSize = true;
            this.addSensorTypeLabel.Location = new System.Drawing.Point(6, 23);
            this.addSensorTypeLabel.Name = "addSensorTypeLabel";
            this.addSensorTypeLabel.Size = new System.Drawing.Size(86, 20);
            this.addSensorTypeLabel.TabIndex = 0;
            this.addSensorTypeLabel.Text = "Typ sensora";
            // 
            // SensorDataGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.bottomPanel);
            this.Name = "SensorDataGenForm";
            this.Text = "SensorDataGen";
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.sensorsGB.ResumeLayout(false);
            this.addSensorGB.ResumeLayout(false);
            this.addSensorGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensorsNumberNUD)).EndInit();
            this.addSensorAdvancePanel.ResumeLayout(false);
            this.addSensorAdvancePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advanceMaxNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advanceMinNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advanceDataPerSecNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startBt;
        private System.Windows.Forms.Button stopBt;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox addSensorGB;
        private System.Windows.Forms.Label addSensorTypeLabel;
        private System.Windows.Forms.ComboBox addSensorTypeCB;
        private System.Windows.Forms.Panel addSensorAdvancePanel;
        private System.Windows.Forms.Label advanceDataPerSecLabel;
        private System.Windows.Forms.Label advanceMinLabel;
        private System.Windows.Forms.Label advanceMaxLabel;
        private System.Windows.Forms.CheckBox addSensorAdvanceCheck;
        private System.Windows.Forms.Button addSensorAddBt;
        private System.Windows.Forms.NumericUpDown advanceMaxNUD;
        private System.Windows.Forms.NumericUpDown advanceDataPerSecNUD;
        private System.Windows.Forms.NumericUpDown advanceMinNUD;
        private System.Windows.Forms.GroupBox sensorsGB;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.NumericUpDown sensorsNumberNUD;
        private System.Windows.Forms.Label sensorsNumberLabel;
        private System.Windows.Forms.ListView sensorsLB;
    }
}

