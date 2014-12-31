namespace StandardTestBench
{
    partial class AdjSensorConfig
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_PLC_RegName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BT_PLCSave = new System.Windows.Forms.Button();
            this.CB_PLC = new System.Windows.Forms.CheckBox();
            this.BT_Pre = new System.Windows.Forms.Button();
            this.Grp_SetPara = new System.Windows.Forms.GroupBox();
            this.Grp_SetPara_Style = new System.Windows.Forms.GroupBox();
            this.DGV_Sensor = new System.Windows.Forms.DataGridView();
            this.Grp_SetPara_Set = new System.Windows.Forms.GroupBox();
            this.TB_RegName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_SavePara = new System.Windows.Forms.Button();
            this.TB_Sensor_Row = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BT_Next = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.Grp_SetPara.SuspendLayout();
            this.Grp_SetPara_Style.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Sensor)).BeginInit();
            this.Grp_SetPara_Set.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_PLC_RegName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.BT_PLCSave);
            this.groupBox2.Controls.Add(this.CB_PLC);
            this.groupBox2.Location = new System.Drawing.Point(312, 538);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 99);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PLC配置";
            // 
            // TB_PLC_RegName
            // 
            this.TB_PLC_RegName.Location = new System.Drawing.Point(178, 44);
            this.TB_PLC_RegName.Name = "TB_PLC_RegName";
            this.TB_PLC_RegName.Size = new System.Drawing.Size(141, 21);
            this.TB_PLC_RegName.TabIndex = 13;
            this.TB_PLC_RegName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "恢复出厂寄存器:";
            // 
            // BT_PLCSave
            // 
            this.BT_PLCSave.Location = new System.Drawing.Point(414, 43);
            this.BT_PLCSave.Name = "BT_PLCSave";
            this.BT_PLCSave.Size = new System.Drawing.Size(75, 23);
            this.BT_PLCSave.TabIndex = 1;
            this.BT_PLCSave.Text = "保存";
            this.BT_PLCSave.UseVisualStyleBackColor = true;
            this.BT_PLCSave.Click += new System.EventHandler(this.BT_PLCSave_Click);
            // 
            // CB_PLC
            // 
            this.CB_PLC.AutoSize = true;
            this.CB_PLC.Location = new System.Drawing.Point(18, 47);
            this.CB_PLC.Name = "CB_PLC";
            this.CB_PLC.Size = new System.Drawing.Size(114, 16);
            this.CB_PLC.TabIndex = 0;
            this.CB_PLC.Text = "PLC恢复出厂设置";
            this.CB_PLC.UseVisualStyleBackColor = true;
            // 
            // BT_Pre
            // 
            this.BT_Pre.Location = new System.Drawing.Point(976, 851);
            this.BT_Pre.Name = "BT_Pre";
            this.BT_Pre.Size = new System.Drawing.Size(102, 27);
            this.BT_Pre.TabIndex = 81;
            this.BT_Pre.Text = "上一页";
            this.BT_Pre.UseVisualStyleBackColor = true;
            this.BT_Pre.Click += new System.EventHandler(this.BT_Pre_Click);
            // 
            // Grp_SetPara
            // 
            this.Grp_SetPara.Controls.Add(this.Grp_SetPara_Style);
            this.Grp_SetPara.Controls.Add(this.Grp_SetPara_Set);
            this.Grp_SetPara.Location = new System.Drawing.Point(71, 62);
            this.Grp_SetPara.Name = "Grp_SetPara";
            this.Grp_SetPara.Size = new System.Drawing.Size(1063, 458);
            this.Grp_SetPara.TabIndex = 82;
            this.Grp_SetPara.TabStop = false;
            this.Grp_SetPara.Text = "传感器参数信息";
            // 
            // Grp_SetPara_Style
            // 
            this.Grp_SetPara_Style.Controls.Add(this.DGV_Sensor);
            this.Grp_SetPara_Style.Location = new System.Drawing.Point(6, 90);
            this.Grp_SetPara_Style.Name = "Grp_SetPara_Style";
            this.Grp_SetPara_Style.Size = new System.Drawing.Size(1051, 362);
            this.Grp_SetPara_Style.TabIndex = 1;
            this.Grp_SetPara_Style.TabStop = false;
            this.Grp_SetPara_Style.Text = "样式";
            // 
            // DGV_Sensor
            // 
            this.DGV_Sensor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Sensor.Location = new System.Drawing.Point(6, 20);
            this.DGV_Sensor.Name = "DGV_Sensor";
            this.DGV_Sensor.RowTemplate.Height = 23;
            this.DGV_Sensor.Size = new System.Drawing.Size(1038, 336);
            this.DGV_Sensor.TabIndex = 1;
            // 
            // Grp_SetPara_Set
            // 
            this.Grp_SetPara_Set.Controls.Add(this.TB_RegName);
            this.Grp_SetPara_Set.Controls.Add(this.label1);
            this.Grp_SetPara_Set.Controls.Add(this.BT_SavePara);
            this.Grp_SetPara_Set.Controls.Add(this.TB_Sensor_Row);
            this.Grp_SetPara_Set.Controls.Add(this.label4);
            this.Grp_SetPara_Set.Location = new System.Drawing.Point(6, 20);
            this.Grp_SetPara_Set.Name = "Grp_SetPara_Set";
            this.Grp_SetPara_Set.Size = new System.Drawing.Size(1051, 64);
            this.Grp_SetPara_Set.TabIndex = 0;
            this.Grp_SetPara_Set.TabStop = false;
            this.Grp_SetPara_Set.Text = "设置";
            // 
            // TB_RegName
            // 
            this.TB_RegName.Location = new System.Drawing.Point(342, 26);
            this.TB_RegName.Name = "TB_RegName";
            this.TB_RegName.Size = new System.Drawing.Size(141, 21);
            this.TB_RegName.TabIndex = 11;
            this.TB_RegName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "校准寄存器";
            // 
            // BT_SavePara
            // 
            this.BT_SavePara.Location = new System.Drawing.Point(964, 24);
            this.BT_SavePara.Name = "BT_SavePara";
            this.BT_SavePara.Size = new System.Drawing.Size(75, 23);
            this.BT_SavePara.TabIndex = 9;
            this.BT_SavePara.Text = "保存";
            this.BT_SavePara.UseVisualStyleBackColor = true;
            this.BT_SavePara.Click += new System.EventHandler(this.BT_SavePara_Click);
            // 
            // TB_Sensor_Row
            // 
            this.TB_Sensor_Row.Location = new System.Drawing.Point(87, 26);
            this.TB_Sensor_Row.Name = "TB_Sensor_Row";
            this.TB_Sensor_Row.Size = new System.Drawing.Size(141, 21);
            this.TB_Sensor_Row.TabIndex = 6;
            this.TB_Sensor_Row.Text = "5";
            this.TB_Sensor_Row.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "每行参数数:";
            // 
            // BT_Next
            // 
            this.BT_Next.Location = new System.Drawing.Point(1104, 851);
            this.BT_Next.Name = "BT_Next";
            this.BT_Next.Size = new System.Drawing.Size(102, 27);
            this.BT_Next.TabIndex = 83;
            this.BT_Next.Text = "下一页";
            this.BT_Next.UseVisualStyleBackColor = true;
            this.BT_Next.Click += new System.EventHandler(this.BT_Next_Click);
            // 
            // AdjSensorConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.BT_Next);
            this.Controls.Add(this.Grp_SetPara);
            this.Controls.Add(this.BT_Pre);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "AdjSensorConfig";
            this.Text = "AdjSensorConfig";
            this.Load += new System.EventHandler(this.AdjSensorConfig_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Grp_SetPara.ResumeLayout(false);
            this.Grp_SetPara_Style.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Sensor)).EndInit();
            this.Grp_SetPara_Set.ResumeLayout(false);
            this.Grp_SetPara_Set.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BT_PLCSave;
        private System.Windows.Forms.CheckBox CB_PLC;
        private System.Windows.Forms.Button BT_Pre;
        private System.Windows.Forms.GroupBox Grp_SetPara;
        private System.Windows.Forms.GroupBox Grp_SetPara_Style;
        private System.Windows.Forms.DataGridView DGV_Sensor;
        private System.Windows.Forms.GroupBox Grp_SetPara_Set;
        private System.Windows.Forms.Button BT_SavePara;
        private System.Windows.Forms.TextBox TB_Sensor_Row;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_RegName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_PLC_RegName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BT_Next;
    }
}