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
    public partial class QueryDBConfig : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string m_INIQueryDBFilePath = Application.StartupPath + @"\Config\QueryDB\QueryDB.ini";

        public QueryDBConfig()
        {
            InitializeComponent();
        }

        private Form1 m_MainFormHandle = null;
        private void QueryDBConfig_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
        }

        private void BT_Pre_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowTestConfigForm();
        }

        private void BT_Next_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowAlarmConfigForm();
        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            bool isCheck = false;
            if (CB_Zoom.Checked == true)
            {
                isCheck = true;
                WritePrivateProfileString("QueryDB", "Zoom", isCheck.ToString(), m_INIQueryDBFilePath);
            }
            else
            {
                isCheck = false;
                WritePrivateProfileString("QueryDB", "Zoom", isCheck.ToString(), m_INIQueryDBFilePath);
            }
            if (CB_PDF.Checked == true)
            {
                isCheck = true;
                WritePrivateProfileString("QueryDB", "PDFReport", isCheck.ToString(), m_INIQueryDBFilePath);
            }
            else
            {
                isCheck = false;
                WritePrivateProfileString("QueryDB", "PDFReport", isCheck.ToString(), m_INIQueryDBFilePath);
            }

            string startPos = TB_Pos_Start.Text;
            WritePrivateProfileString("QueryDB", "CurveStartPos", startPos, m_INIQueryDBFilePath);
            string endPos = TB_Pos_Start.Text;
            WritePrivateProfileString("QueryDB", "CurveEndPos", endPos, m_INIQueryDBFilePath);
            string sheetName = TB_TableName.Text;
            WritePrivateProfileString("QueryDB", "SheetName", sheetName, m_INIQueryDBFilePath);
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        
    }
}
