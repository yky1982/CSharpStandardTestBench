namespace StandardTestBench
{
    partial class SetParaConfig
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
            this.Grp_SetPara = new System.Windows.Forms.GroupBox();
            this.BT_CreatMDB = new System.Windows.Forms.Button();
            this.Grp_SetPara_Style = new System.Windows.Forms.GroupBox();
            this.DGV_SetPara = new System.Windows.Forms.DataGridView();
            this.Grp_SetPara_Set = new System.Windows.Forms.GroupBox();
            this.BT_SavePara = new System.Windows.Forms.Button();
            this.TB_SetPara_MachineNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_SetPara_Row = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Grp_Report = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DGV_Report = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BT_SaveReport = new System.Windows.Forms.Button();
            this.TB_Report = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_Pre = new System.Windows.Forms.Button();
            this.BT_Next = new System.Windows.Forms.Button();
            this.TB_SetPara_RegName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Grp_SetPara.SuspendLayout();
            this.Grp_SetPara_Style.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SetPara)).BeginInit();
            this.Grp_SetPara_Set.SuspendLayout();
            this.Grp_Report.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Report)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_SetPara
            // 
            this.Grp_SetPara.Controls.Add(this.BT_CreatMDB);
            this.Grp_SetPara.Controls.Add(this.Grp_SetPara_Style);
            this.Grp_SetPara.Controls.Add(this.Grp_SetPara_Set);
            this.Grp_SetPara.Location = new System.Drawing.Point(76, 47);
            this.Grp_SetPara.Name = "Grp_SetPara";
            this.Grp_SetPara.Size = new System.Drawing.Size(1063, 461);
            this.Grp_SetPara.TabIndex = 0;
            this.Grp_SetPara.TabStop = false;
            this.Grp_SetPara.Text = "参数信息";
            // 
            // BT_CreatMDB
            // 
            this.BT_CreatMDB.Location = new System.Drawing.Point(975, 429);
            this.BT_CreatMDB.Name = "BT_CreatMDB";
            this.BT_CreatMDB.Size = new System.Drawing.Size(75, 23);
            this.BT_CreatMDB.TabIndex = 2;
            this.BT_CreatMDB.Text = "生成配置表";
            this.BT_CreatMDB.UseVisualStyleBackColor = true;
            this.BT_CreatMDB.Click += new System.EventHandler(this.BT_CreatMDB_Click);
            // 
            // Grp_SetPara_Style
            // 
            this.Grp_SetPara_Style.Controls.Add(this.DGV_SetPara);
            this.Grp_SetPara_Style.Location = new System.Drawing.Point(6, 90);
            this.Grp_SetPara_Style.Name = "Grp_SetPara_Style";
            this.Grp_SetPara_Style.Size = new System.Drawing.Size(1051, 333);
            this.Grp_SetPara_Style.TabIndex = 1;
            this.Grp_SetPara_Style.TabStop = false;
            this.Grp_SetPara_Style.Text = "样式";
            // 
            // DGV_SetPara
            // 
            this.DGV_SetPara.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_SetPara.Location = new System.Drawing.Point(6, 20);
            this.DGV_SetPara.Name = "DGV_SetPara";
            this.DGV_SetPara.RowTemplate.Height = 23;
            this.DGV_SetPara.Size = new System.Drawing.Size(1038, 307);
            this.DGV_SetPara.TabIndex = 1;
            // 
            // Grp_SetPara_Set
            // 
            this.Grp_SetPara_Set.Controls.Add(this.TB_SetPara_RegName);
            this.Grp_SetPara_Set.Controls.Add(this.label2);
            this.Grp_SetPara_Set.Controls.Add(this.BT_SavePara);
            this.Grp_SetPara_Set.Controls.Add(this.TB_SetPara_MachineNum);
            this.Grp_SetPara_Set.Controls.Add(this.label3);
            this.Grp_SetPara_Set.Controls.Add(this.TB_SetPara_Row);
            this.Grp_SetPara_Set.Controls.Add(this.label4);
            this.Grp_SetPara_Set.Location = new System.Drawing.Point(6, 20);
            this.Grp_SetPara_Set.Name = "Grp_SetPara_Set";
            this.Grp_SetPara_Set.Size = new System.Drawing.Size(1051, 64);
            this.Grp_SetPara_Set.TabIndex = 0;
            this.Grp_SetPara_Set.TabStop = false;
            this.Grp_SetPara_Set.Text = "设置";
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
            // TB_SetPara_MachineNum
            // 
            this.TB_SetPara_MachineNum.Location = new System.Drawing.Point(366, 26);
            this.TB_SetPara_MachineNum.Name = "TB_SetPara_MachineNum";
            this.TB_SetPara_MachineNum.Size = new System.Drawing.Size(141, 21);
            this.TB_SetPara_MachineNum.TabIndex = 8;
            this.TB_SetPara_MachineNum.Text = "2";
            this.TB_SetPara_MachineNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SetPara_MachineNum.TextChanged += new System.EventHandler(this.TB_SetPara_MachineNum_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "拖机数:";
            // 
            // TB_SetPara_Row
            // 
            this.TB_SetPara_Row.Location = new System.Drawing.Point(87, 26);
            this.TB_SetPara_Row.Name = "TB_SetPara_Row";
            this.TB_SetPara_Row.Size = new System.Drawing.Size(141, 21);
            this.TB_SetPara_Row.TabIndex = 6;
            this.TB_SetPara_Row.Text = "5";
            this.TB_SetPara_Row.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_SetPara_Row.TextChanged += new System.EventHandler(this.TB_SetPara_Row_TextChanged);
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
            // Grp_Report
            // 
            this.Grp_Report.Controls.Add(this.groupBox2);
            this.Grp_Report.Controls.Add(this.groupBox1);
            this.Grp_Report.Location = new System.Drawing.Point(76, 537);
            this.Grp_Report.Name = "Grp_Report";
            this.Grp_Report.Size = new System.Drawing.Size(1063, 307);
            this.Grp_Report.TabIndex = 1;
            this.Grp_Report.TabStop = false;
            this.Grp_Report.Text = "报表信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DGV_Report);
            this.groupBox2.Location = new System.Drawing.Point(6, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1051, 211);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "样式";
            // 
            // DGV_Report
            // 
            this.DGV_Report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Report.Location = new System.Drawing.Point(6, 20);
            this.DGV_Report.Name = "DGV_Report";
            this.DGV_Report.RowTemplate.Height = 23;
            this.DGV_Report.Size = new System.Drawing.Size(1038, 185);
            this.DGV_Report.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BT_SaveReport);
            this.groupBox1.Controls.Add(this.TB_Report);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1051, 64);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // BT_SaveReport
            // 
            this.BT_SaveReport.Location = new System.Drawing.Point(970, 25);
            this.BT_SaveReport.Name = "BT_SaveReport";
            this.BT_SaveReport.Size = new System.Drawing.Size(75, 23);
            this.BT_SaveReport.TabIndex = 4;
            this.BT_SaveReport.Text = "保存";
            this.BT_SaveReport.UseVisualStyleBackColor = true;
            this.BT_SaveReport.Click += new System.EventHandler(this.BT_SaveReport_Click);
            // 
            // TB_Report
            // 
            this.TB_Report.Location = new System.Drawing.Point(93, 27);
            this.TB_Report.Name = "TB_Report";
            this.TB_Report.Size = new System.Drawing.Size(141, 21);
            this.TB_Report.TabIndex = 1;
            this.TB_Report.Text = "5";
            this.TB_Report.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Report.TextChanged += new System.EventHandler(this.TB_Report_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "每行参数数:";
            // 
            // BT_Pre
            // 
            this.BT_Pre.Location = new System.Drawing.Point(986, 856);
            this.BT_Pre.Name = "BT_Pre";
            this.BT_Pre.Size = new System.Drawing.Size(102, 27);
            this.BT_Pre.TabIndex = 6;
            this.BT_Pre.Text = "上一页";
            this.BT_Pre.UseVisualStyleBackColor = true;
            this.BT_Pre.Click += new System.EventHandler(this.BT_Pre_Click);
            // 
            // BT_Next
            // 
            this.BT_Next.Location = new System.Drawing.Point(1116, 856);
            this.BT_Next.Name = "BT_Next";
            this.BT_Next.Size = new System.Drawing.Size(102, 27);
            this.BT_Next.TabIndex = 7;
            this.BT_Next.Text = "下一页";
            this.BT_Next.UseVisualStyleBackColor = true;
            this.BT_Next.Click += new System.EventHandler(this.BT_Next_Click);
            // 
            // TB_SetPara_RegName
            // 
            this.TB_SetPara_RegName.Location = new System.Drawing.Point(667, 26);
            this.TB_SetPara_RegName.Name = "TB_SetPara_RegName";
            this.TB_SetPara_RegName.Size = new System.Drawing.Size(141, 21);
            this.TB_SetPara_RegName.TabIndex = 11;
            this.TB_SetPara_RegName.Text = "2";
            this.TB_SetPara_RegName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(575, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "拖机数设置参数:";
            // 
            // SetParaConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.BT_Next);
            this.Controls.Add(this.BT_Pre);
            this.Controls.Add(this.Grp_Report);
            this.Controls.Add(this.Grp_SetPara);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "SetParaConfig";
            this.Text = "SetParaConfig";
            this.Load += new System.EventHandler(this.SetParaConfig_Load);
            this.Grp_SetPara.ResumeLayout(false);
            this.Grp_SetPara_Style.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_SetPara)).EndInit();
            this.Grp_SetPara_Set.ResumeLayout(false);
            this.Grp_SetPara_Set.PerformLayout();
            this.Grp_Report.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Report)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_SetPara;
        private System.Windows.Forms.GroupBox Grp_Report;
        private System.Windows.Forms.Button BT_Pre;
        private System.Windows.Forms.Button BT_Next;
        private System.Windows.Forms.GroupBox Grp_SetPara_Style;
        private System.Windows.Forms.GroupBox Grp_SetPara_Set;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TB_Report;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BT_SaveReport;
        private System.Windows.Forms.DataGridView DGV_SetPara;
        private System.Windows.Forms.DataGridView DGV_Report;
        private System.Windows.Forms.Button BT_SavePara;
        private System.Windows.Forms.TextBox TB_SetPara_MachineNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_SetPara_Row;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BT_CreatMDB;
        private System.Windows.Forms.TextBox TB_SetPara_RegName;
        private System.Windows.Forms.Label label2;
    }
}