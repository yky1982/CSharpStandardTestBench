namespace StandardTestBench
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MenuItem_ResetPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_SensorAdj = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Maintance = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_Log_Cancel = new System.Windows.Forms.Button();
            this.button_Log_Sure = new System.Windows.Forms.Button();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.Grp_Login = new System.Windows.Forms.GroupBox();
            this.label_Login_Info = new System.Windows.Forms.Label();
            this.comboBox_UserName = new System.Windows.Forms.ComboBox();
            this.label_Password = new System.Windows.Forms.Label();
            this.label_UserName = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_LogInUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_PageInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_DisSysStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_SystemInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_DisTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuItem_User = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_UserLogIn = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_UserLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.MenuItem_SystemDiscrition = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Practice = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_DataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Alarm = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_UserManager = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Abort = new System.Windows.Forms.ToolStripMenuItem();
            this.Grp_DisArea = new System.Windows.Forms.GroupBox();
            this.PicBox_BackGroud = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.Grp_Login.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.Grp_DisArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_BackGroud)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuItem_ResetPLC
            // 
            this.MenuItem_ResetPLC.Name = "MenuItem_ResetPLC";
            this.MenuItem_ResetPLC.Size = new System.Drawing.Size(184, 22);
            this.MenuItem_ResetPLC.Text = "PLC恢复出厂设置(&S)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(181, 6);
            // 
            // MenuItem_SensorAdj
            // 
            this.MenuItem_SensorAdj.Name = "MenuItem_SensorAdj";
            this.MenuItem_SensorAdj.Size = new System.Drawing.Size(184, 22);
            this.MenuItem_SensorAdj.Text = "传感器校验(&J)";
            this.MenuItem_SensorAdj.Click += new System.EventHandler(this.MenuItem_SensorAdj_Click);
            // 
            // MenuItem_Maintance
            // 
            this.MenuItem_Maintance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_SensorAdj,
            this.toolStripMenuItem3,
            this.MenuItem_ResetPLC});
            this.MenuItem_Maintance.Name = "MenuItem_Maintance";
            this.MenuItem_Maintance.Size = new System.Drawing.Size(88, 21);
            this.MenuItem_Maintance.Text = "系统维护(&M)";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(183, 34);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // button_Log_Cancel
            // 
            this.button_Log_Cancel.Location = new System.Drawing.Point(143, 116);
            this.button_Log_Cancel.Name = "button_Log_Cancel";
            this.button_Log_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Log_Cancel.TabIndex = 5;
            this.button_Log_Cancel.Text = "取消";
            this.button_Log_Cancel.UseVisualStyleBackColor = true;
            this.button_Log_Cancel.Click += new System.EventHandler(this.button_Log_Cancel_Click);
            // 
            // button_Log_Sure
            // 
            this.button_Log_Sure.Location = new System.Drawing.Point(30, 116);
            this.button_Log_Sure.Name = "button_Log_Sure";
            this.button_Log_Sure.Size = new System.Drawing.Size(75, 23);
            this.button_Log_Sure.TabIndex = 4;
            this.button_Log_Sure.Text = "确定";
            this.button_Log_Sure.UseVisualStyleBackColor = true;
            this.button_Log_Sure.Click += new System.EventHandler(this.button_Log_Sure_Click);
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(55, 67);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(121, 21);
            this.textBox_Password.TabIndex = 3;
            // 
            // Grp_Login
            // 
            this.Grp_Login.Controls.Add(this.label_Login_Info);
            this.Grp_Login.Controls.Add(this.pictureBox2);
            this.Grp_Login.Controls.Add(this.button_Log_Cancel);
            this.Grp_Login.Controls.Add(this.button_Log_Sure);
            this.Grp_Login.Controls.Add(this.textBox_Password);
            this.Grp_Login.Controls.Add(this.comboBox_UserName);
            this.Grp_Login.Controls.Add(this.label_Password);
            this.Grp_Login.Controls.Add(this.label_UserName);
            this.Grp_Login.Location = new System.Drawing.Point(430, 420);
            this.Grp_Login.Name = "Grp_Login";
            this.Grp_Login.Size = new System.Drawing.Size(237, 143);
            this.Grp_Login.TabIndex = 8;
            this.Grp_Login.TabStop = false;
            this.Grp_Login.Text = "登陆框";
            // 
            // label_Login_Info
            // 
            this.label_Login_Info.AutoSize = true;
            this.label_Login_Info.Location = new System.Drawing.Point(99, 97);
            this.label_Login_Info.Name = "label_Login_Info";
            this.label_Login_Info.Size = new System.Drawing.Size(41, 12);
            this.label_Login_Info.TabIndex = 7;
            this.label_Login_Info.Text = "label1";
            // 
            // comboBox_UserName
            // 
            this.comboBox_UserName.FormattingEnabled = true;
            this.comboBox_UserName.Location = new System.Drawing.Point(55, 31);
            this.comboBox_UserName.Name = "comboBox_UserName";
            this.comboBox_UserName.Size = new System.Drawing.Size(121, 20);
            this.comboBox_UserName.TabIndex = 2;
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(8, 70);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(29, 12);
            this.label_Password.TabIndex = 1;
            this.label_Password.Text = "密码";
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(8, 34);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(41, 12);
            this.label_UserName.TabIndex = 0;
            this.label_UserName.Text = "用户名";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_LogInUser,
            this.StatusLabel_PageInfo,
            this.StatusLabel_DisSysStatus,
            this.StatusLabel_SystemInfo,
            this.StatusLabel_DisTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 956);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 30);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel_LogInUser
            // 
            this.StatusLabel_LogInUser.AutoSize = false;
            this.StatusLabel_LogInUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.StatusLabel_LogInUser.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusLabel_LogInUser.Name = "StatusLabel_LogInUser";
            this.StatusLabel_LogInUser.Padding = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.StatusLabel_LogInUser.Size = new System.Drawing.Size(200, 25);
            this.StatusLabel_LogInUser.Text = "当前用户";
            // 
            // StatusLabel_PageInfo
            // 
            this.StatusLabel_PageInfo.AutoSize = false;
            this.StatusLabel_PageInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusLabel_PageInfo.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusLabel_PageInfo.Name = "StatusLabel_PageInfo";
            this.StatusLabel_PageInfo.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.StatusLabel_PageInfo.Size = new System.Drawing.Size(200, 25);
            this.StatusLabel_PageInfo.Text = "登陆界面";
            // 
            // StatusLabel_DisSysStatus
            // 
            this.StatusLabel_DisSysStatus.AutoSize = false;
            this.StatusLabel_DisSysStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusLabel_DisSysStatus.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusLabel_DisSysStatus.Name = "StatusLabel_DisSysStatus";
            this.StatusLabel_DisSysStatus.Padding = new System.Windows.Forms.Padding(0, 0, 480, 0);
            this.StatusLabel_DisSysStatus.Size = new System.Drawing.Size(450, 25);
            this.StatusLabel_DisSysStatus.Text = "运行";
            // 
            // StatusLabel_SystemInfo
            // 
            this.StatusLabel_SystemInfo.AutoSize = false;
            this.StatusLabel_SystemInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusLabel_SystemInfo.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusLabel_SystemInfo.Name = "StatusLabel_SystemInfo";
            this.StatusLabel_SystemInfo.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.StatusLabel_SystemInfo.Size = new System.Drawing.Size(150, 25);
            this.StatusLabel_SystemInfo.Text = "系统信息";
            this.StatusLabel_SystemInfo.Visible = false;
            // 
            // StatusLabel_DisTime
            // 
            this.StatusLabel_DisTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusLabel_DisTime.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.StatusLabel_DisTime.Name = "StatusLabel_DisTime";
            this.StatusLabel_DisTime.Padding = new System.Windows.Forms.Padding(0, 0, 144, 0);
            this.StatusLabel_DisTime.Size = new System.Drawing.Size(204, 25);
            this.StatusLabel_DisTime.Text = "时间显示";
            // 
            // MenuItem_User
            // 
            this.MenuItem_User.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_UserLogIn,
            this.MenuItem_UserLogOut,
            this.toolStripMenuItem2,
            this.MenuItem_Exit});
            this.MenuItem_User.Name = "MenuItem_User";
            this.MenuItem_User.Size = new System.Drawing.Size(85, 21);
            this.MenuItem_User.Text = "用户登录(&U)";
            // 
            // MenuItem_UserLogIn
            // 
            this.MenuItem_UserLogIn.Name = "MenuItem_UserLogIn";
            this.MenuItem_UserLogIn.Size = new System.Drawing.Size(118, 22);
            this.MenuItem_UserLogIn.Text = "登录(&L)";
            this.MenuItem_UserLogIn.Click += new System.EventHandler(this.MenuItem_UserLogIn_Click);
            // 
            // MenuItem_UserLogOut
            // 
            this.MenuItem_UserLogOut.Name = "MenuItem_UserLogOut";
            this.MenuItem_UserLogOut.Size = new System.Drawing.Size(118, 22);
            this.MenuItem_UserLogOut.Text = "注销(&Q)";
            this.MenuItem_UserLogOut.Click += new System.EventHandler(this.MenuItem_UserLogOut_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(115, 6);
            // 
            // MenuItem_Exit
            // 
            this.MenuItem_Exit.Name = "MenuItem_Exit";
            this.MenuItem_Exit.Size = new System.Drawing.Size(118, 22);
            this.MenuItem_Exit.Text = "退出(&E)";
            this.MenuItem_Exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_User,
            this.MenuItem_SystemDiscrition,
            this.MenuItem_Practice,
            this.MenuItem_DataBase,
            this.MenuItem_Alarm,
            this.MenuItem_Maintance,
            this.MenuItem_UserManager,
            this.MenuItem_Abort});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(1264, 25);
            this.MenuBar.TabIndex = 6;
            this.MenuBar.Text = "menuStrip1";
            // 
            // MenuItem_SystemDiscrition
            // 
            this.MenuItem_SystemDiscrition.Name = "MenuItem_SystemDiscrition";
            this.MenuItem_SystemDiscrition.Size = new System.Drawing.Size(85, 21);
            this.MenuItem_SystemDiscrition.Text = "系统描述(&D)";
            this.MenuItem_SystemDiscrition.Click += new System.EventHandler(this.MenuItem_SystemDiscrition_Click);
            // 
            // MenuItem_Practice
            // 
            this.MenuItem_Practice.Name = "MenuItem_Practice";
            this.MenuItem_Practice.Size = new System.Drawing.Size(86, 21);
            this.MenuItem_Practice.Text = "试验设置(&O)";
            this.MenuItem_Practice.Click += new System.EventHandler(this.MenuItem_Practice_Click);
            // 
            // MenuItem_DataBase
            // 
            this.MenuItem_DataBase.Name = "MenuItem_DataBase";
            this.MenuItem_DataBase.Size = new System.Drawing.Size(83, 21);
            this.MenuItem_DataBase.Text = "数据查询(&E)";
            this.MenuItem_DataBase.Click += new System.EventHandler(this.MenuItem_DataBase_Click);
            // 
            // MenuItem_Alarm
            // 
            this.MenuItem_Alarm.Name = "MenuItem_Alarm";
            this.MenuItem_Alarm.Size = new System.Drawing.Size(60, 21);
            this.MenuItem_Alarm.Text = "告警(&A)";
            this.MenuItem_Alarm.Click += new System.EventHandler(this.MenuItem_Alarm_Click);
            // 
            // MenuItem_UserManager
            // 
            this.MenuItem_UserManager.Name = "MenuItem_UserManager";
            this.MenuItem_UserManager.Size = new System.Drawing.Size(68, 21);
            this.MenuItem_UserManager.Text = "用户管理";
            this.MenuItem_UserManager.Click += new System.EventHandler(this.MenuItem_UserManager_Click);
            // 
            // MenuItem_Abort
            // 
            this.MenuItem_Abort.Name = "MenuItem_Abort";
            this.MenuItem_Abort.Size = new System.Drawing.Size(44, 21);
            this.MenuItem_Abort.Text = "关于";
            this.MenuItem_Abort.Click += new System.EventHandler(this.MenuItem_Abort_Click);
            // 
            // Grp_DisArea
            // 
            this.Grp_DisArea.Controls.Add(this.PicBox_BackGroud);
            this.Grp_DisArea.Location = new System.Drawing.Point(13, 28);
            this.Grp_DisArea.Name = "Grp_DisArea";
            this.Grp_DisArea.Size = new System.Drawing.Size(1240, 900);
            this.Grp_DisArea.TabIndex = 7;
            this.Grp_DisArea.TabStop = false;
            // 
            // PicBox_BackGroud
            // 
            this.PicBox_BackGroud.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PicBox_BackGroud.BackgroundImage")));
            this.PicBox_BackGroud.Location = new System.Drawing.Point(12, 20);
            this.PicBox_BackGroud.Name = "PicBox_BackGroud";
            this.PicBox_BackGroud.Size = new System.Drawing.Size(1228, 894);
            this.PicBox_BackGroud.TabIndex = 5;
            this.PicBox_BackGroud.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1264, 986);
            this.Controls.Add(this.Grp_DisArea);
            this.Controls.Add(this.Grp_Login);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MenuBar);
            this.Name = "Form1";
            this.Text = "Maximator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.Grp_Login.ResumeLayout(false);
            this.Grp_Login.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.Grp_DisArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_BackGroud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem MenuItem_ResetPLC;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SensorAdj;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Maintance;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button_Log_Cancel;
        private System.Windows.Forms.Button button_Log_Sure;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.GroupBox Grp_Login;
        private System.Windows.Forms.ComboBox comboBox_UserName;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_LogInUser;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_PageInfo;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_DisSysStatus;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_SystemInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_User;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_UserLogIn;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_UserLogOut;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SystemDiscrition;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Practice;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DataBase;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Alarm;
        private System.Windows.Forms.Label label_Login_Info;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_DisTime;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_UserManager;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Abort;
        private System.Windows.Forms.GroupBox Grp_DisArea;
        private System.Windows.Forms.PictureBox PicBox_BackGroud;
    }
}

