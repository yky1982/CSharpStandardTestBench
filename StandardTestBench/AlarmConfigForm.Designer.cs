namespace StandardTestBench
{
    partial class AlarmConfigForm
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
            this.BT_Next = new System.Windows.Forms.Button();
            this.BT_Pre = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DG_AlarmConfig = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BT_Save = new System.Windows.Forms.Button();
            this.CB_HistoryAlarm = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_AlarmConfig)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BT_Next
            // 
            this.BT_Next.Location = new System.Drawing.Point(1100, 851);
            this.BT_Next.Name = "BT_Next";
            this.BT_Next.Size = new System.Drawing.Size(102, 27);
            this.BT_Next.TabIndex = 81;
            this.BT_Next.Text = "下一页";
            this.BT_Next.UseVisualStyleBackColor = true;
            this.BT_Next.Click += new System.EventHandler(this.BT_Next_Click);
            // 
            // BT_Pre
            // 
            this.BT_Pre.Location = new System.Drawing.Point(971, 851);
            this.BT_Pre.Name = "BT_Pre";
            this.BT_Pre.Size = new System.Drawing.Size(102, 27);
            this.BT_Pre.TabIndex = 80;
            this.BT_Pre.Text = "上一页";
            this.BT_Pre.UseVisualStyleBackColor = true;
            this.BT_Pre.Click += new System.EventHandler(this.BT_Pre_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DG_AlarmConfig);
            this.groupBox1.Location = new System.Drawing.Point(161, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(909, 202);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "告警信息";
            // 
            // DG_AlarmConfig
            // 
            this.DG_AlarmConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_AlarmConfig.Location = new System.Drawing.Point(6, 20);
            this.DG_AlarmConfig.Name = "DG_AlarmConfig";
            this.DG_AlarmConfig.RowTemplate.Height = 23;
            this.DG_AlarmConfig.Size = new System.Drawing.Size(897, 176);
            this.DG_AlarmConfig.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BT_Save);
            this.groupBox2.Controls.Add(this.CB_HistoryAlarm);
            this.groupBox2.Location = new System.Drawing.Point(161, 417);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(909, 82);
            this.groupBox2.TabIndex = 82;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配置";
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(828, 32);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(75, 23);
            this.BT_Save.TabIndex = 1;
            this.BT_Save.Text = "保存";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
            // 
            // CB_HistoryAlarm
            // 
            this.CB_HistoryAlarm.AutoSize = true;
            this.CB_HistoryAlarm.Location = new System.Drawing.Point(68, 39);
            this.CB_HistoryAlarm.Name = "CB_HistoryAlarm";
            this.CB_HistoryAlarm.Size = new System.Drawing.Size(120, 16);
            this.CB_HistoryAlarm.TabIndex = 0;
            this.CB_HistoryAlarm.Text = "是否显示历史告警";
            this.CB_HistoryAlarm.UseVisualStyleBackColor = true;
            // 
            // AlarmConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BT_Next);
            this.Controls.Add(this.BT_Pre);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "AlarmConfigForm";
            this.Text = "AlarmConfigForm";
            this.Load += new System.EventHandler(this.AlarmConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG_AlarmConfig)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BT_Next;
        private System.Windows.Forms.Button BT_Pre;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DG_AlarmConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox CB_HistoryAlarm;
        private System.Windows.Forms.Button BT_Save;
    }
}