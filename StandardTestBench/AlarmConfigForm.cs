using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;

using System.IO;
using System.Xml;
using System.Runtime.InteropServices;

namespace StandardTestBench
{
    public partial class AlarmConfigForm : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string m_INIAlarmFilePath = Application.StartupPath + @"\Config\AlarmInfo\AlarmInfo.ini";

        private Form1 m_MainFormHandle = null;
        private event StateSysInfo ShowDebugInfo;
        public class AlarmList
        {
            public string m_RegName;
            public string m_RegNameCH;
            public AlarmList(string RegName, string RegNameCH)
            {
                m_RegName = RegName;
                m_RegNameCH = RegNameCH;
            }
        }
        public List<AlarmList> m_AlarmLists = new List<AlarmList>();
        private string m_XMLAlarmFilePath = Application.StartupPath + @"\SystemFile\Alarm\Alarm.xml";
        private string m_TXTHistoryAlarmFilePath = Application.StartupPath + @"\Config\AlarmInfo\AlarmInfo.txt";

        public AlarmConfigForm()
        {
            InitializeComponent();
        }

        private void AlarmConfigForm_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);

            DG_AlarmConfig.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadXML();
            

            DisAlarmInfo();
        }

        private void LoadXML()
        {
            string regName = "";
            string regNameCH = "";
            if (!File.Exists(m_XMLAlarmFilePath))
            {
                SendDebugInfo("AlarmConig XML 文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLAlarmFilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "RegName")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "RegNameCH")
                    {
                        regNameCH = SubChild.InnerText;
                    }
                }
                m_AlarmLists.Add(new AlarmList(regName, regNameCH));
            }
        }

        private void DisAlarmInfo()
        {
            DG_AlarmConfig.Rows.Clear();
            int count = m_AlarmLists.Count;
            if (count <= 0)
            {
                return;
            }
            int MaxColoum = 5;
            int MaxRows = 0;
            if (MaxColoum == 0)
            {
                return;
            }
            if (count % MaxColoum != 0)
            {
                MaxRows = count / MaxColoum + 1;
            }
            else
            {
                MaxRows = count / MaxColoum;
            }
            DG_AlarmConfig.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DG_AlarmConfig.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DG_AlarmConfig.Rows[i * 2].Cells[j].Value = m_AlarmLists[i * MaxColoum + j].m_RegName;
                    DG_AlarmConfig.Rows[i * 2 + 1].Cells[j].Value = m_AlarmLists[i * MaxColoum + j].m_RegNameCH;
                }
            }
            for (int i = 0; i < DG_AlarmConfig.Rows.Count; i += 2)
            {
                DG_AlarmConfig.Rows[i].ReadOnly = true;
            }
        }

        private void BT_Pre_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowQueryDBConfigForm();
        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            bool isCheck = false;
            if (CB_HistoryAlarm.Checked == true)
            {
                isCheck = true;
                WritePrivateProfileString("AlarmInfo", "History", isCheck.ToString(), m_INIAlarmFilePath);
            }
            else
            {
                isCheck = false;
                WritePrivateProfileString("AlarmInfo", "History", isCheck.ToString(), m_INIAlarmFilePath);
            }

            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BT_Next_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowAdjSensorConfigForm();
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
