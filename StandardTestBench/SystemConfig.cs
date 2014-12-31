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
    public partial class SystemConfig : Form
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";
        private Form1 m_MainFormHandle = null;

        public SystemConfig()
        {
            InitializeComponent();
        }

        private void SystemConfig_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            string sLanguage = "";
            if (RB_Chinese.Checked == true)
            {
                sLanguage = "Chinese";
            }
            if (RB_English.Checked == true)
            {
                sLanguage = "English";
            }
            WritePrivateProfileString("SystemCofig", "Language", sLanguage, m_INISystemConfigFilePath);

            bool isSaveDebug = false;
            if (CB_SaveDebugInfo.Checked == true)
            {
                isSaveDebug = true;  
            }
            else
            {
                isSaveDebug = false;
            }
            WritePrivateProfileString("SystemCofig", "SaveDebugInfo", isSaveDebug.ToString(), m_INISystemConfigFilePath);

            bool isDisDebug = false;
            if (CB_DisDebugInfo.Checked == true)
            {
                isDisDebug = true;  
            }
            else
            {
                isDisDebug = false;
            }
            WritePrivateProfileString("SystemCofig", "DisDebugInfo", isDisDebug.ToString(), m_INISystemConfigFilePath);
        }

        private void BT_Pre_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowAdjSensorConfigForm();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


    }
}
