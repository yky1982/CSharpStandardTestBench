using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StandardTestBench
{
    public partial class UserManagerForm : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";
        private event StatePageInfo ShowPageInfo;
        public UserManagerForm()
        {
            InitializeComponent();        
        }

        private UserManager m_UserManagerHandle = null;
        private Form1 m_MainFormHandle = null;
        private FileManger m_FileMangerHandle = null;
        private void UserManagerForm_Load(object sender, EventArgs e)
        {
            Init();
            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                TB_Exit.Text = "Exit";
                Grp_ModifyPWD.Text = "Modify Password";
                LB_PWD_Modify1.Text = "Press Password";
                LB_PWD_Modify2.Text = "Press Password Again";
                BT_ModifyPWD.Text = "Config";

                Grp_AddUser.Text = "Add User";
                LB_UserName.Text = "Name";
                LB_PWD_Set1.Text = "Press Password";
                LB_PWD_Set2.Text = "Press Password Again";
                BT_CheckUser.Text = "Check User";
                CB_Permission.Text = "Permission";
                button_Add_Add.Text = "Add";
                button_Add_Clear.Text = "Clear";

                Grp_Delete.Text = "Delete User";
                LB_DeleteUserName.Text = "Name";
                button_Del.Text = "Delete";
            }
        }

        public void Init()
        {
            if (m_MainFormHandle == null)
            {
                m_MainFormHandle = Form1.GetHandle();
            }

            if (m_FileMangerHandle == null)
            {
                m_FileMangerHandle = FileManger.GetHandle();
            }

            if (m_UserManagerHandle == null)
            {
                m_UserManagerHandle = UserManager.GetHandle();
            }

            string userType = m_UserManagerHandle.m_CurrenUserType;
            if (userType == "Admin")
            {
                Grp_AddUser.Visible = true;
                Grp_ModifyPWD.Visible = true;
                Grp_Delete.Visible = true;
                string[] user;
                m_UserManagerHandle.GetALLUserName(out user);

                if (user.Length != 0)
                {
                    for (int i = 0; i < user.Length; i++)
                    {
                        if (user[i] == "Admin" || user[i] == "Guest")
                        {
                            continue;
                        }
                        CB_UserName.Items.Add(user[i]);
                        CB_UserName.SelectedIndex = 0;
                    }
                }
            }
            if (userType == "Guest")
            {
                Grp_AddUser.Visible = false;
                Grp_ModifyPWD.Visible = true;
                Grp_Delete.Visible = false;
            }
            
        }

        private void TB_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void BT_ModifyPWD_Click(object sender, EventArgs e)
        {
            string pwd1 = textBox_MDY1.Text;
            string pwd2 = textBox_MDY2.Text;
            if (pwd1 == pwd2)
            {
                string userName = m_UserManagerHandle.m_CurrenUser;
                bool code = m_UserManagerHandle.ModifyPWD(userName, pwd2);
                if(code)
                MessageBox.Show("修改成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_MDY1.Text = "";
                textBox_MDY2.Text = "";
            }
            else
            {
                MessageBox.Show("两次密码不一致", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void BT_CheckUser_Click(object sender, EventArgs e)
        {
            string sUser = textBox_Add_Use.Text;
            string[] aUser;
            m_UserManagerHandle.GetALLUserName(out aUser);
            int index = -1;
            for (int i = 0; i < aUser.Length; i++)
            {
                if (aUser[i] == sUser)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                MessageBox.Show("用户名已存在！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                MessageBox.Show("该用户名可以使用", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button_Add_Clear_Click(object sender, EventArgs e)
        {
            textBox_Add_Use.Text = "";
            textBox_Add_PWD1.Text = "";
            textBox_Add_PWD2.Text = "";
        }

        private void button_Add_Add_Click(object sender, EventArgs e)
        {
            string sUser = textBox_Add_Use.Text;
            string[] aUser;
            m_UserManagerHandle.GetALLUserName(out aUser);
            int index = -1;
            for (int i = 0; i < aUser.Length; i++)
            {
                if (aUser[i] == sUser)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                MessageBox.Show("用户名已存在！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            string pwd1 = textBox_Add_PWD1.Text;
            string pwd2 = textBox_Add_PWD2.Text;
            if (pwd1 != pwd2)
            {
                MessageBox.Show("两次密码不一致", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                bool code = m_UserManagerHandle.AddUser(sUser, pwd1);
                if (!code)
                {
                    MessageBox.Show("添加用户失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                MessageBox.Show("添加用户成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bool isPermission = false;
                if (CB_Permission.Checked == true)
                {
                    isPermission = true;
                }
                else
                {
                    isPermission = false;
                }
                m_FileMangerHandle.CreateUserSetINIFile(sUser, isPermission);//创建用户数据
                CB_UserName.Items.Add(sUser);
            }

        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            MsgBoxResult = MessageBox.Show("确定删除该用户吗吗？", "提示", MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                string user = CB_UserName.Text;
                m_UserManagerHandle.RemoveUser(user);
                CB_UserName.SelectedIndex = 0;
                MessageBox.Show("删除用户成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_FileMangerHandle.DeleteUserSetINIFiles(user);
                CB_UserName.Items.Remove(user);
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }

        private void UpdateMainForm(string s)
        {
            if (ShowPageInfo != null)
            {
                ShowPageInfo(s);
            }
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
    }
}
