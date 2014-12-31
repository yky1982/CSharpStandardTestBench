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
using DrawCurveDLL;
using DataBaseLib;

namespace StandardTestBench
{
    public partial class TestFormConfig : Form
    {
        #region AccessDLL
        [DllImport("DataBaseLib.dll", EntryPoint = "DBInit", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DBInit(string DBSelect, string FilePath, string DBCommand, string TableName);

        [DllImport("DataBaseLib.dll", EntryPoint = "CreatDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreatDB(string FilePath, string TableName);

        [DllImport("DataBaseLib.dll", EntryPoint = "OpenDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenDB(string FilePath, string TableName);

        [DllImport("DataBaseLib.dll", EntryPoint = "CloseDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseDB();

        [DllImport("DataBaseLib.dll", EntryPoint = "AddTable", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AddTable(string TableName);

        [DllImport("DataBaseLib.dll", EntryPoint = "AddColumn", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AddColumn(string TableName, string ColName, string Datatype);

        [DllImport("DataBaseLib.dll", EntryPoint = "AddRows", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AddRows(string TableName, ref int index);

        [DllImport("DataBaseLib.dll", EntryPoint = "AddDBtoBuffer", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AddDBtoBuffer();

        [DllImport("DataBaseLib.dll", EntryPoint = "ClearBuffer", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClearBuffer();

        [DllImport("DataBaseLib.dll", EntryPoint = "FindDataInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FindDataInDB(string TableName, string ColName, object data, ref int index);

        [DllImport("DataBaseLib.dll", EntryPoint = "GetIndexInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetIndexInDB(string TableName, int RecordId, ref int index);

        [DllImport("DataBaseLib.dll", EntryPoint = "WriteFloatsToDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteFloatsToDB(string TableName, int index, string ColunmName, float[] data);

        [DllImport("DataBaseLib.dll", EntryPoint = "WriteIntsToDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteIntsToDB(string TableName, int index, string ColunmName, int[] data);

        [DllImport("DataBaseLib.dll", EntryPoint = "WriteSigleDataToDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteSigleDataToDB(string TableName, int index, string ColunmName, object data);

        [DllImport("DataBaseLib.dll", EntryPoint = "OverwriteSigleDataToDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OverwriteSigleDataToDB(string TableName, int index, string ColunmName, object data);

        [DllImport("DataBaseLib.dll", EntryPoint = "OverwriteIntsToDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OverwriteIntsToDB(string TableName, int index, string ColunmName, int[] data);

        [DllImport("DataBaseLib.dll", EntryPoint = "OverwriteFloatsToDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OverwriteFloatsToDB(string TableName, int index, string ColunmName, float[] data);

        [DllImport("DataBaseLib.dll", EntryPoint = "SaveDataToBuffer", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SaveDataToBuffer();

        [DllImport("DataBaseLib.dll", EntryPoint = "SaveDateToDataBase", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SaveDateToDataBase();

        [DllImport("DataBaseLib.dll", EntryPoint = "ReadFloatsInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReadFloatsInDB(string TableName, int index, string ColunmName, out float[] data);

        [DllImport("DataBaseLib.dll", EntryPoint = "ReadIntsInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReadIntsInDB(string TableName, int index, string ColunmName, out int[] data);

        [DllImport("DataBaseLib.dll", EntryPoint = "ReadSigleDataInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReadSigleDataInDB(string TableName, int index, string ColunmName, ref object data);

        [DllImport("DataBaseLib.dll", EntryPoint = "QueryInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QueryInDB(string com, out DataSet dt);

        [DllImport("DataBaseLib.dll", EntryPoint = "UpdateDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateDB(DataSet ds);

        [DllImport("DataBaseLib.dll", EntryPoint = "RemoveDataIndexInDB", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RemoveDataIndexInDB(int index);

        #endregion

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private string m_FileFoldPath = Application.StartupPath + @"\Config\TestConfig";
        private string m_INICurveInfoFilePath = Application.StartupPath + @"\Config\TestConfig\CurveInfo.ini";
        private string m_XMLRuntimeInfoFilePath1 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench1.xml";
        private string m_XMLRuntimeInfoFilePath2 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench2.xml";
        private string m_XMLRuntimeInfoFilePath3 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench3.xml";
        private string m_XMLRuntimeInfoFilePath4 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench4.xml";
        private string m_ConfigFilePath = Application.StartupPath + @"\SystemFile\SetPara\SetParaConfig.xml";
        private string m_ReportFilePath = Application.StartupPath + @"\SystemFile\SetPara\ReportXML.xml";
        private string m_INIRuntimeParaConfigFilePath = Application.StartupPath + @"\Config\TestConfig\RuntimeParaConfig.ini";
        private string m_INIToolBar01FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar01.ini";
        private string m_INIToolBar02FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar02.ini";
        private string m_INIToolBar03FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar03.ini";
        private string m_INIToolBar04FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar04.ini";
        private Form1 m_MainFormHandle = null;
        private event StateSysInfo ShowDebugInfo;
        private int m_MachineNum = 0;

        private DataBaseLib.DataBaseInterface m_DBM1Handle = null;
        private DataBaseLib.DataBaseInterface m_DBM2Handle = null;
        private DataBaseLib.DataBaseInterface m_DBM3Handle = null;
        private DataBaseLib.DataBaseInterface m_DBM4Handle = null;
        private string m_DBBench1FilePath = Application.StartupPath + @"\DataBase\M1.mdb";
        private string m_DBBench2FilePath = Application.StartupPath + @"\DataBase\M2.mdb";
        private string m_DBBench3FilePath = Application.StartupPath + @"\DataBase\M3.mdb";
        private string m_DBBench4FilePath = Application.StartupPath + @"\DataBase\M4.mdb";
        private string m_TableName = "TestResult";
        public class RuntimeInfoList
        {
            public string m_RegName;
            public string m_RegNameCH;//中文名称
            public string m_DataType;
            public string m_DataNum;//数据数
            public string m_ParaUint;
            public string[] m_State;
            public string m_isSave;
            public RuntimeInfoList(string regName, string regNameCH, string Datatype, string paraUint, string DataNum, string[] state, string isSave)
            {
                m_RegName = regName;
                m_RegNameCH = regNameCH;
                m_DataType = Datatype;
                m_ParaUint = paraUint;
                m_DataNum = DataNum;
                m_State = new string[state.Length];
                for (int i = 0; i < state.Length; i++)
                {
                    m_State[i] = state[i];
                }
                m_isSave = isSave;
            }
        }
        public List<RuntimeInfoList> m_RuntimeInfoBench1Lists = new List<RuntimeInfoList>();
        public List<RuntimeInfoList> m_RuntimeInfoBench2Lists = new List<RuntimeInfoList>();
        public List<RuntimeInfoList> m_RuntimeInfoBench3Lists = new List<RuntimeInfoList>();
        public List<RuntimeInfoList> m_RuntimeInfoBench4Lists = new List<RuntimeInfoList>();

        public class SetParaList
        {
            public string m_RegName;
            public string m_RegNameCh;//中文名称
            public string m_DataType;
            public object m_MinValue;//最小值
            public object m_MaxValue;//最大值
            public string m_ParaUint;//单位
            public string m_isSave;
            public string m_BenchNo;//工位号
            public SetParaList(string regName, string regNameCH, string dataType, object minValue, object maxValue, string paraUnit, string isSave ,string BenchNo)
            {
                m_RegName = regName;
                m_RegNameCh = regNameCH;
                m_DataType = dataType;
                m_MinValue = minValue;
                m_MaxValue = maxValue;
                m_ParaUint = paraUnit;
                m_isSave = isSave;
                m_BenchNo = BenchNo;
            }
        }
        public List<SetParaList> m_SetParaBench1Lists = new List<SetParaList>();
        public List<SetParaList> m_SetParaBench2Lists = new List<SetParaList>();
        public List<SetParaList> m_SetParaBench3Lists = new List<SetParaList>();
        public List<SetParaList> m_SetParaBench4Lists = new List<SetParaList>();

        public class ReportParaList
        {
            public string m_ParaName;
            public string m_ParaNameCH;
            public string m_ParaUint;
            public string m_isSave;
            public ReportParaList(string paraName, string paraNameCH, string paraUint, string isSave)
            {
                m_ParaName = paraName;
                m_ParaNameCH = paraNameCH;
                m_ParaUint = paraUint;
                m_isSave = isSave;
            }
        }
        public List<ReportParaList> m_ReportParaLists = new List<ReportParaList>();

        public TestFormConfig()
        {
            InitializeComponent();
        }

        private void TestFormConfig_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);

            AddTimeInfoToList();
            CreateFileFold();
            InitDis();
            InitCurveDis();
            InitRuntimeDis();

            m_DBM1Handle = new DataBaseLib.DataBaseInterface();
            m_DBM2Handle = new DataBaseLib.DataBaseInterface();
            m_DBM3Handle = new DataBaseLib.DataBaseInterface();
            m_DBM4Handle = new DataBaseLib.DataBaseInterface();
        }

        private void AddTimeInfoToList()
        {
            string RegName  = "StartTime";
            string RegNameCH = "开始时间";//中文名称
            string DataNum = "1";//数据数
            string dataType = "DateTime";
            string ParaUint = "T";
            string[] State = new string[1];
            State[0] = "";
            string isSave = "";

            m_RuntimeInfoBench1Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
            m_RuntimeInfoBench2Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
            m_RuntimeInfoBench3Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
            m_RuntimeInfoBench4Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));

            RegName = "EndTime";
            RegNameCH = "停止时间";//中文名称
            DataNum = "1";//数据数
            ParaUint = "T";
            State = new string[1];
            State[0] = "";

            m_RuntimeInfoBench1Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
            m_RuntimeInfoBench2Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
            m_RuntimeInfoBench3Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
            m_RuntimeInfoBench4Lists.Add(new RuntimeInfoList(RegName, RegNameCH, dataType, ParaUint, DataNum, State, isSave));
        }

        private void InitDis()
        {
            string INIFilePath = Application.StartupPath + @"\Config\SetPara\SetParaConfig.ini";
            string sMachineNum = ContentValue("SetParaConfig", "Machine", INIFilePath);
            int machineNum = Convert.ToInt32(sMachineNum);
            switch (machineNum)
            {
            case  1:
                    Grp_Style01.Enabled = true;
                    Grp_Tool01.Enabled = true;
                    Grp_Style02.Enabled = false;
                    Grp_Tool02.Enabled = false;
                    Grp_Style03.Enabled = false;
                    Grp_Tool03.Enabled = false;
                    Grp_Style04.Enabled = false;
                    Grp_Tool04.Enabled = false;
            	break;
            case 2:
                Grp_Style01.Enabled = true;
                Grp_Tool01.Enabled = true;
                Grp_Style02.Enabled = true;
                Grp_Tool02.Enabled = true;
                Grp_Style03.Enabled = false;
                Grp_Tool03.Enabled = false;
                Grp_Style04.Enabled = false;
                Grp_Tool04.Enabled = false;
                break;
            case 3:
                Grp_Style01.Enabled = true;
                Grp_Tool01.Enabled = true;
                Grp_Style02.Enabled = true;
                Grp_Tool02.Enabled = true;
                Grp_Style03.Enabled = true;
                Grp_Tool03.Enabled = true;
                Grp_Style04.Enabled = false;
                Grp_Tool04.Enabled = false;
                break;
            case 4:
                Grp_Style01.Enabled = true;
                Grp_Tool01.Enabled = true;
                Grp_Style02.Enabled = true;
                Grp_Tool02.Enabled = true;
                Grp_Style03.Enabled = true;
                Grp_Tool03.Enabled = true;
                Grp_Style04.Enabled = true;
                Grp_Tool04.Enabled = true;
                break;
            }
            m_MachineNum = machineNum;
        }

        private void CreateFileFold()
        {
            if (!Directory.Exists(m_FileFoldPath))
            {
                Directory.CreateDirectory(m_FileFoldPath);
            }
        }

        #region 初始化显示
        private void InitCurveDis()
        {
            int code = CB_DrawCurveMethod.Items.Add("GDI");
            code = CB_DrawCurveMethod.Items.Add("OpenGL");
            CB_DrawCurveMethod.SelectedIndex = 0;
            if (code != 1)
            {
                ShowDebugInfo("TestFormConfig 初始化曲线模式失败");
            }


            code = CB_Algorithm.Items.Add("Line");
            code = CB_Algorithm.Items.Add("PLine");
            code = CB_Algorithm.Items.Add("BTypeLine");
            code = CB_Algorithm.Items.Add("DeboorLine");
            code = CB_Algorithm.Items.Add("ThreeDivBezier");
            code = CB_Algorithm.Items.Add("ThreePhaseBezier");
            CB_Algorithm.SelectedIndex = 0;
            if (code != 1)
            {
                ShowDebugInfo("TestFormConfig 初始化曲线线型失败");
            }

            code = CB_LineColor.Items.Add("红色");
            code = CB_LineColor.Items.Add("蓝色");
            code = CB_LineColor.Items.Add("黄色");
            code = CB_LineColor.Items.Add("绿色");
            code = CB_LineColor.Items.Add("紫色");
            code = CB_LineColor.Items.Add("黑色");
            CB_LineColor.SelectedIndex = 0;
            if (code != 1)
            {
                ShowDebugInfo("TestFormConfig 初始化曲线颜色失败");
            }

            code = CB_DisType.Items.Add("压缩曲线");
            code = CB_DisType.Items.Add("平移曲线");
            CB_DisType.SelectedIndex = 0;
            if (code != 1)
            {
                ShowDebugInfo("TestFormConfig 初始化曲线绘制模式失败");
            }

            CB_SaveM1.Checked = true;
            CB_SaveM2.Checked = true;
            CB_SaveM3.Checked = true;
            CB_SaveM4.Checked = true;
        }

        private void InitRuntimeDis()
        {
            DGV_Report.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Report02.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Report03.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Report04.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGV_Report.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Report02.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Report03.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Report04.DefaultCellStyle = dataGridViewCellStyle1;

            switch (m_MachineNum)
            {
                case 1:
                    LoadRuntimeInfoXMLBench1();
                    DisRuntimeInfoBench1();
                    break;
                case 2:
                    LoadRuntimeInfoXMLBench1();
                    DisRuntimeInfoBench1();
                    LoadRuntimeInfoXMLBench2();
                    DisRuntimeInfoBench2();
                    break;
                case 3:
                    LoadRuntimeInfoXMLBench1();
                    DisRuntimeInfoBench1();
                    LoadRuntimeInfoXMLBench2();
                    DisRuntimeInfoBench2();
                    LoadRuntimeInfoXMLBench3();
                    DisRuntimeInfoBench3();
                    break;
                case 4:
                    LoadRuntimeInfoXMLBench1();
                    DisRuntimeInfoBench1();
                    LoadRuntimeInfoXMLBench2();
                    DisRuntimeInfoBench2();
                    LoadRuntimeInfoXMLBench3();
                    DisRuntimeInfoBench3();
                    LoadRuntimeInfoXMLBench4();
                    DisRuntimeInfoBench4();
                    break;
            }
        }

        private void LoadRuntimeInfoXMLBench1()
        {
            string regName = "";
            string regNameCH = "";
            string dataNum = "";
            string paraUnit = "";
            string dataType = "";
            string[] state  = new string[0];
            string isSave = "";
            if (!File.Exists(m_XMLRuntimeInfoFilePath1))
            {
                SendDebugInfo("TestFormConfig 实时信息配置表1不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLRuntimeInfoFilePath1);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "Name")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "NameCH")
                    {
                        regNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "DataType")
                    {
                        dataType = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUnit = SubChild.InnerText;
                    }
                    int Num = 0;
                    if (SubChild.Name == "DataNum")
                    {
                        dataNum = SubChild.InnerText;
                        Num = Convert.ToInt32(dataNum);
                        state = new string[Num];  
                    }        
                    if (SubChild.Name == "DataState")
                    {
                        int m = 0;
                        foreach (XmlNode SecSubChild in SubChild)
                        {                           
                            if (SecSubChild.Name == "DataState" + (m + 1).ToString())
                            {
                                state[m] = SecSubChild.InnerText;
                                m++;
                            }
                        }
                    }
                    if (SubChild.Name == "SaveDB")
                    {
                        isSave = SubChild.InnerText;
                    }
                }
                m_RuntimeInfoBench1Lists.Add(new RuntimeInfoList(regName, regNameCH, dataType, paraUnit, dataNum, state, isSave));
            }
        }

        private void LoadRuntimeInfoXMLBench2()
        {
            string regName = "";
            string regNameCH = "";
            string dataNum = "";
            string paraUnit = "";
            string dataType = "";
            string[] state = new string[0];
            string isSave = "";
            if (!File.Exists(m_XMLRuntimeInfoFilePath2))
            {
                SendDebugInfo("TestFormConfig 实时信息配置表2不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLRuntimeInfoFilePath2);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "Name")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "NameCH")
                    {
                        regNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "DataType")
                    {
                        dataType = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUnit = SubChild.InnerText;
                    }
                    int Num = 0;
                    if (SubChild.Name == "DataNum")
                    {
                        dataNum = SubChild.InnerText;
                        Num = Convert.ToInt32(dataNum);
                        state = new string[Num];
                    }
                    if (SubChild.Name == "DataState")
                    {
                        int m = 0;
                        foreach (XmlNode SecSubChild in SubChild)
                        {
                            if (SecSubChild.Name == "DataState" + (m + 1).ToString())
                            {
                                state[m] = SecSubChild.InnerText;
                                m++;
                            }
                        }
                    }
                    if (SubChild.Name == "SaveDB")
                    {
                        isSave = SubChild.InnerText;
                    }
                }
                m_RuntimeInfoBench2Lists.Add(new RuntimeInfoList(regName, regNameCH, dataType, paraUnit, dataNum, state, isSave));
            }
        }

        private void LoadRuntimeInfoXMLBench3()
        {
            string regName = "";
            string regNameCH = "";
            string dataNum = "";
            string dataType = "";
            string paraUnit = "";
            string[] state = new string[0];
            string isSave = "";
            if (!File.Exists(m_XMLRuntimeInfoFilePath3))
            {
                SendDebugInfo("TestFormConfig 实时信息配置表3不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLRuntimeInfoFilePath3);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "Name")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "NameCH")
                    {
                        regNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "DataType")
                    {
                        dataType = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUnit = SubChild.InnerText;
                    }
                    int Num = 0;
                    if (SubChild.Name == "DataNum")
                    {
                        dataNum = SubChild.InnerText;
                        Num = Convert.ToInt32(dataNum);
                        state = new string[Num];
                    }
                    if (SubChild.Name == "DataState")
                    {
                        int m = 0;
                        foreach (XmlNode SecSubChild in SubChild)
                        {
                            if (SecSubChild.Name == "DataState" + (m + 1).ToString())
                            {
                                state[m] = SecSubChild.InnerText;
                                m++;
                            }
                        }
                    }
                    if (SubChild.Name == "SaveDB")
                    {
                        isSave = SubChild.InnerText;
                    }
                }
                m_RuntimeInfoBench3Lists.Add(new RuntimeInfoList(regName, regNameCH, dataType, paraUnit, dataNum, state, isSave));
            }
        }

        private void LoadRuntimeInfoXMLBench4()
        {
            string regName = "";
            string regNameCH = "";
            string dataType = "";
            string dataNum = "";
            string paraUnit = "";
            string[] state = new string[0];
            string isSave = "";
            if (!File.Exists(m_XMLRuntimeInfoFilePath4))
            {
                SendDebugInfo("TestFormConfig 实时信息配置表4不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLRuntimeInfoFilePath4);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "Name")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "NameCH")
                    {
                        regNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "DataType")
                    {
                        dataType = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUnit = SubChild.InnerText;
                    }
                    int Num = 0;
                    if (SubChild.Name == "DataNum")
                    {
                        dataNum = SubChild.InnerText;
                        Num = Convert.ToInt32(dataNum);
                        state = new string[Num];
                    }
                    if (SubChild.Name == "DataState")
                    {
                        int m = 0;
                        foreach (XmlNode SecSubChild in SubChild)
                        {
                            if (SecSubChild.Name == "DataState" + (m + 1).ToString())
                            {
                                state[m] = SecSubChild.InnerText;
                                m++;
                            }
                        }
                    }
                    if (SubChild.Name == "SaveDB")
                    {
                        isSave = SubChild.InnerText;
                    }
                }
                m_RuntimeInfoBench4Lists.Add(new RuntimeInfoList(regName, regNameCH, dataType, paraUnit, dataNum, state, isSave));
            }
        }
        #endregion


        private void BT_SaveCurveInfo_Click(object sender, EventArgs e)
        {
            if (File.Exists(m_INICurveInfoFilePath))
            {
                File.Delete(m_INICurveInfoFilePath);
            }
            string sCurveMethod = CB_DrawCurveMethod.Text;
            string sAlgorithm = CB_Algorithm.Text;
            string sColor = CB_LineColor.Text;
            string sregNameM1 = TB_SetParaM1.Text;
            string sregNameM2 = TB_SetParaM2.Text;
            string sregNameM3 = TB_SetParaM3.Text;
            string sregNameM4 = TB_SetParaM4.Text;
            string syMax = TB_yMax.Text;
            string sxMax = TB_xDefault.Text;
            string sInterval = TB_xInterval.Text;
            string sLineWidth = TB_LineWidth.Text;
            string sGridWidth = TB_BK_Width.Text;
            string sGridHeight = TB_BK_Height.Text;
            string sDisType = CB_DisType.Text;
            string sSampleTime = TB_SampleTime.Text;
            string sDrawTime = TB_DrawTime.Text;
            bool isChecked = false;
            

            int sampletime = Convert.ToInt32(sSampleTime);
            int drawTime = Convert.ToInt32(sDrawTime);
            if (drawTime < sampletime)
            {
                MessageBox.Show("绘图时间必须大于采样时间！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CB_SaveM1.Checked == true)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            WritePrivateProfileString("CurveInfo", "SaveM1", isChecked.ToString(), m_INICurveInfoFilePath);
            if (CB_SaveM2.Checked == true)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            WritePrivateProfileString("CurveInfo", "SaveM2", isChecked.ToString(), m_INICurveInfoFilePath);
            if (CB_SaveM3.Checked == true)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            WritePrivateProfileString("CurveInfo", "SaveM3", isChecked.ToString(), m_INICurveInfoFilePath);
            if (CB_SaveM4.Checked == true)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            WritePrivateProfileString("CurveInfo", "SaveM4", isChecked.ToString(), m_INICurveInfoFilePath);

            WritePrivateProfileString("CurveInfo", "DisType", sDisType, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "CurveMethod", sCurveMethod, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "Algorithm", sAlgorithm, m_INICurveInfoFilePath);           
            WritePrivateProfileString("CurveInfo", "RegNameM1", sregNameM1, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "RegNameM2", sregNameM1, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "RegNameM3", sregNameM1, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "RegNameM4", sregNameM1, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "DrawTime", sDrawTime, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "SampleTime", sSampleTime, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "yMax", syMax, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "xMax", sxMax, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "Interval", sInterval, m_INICurveInfoFilePath);         
            WritePrivateProfileString("CurveInfo", "GridWidth", sGridWidth, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "GridHeigh", sGridHeight, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "Color", sColor, m_INICurveInfoFilePath);
            WritePrivateProfileString("CurveInfo", "LineWidth", sLineWidth, m_INICurveInfoFilePath);

            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        #region 保存实时信息配置
        private void TB_Report_TextChanged(object sender, EventArgs e)
        {
            int ColumnsCount = 0;
            try
            {
                ColumnsCount = Convert.ToInt32(TB_Report.Text);
            }
            catch (Exception)
            {
                return;
            }

            DisRuntimeInfoBench1();
            DisRuntimeInfoBench2();
            DisRuntimeInfoBench3();
            DisRuntimeInfoBench4();
            
        }

        private void DisRuntimeInfoBench1()
        {
            DGV_Report.Rows.Clear();
            int count = m_RuntimeInfoBench1Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(TB_Report.Text);
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
            DGV_Report.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DGV_Report.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DGV_Report.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench1Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench1Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void DisRuntimeInfoBench2()
        {
            DGV_Report02.Rows.Clear();
            int count = m_RuntimeInfoBench2Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(TB_Report.Text);
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
            DGV_Report02.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DGV_Report02.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DGV_Report02.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench2Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench2Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void DisRuntimeInfoBench3()
        {
            DGV_Report03.Rows.Clear();
            int count = m_RuntimeInfoBench3Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(TB_Report.Text);
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
            DGV_Report03.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DGV_Report03.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DGV_Report03.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench3Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench3Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void DisRuntimeInfoBench4()
        {
            DGV_Report04.Rows.Clear();
            int count = m_RuntimeInfoBench4Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(TB_Report.Text);
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
            DGV_Report04.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DGV_Report04.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DGV_Report04.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench4Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench4Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void BT_SaveReport_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("RuntimeParaConfig", "Rows", TB_Report.Text, m_INIRuntimeParaConfigFilePath);
            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region 按钮配置--工位一
        private void TB_Start_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_Start.Text = regNameCH;
            TB_Start.Image = Bitmap.FromFile(picPath);
        }

        private void TB_End_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();
           
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_End.Text = regNameCH;
            TB_End.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom01_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();
        
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom01.Text = regNameCH;
            BTCustom01.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom02_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();


            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom02.Text = regNameCH;
            BTCustom02.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom03_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                return;
            }

            BTCustom03.Text = regNameCH;
            BTCustom03.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom04_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom04.Text = regNameCH;
            BTCustom04.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom05_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar01FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                return;
            }

            BTCustom05.Text = regNameCH;
            BTCustom05.Image = Bitmap.FromFile(picPath);
        }
        #endregion

        #region 按钮配置--工位二
        private void TB_Start_2_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_Start.Text = regNameCH;
            TB_Start.Image = Bitmap.FromFile(picPath);
        }

        private void TB_Exit_2_Click(object sender, EventArgs e)
        {
        }

        private void TB_End_2_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();
      
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_End.Text = regNameCH;
            TB_End.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom01_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();
            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom01_2.Text = regNameCH;
            BTCustom01_2.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom02_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom02_2.Text = regNameCH;
            BTCustom02_2.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom03_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom03_2.Text = regNameCH;
            BTCustom03_2.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom04_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom04_2.Text = regNameCH;
            BTCustom04_2.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom05_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar02FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom05_2.Text = regNameCH;
            BTCustom05_2.Image = Bitmap.FromFile(picPath);
        }
        #endregion

        #region 按钮配置--工位三
        private void TB_Start_3_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_Start_3.Text = regNameCH;
            TB_Start_3.Image = Bitmap.FromFile(picPath);
        }

        private void TB_End_3_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_End_3.Text = regNameCH;
            TB_End_3.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom01_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom01_3.Text = regNameCH;
            BTCustom01_3.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom02_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom02_3.Text = regNameCH;
            BTCustom02_3.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom03_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom03_3.Text = regNameCH;
            BTCustom03_3.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom04_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom04_3.Text = regNameCH;
            BTCustom04_3.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom05_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar03FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom05_3.Text = regNameCH;
            BTCustom05_3.Image = Bitmap.FromFile(picPath);
        }

        #endregion

        #region 按钮配置--工位四
        private void TB_Start_4_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_Start_4.Text = regNameCH;
            TB_Start_4.Image = Bitmap.FromFile(picPath);
        }

        private void TB_End_4_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            TB_End_4.Text = regNameCH;
            TB_End_4.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom01_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom01_4.Text = regNameCH;
            BTCustom01_4.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom02_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom02_4.Text = regNameCH;
            BTCustom02_4.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom03_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName, INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom03_4.Text = regNameCH;
            BTCustom03_4.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom04_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom04_4.Text = regNameCH;
            BTCustom04_4.Image = Bitmap.FromFile(picPath);
        }

        private void BTCustom05_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar04FilePath;
            TestBTSetForm handle = new TestBTSetForm(BName,INIPath);
            handle.ShowDialog();

            
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);

            if (sEnable == "False")
            {
                return;
            }

            BTCustom05_4.Text = regNameCH;
            BTCustom05_4.Image = Bitmap.FromFile(picPath);
        }
        #endregion



        private void BT_Pre_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowSetParaConfigForm();
        }

        private void BT_Next_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowQueryDBConfigForm();
        }

        #region 
        private void BT_CreatDB_Click(object sender, EventArgs e)
        {
            LoadSetXML();
            LoadReportXML();
            bool code = CreatBenchDB();
            if (!code)
            {
                SendDebugInfo("TestFormConfig 创建数据库失败");
                MessageBox.Show("创建数据库失败！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void LoadSetXML()
        {
            string regName = "";
            string regNameCH = "";
            string dataType = "";
            object minValue = default(object);
            object maxValue = default(object);
            string paraUnit = "";
            string isSave = "";
            string BenchNo = "";
            if (!File.Exists(m_ConfigFilePath))
            {
                SendDebugInfo("TestFormConfig 设置信息配置表1不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_ConfigFilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "Name")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "NameCH")
                    {
                        regNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "DataType")
                    {
                        dataType = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ValueMin")
                    {
                        if (dataType == "bit" || dataType == "byte")
                        {
                            minValue = sbyte.Parse(SubChild.InnerText);
                        }
                        if (dataType == "ubyte")
                        {
                            minValue = byte.Parse(SubChild.InnerText);
                        }
                        if (dataType == "word")
                        {
                            minValue = Int16.Parse(SubChild.InnerText);
                        }
                        if (dataType == "uword")
                        {
                            minValue = UInt16.Parse(SubChild.InnerText);
                        }
                        if (dataType == "dword")
                        {
                            minValue = Int32.Parse(SubChild.InnerText);
                        }
                        if (dataType == "udword")
                        {
                            minValue = UInt32.Parse(SubChild.InnerText);
                        }
                        if (dataType == "float")
                        {
                            minValue = float.Parse(SubChild.InnerText);
                        }
                    }
                    if (SubChild.Name == "ValueMax")
                    {
                        if (dataType == "bit" || dataType == "byte")
                        {
                            maxValue = sbyte.Parse(SubChild.InnerText);
                        }
                        if (dataType == "ubyte")
                        {
                            maxValue = byte.Parse(SubChild.InnerText);
                        }
                        if (dataType == "word")
                        {
                            maxValue = Int16.Parse(SubChild.InnerText);
                        }
                        if (dataType == "uword")
                        {
                            maxValue = UInt16.Parse(SubChild.InnerText);
                        }
                        if (dataType == "dword")
                        {
                            maxValue = Int32.Parse(SubChild.InnerText);
                        }
                        if (dataType == "udword")
                        {
                            maxValue = UInt32.Parse(SubChild.InnerText);
                        }
                        if (dataType == "float")
                        {
                            maxValue = float.Parse(SubChild.InnerText);
                        }
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUnit = SubChild.InnerText;
                    }
                    if (SubChild.Name == "SaveDB")
                    {
                        isSave = SubChild.InnerText;
                    }
                    if (SubChild.Name == "BenchNo")
                    {
                        BenchNo = SubChild.InnerText;
                    }
                }
                if (BenchNo == "1")
                {
                    m_SetParaBench1Lists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, isSave,BenchNo));
                }
                if (BenchNo == "2")
                {
                    m_SetParaBench2Lists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, isSave, BenchNo));
                }
                if (BenchNo == "3")
                {
                    m_SetParaBench3Lists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, isSave, BenchNo));
                }
                if (BenchNo == "4")
                {
                    m_SetParaBench4Lists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, isSave, BenchNo));
                }
            }
        }

        private void LoadReportXML()
        {
            string paraName = "";
            string paraNameCH = "";
            string paraUint = "";
            string isSave = "";
            if (!File.Exists(m_ReportFilePath))
            {
                SendDebugInfo("TestFormConfig 报表信息配置表1不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_ReportFilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "Name")
                    {
                        paraName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "NameCH")
                    {
                        paraNameCH = SubChild.InnerText;
                    }
                    if (SubChild.Name == "ParaUint")
                    {
                        paraUint = SubChild.InnerText;
                    }
                    if (SubChild.Name == "SaveDB")
                    {
                        isSave = SubChild.InnerText;
                    }
                }
                m_ReportParaLists.Add(new ReportParaList(paraName, paraNameCH, paraUint, isSave));
            }
        }

        private bool CreatBenchDB()
        {
            bool code = false;
            if (m_MachineNum == 1)
            {
                code = CreatBench1DB();
                if (!code)
                {
                    return false;
                }
            }
            if (m_MachineNum == 2)
            {
                code = CreatBench1DB();
                if (!code)
                {
                    return false;
                }
                code = CreatBench2DB();
                if (!code)
                {
                    return false;
                }
            }
            if (m_MachineNum == 3)
            {
                code = CreatBench1DB();
                if (!code)
                {
                    return false;
                }
                code = CreatBench2DB();
                if (!code)
                {
                    return false;
                }
                code = CreatBench3DB();
                if (!code)
                {
                    return false;
                }
            }
            if (m_MachineNum == 4)
            {
                code = CreatBench1DB();
                if (!code)
                {
                    return false;
                }
                code = CreatBench2DB();
                if (!code)
                {
                    return false;
                }
                code = CreatBench3DB();
                if (!code)
                {
                    return false;
                }
                code = CreatBench4DB();
                if (!code)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CreatBench1DB()
        {
            if (File.Exists(m_DBBench1FilePath))
            {
                File.Delete(m_DBBench1FilePath);
            }
            bool code;
            code = m_DBM1Handle.DBInit("Access", m_DBBench1FilePath, "Create", m_TableName);
            if (!code)
            {
                SendDebugInfo("TestFormConfig 创建失败, 位置：Create，库1");
                //MessageBox.Show("创建失败, 位置：Create", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //配置表名称
            code = m_DBM1Handle.AddColumn(m_TableName, "UserName", "VarChar");
            if (!code)
            {
                SendDebugInfo("TestFormConfig 添加列失败，定位：UserName，库1");
                File.Delete(m_DBBench1FilePath);
                return false;
            }

            int count = 0;
            string regName = "";
            //实时信息
            count = m_RuntimeInfoBench1Lists.Count;
            for (int i = 0; i < count; i++)
            {
                regName = m_RuntimeInfoBench1Lists[i].m_RegName;
                if (m_RuntimeInfoBench1Lists[i].m_isSave != "y")
                {
                    continue;
                }
                if (m_RuntimeInfoBench1Lists[i].m_DataType == "DateTime")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "VarChar");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName +"，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
                else
                {
                    if (m_RuntimeInfoBench1Lists[i].m_DataType == "string")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "VarChar");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench1Lists[i].m_DataType == "bit" || m_SetParaBench1Lists[i].m_DataType == "byte")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench1Lists[i].m_DataType == "ubyte")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "TINYINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench1Lists[i].m_DataType == "word")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench1Lists[i].m_DataType == "uword" || m_SetParaBench1Lists[i].m_DataType == "dword")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "INTEGER");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench1Lists[i].m_DataType == "udword")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "DECIMAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench1Lists[i].m_DataType == "float")
                    {
                        code = m_DBM1Handle.AddColumn(m_TableName, regName, "REAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                            File.Delete(m_DBBench1FilePath);
                            return false;
                        }
                    }
                }
            }

            //报表参数
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM1Handle.AddColumn(m_TableName, regName, "VarChar");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                    m_DBM1Handle.CloseDB();
                    File.Delete(m_DBBench1FilePath);
                    return false;
                }
            }

            //设置参数
            count = m_SetParaBench1Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaBench1Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaBench1Lists[i].m_RegName;
                if (m_SetParaBench1Lists[i].m_DataType == "bit" || m_SetParaBench1Lists[i].m_DataType == "byte")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench1Lists[i].m_DataType == "ubyte")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "TINYINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench1Lists[i].m_DataType == "word")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench1Lists[i].m_DataType == "uword" || m_SetParaBench1Lists[i].m_DataType == "dword")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "INTEGER");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench1Lists[i].m_DataType == "udword")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "DECIMAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench1Lists[i].m_DataType == "float")
                {
                    code = m_DBM1Handle.AddColumn(m_TableName, regName, "REAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                        File.Delete(m_DBBench1FilePath);
                        return false;
                    }
                }
            }
            //曲线信息
            regName = TB_SetParaM1.Text;
            if (CB_SaveM1.Checked)
            {
                code = m_DBM1Handle.AddColumn(m_TableName, regName + "_X", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                    File.Delete(m_DBBench1FilePath);
                    return false;
                }
                code = m_DBM1Handle.AddColumn(m_TableName, regName + "_Y", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库1");
                    File.Delete(m_DBBench1FilePath);
                    return false;
                }
            }

            return true;
        }

        private bool CreatBench2DB()
        {
            if (File.Exists(m_DBBench2FilePath))
            {
                File.Delete(m_DBBench2FilePath);
            }
            bool code;
            code = m_DBM2Handle.DBInit("Access", m_DBBench2FilePath, "Create", m_TableName);
            if (!code)
            {
                SendDebugInfo("TestFormConfig 创建失败, 位置：Create，库2");
                //MessageBox.Show("创建失败, 位置：Create", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //配置表名称
            code = m_DBM2Handle.AddColumn(m_TableName, "UserName", "VarChar");
            if (!code)
            {
                SendDebugInfo("TestFormConfig 添加列失败, 位置：UserName，库2");
                File.Delete(m_DBBench2FilePath);
                return false;
            }

            int count = 0;
            string regName = "";
            //实时信息
            count = m_RuntimeInfoBench2Lists.Count;
            for (int i = 0; i < count; i++)
            {
                regName = m_RuntimeInfoBench2Lists[i].m_RegName;
                if (m_RuntimeInfoBench2Lists[i].m_isSave != "y")
                {
                    continue;
                }
                if (m_RuntimeInfoBench2Lists[i].m_DataType == "DateTime")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "VarChar");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
                else
                {
                    if (m_RuntimeInfoBench2Lists[i].m_DataType == "string")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "VarChar");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench2Lists[i].m_DataType == "bit" || m_SetParaBench2Lists[i].m_DataType == "byte")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench2Lists[i].m_DataType == "ubyte")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "TINYINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench2Lists[i].m_DataType == "word")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench2Lists[i].m_DataType == "uword" || m_SetParaBench2Lists[i].m_DataType == "dword")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "INTEGER");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench2Lists[i].m_DataType == "udword")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "DECIMAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench2Lists[i].m_DataType == "float")
                    {
                        code = m_DBM2Handle.AddColumn(m_TableName, regName, "REAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                            File.Delete(m_DBBench2FilePath);
                            return false;
                        }
                    }
                }
            }

            //报表参数
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM2Handle.AddColumn(m_TableName, regName, "VarChar");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                    File.Delete(m_DBBench2FilePath);
                    return false;
                }
            }

            //设置参数
            count = m_SetParaBench2Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaBench2Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaBench2Lists[i].m_RegName;
                if (m_SetParaBench2Lists[i].m_DataType == "bit" || m_SetParaBench2Lists[i].m_DataType == "byte")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench2Lists[i].m_DataType == "ubyte")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "TINYINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench2Lists[i].m_DataType == "word")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench2Lists[i].m_DataType == "uword" || m_SetParaBench2Lists[i].m_DataType == "dword")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "INTEGER");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench2Lists[i].m_DataType == "udword")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "DECIMAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench2Lists[i].m_DataType == "float")
                {
                    code = m_DBM2Handle.AddColumn(m_TableName, regName, "REAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                        File.Delete(m_DBBench2FilePath);
                        return false;
                    }
                }
            }
            //曲线信息
            regName = TB_SetParaM2.Text;
            if (CB_SaveM2.Checked)
            {
                code = m_DBM2Handle.AddColumn(m_TableName, regName + "_X", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                    File.Delete(m_DBBench2FilePath);
                    return false;
                }
                code = m_DBM2Handle.AddColumn(m_TableName, regName + "_Y", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库2");
                    File.Delete(m_DBBench2FilePath);
                    return false;
                }
            }
            return true;
        }

        private bool CreatBench3DB()
        {
            if (File.Exists(m_DBBench3FilePath))
            {
                File.Delete(m_DBBench3FilePath);
            }
            bool code;
            code = m_DBM3Handle.DBInit("Access", m_DBBench3FilePath, "Create", m_TableName);
            if (!code)
            {
                SendDebugInfo("TestFormConfig 创建失败, 位置：Create，库3");
                //MessageBox.Show("创建失败, 位置：Create", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //配置表名称
            code = m_DBM3Handle.AddColumn(m_TableName, "UserName", "VarChar");
            if (!code)
            {
                SendDebugInfo("TestFormConfig 添加列失败, 位置：UserName，库3");
                File.Delete(m_DBBench3FilePath);
                return false;
            }

            int count = 0;
            string regName = "";
            //实时信息
            count = m_RuntimeInfoBench3Lists.Count;
            for (int i = 0; i < count; i++)
            {
                regName = m_RuntimeInfoBench3Lists[i].m_RegName;
                if (m_RuntimeInfoBench3Lists[i].m_isSave != "y")
                {
                    continue;
                }
                if (m_RuntimeInfoBench3Lists[i].m_DataType == "DateTime")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "VarChar");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
                else
                {
                    if (m_RuntimeInfoBench3Lists[i].m_DataType == "string")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "VarChar");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench3Lists[i].m_DataType == "bit" || m_SetParaBench3Lists[i].m_DataType == "byte")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench3Lists[i].m_DataType == "ubyte")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "TINYINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench3Lists[i].m_DataType == "word")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench3Lists[i].m_DataType == "uword" || m_SetParaBench3Lists[i].m_DataType == "dword")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "INTEGER");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench3Lists[i].m_DataType == "udword")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "DECIMAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench3Lists[i].m_DataType == "float")
                    {
                        code = m_DBM3Handle.AddColumn(m_TableName, regName, "REAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                            File.Delete(m_DBBench3FilePath);
                            return false;
                        }
                    }
                }
            }

            //报表参数
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM3Handle.AddColumn(m_TableName, regName, "VarChar");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                    File.Delete(m_DBBench3FilePath);
                    return false;
                }
            }

            //设置参数
            count = m_SetParaBench3Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaBench3Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaBench3Lists[i].m_RegName;
                if (m_SetParaBench3Lists[i].m_DataType == "bit" || m_SetParaBench3Lists[i].m_DataType == "byte")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench3Lists[i].m_DataType == "ubyte")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "TINYINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench3Lists[i].m_DataType == "word")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench3Lists[i].m_DataType == "uword" || m_SetParaBench3Lists[i].m_DataType == "dword")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "INTEGER");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench3Lists[i].m_DataType == "udword")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "DECIMAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench3Lists[i].m_DataType == "float")
                {
                    code = m_DBM3Handle.AddColumn(m_TableName, regName, "REAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                        File.Delete(m_DBBench3FilePath);
                        return false;
                    }
                }
            }
            //曲线信息
            regName = TB_SetParaM3.Text;
            if (CB_SaveM3.Checked)
            {
                code = m_DBM3Handle.AddColumn(m_TableName, regName + "_X", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                    File.Delete(m_DBBench3FilePath);
                    return false;
                }
                code = m_DBM3Handle.AddColumn(m_TableName, regName + "_Y", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库3");
                    File.Delete(m_DBBench3FilePath);
                    return false;
                }
            }
            return true;
        }

        private bool CreatBench4DB()
        {
            if (File.Exists(m_DBBench4FilePath))
            {
                File.Delete(m_DBBench4FilePath);
            }
            bool code;
            code = m_DBM4Handle.DBInit("Access", m_DBBench4FilePath, "Create", m_TableName);
            if (!code)
            {
                SendDebugInfo("TestFormConfig 创建失败, 位置：Create，库4");
                //MessageBox.Show("创建失败, 位置：Create", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //配置表名称
            code = m_DBM4Handle.AddColumn(m_TableName, "UserName", "VarChar");
            if (!code)
            {
                SendDebugInfo("TestFormConfig 添加列失败, 位置：UserName，库4");
                File.Delete(m_DBBench4FilePath);
                return false;
            }

            int count = 0;
            string regName = "";
            //实时信息
            count = m_RuntimeInfoBench4Lists.Count;
            for (int i = 0; i < count; i++)
            {
                regName = m_RuntimeInfoBench4Lists[i].m_RegName;
                if (m_RuntimeInfoBench4Lists[i].m_isSave != "y")
                {
                    continue;
                }
                if (m_RuntimeInfoBench4Lists[i].m_DataType == "DateTime")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "VarChar");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
                else
                {
                    if (m_RuntimeInfoBench4Lists[i].m_DataType == "string")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "VarChar");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench4Lists[i].m_DataType == "bit" || m_SetParaBench4Lists[i].m_DataType == "byte")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench4Lists[i].m_DataType == "ubyte")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "TINYINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench4Lists[i].m_DataType == "word")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "SMALLINT");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench4Lists[i].m_DataType == "uword" || m_SetParaBench4Lists[i].m_DataType == "dword")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "INTEGER");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench4Lists[i].m_DataType == "udword")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "DECIMAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                    if (m_SetParaBench4Lists[i].m_DataType == "float")
                    {
                        code = m_DBM4Handle.AddColumn(m_TableName, regName, "REAL");
                        if (!code)
                        {
                            SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                            File.Delete(m_DBBench4FilePath);
                            return false;
                        }
                    }
                }
            }

            //报表参数
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM4Handle.AddColumn(m_TableName, regName, "VarChar");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                    File.Delete(m_DBBench4FilePath);
                    return false;
                }
            }

            //设置参数
            count = m_SetParaBench4Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaBench4Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaBench4Lists[i].m_RegName;
                if (m_SetParaBench4Lists[i].m_DataType == "bit" || m_SetParaBench4Lists[i].m_DataType == "byte")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench4Lists[i].m_DataType == "ubyte")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "TINYINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench4Lists[i].m_DataType == "word")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "SMALLINT");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench4Lists[i].m_DataType == "uword" || m_SetParaBench4Lists[i].m_DataType == "dword")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "INTEGER");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench4Lists[i].m_DataType == "udword")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "DECIMAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
                if (m_SetParaBench4Lists[i].m_DataType == "float")
                {
                    code = m_DBM4Handle.AddColumn(m_TableName, regName, "REAL");
                    if (!code)
                    {
                        SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                        File.Delete(m_DBBench4FilePath);
                        return false;
                    }
                }
            }
            //曲线信息
            regName = TB_SetParaM4.Text;
            if (CB_SaveM4.Checked == true)
            {
                code = m_DBM4Handle.AddColumn(m_TableName, regName + "_X", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                    File.Delete(m_DBBench4FilePath);
                    return false;
                }
                code = m_DBM4Handle.AddColumn(m_TableName, regName + "_Y", "IMAGE");
                if (!code)
                {
                    SendDebugInfo("TestFormConfig 添加列失败，定位：" + regName + "，库4");
                    File.Delete(m_DBBench4FilePath);
                    return false;
                }
            }
            return true;
        }
        #endregion

        private void SendDebugInfo(string s)
        {
            if (ShowDebugInfo != null)
            {
                ShowDebugInfo(s);
            }
        }



    }
}
