using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CommDriveBaseXML;
using System.Runtime.InteropServices;

using System.IO;

namespace StandardTestBench
{
    public delegate void StateUserLogin(string sContent);
    public delegate void StatePageInfo(string sContent);
    public delegate void StateRun(string sContent);
    public delegate void StateSysInfo(string sContent); 
    public partial class Form1 : Form
    {
        #region Com
        [DllImport("CommDriveBaseXML.dll", EntryPoint = "Init", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Init(string XMLFilePath, string ProtocolType, string DriveType);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "GetCommState", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetCommState(ref bool CommState);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteBlockData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteBlockData(int BlockNum, byte[] data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref bool data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref byte data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref sbyte data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref Int16 data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref UInt16 data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref Int32 data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref UInt32 data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadData(string RegName, ref float data, ref DateTime dt);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, bool Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, byte Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, sbyte Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, Int16 Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, UInt16 Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, Int32 Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, UInt32 Data);

        [DllImport("CommDriveBaseXML.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteData(string RegName, float Data);
        #endregion

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        public Form1()
        {
            InitializeComponent();
        }

        private string m_BaseFilePath = Application.StartupPath;
        private static Form1 m_SelfHandle = null;
        private UserManager m_UserManagerHandle = null;
        private FileManger m_FileMangerHandle = null;
        private AlarmManage m_AlarmManageHandle = null;

        private string m_XMLFilePath = Application.StartupPath + "\\SystemFile\\sys\\xmlConfig.xml";
        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";
        private string m_SystemDebugInfoFilePath = Application.StartupPath + @"\SystemFile\SystemDebugInfo.txt";

        //系统信息
        private string mSystemLanguage = "";
        private bool misSaveDebugInfo = false;
        private bool misDisDebugInfo = false;
         
        private string m_DriveType = "";
        private string m_ProtocolType = "";
        public bool m_isTesting = false;
        public IComm m_DriveHandle = new IComm();
        public System.Windows.Forms.Timer m_Timer = new System.Windows.Forms.Timer();
        private string m_SystemInfo = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            m_FileMangerHandle = FileManger.GetHandle();
            m_FileMangerHandle.CreatFileFold();
            
            m_SelfHandle = this;
            m_UserManagerHandle = UserManager.GetHandle();

            m_AlarmManageHandle = AlarmManage.GetHandle();
            m_AlarmManageHandle.Init();

            StatusLabel_LogInUser.Text = "当前用户：无";
            StatusLabel_PageInfo.Text = "登录界面";
            textBox_Password.PasswordChar = '*';
            Grp_DisArea.Visible = false;

            FormDisConfig("Default");

            PicBox_BackGroud.Visible = true;
            Grp_Login.Visible = false;
            label_Login_Info.Visible = false;
        }

        private void SystemInit()
        {
            UILanugageInit();

            string sSaveDebugInfo = ContentValue("SystemCofig", "SaveDebugInfo", m_INISystemConfigFilePath);
            string sDisDebugInfo = ContentValue("SystemCofig", "DisDebugInfo", m_INISystemConfigFilePath);

            if (sSaveDebugInfo == "True")
            {
                misSaveDebugInfo = true;
            }
            if (sSaveDebugInfo == "False")
            {
                misSaveDebugInfo = false;
            }

            if (sDisDebugInfo == "True")
            {
                misDisDebugInfo = true;
                StatusLabel_SystemInfo.Visible = true;
            }
            if (sDisDebugInfo == "False")
            {
                misDisDebugInfo = false;
                StatusLabel_SystemInfo.Visible = false;
            }

            m_Timer.Interval = 4000;
            m_Timer.Tick += new EventHandler(TimerFun);
            return;
            m_Timer.Start();


            //DriveInit();
        }

        /// <summary>
        /// PLC驱动初始化
        /// </summary>
        private void DriveInit()
        {
            m_ProtocolType = "S7200";
            m_DriveType = "Socket";
            int code = m_DriveHandle.Init(m_XMLFilePath, m_ProtocolType, m_DriveType);
            if (code != 1)
            {
                MessageBox.Show("初始化失败, 错误代码：" + code.ToString());
            }
        }

        private void UILanugageInit()
        {
            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);
            mSystemLanguage = sLanguage;
            if (sLanguage == "English")
            {
                MenuItem_User.Text = "User Login";
                MenuItem_UserLogIn.Text = "Login In";
                MenuItem_UserLogOut.Text = "Login Out";
                MenuItem_Exit.Text = "Exit";

                MenuItem_SystemDiscrition.Text = "System Description";

                MenuItem_Practice.Text = "Set Parameter";

                MenuItem_DataBase.Text = "DataBase";

                MenuItem_Alarm.Text = "Alarm";

                MenuItem_Maintance.Text = "Maintance";
                MenuItem_SensorAdj.Text = "Sensor Adjust";
                MenuItem_ResetPLC.Text = "Reset PLC";

                MenuItem_UserManager.Text = "User Manager";

                MenuItem_Abort.Text = "Abort";

                StatusLabel_LogInUser.Text = "Current User：None";
                StatusLabel_PageInfo.Text = "Login Form";

                Grp_Login.Text = "Login";
                label_UserName.Text = "Name";
                label_Password.Text = "Password";
                button_Log_Sure.Text = "Confirm";
                button_Log_Cancel.Text = "Cancel";

                StatusLabel_DisSysStatus.Text = "Running";

            }
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void TimerFun(object o, EventArgs e)
        {
            AlarmFun();

            ShowSystemInfo(m_SystemInfo);

            StatusLabel_DisTime.Text = DateTime.Now.ToString("MM/DD/YYYY  hh:mm");
        }

        public static Form1 GetHandle()
        {
            return m_SelfHandle;
        }

        private void MenuItem_UserLogIn_Click(object sender, EventArgs e)
        {
            Grp_Login.Visible = true;
            textBox_Password.Focus();
        }

        private void MenuItem_UserLogOut_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            if (mSystemLanguage == "English")
            {
                MsgBoxResult = MessageBox.Show("Login Out？",//对话框的显示内容
                "Info",//对话框的标题
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            }
            else
            {
                MsgBoxResult = MessageBox.Show("确定注销本用户吗？",//对话框的显示内容
                "提示",//对话框的标题
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样s
            }

            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                if (m_PreFormHandle != null)
                {
                    m_PreFormHandle.Close();
                }              
                Grp_DisArea.Visible = false;
                StatusLabel_LogInUser.Text = "当前用户：无";
                ShowPageInfo("主界面");
                if (mSystemLanguage == "English")
                {
                    StatusLabel_LogInUser.Text = "Current User : None";
                    ShowPageInfo("Main Form");
                }
                FormDisConfig("Default");
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            if (mSystemLanguage == "English")
            {
                MsgBoxResult = MessageBox.Show("Exit？",//对话框的显示内容
                "Info",//对话框的标题
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            }
            else
            {
                MsgBoxResult = MessageBox.Show("确定退出本程序吗？",//对话框的显示内容
                "提示",//对话框的标题
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
                MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            }

            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                Application.Exit();
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }

        private void button_Log_Sure_Click(object sender, EventArgs e)
        {
            string userName = comboBox_UserName.Text;
            string passWord = textBox_Password.Text;

            string userAuthority = default(string);
            bool isFind = false;
            int returnCode = 0;
            m_UserManagerHandle.CheckUserInfo(userName, passWord, ref userAuthority, ref isFind, ref returnCode);

            if (!isFind)
            {
                if (returnCode == 1)
                {
                    label_Login_Info.ForeColor = Color.Red;
                    label_Login_Info.Text = "用户不存在";
                    if (mSystemLanguage == "English")
                    {
                        label_Login_Info.Text = "User non-existent";
                    }
                }
                if (returnCode == 2)
                {
                    label_Login_Info.ForeColor = Color.Red;
                    label_Login_Info.Text = "密码错误";
                    if (mSystemLanguage == "English")
                    {
                        label_Login_Info.Text = "Pwd Error";
                    }
                }
                label_Login_Info.Visible = true;
                textBox_Password.Text = "";
                textBox_Password.Focus();
            }
            else
            {
                Grp_Login.Visible = false;
                label_Login_Info.Visible = false;
                Grp_DisArea.Visible = true;
                FormDisConfig(m_UserManagerHandle.m_CurrenUserType);
                textBox_Password.Text = "";
                SystemInit();
            }
            ShowLoginInfo(m_UserManagerHandle.m_CurrenUser);
            if (m_UserManagerHandle.m_CurrenUser == "Config")
            {
                ShowSysDescripConfigForm();
                PicBox_BackGroud.Visible = false;
            }
        }

        private void button_Log_Cancel_Click(object sender, EventArgs e)
        {
            Grp_Login.Visible = false;
            FormDisConfig("Default");
        }

        private void FormDisConfig(string UserType)
        {
            switch (UserType)
            {
                case "Default":
                    MenuItem_UserLogOut.Enabled = false;
                    MenuItem_UserLogIn.Enabled = true;
                    MenuItem_Practice.Enabled = false;
                    MenuItem_SystemDiscrition.Enabled = false;
                    MenuItem_DataBase.Enabled = false;
                    MenuItem_Alarm.Enabled = false;
                    MenuItem_Maintance.Enabled = false;
                    MenuItem_UserManager.Enabled = false;
                    break;
                case "Config":
                    MenuItem_UserLogOut.Enabled = true;
                    MenuItem_UserLogIn.Enabled = false;
                    MenuItem_Practice.Enabled = false;
                    MenuItem_SystemDiscrition.Enabled = false;
                    MenuItem_DataBase.Enabled = false;
                    MenuItem_Alarm.Enabled = false;
                    MenuItem_Maintance.Enabled = false;
                    MenuItem_Maintance.Visible = false;
                    MenuItem_UserManager.Enabled = false;
                    break;
                case "Admin":
                    MenuItem_UserLogOut.Enabled = true;
                    MenuItem_UserLogIn.Enabled = false;
                    MenuItem_Practice.Enabled = true;
                    MenuItem_SystemDiscrition.Enabled = true;
                    MenuItem_DataBase.Enabled = true;
                    MenuItem_Alarm.Enabled = true;
                    MenuItem_Maintance.Enabled = true;
                    MenuItem_UserManager.Enabled = true;
                    break;
                case "Guest":
                    MenuItem_UserLogOut.Enabled = true;
                    MenuItem_UserLogIn.Enabled = false;
                    MenuItem_Practice.Enabled = true;
                    MenuItem_SystemDiscrition.Enabled = true;
                    MenuItem_DataBase.Enabled = true;
                    MenuItem_Alarm.Enabled = true;
                    MenuItem_Maintance.Enabled = false;
                    MenuItem_UserManager.Enabled = true;
                    break;
            }
            comboBox_UserName.Items.Clear();
            string[] user;
            m_UserManagerHandle.GetALLUserName(out user);
            for (int i = 0; i < user.Length; i++)
            {
                comboBox_UserName.Items.Add(user[i]);
            }
            comboBox_UserName.SelectedIndex = 0;
        }

        //创建系统目录
        private void CreateFileFold()
        {
            m_FileMangerHandle.CreateUserFile("Admin");
            m_FileMangerHandle.CreateUserFile("Guest");
        }

        #region 状态栏显示
        public void ShowLoginInfo(string sContext)
        {
            StatusLabel_LogInUser.Text = "当前用户：" + sContext;
            if (mSystemLanguage == "English")
            {
                StatusLabel_LogInUser.Text = "Current User：" + sContext;
            }
        }

        public void ShowPageInfo(string sContext)
        {
            StatusLabel_PageInfo.Text = "当前界面：" + sContext;
            if (mSystemLanguage == "English")
            {
                StatusLabel_PageInfo.Text = "Current Form：" + sContext;
            }
            if (sContext == "主界面" || sContext == "Main Form")
            {
                PicBox_BackGroud.Visible = true;
            }
        }

        public void ShowRunInfo(string sContext)
        {
            m_SystemInfo = sContext;
            if (sContext == "告警" || sContext == "Alarm")
            {
                StatusLabel_SystemInfo.BackColor = Color.Red;
                StatusLabel_SystemInfo.Text = "警报，请查看告警！";
                if (mSystemLanguage == "English")
                {
                    StatusLabel_SystemInfo.Text = "Alarm，Please Check！";
                }
            }
            else
            {
                StatusLabel_SystemInfo.BackColor = StatusLabel_LogInUser.BackColor;
                StatusLabel_SystemInfo.Text = "运行";
                if (mSystemLanguage == "English")
                {
                    StatusLabel_SystemInfo.Text = "Running";
                }
            }
        }

        private object oDebugInfoBlock = new object();
        public void ShowSystemInfo(string sContext)
        {
            lock (oDebugInfoBlock)
            {
                DateTime dt = DateTime.Now;
                string sRecord = dt.ToLongDateString() + "                    " + sContext +"\n";
                if (misDisDebugInfo)
                {
                    WriteLog(m_SystemDebugInfoFilePath, sRecord);
                }

                if (misDisDebugInfo)
                {
                    StatusLabel_SystemInfo.Text = sContext;
                }
            }


        }
        #endregion

        #region 菜单栏按钮事件
        private void MenuItem_Abort_Click(object sender, EventArgs e)
        {
            AboutForm Handle = new AboutForm();
            ShowPageInfo("关于界面");
            if (mSystemLanguage == "English")
            {
                ShowPageInfo("Abort");
            }
            Handle.ShowDialog();
        }

        private Form m_PreFormHandle = null;
        private void MenuItem_UserManager_Click(object sender, EventArgs e)
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }         
            UserManagerForm handle = new UserManagerForm();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
            ShowPageInfo("用户管理");
            if (mSystemLanguage == "English")
            {
                ShowPageInfo("User Manager");
            }

            PicBox_BackGroud.Visible = false;
        }

        private void MenuItem_SystemDiscrition_Click(object sender, EventArgs e)
        {
            string CurrentUser = m_UserManagerHandle.m_CurrenUserType;
            if (CurrentUser == "Config")
            {

            }
            else
            {
                if (m_PreFormHandle != null)
                {
                    m_PreFormHandle.Close();
                }
                SysDescrip handle = new SysDescrip();
                handle.TopLevel = false;
                Grp_DisArea.Controls.Add(handle);
                handle.Show();
                m_PreFormHandle = handle;
                ShowPageInfo("系统描述");
                if (mSystemLanguage == "English")
                {
                    ShowPageInfo("System Description");
                }
            }
            PicBox_BackGroud.Visible = false;
        }

        private void MenuItem_Practice_Click(object sender, EventArgs e)
        {
            string CurrentUser = m_UserManagerHandle.m_CurrenUserType;
            if (CurrentUser == "Config")
            {

            }
            else
            {
                if (m_PreFormHandle != null)
                {
                    m_PreFormHandle.Close();
                }
                SetPara handle = new SetPara();
                handle.TopLevel = false;
                Grp_DisArea.Controls.Add(handle);
                handle.Show();
                m_PreFormHandle = handle;
                ShowPageInfo("参数设置");
                if (mSystemLanguage == "English")
                {
                    ShowPageInfo("Set Parameter");
                }
            }
            PicBox_BackGroud.Visible = false;
        }

        private void MenuItem_DataBase_Click(object sender, EventArgs e)
        {
            string CurrentUser = m_UserManagerHandle.m_CurrenUserType;
            if (CurrentUser == "Config")
            {

            }
            else
            {
                if (m_PreFormHandle != null)
                {
                    m_PreFormHandle.Close();
                }
                QueryDB handle = new QueryDB();
                handle.TopLevel = false;
                Grp_DisArea.Controls.Add(handle);
                handle.Show();
                m_PreFormHandle = handle;
                ShowPageInfo("数据查询");
                if (mSystemLanguage == "English")
                {
                    ShowPageInfo("DataBase Query");
                }
            }
            PicBox_BackGroud.Visible = false;
        }

        private void MenuItem_SensorAdj_Click(object sender, EventArgs e)
        {
            string CurrentUser = m_UserManagerHandle.m_CurrenUserType;
            if (CurrentUser == "Config")
            {

            }
            else
            {
                if (m_PreFormHandle != null)
                {
                    m_PreFormHandle.Close();
                }
                AdjSensor handle = new AdjSensor();
                handle.TopLevel = false;
                Grp_DisArea.Controls.Add(handle);
                handle.Show();
                m_PreFormHandle = handle;
                ShowPageInfo("传感器校验");
                if (mSystemLanguage == "English")
                {
                    ShowPageInfo("Sensor Adjust");
                }
            }
            PicBox_BackGroud.Visible = false;
        }

        private void MenuItem_Alarm_Click(object sender, EventArgs e)
        {
            string CurrentUser = m_UserManagerHandle.m_CurrenUserType;
            if (CurrentUser == "Config")
            {

            }
            else
            {
                if (m_PreFormHandle != null)
                {
                    m_PreFormHandle.Close();
                }
                AlarmForm handle = new AlarmForm();
                handle.TopLevel = false;
                Grp_DisArea.Controls.Add(handle);
                handle.Show();
                m_PreFormHandle = handle;
                ShowPageInfo("告警");
                if (mSystemLanguage == "English")
                {
                    ShowPageInfo("Alarm");
                }
            }
            PicBox_BackGroud.Visible = false;
        }

        #endregion

        #region 告警
        private void AlarmFun()
        {
            int count = 0;
            if (m_AlarmManageHandle != null)
            {
                m_AlarmManageHandle.GetRuntimeAlarmCount(ref count);
            }

            if (count <= 0)
            {
                return;
            }
            else
            {
                ShowRunInfo("告警");
                if (mSystemLanguage == "English")
                {
                    ShowRunInfo("Alarm");
                }
            }
        }

        private void SampleAlarm()
        {
            string[] regName;
            m_AlarmManageHandle.GetAlarmReg(out regName);

            int len = regName.Length;
            for (int i = 0; i < len; i++)
            {
                byte data = 0;
                DateTime dt = default(DateTime);
                int code = m_DriveHandle.ReadData(regName[i],ref data, ref dt);
                if (code != 1)
                {
                    MessageBox.Show("读取告警失败， 定位：" + regName[i], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (data == 1)
                {
                    m_AlarmManageHandle.CreateRuntimeAlarm(regName[i], dt);
                }
                else
                {
                    m_AlarmManageHandle.ClearRuntimeAlarm(regName[i], dt);
                }
            }
        }
        #endregion

        public void ShowTestingForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            TestForm handle = new TestForm();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        #region 配置界面
        public void ShowSysDescripConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            SysDescripConfig handle = new SysDescripConfig();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        public void ShowSetParaConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            SetParaConfig handle = new SetParaConfig();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        public void ShowTestConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            TestFormConfig handle = new TestFormConfig();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        public void ShowQueryDBConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            QueryDBConfig handle = new QueryDBConfig();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        public void ShowAlarmConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            AlarmConfigForm handle = new AlarmConfigForm();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        public void ShowAdjSensorConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            AdjSensorConfig handle = new AdjSensorConfig();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }

        public void ShowSystemConfigForm()
        {
            if (m_PreFormHandle != null)
            {
                m_PreFormHandle.Close();
            }
            SystemConfig handle = new SystemConfig();
            handle.TopLevel = false;
            Grp_DisArea.Controls.Add(handle);
            handle.Show();
            m_PreFormHandle = handle;
        }
        #endregion

#region 写日志文件
        private void WriteLog(string LogFileName, string s)
        {
            if (s == null)
            {
                return;
            }
            FileStream filestream = new FileStream(LogFileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(filestream, Encoding.GetEncoding("gb2312"));
            StreamReader sr = new StreamReader(filestream, Encoding.GetEncoding("gb2312"));
            string sTem = sr.ReadToEnd();
            //sw.WriteLine("{0}" + s, sTem);
            sw.WriteLine(s);

            sw.Flush();
            sw.Close();
            sr.Close();
            filestream.Close();
        }

        private void ReadLog(string LogFileName, ref string s)
        {
            FileStream filestream = new FileStream(LogFileName, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(filestream, Encoding.GetEncoding("gb2312"));
            string sTem = sr.ReadToEnd();
            s = sTem;
            sr.Close();
            filestream.Close();
        }
#endregion

    }
}
