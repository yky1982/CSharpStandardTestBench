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
    public partial class LoadSetParaConfig : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private SetPara m_ParentHandle = null;
        private UserManager m_UserManagerHandle = null;
        public LoadSetParaConfig(SetPara Handle)
        {
            InitializeComponent();
            m_ParentHandle = Handle;
        }

        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";
        private int m_SelectRecordId = -1;
        private Form1 m_MainFormHandle = null;
        private event StateSysInfo ShowDebugInfo;
        private void LoadSetParaConfig_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
            m_UserManagerHandle = UserManager.GetHandle();
            
            dataGridView1.ReadOnly = true;
            DataSet ds;
            //m_ParentHandle.m_DBHandle.QueryInDB("select * from SetParaTable where UserName = '"+m_UserManagerHandle.m_CurrenUser +"'", out ds);
            bool ret = m_ParentHandle.m_DBHandle.QueryInDB("select * from SetParaTable", out ds);
            if (!ret)
            {
                SendDebugInfo("LoadSetParaConfig 数据查询失败");
                return;
            }
            DataTable dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
           

            for (int i = 3; i < dt.Columns.Count; i++)
            {
                if (i % 2 != 0)
                {
                    dataGridView1.Columns[i].Visible = false;
                }
            }

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                Grp_ParaList.Text = "Parameter List";
                BT_Sure.Text = "Config";
                BT_Exit.Text = "Exit";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            string sRecordId = dataGridView1.Rows[index].Cells[0].Value.ToString();
            m_SelectRecordId = Convert.ToInt32(sRecordId);
        }

        private void BT_Sure_Click(object sender, EventArgs e)
        {
            m_ParentHandle.m_SelectRecordId = m_SelectRecordId;
            this.Close();
        }

        private void BT_Exit_Click(object sender, EventArgs e)
        {
            m_ParentHandle.m_SelectRecordId = -1;
            this.Close();
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
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
