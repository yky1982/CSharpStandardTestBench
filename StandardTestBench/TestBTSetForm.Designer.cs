namespace StandardTestBench
{
    partial class TestBTSetForm
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
            this.Grp_Set = new System.Windows.Forms.GroupBox();
            this.BT_LoadPic = new System.Windows.Forms.Button();
            this.TB_FilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_RegNameCH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_RegName = new System.Windows.Forms.TextBox();
            this.Lable = new System.Windows.Forms.Label();
            this.BT_Save = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.CB_Start = new System.Windows.Forms.CheckBox();
            this.picFilePath = new System.Windows.Forms.OpenFileDialog();
            this.BT_SaveM = new System.Windows.Forms.Button();
            this.Grp_Set.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_Set
            // 
            this.Grp_Set.Controls.Add(this.BT_LoadPic);
            this.Grp_Set.Controls.Add(this.TB_FilePath);
            this.Grp_Set.Controls.Add(this.label3);
            this.Grp_Set.Controls.Add(this.TB_RegNameCH);
            this.Grp_Set.Controls.Add(this.label2);
            this.Grp_Set.Controls.Add(this.TB_RegName);
            this.Grp_Set.Controls.Add(this.Lable);
            this.Grp_Set.Location = new System.Drawing.Point(12, 49);
            this.Grp_Set.Name = "Grp_Set";
            this.Grp_Set.Size = new System.Drawing.Size(372, 152);
            this.Grp_Set.TabIndex = 0;
            this.Grp_Set.TabStop = false;
            this.Grp_Set.Text = "配置";
            // 
            // BT_LoadPic
            // 
            this.BT_LoadPic.Location = new System.Drawing.Point(250, 92);
            this.BT_LoadPic.Name = "BT_LoadPic";
            this.BT_LoadPic.Size = new System.Drawing.Size(75, 23);
            this.BT_LoadPic.TabIndex = 6;
            this.BT_LoadPic.Text = "载入";
            this.BT_LoadPic.UseVisualStyleBackColor = true;
            this.BT_LoadPic.Click += new System.EventHandler(this.BT_LoadPic_Click);
            // 
            // TB_FilePath
            // 
            this.TB_FilePath.Location = new System.Drawing.Point(34, 94);
            this.TB_FilePath.Name = "TB_FilePath";
            this.TB_FilePath.Size = new System.Drawing.Size(192, 21);
            this.TB_FilePath.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "背景图路径(像素35*35)";
            // 
            // TB_RegNameCH
            // 
            this.TB_RegNameCH.Location = new System.Drawing.Point(225, 42);
            this.TB_RegNameCH.Name = "TB_RegNameCH";
            this.TB_RegNameCH.Size = new System.Drawing.Size(100, 21);
            this.TB_RegNameCH.TabIndex = 3;
            this.TB_RegNameCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "按钮名称";
            // 
            // TB_RegName
            // 
            this.TB_RegName.Location = new System.Drawing.Point(34, 42);
            this.TB_RegName.Name = "TB_RegName";
            this.TB_RegName.Size = new System.Drawing.Size(100, 21);
            this.TB_RegName.TabIndex = 1;
            this.TB_RegName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Lable
            // 
            this.Lable.AutoSize = true;
            this.Lable.Location = new System.Drawing.Point(32, 27);
            this.Lable.Name = "Lable";
            this.Lable.Size = new System.Drawing.Size(53, 12);
            this.Lable.TabIndex = 0;
            this.Lable.Text = "关联参数";
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(309, 205);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(75, 23);
            this.BT_Save.TabIndex = 3;
            this.BT_Save.Text = "保存";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(-1, 240);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BT_Cancel.TabIndex = 4;
            this.BT_Cancel.Text = "取消";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.button3_Click);
            // 
            // CB_Start
            // 
            this.CB_Start.AutoSize = true;
            this.CB_Start.Location = new System.Drawing.Point(12, 18);
            this.CB_Start.Name = "CB_Start";
            this.CB_Start.Size = new System.Drawing.Size(48, 16);
            this.CB_Start.TabIndex = 5;
            this.CB_Start.Text = "启用";
            this.CB_Start.UseVisualStyleBackColor = true;
            this.CB_Start.CheckedChanged += new System.EventHandler(this.CB_Start_CheckedChanged);
            // 
            // picFilePath
            // 
            this.picFilePath.FileName = "picFilePath";
            // 
            // BT_SaveM
            // 
            this.BT_SaveM.Location = new System.Drawing.Point(309, 235);
            this.BT_SaveM.Name = "BT_SaveM";
            this.BT_SaveM.Size = new System.Drawing.Size(75, 23);
            this.BT_SaveM.TabIndex = 6;
            this.BT_SaveM.Text = "批量保存";
            this.BT_SaveM.UseVisualStyleBackColor = true;
            this.BT_SaveM.Click += new System.EventHandler(this.BT_SaveM_Click);
            // 
            // TestBTSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 262);
            this.Controls.Add(this.BT_SaveM);
            this.Controls.Add(this.CB_Start);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_Save);
            this.Controls.Add(this.Grp_Set);
            this.Name = "TestBTSetForm";
            this.Text = "TestBTSetForm";
            this.Load += new System.EventHandler(this.TestBTSetForm_Load);
            this.Grp_Set.ResumeLayout(false);
            this.Grp_Set.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_Set;
        private System.Windows.Forms.Label Lable;
        private System.Windows.Forms.Button BT_LoadPic;
        private System.Windows.Forms.TextBox TB_FilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_RegNameCH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_RegName;
        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.CheckBox CB_Start;
        private System.Windows.Forms.OpenFileDialog picFilePath;
        private System.Windows.Forms.Button BT_SaveM;
    }
}