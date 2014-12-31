namespace StandardTestBench
{
    partial class AlarmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmForm));
            this.TB_ActualAlarm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TB_Exit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Alarm_Dis = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TB_HistoryAlarm = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_ActualAlarm
            // 
            this.TB_ActualAlarm.AutoSize = false;
            this.TB_ActualAlarm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_ActualAlarm.BackgroundImage")));
            this.TB_ActualAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_ActualAlarm.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.TB_ActualAlarm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TB_ActualAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_ActualAlarm.Name = "TB_ActualAlarm";
            this.TB_ActualAlarm.Size = new System.Drawing.Size(65, 65);
            this.TB_ActualAlarm.Text = "实时告警";
            this.TB_ActualAlarm.Click += new System.EventHandler(this.TB_ActualAlarm_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 61);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 61);
            // 
            // TB_Exit
            // 
            this.TB_Exit.AutoSize = false;
            this.TB_Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_Exit.BackgroundImage")));
            this.TB_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_Exit.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.TB_Exit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TB_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_Exit.Name = "TB_Exit";
            this.TB_Exit.Size = new System.Drawing.Size(65, 65);
            this.TB_Exit.Text = "退出";
            this.TB_Exit.Click += new System.EventHandler(this.TB_Exit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Alarm_Dis);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1211, 866);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // RB_Alarm_Dis
            // 
            this.RB_Alarm_Dis.Location = new System.Drawing.Point(6, 90);
            this.RB_Alarm_Dis.Name = "RB_Alarm_Dis";
            this.RB_Alarm_Dis.Size = new System.Drawing.Size(1199, 770);
            this.RB_Alarm_Dis.TabIndex = 1;
            this.RB_Alarm_Dis.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TB_Exit,
            this.toolStripSeparator1,
            this.TB_ActualAlarm,
            this.toolStripSeparator2,
            this.TB_HistoryAlarm});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1205, 61);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TB_HistoryAlarm
            // 
            this.TB_HistoryAlarm.AutoSize = false;
            this.TB_HistoryAlarm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TB_HistoryAlarm.BackgroundImage")));
            this.TB_HistoryAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TB_HistoryAlarm.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.TB_HistoryAlarm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TB_HistoryAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TB_HistoryAlarm.Name = "TB_HistoryAlarm";
            this.TB_HistoryAlarm.Size = new System.Drawing.Size(65, 65);
            this.TB_HistoryAlarm.Text = "历史告警";
            this.TB_HistoryAlarm.Click += new System.EventHandler(this.TB_HistoryAlarm_Click);
            // 
            // AlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlarmForm";
            this.Text = "AlarmForm";
            this.Load += new System.EventHandler(this.AlarmForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton TB_ActualAlarm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TB_Exit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox RB_Alarm_Dis;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TB_HistoryAlarm;
    }
}