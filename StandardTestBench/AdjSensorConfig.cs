using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace StandardTestBench
{
    public partial class AdjSensorConfig : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private event StateSysInfo ShowDebugInfo;
        //传感器校验
        private string m_XMLAdjSensorFilePath = Application.StartupPath + @"\SystemFile\AdjSensor\AdjSensor.xml";
        private string m_INIAdjSensorFilePath = Application.StartupPath + @"\Config\AdjSensor\AdjSensor.ini";

        public class AdjSensorList
        {
            public string m_ParaName;
            public string m_ParaNameCH;
            public string m_ParaUint;
            public AdjSensorList(string paraName, string paraNameCH, string paraUint)
            {
                m_ParaName = paraName;
                m_ParaNameCH = paraNameCH;
                m_ParaUint = paraUint;
            }
        }
        public List<AdjSensorList> m_ReportParaLists = new List<AdjSensorList>();

        //PLC恢复出厂设置
        private string m_INIPLCDefaultFilePath = Application.StartupPath + @"\Config\PLCDefault\PLCDefault.ini";
        private Form1 m_MainFormHandle = null;
        public AdjSensorConfig()
        {
            InitializeComponent();
        }

        private void AdjSensorConfig_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
            LoadAdjXML();
            DisAdjSensorPara();
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void LoadAdjXML()
        {
            string paraName = "";
            string paraNameCH = "";
            string paraUint = "";
            if (!File.Exists(m_XMLAdjSensorFilePath))
            {
                SendDebugInfo("AdjConfig XML 文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLAdjSensorFilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "RegName")
                    {
                        paraName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "RegNameCH")
                    {
                        paraNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUint = SubChild.InnerText;
                    }
                }
                m_ReportParaLists.Add(new AdjSensorList(paraName, paraNameCH, paraUint));
            }
        }

        private void DisAdjSensorPara()
        {
            DGV_Sensor.Rows.Clear();
            int count = m_ReportParaLists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(TB_Sensor_Row.Text);
            }
            catch (System.Exception)
            {
                MaxColoum = 5;
            }

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
            DGV_Sensor.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows; i++)
            {
                DGV_Sensor.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DGV_Sensor.Rows[i * 2].Cells[j].Value = m_ReportParaLists[i * MaxColoum + j].m_ParaNameCH +
                                                                                                   " (" + m_ReportParaLists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void BT_PLCSave_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("PLCDefault", "isOpen", CB_PLC.CheckState.ToString(), m_INIPLCDefaultFilePath);
            if (CB_PLC.CheckState == CheckState.Checked)
            {
                WritePrivateProfileString("PLCDefault", "RegName", TB_PLC_RegName.ToString(), m_INIPLCDefaultFilePath);
            }
            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void BT_Pre_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowAlarmConfigForm();
        }

        private void BT_SavePara_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_ReportParaLists.Count; i++)
            {
                WritePrivateProfileString("AdjSensor", "RegName" + i.ToString(), m_ReportParaLists[i].m_ParaName, m_INIAdjSensorFilePath);
                WritePrivateProfileString("AdjSensor", "RegNameCH" + i.ToString(), m_ReportParaLists[i].m_ParaNameCH, m_INIAdjSensorFilePath);
                WritePrivateProfileString("AdjSensor", "ParaUnit" + i.ToString(), m_ReportParaLists[i].m_ParaUint, m_INIAdjSensorFilePath);
            }
            WritePrivateProfileString("AdjSensor", "Rows", TB_Sensor_Row.Text, m_INIAdjSensorFilePath);
            WritePrivateProfileString("AdjSensor", "AdjSensorRegName", TB_RegName.Text, m_INIAdjSensorFilePath);
            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);      
        }

        private void BT_Next_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowSystemConfigForm();
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
