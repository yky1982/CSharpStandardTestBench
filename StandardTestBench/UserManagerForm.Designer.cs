namespace StandardTestBench
{
    partial class UserManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManagerForm));
            this.Grp_ModifyPWD = new System.Windows.Forms.GroupBox();
            this.BT_ModifyPWD = new System.Windows.Forms.Button();
            this.LB_PWD_Modify2 = new System.Windows.Forms.Label();
            this.textBox_MDY2 = new System.Windows.Forms.TextBox();
            this.LB_PWD_Modify1 = new System.Windows.Forms.Label();
            this.textBox_MDY1 = new System.Windows.Forms.TextBox();
            this.Grp_AddUser = new System.Windows.Forms.GroupBox();
            this.CB_Permission = new System.Windows.Forms.CheckBox();
            this.BT_CheckUser = new System.Windows.Forms.Button();
            this.button_Add_Clear = new System.Windows.Forms.Button();
            this.LB_PWD_Set2 = new System.Windows.Forms.Label();
            this.textBox_Add_PWD2 = new System.Windows.Forms.TextBox();
            this.button_Add_Add = new System.Windows.Forms.Button();
            this.LB_PWD_Set1 = new System.Windows.Forms.Label();
            this.textBox_Add_PWD1 = new System.Windows.Forms.TextBox();
            this.LB_UserName = new System.Windows.Forms.Label();
            this.textBox_Add_Use = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TB_Exit = new System.Windows.Forms.ToolStripButton();
            this.Grp_Delete = new System.Windows.Forms.GroupBox();
            this.button_Del = new System.Windows.Forms.Button();
            this.CB_UserName = new System.Windows.Forms.ComboBox();
            this.LB_DeleteUserName = new System.Windows.Forms.Label();
            this.Grp_ModifyPWD.SuspendLayout();
            this.Grp_AddUser.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.Grp_Delete.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_ModifyPWD
            // 
            this.Grp_ModifyPWD.Controls.Add(this.BT_ModifyPWD);
            this.Grp_ModifyPWD.Controls.Add(this.LB_PWD_Modify2);
            this.Grp_ModifyPWD.Controls.Add(this.textBox_MDY2);
            this.Grp_ModifyPWD.Controls.Add(this.LB_PWD_Modify1);
            this.Grp_ModifyPWD.Controls.Add(this.textBox_MDY1);
            this.Grp_ModifyPWD.Location = new System.Drawing.Point(277, 126);
            this.Grp_ModifyPWD.Name = "Grp_ModifyPWD";
            this.Grp_ModifyPWD.Size = new System.Drawing.Size(612, 191);
            this.Grp_ModifyPWD.TabIndex = 0;
            this.Grp_ModifyPWD.TabStop = false;
            this.Grp_ModifyPWD.Text = "密码修改";
            // 
            // BT_ModifyPWD
            // 
            this.BT_ModifyPWD.Location = new System.Drawing.Point(451, 126);
            this.BT_ModifyPWD.Name = "BT_ModifyPWD";
            this.BT_ModifyPWD.Size = new System.Drawing.Size(75, 23);
            this.BT_ModifyPWD.TabIndex = 4;
            this.BT_ModifyPWD.Text = "确认";
            this.BT_ModifyPWD.UseVisualStyleBackColor = true;
            this.BT_ModifyPWD.Click += new System.EventHandler(this.BT_ModifyPWD_Click);
            // 
            // LB_PWD_Modify2
            // 
            this.LB_PWD_Modify2.AutoSize = true;
            this.LB_PWD_Modify2.Location = new System.Drawing.Point(364, 48);
            this.LB_PWD_Modify2.Name = "LB_PWD_Modify2";
            this.LB_PWD_Modify2.Size = new System.Drawing.Size(89, 12);
            this.LB_PWD_Modify2.TabIndex = 3;
            this.LB_PWD_Modify2.Text = "请再次输入密码";
            // 
            // textBox_MDY2
            // 
            this.textBox_MDY2.Location = new System.Drawing.Point(362, 66);
            this.textBox_MDY2.Name = "textBox_MDY2";
            this.textBox_MDY2.PasswordChar = '*';
            this.textBox_MDY2.Size = new System.Drawing.Size(164, 21);
            this.textBox_MDY2.TabIndex = 2;
            // 
            // LB_PWD_Modify1
            // 
            this.LB_PWD_Modify1.AutoSize = true;
            this.LB_PWD_Modify1.Location = new System.Drawing.Point(68, 48);
            this.LB_PWD_Modify1.Name = "LB_PWD_Modify1";
            this.LB_PWD_Modify1.Size = new System.Drawing.Size(65, 12);
            this.LB_PWD_Modify1.TabIndex = 1;
            this.LB_PWD_Modify1.Text = "请输入密码";
            // 
            // textBox_MDY1
            // 
            this.textBox_MDY1.Location = new System.Drawing.Point(66, 66);
            this.textBox_MDY1.Name = "textBox_MDY1";
            this.textBox_MDY1.PasswordChar = '*';
            this.textBox_MDY1.Size = new System.Drawing.Size(164, 21);
            this.textBox_MDY1.TabIndex = 0;
            // 
            // Grp_AddUser
            // 
            this.Grp_AddUser.Controls.Add(this.CB_Permission);
            this.Grp_AddUser.Controls.Add(this.BT_CheckUser);
            this.Grp_AddUser.Controls.Add(this.button_Add_Clear);
            this.Grp_AddUser.Controls.Add(this.LB_PWD_Set2);
            this.Grp_AddUser.Controls.Add(this.textBox_Add_PWD2);
            this.Grp_AddUser.Controls.Add(this.button_Add_Add);
            this.Grp_AddUser.Controls.Add(this.LB_PWD_Set1);
            this.Grp_AddUser.Controls.Add(this.textBox_Add_PWD1);
            this.Grp_AddUser.Controls.Add(this.LB_UserName);
            this.Grp_AddUser.Controls.Add(this.textBox_Add_Use);
            this.Grp_AddUser.Location = new System.Drawing.Point(277, 346);
            this.Grp_AddUser.Name = "Grp_AddUser";
            this.Grp_AddUser.Size = new System.Drawing.Size(612, 286);
            this.Grp_AddUser.TabIndex = 5;
            this.Grp_AddUser.TabStop = false;
            this.Grp_AddUser.Text = "添加用户";
            // 
            // CB_Permission
            // 
            this.CB_Permission.AutoSize = true;
            this.CB_Permission.Location = new System.Drawing.Point(111, 210);
            this.CB_Permission.Name = "CB_Permission";
            this.CB_Permission.Size = new System.Drawing.Size(156, 16);
            this.CB_Permission.TabIndex = 9;
            this.CB_Permission.Text = "允许修改设置参数配置表";
            this.CB_Permission.UseVisualStyleBackColor = true;
            // 
            // BT_CheckUser
            // 
            this.BT_CheckUser.Location = new System.Drawing.Point(451, 49);
            this.BT_CheckUser.Name = "BT_CheckUser";
            this.BT_CheckUser.Size = new System.Drawing.Size(75, 23);
            this.BT_CheckUser.TabIndex = 8;
            this.BT_CheckUser.Text = "检测用户名";
            this.BT_CheckUser.UseVisualStyleBackColor = true;
            this.BT_CheckUser.Click += new System.EventHandler(this.BT_CheckUser_Click);
            // 
            // button_Add_Clear
            // 
            this.button_Add_Clear.Location = new System.Drawing.Point(451, 253);
            this.button_Add_Clear.Name = "button_Add_Clear";
            this.button_Add_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Add_Clear.TabIndex = 7;
            this.button_Add_Clear.Text = "清除";
            this.button_Add_Clear.UseVisualStyleBackColor = true;
            this.button_Add_Clear.Click += new System.EventHandler(this.button_Add_Clear_Click);
            // 
            // LB_PWD_Set2
            // 
            this.LB_PWD_Set2.AutoSize = true;
            this.LB_PWD_Set2.Location = new System.Drawing.Point(113, 145);
            this.LB_PWD_Set2.Name = "LB_PWD_Set2";
            this.LB_PWD_Set2.Size = new System.Drawing.Size(89, 12);
            this.LB_PWD_Set2.TabIndex = 6;
            this.LB_PWD_Set2.Text = "请再次输入密码";
            // 
            // textBox_Add_PWD2
            // 
            this.textBox_Add_PWD2.Location = new System.Drawing.Point(111, 163);
            this.textBox_Add_PWD2.Name = "textBox_Add_PWD2";
            this.textBox_Add_PWD2.PasswordChar = '*';
            this.textBox_Add_PWD2.Size = new System.Drawing.Size(415, 21);
            this.textBox_Add_PWD2.TabIndex = 5;
            // 
            // button_Add_Add
            // 
            this.button_Add_Add.Location = new System.Drawing.Point(339, 253);
            this.button_Add_Add.Name = "button_Add_Add";
            this.button_Add_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add_Add.TabIndex = 4;
            this.button_Add_Add.Text = "添加";
            this.button_Add_Add.UseVisualStyleBackColor = true;
            this.button_Add_Add.Click += new System.EventHandler(this.button_Add_Add_Click);
            // 
            // LB_PWD_Set1
            // 
            this.LB_PWD_Set1.AutoSize = true;
            this.LB_PWD_Set1.Location = new System.Drawing.Point(113, 89);
            this.LB_PWD_Set1.Name = "LB_PWD_Set1";
            this.LB_PWD_Set1.Size = new System.Drawing.Size(89, 12);
            this.LB_PWD_Set1.TabIndex = 3;
            this.LB_PWD_Set1.Text = "请再次输入密码";
            // 
            // textBox_Add_PWD1
            // 
            this.textBox_Add_PWD1.Location = new System.Drawing.Point(111, 106);
            this.textBox_Add_PWD1.Name = "textBox_Add_PWD1";
            this.textBox_Add_PWD1.PasswordChar = '*';
            this.textBox_Add_PWD1.Size = new System.Drawing.Size(415, 21);
            this.textBox_Add_PWD1.TabIndex = 2;
            // 
            // LB_UserName
            // 
            this.LB_UserName.AutoSize = true;
            this.LB_UserName.Location = new System.Drawing.Point(113, 33);
            this.LB_UserName.Name = "LB_UserName";
            this.LB_UserName.Size = new System.Drawing.Size(41, 12);
            this.LB_UserName.TabIndex = 1;
            this.LB_UserName.Text = "用户名";
            // 
            // textBox_Add_Use
            // 
            this.textBox_Add_Use.Location = new System.Drawing.Point(111, 51);
            this.textBox_Add_Use.Name = "textBox_Add_Use";
            this.textBox_Add_Use.Size = new System.Drawing.Size(303, 21);
            this.textBox_Add_Use.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1200, 93);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
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
            // Grp_Delete
            // 
            this.Grp_Delete.Controls.Add(this.button_Del);
            this.Grp_Delete.Controls.Add(this.CB_UserName);
            this.Grp_Delete.Controls.Add(this.LB_DeleteUserName);
            this.Grp_Delete.Location = new System.Drawing.Point(277, 681);
            this.Grp_Delete.Name = "Grp_Delete";
            this.Grp_Delete.Size = new System.Drawing.Size(612, 123);
            this.Grp_Delete.TabIndex = 9;
            this.Grp_Delete.TabStop = false;
            this.Grp_Delete.Text = "删除用户";
            // 
            // button_Del
            // 
            this.button_Del.Location = new System.Drawing.Point(451, 82);
            this.button_Del.Name = "button_Del";
            this.button_Del.Size = new System.Drawing.Size(75, 23);
            this.button_Del.TabIndex = 2;
            this.button_Del.Text = "删除";
            this.button_Del.UseVisualStyleBackColor = true;
            this.button_Del.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // CB_UserName
            // 
            this.CB_UserName.FormattingEnabled = true;
            this.CB_UserName.Location = new System.Drawing.Point(111, 51);
            this.CB_UserName.Name = "CB_UserName";
            this.CB_UserName.Size = new System.Drawing.Size(415, 20);
            this.CB_UserName.TabIndex = 1;
            // 
            // LB_DeleteUserName
            // 
            this.LB_DeleteUserName.AutoSize = true;
            this.LB_DeleteUserName.Location = new System.Drawing.Point(113, 30);
            this.LB_DeleteUserName.Name = "LB_DeleteUserName";
            this.LB_DeleteUserName.Size = new System.Drawing.Size(41, 12);
            this.LB_DeleteUserName.TabIndex = 0;
            this.LB_DeleteUserName.Text = "用户名";
            // 
            // UserManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 890);
            this.Controls.Add(this.Grp_Delete);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Grp_AddUser);
            this.Controls.Add(this.Grp_ModifyPWD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "UserManagerForm";
            this.Text = "UserManagerForm";
            this.Load += new System.EventHandler(this.UserManagerForm_Load);
            this.Grp_ModifyPWD.ResumeLayout(false);
            this.Grp_ModifyPWD.PerformLayout();
            this.Grp_AddUser.ResumeLayout(false);
            this.Grp_AddUser.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Grp_Delete.ResumeLayout(false);
            this.Grp_Delete.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_ModifyPWD;
        private System.Windows.Forms.Label LB_PWD_Modify2;
        private System.Windows.Forms.TextBox textBox_MDY2;
        private System.Windows.Forms.Label LB_PWD_Modify1;
        private System.Windows.Forms.TextBox textBox_MDY1;
        private System.Windows.Forms.Button BT_ModifyPWD;
        private System.Windows.Forms.GroupBox Grp_AddUser;
        private System.Windows.Forms.Button button_Add_Clear;
        private System.Windows.Forms.Label LB_PWD_Set2;
        private System.Windows.Forms.TextBox textBox_Add_PWD2;
        private System.Windows.Forms.Button button_Add_Add;
        private System.Windows.Forms.Label LB_PWD_Set1;
        private System.Windows.Forms.TextBox textBox_Add_PWD1;
        private System.Windows.Forms.Label LB_UserName;
        private System.Windows.Forms.TextBox textBox_Add_Use;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TB_Exit;
        private System.Windows.Forms.GroupBox Grp_Delete;
        private System.Windows.Forms.ComboBox CB_UserName;
        private System.Windows.Forms.Label LB_DeleteUserName;
        private System.Windows.Forms.Button button_Del;
        private System.Windows.Forms.Button BT_CheckUser;
        private System.Windows.Forms.CheckBox CB_Permission;

    }
}