namespace StandardTestBench
{
    partial class SetPara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetPara));
            this.Grp_Tool = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TB_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TB_StartTest = new System.Windows.Forms.ToolStripButton();
            this.Grp_SetPara_Style = new System.Windows.Forms.GroupBox();
            this.BT_SetPara = new System.Windows.Forms.Button();
            this.DGV_SetPara = new System.Windows.Forms.DataGridView();
            this.Grp_Report = new System.Windows.Forms.GroupBox();
            this.DGV_Report = new System.Windows.Forms.DataGridView();
            this.Grp_Para = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BT_SaveReport = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BT_SaveSetPara = new System.Windows.Forms.Button();
            this.BT_LoadSetConfig = new System.Windows.Forms.Button();
            this.TB_ConfigTable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox_M4 = new System.Windows.Forms.CheckBox();
            this.checkBox_M3 = new System.Windows.Forms.CheckBox();
            this.checkBox_M2 = new System.Windows.Forms.CheckBox();
            this.checkBox_M1 = new System.Windows.Forms.CheckBox();
            this.Grp_Tool.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.Grp_SetPara_Style.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SetPara)).BeginInit();
            this.Grp_Report.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Report)).BeginInit();
            this.Grp_Para.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_Tool
            // 
            this.Grp_Tool.Controls.Add(this.toolStrip1);
            this.Grp_Tool.Location = new System.Drawing.Point(12, 12);
            this.Grp_Tool.Name = "Grp_Tool";
            this.Grp_Tool.Size = new System.Drawing.Size(1200, 93);
            this.Grp_Tool.TabIndex = 9;
            this.Grp_Tool.TabStop = false;
            this.Grp_Tool.Text = "工具栏";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TB_Exit,
            this.toolStripSeparator1,
            this.TB_StartTest});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1194, 65);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TB_Exit
            // 
            this.TB_Exit.AutoSize = false;
            this.TB_Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_Exit.BackgroundImage")));
            this.TB_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TB_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_Exit.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.TB_Exit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TB_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Exit.Name = "TB_Exit";
            this.TB_Exit.Size = new System.Drawing.Size(65, 65);
            this.TB_Exit.Text = "退出";
            this.TB_Exit.Click += new System.EventHandler(this.TB_Exit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 65);
            // 
            // TB_StartTest
            // 
            this.TB_StartTest.AutoSize = false;
            this.TB_StartTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_StartTest.Image = ((System.Drawing.Image)(resources.GetObject("TB_StartTest.Image")));
            this.TB_StartTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TB_StartTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_StartTest.Name = "TB_StartTest";
            this.TB_StartTest.Size = new System.Drawing.Size(65, 65);
            this.TB_StartTest.Text = "启动试验";
            this.TB_StartTest.Click += new System.EventHandler(this.TB_StartTest_Click);
            // 
            // Grp_SetPara_Style
            // 
            this.Grp_SetPara_Style.Controls.Add(this.BT_SetPara);
            this.Grp_SetPara_Style.Controls.Add(this.DGV_SetPara);
            this.Grp_SetPara_Style.Location = new System.Drawing.Point(17, 20);
            this.Grp_SetPara_Style.Name = "Grp_SetPara_Style";
            this.Grp_SetPara_Style.Size = new System.Drawing.Size(1051, 459);
            this.Grp_SetPara_Style.TabIndex = 10;
            this.Grp_SetPara_Style.TabStop = false;
            this.Grp_SetPara_Style.Text = "试验参数";
            // 
            // BT_SetPara
            // 
            this.BT_SetPara.Location = new System.Drawing.Point(969, 430);
            this.BT_SetPara.Name = "BT_SetPara";
            this.BT_SetPara.Size = new System.Drawing.Size(75, 23);
            this.BT_SetPara.TabIndex = 2;
            this.BT_SetPara.Text = "设置";
            this.BT_SetPara.UseVisualStyleBackColor = true;
            this.BT_SetPara.Click += new System.EventHandler(this.BT_SetPara_Click);
            // 
            // DGV_SetPara
            // 
            this.DGV_SetPara.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_SetPara.Location = new System.Drawing.Point(6, 16);
            this.DGV_SetPara.Name = "DGV_SetPara";
            this.DGV_SetPara.RowTemplate.Height = 23;
            this.DGV_SetPara.Size = new System.Drawing.Size(1038, 408);
            this.DGV_SetPara.TabIndex = 1;
            this.DGV_SetPara.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_SetPara_CellClick);
            this.DGV_SetPara.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_SetPara_CellValueChanged);
            // 
            // Grp_Report
            // 
            this.Grp_Report.Controls.Add(this.DGV_Report);
            this.Grp_Report.Location = new System.Drawing.Point(17, 485);
            this.Grp_Report.Name = "Grp_Report";
            this.Grp_Report.Size = new System.Drawing.Size(1051, 254);
            this.Grp_Report.TabIndex = 11;
            this.Grp_Report.TabStop = false;
            this.Grp_Report.Text = "报表参数";
            // 
            // DGV_Report
            // 
            this.DGV_Report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Report.Location = new System.Drawing.Point(6, 16);
            this.DGV_Report.Name = "DGV_Report";
            this.DGV_Report.RowTemplate.Height = 23;
            this.DGV_Report.Size = new System.Drawing.Size(1038, 232);
            this.DGV_Report.TabIndex = 0;
            this.DGV_Report.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Report_CellValueChanged);
            // 
            // Grp_Para
            // 
            this.Grp_Para.Controls.Add(this.groupBox5);
            this.Grp_Para.Controls.Add(this.groupBox3);
            this.Grp_Para.Controls.Add(this.groupBox4);
            this.Grp_Para.Controls.Add(this.Grp_SetPara_Style);
            this.Grp_Para.Controls.Add(this.Grp_Report);
            this.Grp_Para.Location = new System.Drawing.Point(12, 114);
            this.Grp_Para.Name = "Grp_Para";
            this.Grp_Para.Size = new System.Drawing.Size(1197, 753);
            this.Grp_Para.TabIndex = 12;
            this.Grp_Para.TabStop = false;
            this.Grp_Para.Text = "参数设置";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BT_SaveReport);
            this.groupBox5.Location = new System.Drawing.Point(1074, 485);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(117, 254);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "报表信息配置表";
            // 
            // BT_SaveReport
            // 
            this.BT_SaveReport.Location = new System.Drawing.Point(36, 214);
            this.BT_SaveReport.Name = "BT_SaveReport";
            this.BT_SaveReport.Size = new System.Drawing.Size(75, 23);
            this.BT_SaveReport.TabIndex = 5;
            this.BT_SaveReport.Text = "保存";
            this.BT_SaveReport.UseVisualStyleBackColor = true;
            this.BT_SaveReport.Click += new System.EventHandler(this.BT_SaveReport_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BT_SaveSetPara);
            this.groupBox3.Controls.Add(this.BT_LoadSetConfig);
            this.groupBox3.Controls.Add(this.TB_ConfigTable);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(1074, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 201);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数配置表操作";
            // 
            // BT_SaveSetPara
            // 
            this.BT_SaveSetPara.Location = new System.Drawing.Point(36, 172);
            this.BT_SaveSetPara.Name = "BT_SaveSetPara";
            this.BT_SaveSetPara.Size = new System.Drawing.Size(75, 23);
            this.BT_SaveSetPara.TabIndex = 4;
            this.BT_SaveSetPara.Text = "保存";
            this.BT_SaveSetPara.UseVisualStyleBackColor = true;
            this.BT_SaveSetPara.Click += new System.EventHandler(this.BT_SaveSetPara_Click);
            // 
            // BT_LoadSetConfig
            // 
            this.BT_LoadSetConfig.Location = new System.Drawing.Point(36, 130);
            this.BT_LoadSetConfig.Name = "BT_LoadSetConfig";
            this.BT_LoadSetConfig.Size = new System.Drawing.Size(75, 23);
            this.BT_LoadSetConfig.TabIndex = 3;
            this.BT_LoadSetConfig.Text = "载入";
            this.BT_LoadSetConfig.UseVisualStyleBackColor = true;
            this.BT_LoadSetConfig.Click += new System.EventHandler(this.BT_LoadSetConfig_Click);
            // 
            // TB_ConfigTable
            // 
            this.TB_ConfigTable.Location = new System.Drawing.Point(11, 56);
            this.TB_ConfigTable.Name = "TB_ConfigTable";
            this.TB_ConfigTable.Size = new System.Drawing.Size(100, 21);
            this.TB_ConfigTable.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表名";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox_M4);
            this.groupBox4.Controls.Add(this.checkBox_M3);
            this.groupBox4.Controls.Add(this.checkBox_M2);
            this.groupBox4.Controls.Add(this.checkBox_M1);
            this.groupBox4.Location = new System.Drawing.Point(1074, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(116, 252);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "工位选择";
            // 
            // checkBox_M4
            // 
            this.checkBox_M4.AutoSize = true;
            this.checkBox_M4.Location = new System.Drawing.Point(28, 223);
            this.checkBox_M4.Name = "checkBox_M4";
            this.checkBox_M4.Size = new System.Drawing.Size(60, 16);
            this.checkBox_M4.TabIndex = 3;
            this.checkBox_M4.Text = "工位四";
            this.checkBox_M4.UseVisualStyleBackColor = true;
            this.checkBox_M4.CheckStateChanged += new System.EventHandler(this.checkBox_M4_CheckStateChanged);
            // 
            // checkBox_M3
            // 
            this.checkBox_M3.AutoSize = true;
            this.checkBox_M3.Location = new System.Drawing.Point(28, 164);
            this.checkBox_M3.Name = "checkBox_M3";
            this.checkBox_M3.Size = new System.Drawing.Size(60, 16);
            this.checkBox_M3.TabIndex = 2;
            this.checkBox_M3.Text = "工位三";
            this.checkBox_M3.UseVisualStyleBackColor = true;
            this.checkBox_M3.CheckStateChanged += new System.EventHandler(this.checkBox_M3_CheckStateChanged);
            // 
            // checkBox_M2
            // 
            this.checkBox_M2.AutoSize = true;
            this.checkBox_M2.Location = new System.Drawing.Point(28, 105);
            this.checkBox_M2.Name = "checkBox_M2";
            this.checkBox_M2.Size = new System.Drawing.Size(60, 16);
            this.checkBox_M2.TabIndex = 1;
            this.checkBox_M2.Text = "工位二";
            this.checkBox_M2.UseVisualStyleBackColor = true;
            this.checkBox_M2.CheckStateChanged += new System.EventHandler(this.checkBox_M2_CheckStateChanged);
            // 
            // checkBox_M1
            // 
            this.checkBox_M1.AutoSize = true;
            this.checkBox_M1.Location = new System.Drawing.Point(28, 46);
            this.checkBox_M1.Name = "checkBox_M1";
            this.checkBox_M1.Size = new System.Drawing.Size(60, 16);
            this.checkBox_M1.TabIndex = 0;
            this.checkBox_M1.Text = "工位一";
            this.checkBox_M1.UseVisualStyleBackColor = true;
            this.checkBox_M1.CheckStateChanged += new System.EventHandler(this.checkBox_M1_CheckStateChanged);
            // 
            // SetPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.Grp_Para);
            this.Controls.Add(this.Grp_Tool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "SetPara";
            this.Text = "SetPara";
            this.Load += new System.EventHandler(this.SetPara_Load);
            this.Grp_Tool.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Grp_SetPara_Style.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SetPara)).EndInit();
            this.Grp_Report.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Report)).EndInit();
            this.Grp_Para.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_Tool;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TB_Exit;
        private System.Windows.Forms.GroupBox Grp_SetPara_Style;
        private System.Windows.Forms.DataGridView DGV_SetPara;
        private System.Windows.Forms.GroupBox Grp_Report;
        private System.Windows.Forms.DataGridView DGV_Report;
        private System.Windows.Forms.GroupBox Grp_Para;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox_M4;
        private System.Windows.Forms.CheckBox checkBox_M3;
        private System.Windows.Forms.CheckBox checkBox_M2;
        private System.Windows.Forms.CheckBox checkBox_M1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BT_SaveSetPara;
        private System.Windows.Forms.Button BT_LoadSetConfig;
        private System.Windows.Forms.TextBox TB_ConfigTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BT_SaveReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton TB_StartTest;
        private System.Windows.Forms.Button BT_SetPara;
    }
}