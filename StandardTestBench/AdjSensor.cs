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
    public partial class AdjSensor : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private event StateSysInfo ShowDebugInfo;

        private event StatePageInfo ShowPageInfo;

        //传感器校验
        private string m_XMLAdjSensorFilePath = Application.StartupPath + @"\SystemFile\AdjSensor\AdjSensor.xml";
        private string m_INIAdjSensorFilePath = Application.StartupPath + @"\Config\AdjSensor\AdjSensor.ini";
        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

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
        public List<AdjSensorList> m_AdjSensorLists = new List<AdjSensorList>();

        //PLC恢复出厂设置
        private string m_INIPLCDefaultFilePath = Application.StartupPath + @"\Config\PLCDefault\PLCDefault.ini";
        private Form1 m_MainFormHandle = null;
        private System.Windows.Forms.Timer mSampleTime = new System.Windows.Forms.Timer();
        public AdjSensor()
        {
            InitializeComponent();
        }

        private void AdjSensor_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();

            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
            LoadAdjXML();
            DisAdjSensorPara();

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);
            if (sLanguage == "English")
            {
                Grp_SetPara_Style.Text = "Sensor Information";
                Grp_Info.Text = "Operation Intruduction";
                Grp_Operation.Text = "Operation";
                TB_Info.Text = "Before verification, please press the emergency button, make sure  to finish releasing pressure!";
                button_Adj.Text = "Confirm";
                button_Return.Text = "Exit";
            }

            mSampleTime.Interval = 1000;
            mSampleTime.Tick += new EventHandler(SampleFun);
            return;
            mSampleTime.Start();


        }


        private void LoadAdjXML()
        {
            string paraName = "";
            string paraNameCH = "";
            string paraUint = "";
            if (!File.Exists(m_XMLAdjSensorFilePath))
            {
                SendDebugInfo("AdjXML 文件不存在");
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
                m_AdjSensorLists.Add(new AdjSensorList(paraName, paraNameCH, paraUint));
            }
        }

        private void DisAdjSensorPara()
        {
            DGV_Sensor.Rows.Clear();
            int count = m_AdjSensorLists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(ContentValue("AdjSensor", "Rows", m_INIAdjSensorFilePath));
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
                    DGV_Sensor.Rows[i * 2].Cells[j].Value = m_AdjSensorLists[i * MaxColoum + j].m_ParaNameCH +
                                                                                                   " (" + m_AdjSensorLists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void SampleFun(object o, EventArgs e)
        {
            string regName = "";
            DateTime dt = default(DateTime);

            int rows = DGV_Sensor.RowCount;
            int columns = DGV_Sensor.ColumnCount;
            int count = m_AdjSensorLists.Count;
            for (int i = 0; i < rows; i += 2)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i * columns + j) > count)
                    {
                        break;
                    }
                    string regNameCH = DGV_Sensor.Rows[i].Cells[j].Value.ToString();
                    int index = m_AdjSensorLists.FindIndex(r => r.m_ParaNameCH == regNameCH);
                    if (index < 0)
                    {
                        continue;
                    }
                    regName = m_AdjSensorLists[index].m_ParaName;

                    float data = 0;
                    m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                    DGV_Sensor.Rows[i + 1].Cells[j].Value = data;
                }
            }
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }


        private void button_Adj_Click(object sender, EventArgs e)
        {
            byte data = 1;
            string regName = ContentValue("AdjSensor", "AdjSensorRegName", m_INIAdjSensorFilePath);
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                SendDebugInfo("Adj 校验失败");
                MessageBox.Show("校验失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void button_Return_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void UpdateMainForm(string s)
        {
            if (ShowPageInfo != null)
            {
                ShowPageInfo(s);
            }
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
