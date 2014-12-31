namespace StandardTestBench
{
    partial class AboutForm
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
            this.BT_Save = new System.Windows.Forms.Button();
            this.LB_Address = new System.Windows.Forms.Label();
            this.LB_Internet = new System.Windows.Forms.Label();
            this.LB_Fax = new System.Windows.Forms.Label();
            this.LB_Phone = new System.Windows.Forms.Label();
            this.LB_Editor = new System.Windows.Forms.Label();
            this.LB_Product = new System.Windows.Forms.Label();
            this.LB_Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(261, 216);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(75, 23);
            this.BT_Save.TabIndex = 20;
            this.BT_Save.Text = "确定";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.button1_Click);
            // 
            // LB_Address
            // 
            this.LB_Address.AutoSize = true;
            this.LB_Address.Location = new System.Drawing.Point(70, 193);
            this.LB_Address.Name = "LB_Address";
            this.LB_Address.Size = new System.Drawing.Size(161, 12);
            this.LB_Address.TabIndex = 19;
            this.LB_Address.Text = "公司地址：上海市东陆路89号";
            // 
            // LB_Internet
            // 
            this.LB_Internet.AutoSize = true;
            this.LB_Internet.Location = new System.Drawing.Point(70, 164);
            this.LB_Internet.Name = "LB_Internet";
            this.LB_Internet.Size = new System.Drawing.Size(161, 12);
            this.LB_Internet.TabIndex = 18;
            this.LB_Internet.Text = "公司网址：www.maximator.cn";
            // 
            // LB_Fax
            // 
            this.LB_Fax.AutoSize = true;
            this.LB_Fax.Location = new System.Drawing.Point(70, 135);
            this.LB_Fax.Name = "LB_Fax";
            this.LB_Fax.Size = new System.Drawing.Size(143, 12);
            this.LB_Fax.TabIndex = 17;
            this.LB_Fax.Text = "公司传真：021-5868 0926";
            // 
            // LB_Phone
            // 
            this.LB_Phone.AutoSize = true;
            this.LB_Phone.Location = new System.Drawing.Point(70, 106);
            this.LB_Phone.Name = "LB_Phone";
            this.LB_Phone.Size = new System.Drawing.Size(143, 12);
            this.LB_Phone.TabIndex = 16;
            this.LB_Phone.Text = "公司电话：021-5868 2266";
            // 
            // LB_Editor
            // 
            this.LB_Editor.AutoSize = true;
            this.LB_Editor.Location = new System.Drawing.Point(70, 77);
            this.LB_Editor.Name = "LB_Editor";
            this.LB_Editor.Size = new System.Drawing.Size(83, 12);
            this.LB_Editor.TabIndex = 15;
            this.LB_Editor.Text = "软件版本：1.0";
            // 
            // LB_Product
            // 
            this.LB_Product.AutoSize = true;
            this.LB_Product.Location = new System.Drawing.Point(70, 48);
            this.LB_Product.Name = "LB_Product";
            this.LB_Product.Size = new System.Drawing.Size(149, 12);
            this.LB_Product.TabIndex = 14;
            this.LB_Product.Text = "产品名称：静压爆破试验台";
            // 
            // LB_Name
            // 
            this.LB_Name.AutoSize = true;
            this.LB_Name.Location = new System.Drawing.Point(70, 19);
            this.LB_Name.Name = "LB_Name";
            this.LB_Name.Size = new System.Drawing.Size(257, 12);
            this.LB_Name.TabIndex = 21;
            this.LB_Name.Text = "公司名称：麦格思维特(上海)流体工程有限公司";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.ControlBox = false;
            this.Controls.Add(this.LB_Name);
            this.Controls.Add(this.BT_Save);
            this.Controls.Add(this.LB_Address);
            this.Controls.Add(this.LB_Internet);
            this.Controls.Add(this.LB_Fax);
            this.Controls.Add(this.LB_Phone);
            this.Controls.Add(this.LB_Editor);
            this.Controls.Add(this.LB_Product);
            this.Name = "AboutForm";
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.Label LB_Address;
        private System.Windows.Forms.Label LB_Internet;
        private System.Windows.Forms.Label LB_Fax;
        private System.Windows.Forms.Label LB_Phone;
        private System.Windows.Forms.Label LB_Editor;
        private System.Windows.Forms.Label LB_Product;
        private System.Windows.Forms.Label LB_Name;
    }
}