using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace StandardTestBench
{
    class AlarmManage
    {
        public static AlarmManage m_AlarmManage = null;
        public class AlarmList
        {
            public string m_RegName;
            public string m_RegNameCH;
            public string m_StartTime;
            public string m_EndTime;
            public AlarmList(string RegName, string RegNameCH, string startTime, string endTime)
            {
                m_RegName = RegName;
                m_RegNameCH = RegNameCH;
                m_StartTime = startTime;
                m_EndTime = endTime;
            }
        }
        public List<AlarmList> m_AlarmLists = new List<AlarmList>();
        public List<AlarmList> m_AlarmInfoLists = new List<AlarmList>();
        private string m_XMLAlarmFilePath = Application.StartupPath + @"\SystemFile\Alarm\Alarm.xml";
        private string m_TXTHistoryAlarmFilePath = Application.StartupPath + @"\Config\AlarmInfo\AlarmInfo.txt";
        private string m_INIAlarmFilePath = Application.StartupPath + @"\Config\AlarmInfo\AlarmInfo.ini";
        private Form1 m_MainFormHandle = null;
        private bool m_isRuntimeAlarmCreate = false;
        private System.Windows.Forms.Timer m_Timer = new System.Windows.Forms.Timer();

        private event StateSysInfo ShowDebugInfo;

        public AlarmManage()
        {
            LoadXML();
            m_MainFormHandle = Form1.GetHandle();
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
        }

        public void Init()
        {
            m_Timer.Interval = 2000;
            m_Timer.Tick += new EventHandler(TimerFun);
            return;
            m_Timer.Start();
        }

        private void TimerFun(object o, EventArgs e)
        {
            SampleAlarm();
        }

        public static AlarmManage GetHandle()
        {
            if (m_AlarmManage == null)
            {
                m_AlarmManage = new AlarmManage();
            }
            return m_AlarmManage;
        }

        private void LoadXML()
        {
            string regName = "";
            string regNameCH = "";
            string startTime = "";
            if (!File.Exists(m_XMLAlarmFilePath))
            {
                SendDebugInfo("AlarmManage XML 文件不存在");
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
                m_AlarmLists.Add(new AlarmList(regName, regNameCH, startTime, startTime));
            }
        }

        public void GetAlarmReg(out string[] regName)
        {
            int count = m_AlarmLists.Count;
            regName = new string[count];
            for (int i = 0; i < count; i++)
            {
                regName[i] = m_AlarmLists[i].m_RegName;
            }
        }

        public bool CreateRuntimeAlarm(string regName, DateTime dt)
        {
            int AlarmIndex = m_AlarmLists.FindIndex(r => r.m_RegName == regName);
            if (AlarmIndex < 0)
            {
                SendDebugInfo("AlarmManage 不存在此项报警");
                return false;
            }

            int InfoIndex = m_AlarmInfoLists.FindIndex(r => r.m_RegName == regName);
            if (InfoIndex >= 0)
            {
                SendDebugInfo("AlarmManage 报警信息 + CreateRuntimeAlarm");
                return false;
            }
            m_AlarmInfoLists.Add(new AlarmList(regName, m_AlarmLists[AlarmIndex].m_RegNameCH, dt.ToLongTimeString(), ""));
            return true;
        }

        public void GetRuntimeAlarmCount(ref int count)
        {
            count = m_AlarmInfoLists.Count;
        }

        public bool GetRuntimeInfo(int index, ref string regName, ref string regNameCH, ref string startTime, ref string endTime)
        {
            int count  = m_AlarmInfoLists.Count;
            if (index >= count)
            {
                SendDebugInfo("AlarmManage 报警信息 + GetRuntimeInfo");
                return false;
            }
            regName = m_AlarmInfoLists[index].m_RegName;
            regNameCH = m_AlarmInfoLists[index].m_RegNameCH;
            startTime = m_AlarmInfoLists[index].m_StartTime;
            endTime = m_AlarmInfoLists[index].m_EndTime;
            return true;
        }

        public bool ClearRuntimeAlarm(string regName, DateTime dt)
        {
            string alarmContent = "";
            int index = m_AlarmInfoLists.FindIndex(r => r.m_RegName == regName);
            if (index < 0)
            {
                SendDebugInfo("AlarmManage 报警信息 " + " ClearRuntimeAlarm");
                return false;
            }
            alarmContent = m_AlarmInfoLists[index].m_StartTime + "          " +
                                        dt.ToLongTimeString() + "          " +
                                        m_AlarmInfoLists[index].m_RegNameCH + "\n";
            WriteLog(m_TXTHistoryAlarmFilePath, alarmContent);

            m_AlarmInfoLists.RemoveAt(index);
            return true;
        }

        public void ReadHistoryAlarmInfo(ref string s)
        {
            ReadLog(m_TXTHistoryAlarmFilePath, ref s);
        }

        public  void SampleAlarm()
        {
            string[] regName;
            GetAlarmReg(out regName);

            int len = regName.Length;
            if (len <= 0)
            {
                return;
            }

            for (int i = 0; i < len; i++)
            {
                byte data = 0;
                DateTime dt = default(DateTime);
                if (m_MainFormHandle == null)
                {
                    m_MainFormHandle = Form1.GetHandle();
                }
                int code = m_MainFormHandle.m_DriveHandle.ReadData(regName[i], ref data, ref dt);
                if (code != 1)
                {
                    SendDebugInfo("读取告警失败， 定位：" + regName[i]);
                    //MessageBox.Show("读取告警失败， 定位：" + regName[i], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (data == 1)
                {
                    CreateRuntimeAlarm(regName[i], dt);
                }
                else
                {
                    ClearRuntimeAlarm(regName[i], dt);
                }
            }
            
        }

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

        private void SendDebugInfo(string s)
        {
            if (ShowDebugInfo != null)
            {
                ShowDebugInfo(s);
            }
        }
    }
}
