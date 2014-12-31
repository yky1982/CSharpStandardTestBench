namespace StandardTestBench
{
    partial class QueryDBConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Pos_End = new System.Windows.Forms.TextBox();
            this.TB_TableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Pos_Start = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_Save = new System.Windows.Forms.Button();
            this.CB_PDF = new System.Windows.Forms.CheckBox();
            this.CB_Zoom = new System.Windows.Forms.CheckBox();
            this.BT_Next = new System.Windows.Forms.Button();
            this.BT_Pre = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TB_Pos_End);
            this.groupBox1.Controls.Add(this.TB_TableName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TB_Pos_Start);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BT_Save);
            this.groupBox1.Controls.Add(this.CB_PDF);
            this.groupBox1.Controls.Add(this.CB_Zoom);
            this.groupBox1.Location = new System.Drawing.Point(161, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(909, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "界面配置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "结束";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "起始";
            // 
            // TB_Pos_End
            // 
            this.TB_Pos_End.Location = new System.Drawing.Point(409, 126);
            this.TB_Pos_End.Name = "TB_Pos_End";
            this.TB_Pos_End.Size = new System.Drawing.Size(100, 21);
            this.TB_Pos_End.TabIndex = 7;
            this.TB_Pos_End.Text = "A1";
            this.TB_Pos_End.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_TableName
            // 
            this.TB_TableName.Location = new System.Drawing.Point(261, 169);
            this.TB_TableName.Name = "TB_TableName";
            this.TB_TableName.Size = new System.Drawing.Size(100, 21);
            this.TB_TableName.TabIndex = 6;
            this.TB_TableName.Text = "sheet1";
            this.TB_TableName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "工作表名";
            // 
            // TB_Pos_Start
            // 
            this.TB_Pos_Start.Location = new System.Drawing.Point(261, 126);
            this.TB_Pos_Start.Name = "TB_Pos_Start";
            this.TB_Pos_Start.Size = new System.Drawing.Size(100, 21);
            this.TB_Pos_Start.TabIndex = 4;
            this.TB_Pos_Start.Text = "A1";
            this.TB_Pos_Start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "曲线位于报表内的位置:";
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(807, 167);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(75, 23);
            this.BT_Save.TabIndex = 2;
            this.BT_Save.Text = "保存";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
            // 
            // CB_PDF
            // 
            this.CB_PDF.AutoSize = true;
            this.CB_PDF.Location = new System.Drawing.Point(89, 84);
            this.CB_PDF.Name = "CB_PDF";
            this.CB_PDF.Size = new System.Drawing.Size(66, 16);
            this.CB_PDF.TabIndex = 1;
            this.CB_PDF.Text = "PDF报表";
            this.CB_PDF.UseVisualStyleBackColor = true;
            // 
            // CB_Zoom
            // 
            this.CB_Zoom.AutoSize = true;
            this.CB_Zoom.Location = new System.Drawing.Point(89, 32);
            this.CB_Zoom.Name = "CB_Zoom";
            this.CB_Zoom.Size = new System.Drawing.Size(270, 16);
            this.CB_Zoom.TabIndex = 0;
            this.CB_Zoom.Text = "曲线是否放大（如果是OpenGL模式，则不勾选)";
            this.CB_Zoom.UseVisualStyleBackColor = true;
            // 
            // BT_Next
            // 
            this.BT_Next.Location = new System.Drawing.Point(1101, 851);
            this.BT_Next.Name = "BT_Next";
            this.BT_Next.Size = new System.Drawing.Size(102, 27);
            this.BT_Next.TabIndex = 78;
            this.BT_Next.Text = "下一页";
            this.BT_Next.UseVisualStyleBackColor = true;
            this.BT_Next.Click += new System.EventHandler(this.BT_Next_Click);
            // 
            // BT_Pre
            // 
            this.BT_Pre.Location = new System.Drawing.Point(972, 851);
            this.BT_Pre.Name = "BT_Pre";
            this.BT_Pre.Size = new System.Drawing.Size(102, 27);
            this.BT_Pre.TabIndex = 77;
            this.BT_Pre.Text = "上一页";
            this.BT_Pre.UseVisualStyleBackColor = true;
            this.BT_Pre.Click += new System.EventHandler(this.BT_Pre_Click);
            // 
            // QueryDBConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.BT_Next);
            this.Controls.Add(this.BT_Pre);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "QueryDBConfig";
            this.Text = "QueryDBConfig";
            this.Load += new System.EventHandler(this.QueryDBConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CB_PDF;
        private System.Windows.Forms.CheckBox CB_Zoom;
        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.Button BT_Next;
        private System.Windows.Forms.Button BT_Pre;
        private System.Windows.Forms.TextBox TB_Pos_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_TableName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_Pos_End;
    }
}