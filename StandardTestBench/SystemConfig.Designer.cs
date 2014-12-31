namespace StandardTestBench
{
    partial class SystemConfig
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
            this.RB_English = new System.Windows.Forms.RadioButton();
            this.RB_Chinese = new System.Windows.Forms.RadioButton();
            this.BT_Save = new System.Windows.Forms.Button();
            this.BT_Pre = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CB_DisDebugInfo = new System.Windows.Forms.CheckBox();
            this.CB_SaveDebugInfo = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_English);
            this.groupBox1.Controls.Add(this.RB_Chinese);
            this.groupBox1.Location = new System.Drawing.Point(6, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(932, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "语言配置";
            // 
            // RB_English
            // 
            this.RB_English.AutoSize = true;
            this.RB_English.Location = new System.Drawing.Point(83, 67);
            this.RB_English.Name = "RB_English";
            this.RB_English.Size = new System.Drawing.Size(65, 16);
            this.RB_English.TabIndex = 2;
            this.RB_English.TabStop = true;
            this.RB_English.Text = "English";
            this.RB_English.UseVisualStyleBackColor = true;
            // 
            // RB_Chinese
            // 
            this.RB_Chinese.AutoSize = true;
            this.RB_Chinese.Location = new System.Drawing.Point(83, 29);
            this.RB_Chinese.Name = "RB_Chinese";
            this.RB_Chinese.Size = new System.Drawing.Size(47, 16);
            this.RB_Chinese.TabIndex = 1;
            this.RB_Chinese.TabStop = true;
            this.RB_Chinese.Text = "中文";
            this.RB_Chinese.UseVisualStyleBackColor = true;
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(862, 252);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(75, 23);
            this.BT_Save.TabIndex = 0;
            this.BT_Save.Text = "保存";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
            // 
            // BT_Pre
            // 
            this.BT_Pre.Location = new System.Drawing.Point(1106, 851);
            this.BT_Pre.Name = "BT_Pre";
            this.BT_Pre.Size = new System.Drawing.Size(102, 27);
            this.BT_Pre.TabIndex = 81;
            this.BT_Pre.Text = "上一页";
            this.BT_Pre.UseVisualStyleBackColor = true;
            this.BT_Pre.Click += new System.EventHandler(this.BT_Pre_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.BT_Save);
            this.groupBox2.Location = new System.Drawing.Point(145, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(943, 281);
            this.groupBox2.TabIndex = 82;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统配置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CB_DisDebugInfo);
            this.groupBox3.Controls.Add(this.CB_SaveDebugInfo);
            this.groupBox3.Location = new System.Drawing.Point(6, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(932, 111);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "调试信息";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // CB_DisDebugInfo
            // 
            this.CB_DisDebugInfo.AutoSize = true;
            this.CB_DisDebugInfo.Location = new System.Drawing.Point(83, 69);
            this.CB_DisDebugInfo.Name = "CB_DisDebugInfo";
            this.CB_DisDebugInfo.Size = new System.Drawing.Size(120, 16);
            this.CB_DisDebugInfo.TabIndex = 1;
            this.CB_DisDebugInfo.Text = "是否显示调试信息";
            this.CB_DisDebugInfo.UseVisualStyleBackColor = true;
            // 
            // CB_SaveDebugInfo
            // 
            this.CB_SaveDebugInfo.AutoSize = true;
            this.CB_SaveDebugInfo.Location = new System.Drawing.Point(83, 31);
            this.CB_SaveDebugInfo.Name = "CB_SaveDebugInfo";
            this.CB_SaveDebugInfo.Size = new System.Drawing.Size(120, 16);
            this.CB_SaveDebugInfo.TabIndex = 0;
            this.CB_SaveDebugInfo.Text = "是否保存调试信息";
            this.CB_SaveDebugInfo.UseVisualStyleBackColor = true;
            // 
            // SystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BT_Pre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "SystemConfig";
            this.Text = "SystemConfig";
            this.Load += new System.EventHandler(this.SystemConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_English;
        private System.Windows.Forms.RadioButton RB_Chinese;
        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.Button BT_Pre;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox CB_DisDebugInfo;
        private System.Windows.Forms.CheckBox CB_SaveDebugInfo;
    }
}