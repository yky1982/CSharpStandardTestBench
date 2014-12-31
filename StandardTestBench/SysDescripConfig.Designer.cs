namespace StandardTestBench
{
    partial class SysDescripConfig
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
            this.Grp_Tools = new System.Windows.Forms.GroupBox();
            this.CB_FontStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BT_Save = new System.Windows.Forms.Button();
            this.CB_LineSpaces = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CB_FontColor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_FontSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_FontType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Grp_Txt = new System.Windows.Forms.GroupBox();
            this.RTBox = new System.Windows.Forms.RichTextBox();
            this.BT_Next = new System.Windows.Forms.Button();
            this.Grp_Tools.SuspendLayout();
            this.Grp_Txt.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_Tools
            // 
            this.Grp_Tools.Controls.Add(this.CB_FontStyle);
            this.Grp_Tools.Controls.Add(this.label5);
            this.Grp_Tools.Controls.Add(this.BT_Save);
            this.Grp_Tools.Controls.Add(this.CB_LineSpaces);
            this.Grp_Tools.Controls.Add(this.label4);
            this.Grp_Tools.Controls.Add(this.CB_FontColor);
            this.Grp_Tools.Controls.Add(this.label3);
            this.Grp_Tools.Controls.Add(this.TB_FontSize);
            this.Grp_Tools.Controls.Add(this.label2);
            this.Grp_Tools.Controls.Add(this.CB_FontType);
            this.Grp_Tools.Controls.Add(this.label1);
            this.Grp_Tools.Location = new System.Drawing.Point(12, 12);
            this.Grp_Tools.Name = "Grp_Tools";
            this.Grp_Tools.Size = new System.Drawing.Size(1214, 92);
            this.Grp_Tools.TabIndex = 3;
            this.Grp_Tools.TabStop = false;
            this.Grp_Tools.Text = "工具栏";
            // 
            // CB_FontStyle
            // 
            this.CB_FontStyle.FormattingEnabled = true;
            this.CB_FontStyle.Location = new System.Drawing.Point(735, 53);
            this.CB_FontStyle.Name = "CB_FontStyle";
            this.CB_FontStyle.Size = new System.Drawing.Size(121, 20);
            this.CB_FontStyle.TabIndex = 10;
            this.CB_FontStyle.SelectedIndexChanged += new System.EventHandler(this.CB_FontStyle_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(733, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "字形";
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(1104, 50);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(75, 23);
            this.BT_Save.TabIndex = 8;
            this.BT_Save.Text = "保存";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
            // 
            // CB_LineSpaces
            // 
            this.CB_LineSpaces.FormattingEnabled = true;
            this.CB_LineSpaces.Location = new System.Drawing.Point(544, 53);
            this.CB_LineSpaces.Name = "CB_LineSpaces";
            this.CB_LineSpaces.Size = new System.Drawing.Size(121, 20);
            this.CB_LineSpaces.TabIndex = 7;
            this.CB_LineSpaces.SelectedIndexChanged += new System.EventHandler(this.CB_LineSpaces_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(542, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "行间距";
            // 
            // CB_FontColor
            // 
            this.CB_FontColor.FormattingEnabled = true;
            this.CB_FontColor.Location = new System.Drawing.Point(368, 53);
            this.CB_FontColor.Name = "CB_FontColor";
            this.CB_FontColor.Size = new System.Drawing.Size(121, 20);
            this.CB_FontColor.TabIndex = 5;
            this.CB_FontColor.SelectedIndexChanged += new System.EventHandler(this.CB_FontColor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(366, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "字体颜色";
            // 
            // TB_FontSize
            // 
            this.TB_FontSize.Location = new System.Drawing.Point(197, 53);
            this.TB_FontSize.Name = "TB_FontSize";
            this.TB_FontSize.Size = new System.Drawing.Size(110, 21);
            this.TB_FontSize.TabIndex = 3;
            this.TB_FontSize.TextChanged += new System.EventHandler(this.TB_FontSize_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "字体大小";
            // 
            // CB_FontType
            // 
            this.CB_FontType.FormattingEnabled = true;
            this.CB_FontType.Location = new System.Drawing.Point(19, 53);
            this.CB_FontType.Name = "CB_FontType";
            this.CB_FontType.Size = new System.Drawing.Size(121, 20);
            this.CB_FontType.TabIndex = 1;
            this.CB_FontType.SelectedIndexChanged += new System.EventHandler(this.CB_FontType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字体";
            // 
            // Grp_Txt
            // 
            this.Grp_Txt.Controls.Add(this.RTBox);
            this.Grp_Txt.Location = new System.Drawing.Point(109, 126);
            this.Grp_Txt.Name = "Grp_Txt";
            this.Grp_Txt.Size = new System.Drawing.Size(967, 721);
            this.Grp_Txt.TabIndex = 4;
            this.Grp_Txt.TabStop = false;
            this.Grp_Txt.Text = "文本框";
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
            // BT_Next
            // 
            this.BT_Next.Location = new System.Drawing.Point(1116, 851);
            this.BT_Next.Name = "BT_Next";
            this.BT_Next.Size = new System.Drawing.Size(102, 27);
            this.BT_Next.TabIndex = 5;
            this.BT_Next.Text = "下一页";
            this.BT_Next.UseVisualStyleBackColor = true;
            this.BT_Next.Click += new System.EventHandler(this.BT_Next_Click);
            // 
            // SysDescripConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.BT_Next);
            this.Controls.Add(this.Grp_Txt);
            this.Controls.Add(this.Grp_Tools);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 15);
            this.Name = "SysDescripConfig";
            this.Text = "SysDescripConfig";
            this.Load += new System.EventHandler(this.SysDescripConfig_Load);
            this.Grp_Tools.ResumeLayout(false);
            this.Grp_Tools.PerformLayout();
            this.Grp_Txt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_Tools;
        private System.Windows.Forms.GroupBox Grp_Txt;
        private System.Windows.Forms.RichTextBox RTBox;
        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.ComboBox CB_LineSpaces;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CB_FontColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_FontSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_FontType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BT_Next;
        private System.Windows.Forms.ComboBox CB_FontStyle;
        private System.Windows.Forms.Label label5;

    }
}