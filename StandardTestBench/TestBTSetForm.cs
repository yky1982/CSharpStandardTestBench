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
    public partial class TestBTSetForm : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private string m_INIToolBar01FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar01.ini";
        private string m_INIToolBar02FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar02.ini";
        private string m_INIToolBar03FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar03.ini";
        private string m_INIToolBar04FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar04.ini";

        private string m_BTName;
        private string m_PicPath;
        private string m_FilePath;
        public TestBTSetForm(string BTName, string FilePath)
        {
            InitializeComponent();
            m_BTName = BTName;
            m_FilePath = FilePath;
        }

        private void TestBTSetForm_Load(object sender, EventArgs e)
        {
            LoadINI();
            BT_Cancel.Visible = false;
        }

        private void LoadINI()
        {
            string sEnable = ContentValue(m_BTName, "Enable", m_FilePath);
            string regName = ContentValue(m_BTName, "RegName", m_FilePath);
            string regNameCH = ContentValue(m_BTName, "RegNameCH", m_FilePath);
            string sFilePath = ContentValue(m_BTName, "FilePath", m_FilePath);

            if (sEnable == "True")
            {
                CB_Start.CheckState = CheckState.Checked;
                TB_RegName.Text = regName;
                TB_RegNameCH.Text = regNameCH;
                TB_FilePath.Text = sFilePath;
                m_PicPath = sFilePath;
                Grp_Set.Enabled = true;
            }
            else
            {
                CB_Start.CheckState = CheckState.Unchecked;
                Grp_Set.Enabled = false;
            }
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void CB_Start_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Start.CheckState == CheckState.Checked)
            {
                Grp_Set.Enabled = true;
            }
            else
            {
                Grp_Set.Enabled = false;
            }
        }

        private void BT_LoadPic_Click(object sender, EventArgs e)
        {
            if (picFilePath.ShowDialog() == DialogResult.OK)
            {
                TB_FilePath.Text = picFilePath.FileName;
                m_PicPath = picFilePath.FileName;
            }
        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            //string INIPath = Application.StartupPath + @"\Config\TestConfig\" + m_BTName + ".ini";
            if (CB_Start.CheckState == CheckState.Checked)
            {             
                string regName = TB_RegName.Text;
                string regNameCH = TB_RegNameCH.Text;
                if (TB_FilePath.Text == "")
                {
                    MessageBox.Show("请选择图片文件!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                WritePrivateProfileString(m_BTName, "Enable", "True", m_FilePath);
                WritePrivateProfileString(m_BTName, "RegName", regName, m_FilePath);
                WritePrivateProfileString(m_BTName, "RegNameCH", regNameCH, m_FilePath);
                WritePrivateProfileString(m_BTName, "FilePath", m_PicPath, m_FilePath);
                MessageBox.Show("保存成功!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                WritePrivateProfileString(m_BTName, "Enable", "False", m_FilePath);
            }

            this.Close();


        }

        private void BT_SaveM_Click(object sender, EventArgs e)
        {
            if (CB_Start.CheckState == CheckState.Checked)
            {
                string regName = TB_RegName.Text;
                string regNameCH = TB_RegNameCH.Text;
                if (TB_FilePath.Text == "")
                {
                    MessageBox.Show("请选择图片文件!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                WritePrivateProfileString(m_BTName, "Enable", "True", m_INIToolBar01FilePath);
                WritePrivateProfileString(m_BTName, "Enable", "True", m_INIToolBar02FilePath);
                WritePrivateProfileString(m_BTName, "Enable", "True", m_INIToolBar03FilePath);
                WritePrivateProfileString(m_BTName, "Enable", "True", m_INIToolBar04FilePath);

                WritePrivateProfileString(m_BTName, "RegName", regName, m_INIToolBar01FilePath);
                WritePrivateProfileString(m_BTName, "RegName", regName, m_INIToolBar02FilePath);
                WritePrivateProfileString(m_BTName, "RegName", regName, m_INIToolBar03FilePath);
                WritePrivateProfileString(m_BTName, "RegName", regName, m_INIToolBar04FilePath);

                WritePrivateProfileString(m_BTName, "RegNameCH", regNameCH, m_INIToolBar01FilePath);
                WritePrivateProfileString(m_BTName, "RegNameCH", regNameCH, m_INIToolBar02FilePath);
                WritePrivateProfileString(m_BTName, "RegNameCH", regNameCH, m_INIToolBar03FilePath);
                WritePrivateProfileString(m_BTName, "RegNameCH", regNameCH, m_INIToolBar04FilePath);

                WritePrivateProfileString(m_BTName, "FilePath", m_PicPath, m_INIToolBar01FilePath);
                WritePrivateProfileString(m_BTName, "FilePath", m_PicPath, m_INIToolBar02FilePath);
                WritePrivateProfileString(m_BTName, "FilePath", m_PicPath, m_INIToolBar03FilePath);
                WritePrivateProfileString(m_BTName, "FilePath", m_PicPath, m_INIToolBar04FilePath);
                MessageBox.Show("保存成功!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                WritePrivateProfileString(m_BTName, "Enable", "False", m_INIToolBar01FilePath);
                WritePrivateProfileString(m_BTName, "Enable", "False", m_INIToolBar02FilePath);
                WritePrivateProfileString(m_BTName, "Enable", "False", m_INIToolBar03FilePath);
                WritePrivateProfileString(m_BTName, "Enable", "False", m_INIToolBar04FilePath);
            }

            this.Close();
        }

    }
}
