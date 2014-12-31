namespace StandardTestBench
{
    partial class SysDescrip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysDescrip));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TB_Exit = new System.Windows.Forms.ToolStripButton();
            this.Grp_Txt = new System.Windows.Forms.GroupBox();
            this.RTBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.Grp_Txt.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(18, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1200, 93);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工具栏";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TB_Exit});
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
            // Grp_Txt
            // 
            this.Grp_Txt.Controls.Add(this.RTBox);
            this.Grp_Txt.Location = new System.Drawing.Point(132, 137);
            this.Grp_Txt.Name = "Grp_Txt";
            this.Grp_Txt.Size = new System.Drawing.Size(967, 721);
            this.Grp_Txt.TabIndex = 9;
            this.Grp_Txt.TabStop = false;
            this.Grp_Txt.Text = "描述";
            // 
            // RTBox
            // 
            this.RTBox.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RTBox.Location = new System.Drawing.Point(6, 21);
            this.RTBox.Name = "RTBox";
            this.RTBox.Size = new System.Drawing.Size(955, 694);
            this.RTBox.TabIndex = 0;
            this.RTBox.Text = "";
            // 
            // SysDescrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.Grp_Txt);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "SysDescrip";
            this.Text = "SysDescrip";
            this.Load += new System.EventHandler(this.SysDescrip_Load);
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Grp_Txt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TB_Exit;
        private System.Windows.Forms.GroupBox Grp_Txt;
        private System.Windows.Forms.RichTextBox RTBox;
    }
}