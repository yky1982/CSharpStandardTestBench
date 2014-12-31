namespace StandardTestBench
{
    partial class ZoomForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Set_MaxP = new System.Windows.Forms.TextBox();
            this.TB_Set_StartTime = new System.Windows.Forms.TextBox();
            this.TB_Set_EndTime = new System.Windows.Forms.TextBox();
            this.TB_Def_EndTime = new System.Windows.Forms.TextBox();
            this.TB_Def_StartTime = new System.Windows.Forms.TextBox();
            this.TB_Def_MaxP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BT_Default = new System.Windows.Forms.Button();
            this.BT_Set = new System.Windows.Forms.Button();
            this.BT_Exit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_Set_EndTime);
            this.groupBox1.Controls.Add(this.TB_Set_StartTime);
            this.groupBox1.Controls.Add(this.TB_Set_MaxP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 173);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置值";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_Def_EndTime);
            this.groupBox2.Controls.Add(this.TB_Def_StartTime);
            this.groupBox2.Controls.Add(this.TB_Def_MaxP);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(299, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 173);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "初始值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "最大压力";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "起始时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "结束时间";
            // 
            // TB_Set_MaxP
            // 
            this.TB_Set_MaxP.Location = new System.Drawing.Point(79, 39);
            this.TB_Set_MaxP.Name = "TB_Set_MaxP";
            this.TB_Set_MaxP.Size = new System.Drawing.Size(183, 21);
            this.TB_Set_MaxP.TabIndex = 3;
            this.TB_Set_MaxP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Set_StartTime
            // 
            this.TB_Set_StartTime.Location = new System.Drawing.Point(79, 79);
            this.TB_Set_StartTime.Name = "TB_Set_StartTime";
            this.TB_Set_StartTime.Size = new System.Drawing.Size(183, 21);
            this.TB_Set_StartTime.TabIndex = 4;
            this.TB_Set_StartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Set_EndTime
            // 
            this.TB_Set_EndTime.Location = new System.Drawing.Point(79, 121);
            this.TB_Set_EndTime.Name = "TB_Set_EndTime";
            this.TB_Set_EndTime.Size = new System.Drawing.Size(183, 21);
            this.TB_Set_EndTime.TabIndex = 5;
            this.TB_Set_EndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Def_EndTime
            // 
            this.TB_Def_EndTime.Location = new System.Drawing.Point(73, 121);
            this.TB_Def_EndTime.Name = "TB_Def_EndTime";
            this.TB_Def_EndTime.ReadOnly = true;
            this.TB_Def_EndTime.Size = new System.Drawing.Size(183, 21);
            this.TB_Def_EndTime.TabIndex = 11;
            this.TB_Def_EndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Def_StartTime
            // 
            this.TB_Def_StartTime.Location = new System.Drawing.Point(73, 79);
            this.TB_Def_StartTime.Name = "TB_Def_StartTime";
            this.TB_Def_StartTime.ReadOnly = true;
            this.TB_Def_StartTime.Size = new System.Drawing.Size(183, 21);
            this.TB_Def_StartTime.TabIndex = 10;
            this.TB_Def_StartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_Def_MaxP
            // 
            this.TB_Def_MaxP.Location = new System.Drawing.Point(73, 39);
            this.TB_Def_MaxP.Name = "TB_Def_MaxP";
            this.TB_Def_MaxP.ReadOnly = true;
            this.TB_Def_MaxP.Size = new System.Drawing.Size(183, 21);
            this.TB_Def_MaxP.TabIndex = 9;
            this.TB_Def_MaxP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "结束时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "起始时间";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "最大压力";
            // 
            // BT_Default
            // 
            this.BT_Default.Location = new System.Drawing.Point(299, 223);
            this.BT_Default.Name = "BT_Default";
            this.BT_Default.Size = new System.Drawing.Size(75, 23);
            this.BT_Default.TabIndex = 2;
            this.BT_Default.Text = "恢复初始值";
            this.BT_Default.UseVisualStyleBackColor = true;
            this.BT_Default.Click += new System.EventHandler(this.BT_Default_Click);
            // 
            // BT_Set
            // 
            this.BT_Set.Location = new System.Drawing.Point(397, 223);
            this.BT_Set.Name = "BT_Set";
            this.BT_Set.Size = new System.Drawing.Size(75, 23);
            this.BT_Set.TabIndex = 3;
            this.BT_Set.Text = "设置";
            this.BT_Set.UseVisualStyleBackColor = true;
            this.BT_Set.Click += new System.EventHandler(this.BT_Set_Click);
            // 
            // BT_Exit
            // 
            this.BT_Exit.Location = new System.Drawing.Point(495, 223);
            this.BT_Exit.Name = "BT_Exit";
            this.BT_Exit.Size = new System.Drawing.Size(75, 23);
            this.BT_Exit.TabIndex = 4;
            this.BT_Exit.Text = "退出";
            this.BT_Exit.UseVisualStyleBackColor = true;
            this.BT_Exit.Click += new System.EventHandler(this.BT_Exit_Click);
            // 
            // ZoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 262);
            this.ControlBox = false;
            this.Controls.Add(this.BT_Exit);
            this.Controls.Add(this.BT_Set);
            this.Controls.Add(this.BT_Default);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ZoomForm";
            this.Text = "ZoomForm";
            this.Load += new System.EventHandler(this.ZoomForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Set_EndTime;
        private System.Windows.Forms.TextBox TB_Set_StartTime;
        private System.Windows.Forms.TextBox TB_Set_MaxP;
        private System.Windows.Forms.TextBox TB_Def_EndTime;
        private System.Windows.Forms.TextBox TB_Def_StartTime;
        private System.Windows.Forms.TextBox TB_Def_MaxP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BT_Default;
        private System.Windows.Forms.Button BT_Set;
        private System.Windows.Forms.Button BT_Exit;
    }
}