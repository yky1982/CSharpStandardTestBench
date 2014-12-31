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
    
    public partial class AlarmForm : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string m_INIAlarmFilePath = Application.StartupPath + @"\Config\AlarmInfo\AlarmInfo.ini";
        private string m_TXTHistoryAlarmFilePath = Application.StartupPath + @"\Config\AlarmInfo\AlarmInfo.txt";
        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

        private Form1 m_MainFormHandle = null;
        private AlarmManage m_AlarmManageHandle = null;
        private event StatePageInfo ShowPageInfo;
        private event StateSysInfo ShowDebugInfo;
        public AlarmForm()
        {
            InitializeComponent();
        }

        private void AlarmForm_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
            m_AlarmManageHandle = AlarmManage.GetHandle();

            string isCheck = ContentValue("AlarmInfo", "History", m_INIAlarmFilePath);
            if (isCheck == "True")
            {
                TB_HistoryAlarm.Visible = true;
            }
            if (isCheck == "False")
            {
                TB_HistoryAlarm.Visible = false;
            }

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                TB_Exit.Text = "Exit";
                TB_HistoryAlarm.Text = "History Alarm";
                TB_ActualAlarm.Text = "Alarm";
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

        private void TB_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_ActualAlarm_Click(object sender, EventArgs e)
        {
            int count = 0;
            string AlarmContent = "";
            string regName = "";
            string regNameCH = "";
            string startTime = "";
            string endTime = "";
            m_AlarmManageHandle.GetRuntimeAlarmCount(ref count);

            for (int i = 0; i < count; i++)
            {
                bool ret  = m_AlarmManageHandle.GetRuntimeInfo(i, ref regName, ref regNameCH, ref startTime, ref endTime);
                if (!ret)
                {
                    SendDebugInfo("Alarm 获取实时告警失败, " + regNameCH);
                }
                AlarmContent += startTime + "          " + regNameCH + "\n";
            }

            RB_Alarm_Dis.Text = AlarmContent;
        }

        private void TB_HistoryAlarm_Click(object sender, EventArgs e)
        {
            string s = "";
            m_AlarmManageHandle.ReadHistoryAlarmInfo(ref s);
            RB_Alarm_Dis.Text = "";
            RB_Alarm_Dis.Text = s;
        }

        private void SendDebugInfo(string s)
        {
            if (ShowDebugInfo != null)
            {
                ShowDebugInfo(s);
            }
        }
    }
}
