using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using DrawCurveDLL;
using ExcelLib__Speed;

namespace StandardTestBench
{
    public partial class QueryDB : Form
    {
        #region DrawCurve
        [DllImport("DrawCurve.dll", EntryPoint = "LibInit", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LibInit(string DrawMode);

        [DllImport("DrawCurve.dll", EntryPoint = "SetDrawInitMode", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDrawInitMode(string DrawMode);

        [DllImport("DrawCurve.dll", EntryPoint = "GDIInit", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GDIInit(Panel panel, string CurveType, Color col, float LineWidth, int Gridwidth, int Gridheight);

        [DllImport("DrawCurve.dll", EntryPoint = "OpenGLInit", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenGLInit(Point StartPos, Point EndPos, int Length, int Height, float MaxLength, float MaxHeight, string CurveType, float LineWidth);

        [DllImport("DrawCurve.dll", EntryPoint = "SetModule", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetModule(int DrawType);

        [DllImport("DrawCurve.dll", EntryPoint = "SaveSourcePointF", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SaveSourcePointF(PointF[] pt);

        [DllImport("DrawCurve.dll", EntryPoint = "ClearSourcePointF", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClearSourcePointF();

        [DllImport("DrawCurve.dll", EntryPoint = "GetSourcePointF", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSourcePointF(out PointF[] pt);

        [DllImport("DrawCurve.dll", EntryPoint = "DrawCurve", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawCurve(Panel panel, Color col, float xMax, float yMax);

        [DllImport("DrawCurve.dll", EntryPoint = "ZoomOutBasePoint", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZoomOutBasePoint(Panel panel, Point StartPt, Point EndPt);

        [DllImport("DrawCurve.dll", EntryPoint = "ZoomInBasePoint", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZoomInBasePoint(Panel panel, Point StartPt, Point EndPt);

        [DllImport("DrawCurve.dll", EntryPoint = "ZoomOutBaseXY", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZoomOutBaseXY(Panel panel, float StartX, float StartY, float EndX, float EndY);

        [DllImport("DrawCurve.dll", EntryPoint = "ZoomInBaseXY", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZoomInBaseXY(Panel panel, float StartX, float StartY, float EndX, float EndY);

        #endregion

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

        #region ExcelDLL
        [DllImport("ExcelLib__Speed.dll", EntryPoint = "Init", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init(string FilePath, string Sheetname);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "OpenFile", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OpenFile();

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "CloseFile", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseFile();

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "SetCellColor", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCellColor(string StartCellPos, string EndCellPos, string Command);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "SetCellFontColor", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCellFontColor(string StartCellPos, string EndCellPos, string Command);

        [DllImport("DrawCurve.dll", EntryPoint = "SetCellAutoFit", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCellAutoFit(string StartCellPos, string EndCellPos, string Command);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "SetCellFont", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCellFont(string StartCellPos, string EndCellPos, string FontStyle, int size);

        [DllImport("DrawCurve.dll", EntryPoint = "SetCellAlignment", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCellAlignment(string StartCellPos, string EndCellPos, string HorizontalPosStyle, string VerticalPosStyle);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "MergeCell", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MergeCell(string StartCellPos, string EndCellPos);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "WriteData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteData(string CellPos, string Value);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "ReadData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReadData(string CellPos, ref string Value);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "InsertPic", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void InsertPic(string StartCellPos, string EndCellPos, string PicPath);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "InsertRow", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void InsertRow(int RowIndex);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "Save", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Save();

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "SaveAs", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SaveAs(string FilePath);

        [DllImport("ExcelLib__Speed.dll", EntryPoint = "ExcelToPDF", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ExcelToPDF(string sourcePath, string targetPath);
        #endregion

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private Form1 m_MainFormHandle = null;
        private UserManager m_UserManagerHandle = null;
        private event StatePageInfo ShowPageInfo;
        private event StateSysInfo ShowDebugInfo;

        private TabControl.TabPageCollection m_Pages;
        //private TabPage m_MainPage;
        private TabPage m_Page1;
        private TabPage m_Page2;
        private TabPage m_Page3;
        private TabPage m_Page4;
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
        private string m_INIMachineFilePath = Application.StartupPath + @"\Config\SetPara\SetParaConfig.ini";//试验台数目
        private string m_INIQueryDBFilePath = Application.StartupPath + @"\Config\QueryDB\QueryDB.ini";
        private bool m_isZoom = false;  //是否放大
        private bool m_isCreatePDF = false; //是否创建PDF表报

        private string m_INICurveInfoFilePath = Application.StartupPath + @"\Config\TestConfig\CurveInfo.ini";

        public class ReportList
        {
            public string m_RegName;
            public string m_Value;
            public string m_Position;
            public ReportList(string regName, string value, string position)
            {
                m_RegName = regName;
                m_Value = value;
                m_Position = position;
            }
        }
        public List<ReportList> m_ReportListM1 = new List<ReportList>();
        public List<ReportList> m_ReportListM2 = new List<ReportList>();
        public List<ReportList> m_ReportListM3 = new List<ReportList>();
        public List<ReportList> m_ReportListM4 = new List<ReportList>();

        private string m_XMLReportInfoM1FilePath = Application.StartupPath + @"\SystemFile\Report\ReportInfoM1.xml";//报表信息
        private string m_XMLReportInfoM2FilePath = Application.StartupPath + @"\SystemFile\Report\ReportInfoM2.xml";//报表信息
        private string m_XMLReportInfoM3FilePath = Application.StartupPath + @"\SystemFile\Report\ReportInfoM3.xml";//报表信息
        private string m_XMLReportInfoM4FilePath = Application.StartupPath + @"\SystemFile\Report\ReportInfoM4.xml";//报表信息
        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench1Handle;
        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench2Handle;
        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench3Handle;
        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench4Handle;
        private Color m_LineColor = Color.Red;

        //private float m_M1xMax = 30;
        public float m_M1yMax = 100;
        //private float m_M2xMax = 30;
        public float m_M2yMax = 100;
        //private float m_M3xMax = 30;
        public float m_M3yMax = 100;
        //private float m_M4xMax = 30;
        public float m_M4yMax = 100;
        public DateTime m_M1BaseStartTime = default(DateTime);
        public DateTime m_M1BaseEndTime = default(DateTime);
        public DateTime m_M2BaseStartTime = default(DateTime);
        public DateTime m_M2BaseEndTime = default(DateTime);
        public DateTime m_M3BaseStartTime = default(DateTime);
        public DateTime m_M3BaseEndTime = default(DateTime);
        public DateTime m_M4BaseStartTime = default(DateTime);
        public DateTime m_M4BaseEndTime = default(DateTime);
        public float m_ZoomyMaxM1 = 100;
        public DateTime m_ZoomStartTimeM1 = default(DateTime);
        public DateTime m_ZoomEndTimeM1 = default(DateTime);
        public float m_ZoomyMaxM2 = 100;
        public DateTime m_ZoomStartTimeM2 = default(DateTime);
        public DateTime m_ZoomEndTimeM2 = default(DateTime);
        public float m_ZoomyMaxM3 = 100;
        public DateTime m_ZoomStartTimeM3 = default(DateTime);
        public DateTime m_ZoomEndTimeM3 = default(DateTime);
        public float m_ZoomyMaxM4 = 100;
        public DateTime m_ZoomStartTimeM4 = default(DateTime);
        public DateTime m_ZoomEndTimeM4 = default(DateTime);

        private ExecelLibInterface m_M1ExcelHandle = null;
        private ExecelLibInterface m_M2ExcelHandle = null;
        private ExecelLibInterface m_M3ExcelHandle = null;
        private ExecelLibInterface m_M4ExcelHandle = null;
        private string m_M1ExcelModuleFilePath = Application.StartupPath + @"\SystemFile\M1Module.xls";//获取INI文件路径
        private string m_M2ExcelModuleFilePath = Application.StartupPath + @"\SystemFile\M2Module.xls";//获取INI文件路径
        private string m_M3ExcelModuleFilePath = Application.StartupPath + @"\SystemFile\M3Module.xls";//获取INI文件路径
        private string m_M4ExcelModuleFilePath = Application.StartupPath + @"\SystemFile\M4Module.xls";//获取INI文件路径
        private string m_M1ReportFilePath = Application.StartupPath + @"\Report\M1";
        private string m_M2ReportFilePath = Application.StartupPath + @"\Report\M2";
        private string m_M3ReportFilePath = Application.StartupPath + @"\Report\M3";
        private string m_M4ReportFilePath = Application.StartupPath + @"\Report\M4";
        private string m_PicFilePath = Application.StartupPath + @"\Report\pic.png";


        public QueryDB()
        {
            InitializeComponent();
        }

        private void QueryDB_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);

            m_UserManagerHandle = UserManager.GetHandle();

            m_isZoom = Convert.ToBoolean(ContentValue("QueryDB", "Zoom", m_INIQueryDBFilePath));
            m_isCreatePDF = Convert.ToBoolean(ContentValue("QueryDB", "PDFReport", m_INIQueryDBFilePath));

            string syMax = ContentValue("CurveInfo", "yMax", m_INICurveInfoFilePath);
            m_M1yMax = Convert.ToSingle(syMax);
            m_M2yMax = m_M1yMax;
            m_M3yMax = m_M1yMax;
            m_M4yMax = m_M1yMax;

            DG_DB_M1.Enabled = false;
            DG_DB_M2.Enabled = false;
            DG_DB_M3.Enabled = false;
            DG_DB_M4.Enabled = false;

            InitPages();

            LoadReportXML();

            AccessInit();

            DisDB();

            InitFormDis();

            CurveInit();

            CurveZoomInit();

            ExcelInit();

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                ChangeUIToEnglish();
            }

        }

        #region Page初始化
        private void InitPages()
        {
            m_Pages = tabControl1.TabPages;
            //m_MainPage = m_Pages[0];
            m_Page1 = m_Pages[0];
            m_Page2 = m_Pages[1];
            m_Page3 = m_Pages[2];
            m_Page4 = m_Pages[3];
            RemoveALLPages();
            string sMcount = ContentValue("SetParaConfig", "Machine", m_INIMachineFilePath);
            m_MachineNum = Convert.ToInt32(sMcount);
            AddPages(m_MachineNum);
        }

        private void AddPages(int Num)
        {
            RemoveALLPages();
            if (Num == 1)
            {
                tabControl1.TabPages.Add(m_Page1);
            }
            if (Num == 2)
            {
                tabControl1.TabPages.Add(m_Page1);
                tabControl1.TabPages.Add(m_Page2);
            }
            if (Num == 3)
            {
                tabControl1.TabPages.Add(m_Page1);
                tabControl1.TabPages.Add(m_Page2);
                tabControl1.TabPages.Add(m_Page3);
            }
            if (Num == 4)
            {
                tabControl1.TabPages.Add(m_Page1);
                tabControl1.TabPages.Add(m_Page2);
                tabControl1.TabPages.Add(m_Page3);
                tabControl1.TabPages.Add(m_Page4);
            }
        }

        private void RemoveALLPages()
        {
            m_Pages = tabControl1.TabPages;
            int count = m_Pages.Count;
            for (int i = 0; i < count; i++)
            {
                tabControl1.TabPages.Remove(m_Pages[0]);
            }
        }
        #endregion

        #region 加载报表信息
        private void LoadReportXML()
        {
            if (m_MachineNum == 1)
            {
                LoadReportXMLM1();
            }

            if (m_MachineNum == 2)
            {
                LoadReportXMLM1();
                LoadReportXMLM2();
            }

            if (m_MachineNum == 3)
            {
                LoadReportXMLM1();
                LoadReportXMLM2();
                LoadReportXMLM3();
            }

            if (m_MachineNum == 4)
            {
                LoadReportXMLM1();
                LoadReportXMLM2();
                LoadReportXMLM3();
                LoadReportXMLM4();
            }   
        }

        private void LoadReportXMLM1()
        {
            string regName = "";
            string sValue = "";
            string sPos = "";
            if (!File.Exists(m_XMLReportInfoM1FilePath))
            {
                SendDebugInfo("QueryDB XML1 文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLReportInfoM1FilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "RegName")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Value")
                    {
                        sValue = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Position")
                    {
                        sPos = SubChild.InnerText;
                    }
                }
                m_ReportListM1.Add(new ReportList(regName, sValue, sPos));
            }
        }

        private void LoadReportXMLM2()
        {
            string regName = "";
            string sValue = "";
            string sPos = "";
            if (!File.Exists(m_XMLReportInfoM2FilePath))
            {
                SendDebugInfo("QueryDB XML2 文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLReportInfoM2FilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "RegName")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Value")
                    {
                        sValue = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Position")
                    {
                        sPos = SubChild.InnerText;
                    }
                }
                m_ReportListM2.Add(new ReportList(regName, sValue, sPos));
            }
        }

        private void LoadReportXMLM3()
        {
            string regName = "";
            string sValue = "";
            string sPos = "";
            if (!File.Exists(m_XMLReportInfoM3FilePath))
            {
                SendDebugInfo("QueryDB XML3 文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLReportInfoM3FilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "RegName")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Value")
                    {
                        sValue = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Position")
                    {
                        sPos = SubChild.InnerText;
                    }
                }
                m_ReportListM3.Add(new ReportList(regName, sValue, sPos));
            }
        }

        private void LoadReportXMLM4()
        {
            string regName = "";
            string sValue = "";
            string sPos = "";
            if (!File.Exists(m_XMLReportInfoM4FilePath))
            {
                SendDebugInfo("QueryDB XML4 文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLReportInfoM4FilePath);
            XmlElement root = XMLDoc.DocumentElement;

            foreach (XmlNode Child in root.ChildNodes)
            {
                foreach (XmlNode SubChild in Child)
                {
                    if (SubChild.Name == "RegName")
                    {
                        regName = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Value")
                    {
                        sValue = SubChild.InnerText;
                    }
                    if (SubChild.Name == "Position")
                    {
                        sPos = SubChild.InnerText;
                    }
                }
                m_ReportListM4.Add(new ReportList(regName, sValue, sPos));
            }
        }
        #endregion

        #region 数据库初始化
        private void AccessInit()
        {
            if (m_MachineNum == 1)
            {
                AccessInitM1();
            }

            if (m_MachineNum == 2)
            {
                AccessInitM1();
                AccessInitM2();
            }

            if (m_MachineNum == 3)
            {
                AccessInitM1();
                AccessInitM2();
                AccessInitM3();
            }

            if (m_MachineNum == 4)
            {
                AccessInitM1();
                AccessInitM2();
                AccessInitM3();
                AccessInitM4();
            }
        }

        private void AccessInitM1()
        {
            m_DBM1Handle = new DataBaseLib.DataBaseInterface();

            bool code = m_DBM1Handle.DBInit("Access", m_DBBench1FilePath, "Open", m_TableName);
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表1失败, 位置：Open");
                MessageBox.Show("打开数据表1失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM1Handle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表1加载失败, 位置：AddBuffer");
                MessageBox.Show("打开数据表1加载失败, 位置：AddBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void AccessInitM2()
        {
            m_DBM2Handle = new DataBaseLib.DataBaseInterface();

            bool code = m_DBM2Handle.DBInit("Access", m_DBBench2FilePath, "Open", m_TableName);
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表2失败, 位置：Open");
                MessageBox.Show("打开数据表2失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM2Handle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表2加载失败, 位置：AddBuffer");
                MessageBox.Show("打开数据表2加载失败, 位置：AddBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void AccessInitM3()
        {
            m_DBM3Handle = new DataBaseLib.DataBaseInterface();

            bool code = m_DBM3Handle.DBInit("Access", m_DBBench3FilePath, "Open", m_TableName);
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表3失败, 位置：Open");
                MessageBox.Show("打开数据表3失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM3Handle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表3加载失败, 位置：AddBuffer");
                MessageBox.Show("打开数据表3加载失败, 位置：AddBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void AccessInitM4()
        {
            m_DBM4Handle = new DataBaseLib.DataBaseInterface();

            bool code = m_DBM4Handle.DBInit("Access", m_DBBench4FilePath, "Open", m_TableName);
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表4失败, 位置：Open");
                MessageBox.Show("打开数据表4失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM4Handle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("QueryDB 打开数据表4加载失败, 位置：AddBuffer");
                MessageBox.Show("打开数据表4加载失败, 位置：AddBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region DG显示初始化
        private void DisDB()
        {
            if (m_MachineNum == 1)
            {
                DisDBM1();
            }

            if (m_MachineNum == 2)
            {
                DisDBM1();
                DisDBM2();
            }

            if (m_MachineNum == 3)
            {
                DisDBM1();
                DisDBM2();
                DisDBM3();
            }

            if (m_MachineNum == 4)
            {
                DisDBM1();
                DisDBM2();
                DisDBM3();
                DisDBM4();
            }
        }

        private void DisDBM1()
        {
            DataSet ds;
            string userName = m_UserManagerHandle.m_CurrenUser;
            string com = "select * from " + m_TableName + "where UserName = " + userName;

            if (userName == "Admin")
            {
                com = com = "select * from " + m_TableName;            
            }
            bool ret = m_DBM1Handle.QueryInDB(com, out ds);
            if (!ret)
            {
                SendDebugInfo("QueryDB 查询数据库1失败");
                return;
            }

            DataTable dt = ds.Tables[0];
            DG_DB_M1.DataSource = dt;

            for (int i = 0; i < DG_DB_M1.ColumnCount; i++)
            {
                if (DG_DB_M1.Columns[i].ValueType == typeof(byte[]))
                {
                    DG_DB_M1.Columns[i].Visible = false;
                    continue;
                }
                CB_ColumnM1.Items.Add(DG_DB_M1.Columns[i].Name);
            }
            CB_ColumnM1.SelectedIndex = 0;
        }

        private void DisDBM2()
        {
            DataSet ds;
            string userName = m_UserManagerHandle.m_CurrenUser;
            string com = "select * from " + m_TableName + "where UserName = " + userName;

            if (userName == "Admin")
            {
                com = com = "select * from " + m_TableName;
            }
            bool ret = m_DBM2Handle.QueryInDB(com, out ds);
            if (!ret)
            {
                SendDebugInfo("QueryDB 查询数据库2失败");
                return;
            }

            DataTable dt = ds.Tables[0];
            DG_DB_M2.DataSource = dt;

            for (int i = 0; i < DG_DB_M2.ColumnCount; i++)
            {
                if (DG_DB_M2.Columns[i].ValueType == typeof(byte[]))
                {
                    DG_DB_M2.Columns[i].Visible = false;
                    continue;
                }
                CB_ColumnM2.Items.Add(DG_DB_M2.Columns[i].Name);
            }
            CB_ColumnM2.SelectedIndex = 0;
        }

        private void DisDBM3()
        {
            DataSet ds;
            string userName = m_UserManagerHandle.m_CurrenUser;
            string com = "select * from " + m_TableName + "where UserName = " + userName;

            if (userName == "Admin")
            {
                com = com = "select * from " + m_TableName;
            }
            bool ret = m_DBM3Handle.QueryInDB(com, out ds);
            if (!ret)
            {
                SendDebugInfo("QueryDB 查询数据库3失败");
                return;
            }

            DataTable dt = ds.Tables[0];
            DG_DB_M3.DataSource = dt;

            for (int i = 0; i < DG_DB_M3.ColumnCount; i++)
            {
                if (DG_DB_M3.Columns[i].ValueType == typeof(byte[]))
                {
                    DG_DB_M3.Columns[i].Visible = false;
                    continue;
                }
                CB_ColumnM3.Items.Add(DG_DB_M3.Columns[i].Name);
            }
            CB_ColumnM3.SelectedIndex = 0;
        }

        private void DisDBM4()
        {
            DataSet ds;
            string userName = m_UserManagerHandle.m_CurrenUser;
            string com = "select * from " + m_TableName + "where UserName = " + userName;

            if (userName == "Admin")
            {
                com = com = "select * from " + m_TableName;
            }
            bool ret = m_DBM4Handle.QueryInDB(com, out ds);
            if (!ret)
            {
                SendDebugInfo("QueryDB 查询数据库4失败");
                return;
            }

            DataTable dt = ds.Tables[0];
            DG_DB_M4.DataSource = dt;

            for (int i = 0; i < DG_DB_M4.ColumnCount; i++)
            {
                if (DG_DB_M4.Columns[i].ValueType == typeof(byte[]))
                {
                    DG_DB_M4.Columns[i].Visible = false;
                    continue;
                }
                CB_ColumnM4.Items.Add(DG_DB_M4.Columns[i].Name);
            }
            CB_ColumnM4.SelectedIndex = 0;
        }
        #endregion

        #region 界面显示初始化
        private void InitFormDis()
        {
            if (m_MachineNum == 1)
            {
                InitFormDisM1();
            }

            if (m_MachineNum == 2)
            {
                InitFormDisM1();
                InitFormDisM2();
            }

            if (m_MachineNum == 3)
            {
                InitFormDisM1();
                InitFormDisM2();
                InitFormDisM3();
            }

            if (m_MachineNum == 4)
            {
                InitFormDisM1();
                InitFormDisM2();
                InitFormDisM3();
                InitFormDisM4();
            }
        }

        private void InitFormDisM1()
        {
            CB_TypeM1.Items.Add(">");
            CB_TypeM1.Items.Add(">=");
            CB_TypeM1.Items.Add("==");
            CB_TypeM1.Items.Add("<=");
            CB_TypeM1.Items.Add("<");
            CB_TypeM1.SelectedIndex = 0;
        }

        private void InitFormDisM2()
        {
            CB_TypeM2.Items.Add(">");
            CB_TypeM2.Items.Add(">=");
            CB_TypeM2.Items.Add("==");
            CB_TypeM2.Items.Add("<=");
            CB_TypeM2.Items.Add("<");
            CB_TypeM2.SelectedIndex = 0;
        }

        private void InitFormDisM3()
        {
            CB_TypeM3.Items.Add(">");
            CB_TypeM3.Items.Add(">=");
            CB_TypeM3.Items.Add("==");
            CB_TypeM3.Items.Add("<=");
            CB_TypeM3.Items.Add("<");
            CB_TypeM3.SelectedIndex = 0;
        }

        private void InitFormDisM4()
        {
            CB_TypeM4.Items.Add(">");
            CB_TypeM4.Items.Add(">=");
            CB_TypeM4.Items.Add("==");
            CB_TypeM4.Items.Add("<=");
            CB_TypeM4.Items.Add("<");
            CB_TypeM4.SelectedIndex = 0;
        }
        #endregion

        #region 曲线初始化
        private void CurveInit()
        {
            string sDisType = ContentValue("CurveInfo", "DisType", m_INICurveInfoFilePath);
            string sCurveMethod = ContentValue("CurveInfo", "CurveMethod", m_INICurveInfoFilePath);
            string sAlgorithm = ContentValue("CurveInfo", "Algorithm", m_INICurveInfoFilePath);
            string sRelatedPara = ContentValue("CurveInfo", "RelatedPara", m_INICurveInfoFilePath);
            string sDrawTime = ContentValue("CurveInfo", "DrawTime", m_INICurveInfoFilePath);
            string sSampleTime = ContentValue("CurveInfo", "SampleTime", m_INICurveInfoFilePath);
            string syMax = ContentValue("CurveInfo", "yMax", m_INICurveInfoFilePath);
            string sxMax = ContentValue("CurveInfo", "xMax", m_INICurveInfoFilePath);
            string sInterval = ContentValue("CurveInfo", "Interval", m_INICurveInfoFilePath);
            string sGridWidth = ContentValue("CurveInfo", "GridWidth", m_INICurveInfoFilePath);
            string sGridHeight = ContentValue("CurveInfo", "GridHeigh", m_INICurveInfoFilePath);
            string sColor = ContentValue("CurveInfo", "Color", m_INICurveInfoFilePath);
            string sLineWidth = ContentValue("CurveInfo", "LineWidth", m_INICurveInfoFilePath);

            m_DrawCurveBench1Handle = new DrawCurveDLL.DrawCurveLib();
            m_DrawCurveBench2Handle = new DrawCurveDLL.DrawCurveLib();
            m_DrawCurveBench3Handle = new DrawCurveDLL.DrawCurveLib();
            m_DrawCurveBench4Handle = new DrawCurveDLL.DrawCurveLib();

            m_DrawCurveBench1Handle.LibInit("Compress");
            m_DrawCurveBench2Handle.LibInit("Compress");
            m_DrawCurveBench3Handle.LibInit("Compress");
            m_DrawCurveBench4Handle.LibInit("Compress");

            //绘图方式
            m_DrawCurveBench1Handle.SetDrawInitMode(sCurveMethod);
            m_DrawCurveBench2Handle.SetDrawInitMode(sCurveMethod);
            m_DrawCurveBench3Handle.SetDrawInitMode(sCurveMethod);
            m_DrawCurveBench4Handle.SetDrawInitMode(sCurveMethod);

            //设置参数
            if (sCurveMethod == "GDI")
            {
                m_DrawCurveBench1Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench1Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench1Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench1Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench1Handle.SetCurvePara("LineColor", sColor);

                m_DrawCurveBench2Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench2Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench2Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench2Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench2Handle.SetCurvePara("LineColor", sColor);

                m_DrawCurveBench3Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench3Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench3Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench3Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench3Handle.SetCurvePara("LineColor", sColor);

                m_DrawCurveBench4Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench4Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench4Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench4Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench4Handle.SetCurvePara("LineColor", sColor);

                panel_W1.Visible = true;
                panel_W2.Visible = true;
                panel_W3.Visible = true;
                panel_W4.Visible = true;
            }
            if (sCurveMethod == "OpenGL")
            {
                Point startP = panel_W1.PointToScreen(panel_W1.Location);
                int startX = startP.X;
                int startY = startP.Y;
                Point endP = default(Point);
                endP.X = startX + panel_W1.Width;
                endP.Y = startY + panel_W1.Height;
                m_DrawCurveBench1Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench1Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench1Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench1Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench1Handle.SetCurvePara("StartPoint_X", startX.ToString());
                m_DrawCurveBench1Handle.SetCurvePara("StartPoint_Y", startY.ToString());
                m_DrawCurveBench1Handle.SetCurvePara("EndPoint_X", endP.X.ToString());
                m_DrawCurveBench1Handle.SetCurvePara("EndPoint_Y", endP.Y.ToString());

                startP = panel_W2.PointToScreen(panel_W2.Location);
                startX = startP.X;
                startY = startP.Y;
                endP = default(Point);
                endP.X = startX + panel_W2.Width;
                endP.Y = startY + panel_W2.Height;
                m_DrawCurveBench2Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench2Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench2Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench2Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench2Handle.SetCurvePara("StartPoint_X", startX.ToString());
                m_DrawCurveBench2Handle.SetCurvePara("StartPoint_Y", startY.ToString());
                m_DrawCurveBench2Handle.SetCurvePara("EndPoint_X", endP.X.ToString());
                m_DrawCurveBench2Handle.SetCurvePara("EndPoint_Y", endP.Y.ToString());

                startP = panel_W3.PointToScreen(panel_W3.Location);
                startX = startP.X;
                startY = startP.Y;
                endP = default(Point);
                endP.X = startX + panel_W3.Width;
                endP.Y = startY + panel_W3.Height;
                m_DrawCurveBench3Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench3Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench3Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench3Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench3Handle.SetCurvePara("StartPoint_X", startX.ToString());
                m_DrawCurveBench3Handle.SetCurvePara("StartPoint_Y", startY.ToString());
                m_DrawCurveBench3Handle.SetCurvePara("EndPoint_X", endP.X.ToString());
                m_DrawCurveBench3Handle.SetCurvePara("EndPoint_Y", endP.Y.ToString());

                startP = panel_W4.PointToScreen(panel_W4.Location);
                startX = startP.X;
                startY = startP.Y;
                endP = default(Point);
                endP.X = startX + panel_W4.Width;
                endP.Y = startY + panel_W4.Height;
                m_DrawCurveBench4Handle.SetCurvePara("CurveType", sAlgorithm);
                m_DrawCurveBench4Handle.SetCurvePara("GridWidth", sGridWidth);
                m_DrawCurveBench4Handle.SetCurvePara("GridHeight", sGridHeight);
                m_DrawCurveBench4Handle.SetCurvePara("LineWidth", sLineWidth);
                m_DrawCurveBench4Handle.SetCurvePara("StartPoint_X", startX.ToString());
                m_DrawCurveBench4Handle.SetCurvePara("StartPoint_Y", startY.ToString());
                m_DrawCurveBench4Handle.SetCurvePara("EndPoint_X", endP.X.ToString());
                m_DrawCurveBench4Handle.SetCurvePara("EndPoint_Y", endP.Y.ToString());

                panel_W1.Visible = false;
                panel_W2.Visible = false;
                panel_W3.Visible = false;
                panel_W4.Visible = false;
            }

            //第三步初始化
            m_DrawCurveBench1Handle.DrawCurveDLLInit(panel_W1);
            m_DrawCurveBench1Handle.DrawCurveDLLInit(panel_W2);
            m_DrawCurveBench1Handle.DrawCurveDLLInit(panel_W3);
            m_DrawCurveBench1Handle.DrawCurveDLLInit(panel_W4);

            switch (sColor)
            {
                case "红色":
                    m_LineColor = Color.Red;
                    break;
                case "黄色":
                    m_LineColor = Color.Yellow;
                    break;
                case "蓝色":
                    m_LineColor = Color.Blue;
                    break;
                case "绿色":
                    m_LineColor = Color.Green;
                    break;
                case "橙色":
                    m_LineColor = Color.Orange;
                    break;
                case "黑色":
                    m_LineColor = Color.Black;
                    break;
                case "紫色":
                    m_LineColor = Color.Violet;
                    break;
                default:
                    m_LineColor = Color.Red;
                    break;
            }
        }
        #endregion

        #region 表报初始化
        private void ExcelInit()
        {
            m_M1ExcelHandle = new ExecelLibInterface();
            m_M2ExcelHandle = new ExecelLibInterface();
            m_M3ExcelHandle = new ExecelLibInterface();
            m_M4ExcelHandle = new ExecelLibInterface();

            string sheetName = ContentValue("QueryDB", "SheetName", m_INIQueryDBFilePath);

            int ret = m_M1ExcelHandle.Init(m_M1ExcelModuleFilePath, sheetName);
            if (ret != 1)
            {
                SendDebugInfo("QueryDB 数据库1初始化失败");
            }
            ret = m_M2ExcelHandle.Init(m_M2ExcelModuleFilePath, sheetName);
            if (ret != 1)
            {
                SendDebugInfo("QueryDB 数据库2初始化失败");
            }
            ret = m_M3ExcelHandle.Init(m_M3ExcelModuleFilePath, sheetName);
            if (ret != 1)
            {
                SendDebugInfo("QueryDB 数据库3初始化失败");
            }
            ret = m_M4ExcelHandle.Init(m_M4ExcelModuleFilePath, sheetName);
            if (ret != 1)
            {
                SendDebugInfo("QueryDB 数据库4初始化失败");
            }

        }
        #endregion

        #region 曲线放大界面配置
        private void CurveZoomInit()
        {
            string s_isZoom = ContentValue("QueryDB", "Zoom", m_INIQueryDBFilePath);
            if (s_isZoom == "True")
            {
                Grp_Zoom_W1.Visible = true;
                Grp_Zoom_W2.Visible = true;
                Grp_Zoom_W3.Visible = true;
                Grp_Zoom_W4.Visible = true;
            }
            if (s_isZoom == "False")
            {
                Grp_Zoom_W1.Visible = false;
                Grp_Zoom_W2.Visible = false;
                Grp_Zoom_W3.Visible = false;
                Grp_Zoom_W4.Visible = false;
            }
        }
        #endregion
        
        
        #region 公共函数
        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void UpdateMainForm(string s)
        {
            if (ShowPageInfo != null)
            {
                ShowPageInfo(s);
            }
        }

        private void SavePic()
        {
            int width = 1180;
            int height = 315;
            Bitmap image = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(image);
            Point p1 = default(Point);
            Point p2 = default(Point);
            p1.X = 0;
            p1.Y = 0;
            p2.X = this.Left + 23;
            p2.Y = this.Top + 580;

            g.CopyFromScreen(p2, p1, image.Size);

            string FileName = m_PicFilePath;
            image.Save(FileName);
        }

        private void SendDebugInfo(string s)
        {
            if (ShowDebugInfo != null)
            {
                ShowDebugInfo(s);
            }
        }
        #endregion

        #region 从数据库读取曲线

        private void DG_DB_M1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string isSave = ContentValue("CurveInfo", "SaveM1", m_INICurveInfoFilePath);
            if (isSave == "False")
            {
                return;
            }
            string sRecordId = DG_DB_M1.Rows[e.RowIndex].Cells[0].Value.ToString();
            int recordId = Convert.ToInt32(sRecordId);

            string regName = ContentValue("CurveInfo", "RegNameM1", m_INICurveInfoFilePath);

            float[] pt_x;
            float[] pt_y;

            int index = -1;
            bool code = m_DBM1Handle.GetIndexInDB(m_TableName, recordId, ref index);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：index 库1");
                //MessageBox.Show("数据查询失败，定位：index", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM1Handle.ReadFloatsInDB(m_TableName, index, regName + "_X", out pt_x);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x 库1");
                //MessageBox.Show("数据查询失败，定位：pt_x", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM1Handle.ReadFloatsInDB(m_TableName, index, regName + "_Y", out pt_y);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_y 库1");
                //MessageBox.Show("数据查询失败，定位：pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pt_x.Length != pt_y.Length)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x != pt_y 库1");
                //MessageBox.Show("数据查询失败，定位：pt_x != pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PointF[] pt = new PointF[pt_x.Length];
            int len = pt.Length;
            for (int i = 0; i < len; i++)
            {
                pt[i].X = pt_x[i];
                pt[i].Y = pt_y[i];
            }

            //画图
            m_DrawCurveBench1Handle.ClearPointsInList();
            m_DrawCurveBench1Handle.SavePointToList(pt);
            m_DrawCurveBench1Handle.DrawCurve(panel_W1, m_LineColor, panel_W1.Width, panel_W1.Height);

            //刷新坐标指示值
            object oStartTime = "";
            object oEndTime = "";

            code = m_DBM1Handle.ReadSigleDataInDB(m_TableName, index, "StartTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：StartTime 库1");
                MessageBox.Show("数据查询失败，定位：StartTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM1Handle.ReadSigleDataInDB(m_TableName, index, "EndTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：EndTime 库1");
                MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime startTime = Convert.ToDateTime(oStartTime);
            DateTime endTime = Convert.ToDateTime(oEndTime);
            m_M1BaseStartTime = startTime;
            m_M1BaseEndTime = endTime;
            m_ZoomStartTimeM1 = startTime;
            m_ZoomEndTimeM1 = endTime;
            m_ZoomyMaxM1 = m_M1yMax;
            UpdateLabelTimeM1(startTime, endTime, m_M1yMax);

            //读取数据库数据到buffer中，
            UpdateBufferM1(index);

            //保存图片
            SavePic();

        }

        private void UpdateLabelTimeM1(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label1.Text = yMax.ToString() + "MPa";
            label7.Text = "0";
            int interVal = (int)yMax / 6;
            label6.Text = interVal.ToString() + "MPa";
            label5.Text = (2 * interVal).ToString() + "MPa";
            label4.Text = (3 * interVal).ToString() + "MPa";
            label3.Text = (4 * interVal).ToString() + "MPa";
            label2.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval =(float) (EndTime - StartTime).TotalSeconds;
            label10.Text = StartTime.ToLongDateString();
            int H = 0;
            int M = 0;
            int S = 0;
            GetLableTimeBaseCompress((int)timeInterval / 8, StartTime, ref H, ref M, ref S);
            label11.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 2 / 8, StartTime, ref H, ref M, ref S);
            label12.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 3 / 8, StartTime, ref H, ref M, ref S);
            label13.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 4 / 8, StartTime, ref H, ref M, ref S);
            label14.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 5 / 8, StartTime, ref H, ref M, ref S);
            label15.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 6 / 8, StartTime, ref H, ref M, ref S);
            label16.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 7 / 8, StartTime, ref H, ref M, ref S);
            label17.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 8 / 8, StartTime, ref H, ref M, ref S);
            label18.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
        }

        private void UpdateBufferM1(int index)
        {
            int count = m_ReportListM1.Count;
            for (int i = 0; i < count; i++)
            {
                object oValue = default(object);
                bool code = m_DBM1Handle.ReadSigleDataInDB(m_TableName, index, m_ReportListM1[i].m_RegName, ref oValue);
                if (!code)
                {
                    SendDebugInfo("QueryDB 数据查询失败，定位：" + m_ReportListM1[i].m_RegName + " 库1");
                    //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                m_ReportListM1[i].m_Value = oValue.ToString();
            }
        }

        private void GetLableTimeBaseCompress(int t, DateTime StartTime, ref int H, ref int M, ref int S)
        {
            H = (int)(t / 3600);
            M = (int)((t - H * 3600) / 60);
            S = (int)((t - H * 3600) % 60);

            S += StartTime.Second;
            if (S > 59)
            {
                M++;
                S -= 60;
            }
            M += StartTime.Minute;
            if (M > 59)
            {
                H++;
                M -= 60;
            }
            H += StartTime.Hour;
            if (H > 23)
            {
                H = H - 24;
            }
        }

        private void DG_DB_M2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string isSave = ContentValue("CurveInfo", "SaveM2", m_INICurveInfoFilePath);
            if (isSave == "False")
            {
                return;
            }
            string sRecordId = DG_DB_M2.Rows[e.RowIndex].Cells[0].Value.ToString();
            int recordId = Convert.ToInt32(sRecordId);

            string regName = ContentValue("CurveInfo", "RegNameM2", m_INICurveInfoFilePath);

            float[] pt_x;
            float[] pt_y;

            int index = -1;
            bool code = m_DBM2Handle.GetIndexInDB(m_TableName, recordId, ref index);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：index  库2");
                //MessageBox.Show("数据查询失败，定位：index", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM2Handle.ReadFloatsInDB(m_TableName, index, regName + "_X", out pt_x);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x  库2");
                //MessageBox.Show("数据查询失败，定位：pt_x", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM2Handle.ReadFloatsInDB(m_TableName, index, regName + "_Y", out pt_y);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_y  库2");
                //MessageBox.Show("数据查询失败，定位：pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pt_x.Length != pt_y.Length)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x != pt_y  库2");
                //MessageBox.Show("数据查询失败，定位：pt_x != pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PointF[] pt = new PointF[pt_x.Length];
            int len = pt.Length;
            for (int i = 0; i < len; i++)
            {
                pt[i].X = pt_x[i];
                pt[i].Y = pt_y[i];
            }

            //画图
            m_DrawCurveBench1Handle.ClearPointsInList();
            m_DrawCurveBench2Handle.SavePointToList(pt);
            m_DrawCurveBench2Handle.DrawCurve(panel_W2, m_LineColor, panel_W2.Width, panel_W2.Height);

            //刷新坐标指示值
            object oStartTime = "";
            object oEndTime = "";

            code = m_DBM2Handle.ReadSigleDataInDB(m_TableName, index, "StartTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：StartTime  库2");
                //MessageBox.Show("数据查询失败，定位：StartTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM2Handle.ReadSigleDataInDB(m_TableName, index, "EndTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：EndTime  库2");
                //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime startTime = Convert.ToDateTime(oStartTime);
            DateTime endTime = Convert.ToDateTime(oEndTime);
            m_M2BaseStartTime = startTime;
            m_M2BaseEndTime = endTime;
            m_ZoomStartTimeM2 = startTime;
            m_ZoomEndTimeM2 = endTime;
            m_ZoomyMaxM2 = m_M2yMax;
            UpdateLabelTimeM2(startTime, endTime, m_M2yMax);

            //读取数据库数据到buffer中，
            UpdateBufferM2(index);

            //保存图片
            SavePic();
        }

        private void UpdateLabelTimeM2(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label36.Text = yMax.ToString() + "MPa";
            label30.Text = "0";
            int interVal = (int)yMax / 6;
            label31.Text = interVal.ToString() + "MPa";
            label32.Text = (2 * interVal).ToString() + "MPa";
            label33.Text = (3 * interVal).ToString() + "MPa";
            label34.Text = (4 * interVal).ToString() + "MPa";
            label35.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label29.Text = StartTime.ToLongDateString();
            int H = 0;
            int M = 0;
            int S = 0;
            GetLableTimeBaseCompress((int)timeInterval / 8, StartTime, ref H, ref M, ref S);
            label28.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 2 / 8, StartTime, ref H, ref M, ref S);
            label27.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 3 / 8, StartTime, ref H, ref M, ref S);
            label26.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 4 / 8, StartTime, ref H, ref M, ref S);
            label25.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 5 / 8, StartTime, ref H, ref M, ref S);
            label24.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 6 / 8, StartTime, ref H, ref M, ref S);
            label23.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 7 / 8, StartTime, ref H, ref M, ref S);
            label22.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 8 / 8, StartTime, ref H, ref M, ref S);
            label21.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
        }

        private void UpdateBufferM2(int index)
        {
            int count = m_ReportListM2.Count;
            for (int i = 0; i < count; i++)
            {
                object oValue = default(object);
                bool code = m_DBM2Handle.ReadSigleDataInDB(m_TableName, index, m_ReportListM2[i].m_RegName, ref oValue);
                if (!code)
                {
                    SendDebugInfo("QueryDB 数据查询失败，定位：" + m_ReportListM1[i].m_RegName + " 库2");
                    //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                m_ReportListM2[i].m_Value = oValue.ToString();
            }
        }

        private void DG_DB_M3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string isSave = ContentValue("CurveInfo", "SaveM3", m_INICurveInfoFilePath);
            if (isSave == "False")
            {
                return;
            }
            string sRecordId = DG_DB_M3.Rows[e.RowIndex].Cells[0].Value.ToString();
            int recordId = Convert.ToInt32(sRecordId);

            string regName = ContentValue("CurveInfo", "RegNameM3", m_INICurveInfoFilePath);

            float[] pt_x;
            float[] pt_y;

            int index = -1;
            bool code = m_DBM3Handle.GetIndexInDB(m_TableName, recordId, ref index);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：index 库3");
                //MessageBox.Show("数据查询失败，定位：index", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM3Handle.ReadFloatsInDB(m_TableName, index, regName + "_X", out pt_x);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x  库3");
                //MessageBox.Show("数据查询失败，定位：pt_x", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM3Handle.ReadFloatsInDB(m_TableName, index, regName + "_Y", out pt_y);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_y  库3");
                //MessageBox.Show("数据查询失败，定位：pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pt_x.Length != pt_y.Length)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x != pt_y 库3");
                //MessageBox.Show("数据查询失败，定位：pt_x != pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PointF[] pt = new PointF[pt_x.Length];
            int len = pt.Length;
            for (int i = 0; i < len; i++)
            {
                pt[i].X = pt_x[i];
                pt[i].Y = pt_y[i];
            }

            //画图
            m_DrawCurveBench3Handle.ClearPointsInList();
            m_DrawCurveBench3Handle.SavePointToList(pt);
            m_DrawCurveBench3Handle.DrawCurve(panel_W3, m_LineColor, panel_W3.Width, panel_W3.Height);

            //刷新坐标指示值
            object oStartTime = "";
            object oEndTime = "";

            code = m_DBM3Handle.ReadSigleDataInDB(m_TableName, index, "StartTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：StartTime 库3");
                //MessageBox.Show("数据查询失败，定位：StartTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM3Handle.ReadSigleDataInDB(m_TableName, index, "EndTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：EndTime 库3");
                //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime startTime = Convert.ToDateTime(oStartTime);
            DateTime endTime = Convert.ToDateTime(oEndTime);
            m_M3BaseStartTime = startTime;
            m_M3BaseEndTime = endTime;
            m_ZoomStartTimeM3 = startTime;
            m_ZoomEndTimeM3 = endTime;
            m_ZoomyMaxM3 = m_M3yMax;
            UpdateLabelTimeM3(startTime, endTime, m_M3yMax);

            //读取数据库数据到buffer中，
            UpdateBufferM3(index);

            //保存图片
            SavePic();
        }

        private void UpdateLabelTimeM3(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label54.Text = yMax.ToString() + "MPa";
            label48.Text = "0";
            int interVal = (int)yMax / 6;
            label49.Text = interVal.ToString() + "MPa";
            label50.Text = (2 * interVal).ToString() + "MPa";
            label51.Text = (3 * interVal).ToString() + "MPa";
            label52.Text = (4 * interVal).ToString() + "MPa";
            label53.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label47.Text = StartTime.ToLongDateString();
            int H = 0;
            int M = 0;
            int S = 0;
            GetLableTimeBaseCompress((int)timeInterval / 8, StartTime, ref H, ref M, ref S);
            label46.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 2 / 8, StartTime, ref H, ref M, ref S);
            label45.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 3 / 8, StartTime, ref H, ref M, ref S);
            label44.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 4 / 8, StartTime, ref H, ref M, ref S);
            label43.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 5 / 8, StartTime, ref H, ref M, ref S);
            label42.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 6 / 8, StartTime, ref H, ref M, ref S);
            label41.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 7 / 8, StartTime, ref H, ref M, ref S);
            label40.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 8 / 8, StartTime, ref H, ref M, ref S);
            label39.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
        }

        private void UpdateBufferM3(int index)
        {
            int count = m_ReportListM3.Count;
            for (int i = 0; i < count; i++)
            {
                object oValue = default(object);
                bool code = m_DBM3Handle.ReadSigleDataInDB(m_TableName, index, m_ReportListM3[i].m_RegName, ref oValue);
                if (!code)
                {
                    SendDebugInfo("QueryDB 数据查询失败，定位：" + m_ReportListM1[i].m_RegName + " 库3");
                    //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                m_ReportListM3[i].m_Value = oValue.ToString();
            }
        }

        private void DG_DB_M4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string isSave = ContentValue("CurveInfo", "SaveM4", m_INICurveInfoFilePath);
            if (isSave == "False")
            {
                return;
            }
            string sRecordId = DG_DB_M4.Rows[e.RowIndex].Cells[0].Value.ToString();
            int recordId = Convert.ToInt32(sRecordId);

            string regName = ContentValue("CurveInfo", "RegNameM4", m_INICurveInfoFilePath);

            float[] pt_x;
            float[] pt_y;

            int index = -1;
            bool code = m_DBM4Handle.GetIndexInDB(m_TableName, recordId, ref index);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：index 库4");
                //MessageBox.Show("数据查询失败，定位：index", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM4Handle.ReadFloatsInDB(m_TableName, index, regName + "_X", out pt_x);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x 库4");
                //MessageBox.Show("数据查询失败，定位：pt_x", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM4Handle.ReadFloatsInDB(m_TableName, index, regName + "_Y", out pt_y);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_y 库4");
                //MessageBox.Show("数据查询失败，定位：pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pt_x.Length != pt_y.Length)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：pt_x != pt_y 库4");
                //MessageBox.Show("数据查询失败，定位：pt_x != pt_y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PointF[] pt = new PointF[pt_x.Length];
            int len = pt.Length;
            for (int i = 0; i < len; i++)
            {
                pt[i].X = pt_x[i];
                pt[i].Y = pt_y[i];
            }

            //画图
            m_DrawCurveBench4Handle.ClearPointsInList();
            m_DrawCurveBench4Handle.SavePointToList(pt);
            m_DrawCurveBench4Handle.DrawCurve(panel_W4, m_LineColor, panel_W4.Width, panel_W4.Height);

            //刷新坐标指示值
            object oStartTime = "";
            object oEndTime = "";

            code = m_DBM4Handle.ReadSigleDataInDB(m_TableName, index, "StartTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：StartTime 库4");
                //MessageBox.Show("数据查询失败，定位：StartTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            code = m_DBM4Handle.ReadSigleDataInDB(m_TableName, index, "EndTime", ref oStartTime);
            if (!code)
            {
                SendDebugInfo("QueryDB 数据查询失败，定位：EndTime 库4");
                //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime startTime = Convert.ToDateTime(oStartTime);
            DateTime endTime = Convert.ToDateTime(oEndTime);
            m_M4BaseStartTime = startTime;
            m_M4BaseEndTime = endTime;
            m_ZoomStartTimeM4 = startTime;
            m_ZoomEndTimeM4 = endTime;
            m_ZoomyMaxM4 = m_M4yMax;
            UpdateLabelTimeM4(startTime, endTime, m_M4yMax);

            //读取数据库数据到buffer中，
            UpdateBufferM4(index);

            //保存图片
            SavePic();
        }

        private void UpdateLabelTimeM4(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label72.Text = yMax.ToString() + "MPa";
            label66.Text = "0";
            int interVal = (int)yMax / 6;
            label67.Text = interVal.ToString() + "MPa";
            label68.Text = (2 * interVal).ToString() + "MPa";
            label69.Text = (3 * interVal).ToString() + "MPa";
            label70.Text = (4 * interVal).ToString() + "MPa";
            label71.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label65.Text = StartTime.ToLongDateString();
            int H = 0;
            int M = 0;
            int S = 0;
            GetLableTimeBaseCompress((int)timeInterval / 8, StartTime, ref H, ref M, ref S);
            label64.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 2 / 8, StartTime, ref H, ref M, ref S);
            label63.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 3 / 8, StartTime, ref H, ref M, ref S);
            label62.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 4 / 8, StartTime, ref H, ref M, ref S);
            label61.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 5 / 8, StartTime, ref H, ref M, ref S);
            label60.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 6 / 8, StartTime, ref H, ref M, ref S);
            label59.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 7 / 8, StartTime, ref H, ref M, ref S);
            label58.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            GetLableTimeBaseCompress((int)timeInterval * 8 / 8, StartTime, ref H, ref M, ref S);
            label57.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
        }

        private void UpdateBufferM4(int index)
        {
            int count = m_ReportListM4.Count;
            for (int i = 0; i < count; i++)
            {
                object oValue = default(object);
                bool code = m_DBM4Handle.ReadSigleDataInDB(m_TableName, index, m_ReportListM4[i].m_RegName, ref oValue);
                if (!code)
                {
                    SendDebugInfo("QueryDB 数据查询失败，定位：" + m_ReportListM1[i].m_RegName + " 库4");
                    //MessageBox.Show("数据查询失败，定位：EndTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                m_ReportListM4[i].m_Value = oValue.ToString();
            }
        }

        #endregion

        #region 放大
        private void TB_ZoomM1_Click(object sender, EventArgs e)
        {
            ZoomForm Handle = new ZoomForm(this, "M1");
            Handle.ShowDialog();

            float xStart = (float)(m_ZoomStartTimeM1 - m_M1BaseStartTime).TotalSeconds;
            float xEnd = (float)(m_ZoomEndTimeM1 - m_M1BaseEndTime).TotalSeconds;
            m_DrawCurveBench1Handle.ZoomOutBaseXY(panel_W1, xStart, 0, xEnd, m_ZoomyMaxM1);
            UpdateLabelTimeM1(m_ZoomStartTimeM1, m_ZoomEndTimeM1, m_ZoomyMaxM1);
        }

        private void TB_ZoomM2_Click(object sender, EventArgs e)
        {
            ZoomForm Handle = new ZoomForm(this, "M2");
            Handle.ShowDialog();

            float xStart = (float)(m_ZoomStartTimeM2 - m_M2BaseStartTime).TotalSeconds;
            float xEnd = (float)(m_ZoomEndTimeM2 - m_M2BaseEndTime).TotalSeconds;
            m_DrawCurveBench2Handle.ZoomOutBaseXY(panel_W2, xStart, 0, xEnd, m_ZoomyMaxM2);
            UpdateLabelTimeM2(m_ZoomStartTimeM2, m_ZoomEndTimeM2, m_ZoomyMaxM2);
        }

        private void TB_ZoomM3_Click(object sender, EventArgs e)
        {
            ZoomForm Handle = new ZoomForm(this, "M3");
            Handle.ShowDialog();

            float xStart = (float)(m_ZoomStartTimeM3 - m_M3BaseStartTime).TotalSeconds;
            float xEnd = (float)(m_ZoomEndTimeM3 - m_M3BaseEndTime).TotalSeconds;
            m_DrawCurveBench3Handle.ZoomOutBaseXY(panel_W3, xStart, 0, xEnd, m_ZoomyMaxM3);
            UpdateLabelTimeM3(m_ZoomStartTimeM3, m_ZoomEndTimeM3, m_ZoomyMaxM3);
        }

        private void TB_ZoomM4_Click(object sender, EventArgs e)
        {
            ZoomForm Handle = new ZoomForm(this, "M4");
            Handle.ShowDialog();

            float xStart = (float)(m_ZoomStartTimeM4 - m_M4BaseStartTime).TotalSeconds;
            float xEnd = (float)(m_ZoomEndTimeM4 - m_M4BaseEndTime).TotalSeconds;
            m_DrawCurveBench4Handle.ZoomOutBaseXY(panel_W4, xStart, 0, xEnd, m_ZoomyMaxM4);
            UpdateLabelTimeM4(m_ZoomStartTimeM4, m_ZoomEndTimeM4, m_ZoomyMaxM4);
        }

        #endregion

        #region 报表
        private void TB_Create_M1_Click(object sender, EventArgs e)
        {
            string curveStartPos = ContentValue("QueryDB", "CurveStartPos", m_INIQueryDBFilePath);
            string curveEndPos = ContentValue("QueryDB", "CurveEndPos", m_INIQueryDBFilePath);

            m_M1ExcelHandle.OpenFile();
            int count = m_ReportListM1.Count;
            for (int i = 0; i < count; i++)
            {
                int ret = m_M1ExcelHandle.WriteData(m_ReportListM1[i].m_Position, m_ReportListM1[i].m_Value);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 写入数据失败，定位:" + m_ReportListM1[i].m_Position.ToString() + "库1");
                }
            }

            //插入图片
            m_M1ExcelHandle.InsertPic(curveStartPos, curveEndPos, m_PicFilePath);
            string fileName = m_M1ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            m_M1ExcelHandle.SaveAs(fileName);

            string pdfFileName = m_M1ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            if (m_isCreatePDF)
            {
                int ret = m_M1ExcelHandle.ExcelToPDF(fileName, pdfFileName);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 创建报表失败。库1");
                }
                System.Diagnostics.Process.Start(pdfFileName);
            }
            else
            {
                System.Diagnostics.Process.Start(fileName);
            }
        }

        private void TB_Create_M2_Click(object sender, EventArgs e)
        {
            string curveStartPos = ContentValue("QueryDB", "CurveStartPos", m_INIQueryDBFilePath);
            string curveEndPos = ContentValue("QueryDB", "CurveEndPos", m_INIQueryDBFilePath);

            m_M2ExcelHandle.OpenFile();
            int count = m_ReportListM2.Count;
            for (int i = 0; i < count; i++)
            {
                int ret = m_M2ExcelHandle.WriteData(m_ReportListM2[i].m_Position, m_ReportListM2[i].m_Value);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 写入数据失败，定位:" + m_ReportListM2[i].m_Position.ToString() + "库2");
                }
            }

            //插入图片
            m_M2ExcelHandle.InsertPic(curveStartPos, curveEndPos, m_PicFilePath);
            string fileName = m_M2ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            m_M2ExcelHandle.SaveAs(fileName);

            string pdfFileName = m_M2ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            if (m_isCreatePDF)
            {
                int ret = m_M2ExcelHandle.ExcelToPDF(fileName, pdfFileName);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 创建报表失败。库2");
                }
                System.Diagnostics.Process.Start(pdfFileName);
            }
            else
            {
                System.Diagnostics.Process.Start(fileName);
            }

        }

        private void TB_Create_M3_Click(object sender, EventArgs e)
        {
            string curveStartPos = ContentValue("QueryDB", "CurveStartPos", m_INIQueryDBFilePath);
            string curveEndPos = ContentValue("QueryDB", "CurveEndPos", m_INIQueryDBFilePath);

            m_M3ExcelHandle.OpenFile();
            int count = m_ReportListM3.Count;
            for (int i = 0; i < count; i++)
            {
                int ret = m_M3ExcelHandle.WriteData(m_ReportListM3[i].m_Position, m_ReportListM3[i].m_Value);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 写入数据失败，定位:" + m_ReportListM3[i].m_Position.ToString() + "库3");
                }
            }

            //插入图片
            m_M3ExcelHandle.InsertPic(curveStartPos, curveEndPos, m_PicFilePath);
            string fileName = m_M3ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            m_M3ExcelHandle.SaveAs(fileName);

            string pdfFileName = m_M3ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            if (m_isCreatePDF)
            {
                int ret = m_M3ExcelHandle.ExcelToPDF(fileName, pdfFileName);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 创建报表失败。库3");
                }
                System.Diagnostics.Process.Start(pdfFileName);
            }
            else
            {
                System.Diagnostics.Process.Start(fileName);
            }
        }

        private void TB_Create_M4_Click(object sender, EventArgs e)
        {
            string curveStartPos = ContentValue("QueryDB", "CurveStartPos", m_INIQueryDBFilePath);
            string curveEndPos = ContentValue("QueryDB", "CurveEndPos", m_INIQueryDBFilePath);

            m_M4ExcelHandle.OpenFile();
            int count = m_ReportListM4.Count;
            for (int i = 0; i < count; i++)
            {
                int ret = m_M4ExcelHandle.WriteData(m_ReportListM4[i].m_Position, m_ReportListM4[i].m_Value);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 写入数据失败，定位:" + m_ReportListM4[i].m_Position.ToString() + "库4");
                }
            }

            //插入图片
            m_M4ExcelHandle.InsertPic(curveStartPos, curveEndPos, m_PicFilePath);
            string fileName = m_M4ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            m_M4ExcelHandle.SaveAs(fileName);

            string pdfFileName = m_M4ReportFilePath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            if (m_isCreatePDF)
            {
                int ret = m_M4ExcelHandle.ExcelToPDF(fileName, pdfFileName);
                if (ret != 1)
                {
                    SendDebugInfo("QueryDB 创建报表失败。库4");
                }
                System.Diagnostics.Process.Start(pdfFileName);
            }
            else
            {
                System.Diagnostics.Process.Start(fileName);
            }


        }
        #endregion

        #region 英语
        private void ChangeUIToEnglish()
        {
            Grp_Tool01.Text = "Tool Bar";
            Grp_Tool02.Text = "Tool Bar";
            Grp_Tool03.Text = "Tool Bar";
            Grp_Tool04.Text = "Tool Bar";

            Grp_QueryM1.Text = "Query Condition";
            Grp_QueryM2.Text = "Query Condition";
            Grp_QueryM3.Text = "Query Condition";
            Grp_QueryM4.Text = "Query Condition";

            Grp_DBM1.Text = "DataBase";
            Grp_DBM2.Text = "DataBase";
            Grp_DBM3.Text = "DataBase";
            Grp_DBM4.Text = "DataBase";

            Grp_CurveM1.Text = "Curve";
            Grp_CurveM2.Text = "Curve";
            Grp_CurveM3.Text = "Curve";
            Grp_CurveM4.Text = "Curve";

            TB_Exit_M1.Text = "Exit";
            TB_Exit_M2.Text = "Exit";
            TB_Exit_M3.Text = "Exit";
            TB_Exit_M4.Text = "Exit";

            TB_Create_M1.Text = "Create Report";
            TB_Create_M2.Text = "Create Report";
            TB_Create_M3.Text = "Create Report";
            TB_Create_M4.Text = "Create Report";

            TB_ZoomM1.Text = "Zoom";
            TB_ZoomM2.Text = "Zoom";
            TB_ZoomM3.Text = "Zoom";
            TB_ZoomM4.Text = "Zoom";
        }
        #endregion

        private void TB_Exit_M1_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Exit_M2_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Exit_M3_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Exit_M4_Click(object sender, EventArgs e)
        {
            this.Close();
            UpdateMainForm("主界面");
        }

        private void RB_StandardTime_W1_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_StandardTime_W1.Checked == true)
            {
                TB_Pressure_W1.Enabled = false;
                TB_Time_W1.Enabled = false;

                UpdateLabelTimeM1(m_ZoomStartTimeM1, m_ZoomEndTimeM1, m_ZoomyMaxM1);
            }
        }

        private void RB_StandardTime_W2_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_StandardTime_W2.Checked == true)
            {
                TB_Pressure_W2.Enabled = false;
                TB_Time_W2.Enabled = false;
                UpdateLabelTimeM2(m_ZoomStartTimeM2, m_ZoomEndTimeM2, m_ZoomyMaxM2);
            }
        }

        private void RB_StandardTime_W3_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_StandardTime_W3.Checked == true)
            {
                TB_Pressure_W3.Enabled = false;
                TB_Time_W3.Enabled = false;
                UpdateLabelTimeM3(m_ZoomStartTimeM3, m_ZoomEndTimeM3, m_ZoomyMaxM3);
            }
        }

        private void RB_StandardTime_W4_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_StandardTime_W4.Checked == true)
            {
                TB_Pressure_W4.Enabled = false;
                TB_Time_W4.Enabled = false;
                UpdateLabelTimeM4(m_ZoomStartTimeM4, m_ZoomEndTimeM4, m_ZoomyMaxM4);
            }
        }

        private void RB_AbsoluteTime_W1_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_AbsoluteTime_W1.Checked == true)
            {
                TB_Pressure_W1.Enabled = true;
                TB_Time_W1.Enabled = true;

                UpdateLabelTimeM1BaseAbsoluteTime(m_ZoomStartTimeM1, m_ZoomEndTimeM1, m_ZoomyMaxM1);
            }
        }

        private void UpdateLabelTimeM1BaseAbsoluteTime(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label1.Text = yMax.ToString() + "MPa";
            label7.Text = "0";
            int interVal = (int)yMax / 6;
            label6.Text = interVal.ToString() + "MPa";
            label5.Text = (2 * interVal).ToString() + "MPa";
            label4.Text = (3 * interVal).ToString() + "MPa";
            label3.Text = (4 * interVal).ToString() + "MPa";
            label2.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label10.Text = "0s";
            
            label11.Text = (timeInterval / 8).ToString() + "s";
            label12.Text = (timeInterval * 2 / 8).ToString() + "s";
            label13.Text = (timeInterval * 3 / 8).ToString() + "s";
            label14.Text = (timeInterval * 4 / 8).ToString() + "s";
            label15.Text = (timeInterval * 5 / 8).ToString() + "s";
            label16.Text = (timeInterval * 6 / 8).ToString() + "s";
            label17.Text = (timeInterval * 7 / 8).ToString() + "s";
            label18.Text = (timeInterval * 8 / 8).ToString() + "s";
        }

        private void RB_AbsoluteTime_W2_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_AbsoluteTime_W2.Checked == true)
            {
                TB_Pressure_W2.Enabled = true;
                TB_Time_W2.Enabled = true;
                UpdateLabelTimeM2BaseAbsoluteTime(m_ZoomStartTimeM2, m_ZoomEndTimeM2, m_ZoomyMaxM2);
            }
        }

        private void UpdateLabelTimeM2BaseAbsoluteTime(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label36.Text = yMax.ToString() + "MPa";
            label30.Text = "0";
            int interVal = (int)yMax / 6;
            label31.Text = interVal.ToString() + "MPa";
            label32.Text = (2 * interVal).ToString() + "MPa";
            label33.Text = (3 * interVal).ToString() + "MPa";
            label34.Text = (4 * interVal).ToString() + "MPa";
            label35.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label29.Text = "0s";

            label28.Text = (timeInterval / 8).ToString() + "s";
            label27.Text = (timeInterval * 2 / 8).ToString() + "s";
            label26.Text = (timeInterval * 3 / 8).ToString() + "s";
            label25.Text = (timeInterval * 4 / 8).ToString() + "s";
            label24.Text = (timeInterval * 5 / 8).ToString() + "s";
            label23.Text = (timeInterval * 6 / 8).ToString() + "s";
            label22.Text = (timeInterval * 7 / 8).ToString() + "s";
            label21.Text = (timeInterval * 8 / 8).ToString() + "s";
        }

        private void RB_AbsoluteTime_W3_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_AbsoluteTime_W3.Checked == true)
            {
                TB_Pressure_W3.Enabled = true;
                TB_Time_W3.Enabled = true;

                UpdateLabelTimeM3BaseAbsoluteTime(m_ZoomStartTimeM3, m_ZoomEndTimeM3, m_ZoomyMaxM3);
            }
        }

        private void UpdateLabelTimeM3BaseAbsoluteTime(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label54.Text = yMax.ToString() + "MPa";
            label48.Text = "0";
            int interVal = (int)yMax / 6;
            label49.Text = interVal.ToString() + "MPa";
            label50.Text = (2 * interVal).ToString() + "MPa";
            label51.Text = (3 * interVal).ToString() + "MPa";
            label52.Text = (4 * interVal).ToString() + "MPa";
            label53.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label47.Text = "0s";

            label46.Text = (timeInterval / 8).ToString() + "s";
            label45.Text = (timeInterval * 2 / 8).ToString() + "s";
            label44.Text = (timeInterval * 3 / 8).ToString() + "s";
            label43.Text = (timeInterval * 4 / 8).ToString() + "s";
            label42.Text = (timeInterval * 5 / 8).ToString() + "s";
            label41.Text = (timeInterval * 6 / 8).ToString() + "s";
            label40.Text = (timeInterval * 7 / 8).ToString() + "s";
            label39.Text = (timeInterval * 8 / 8).ToString() + "s";
        }

        private void RB_AbsoluteTime_W4_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_AbsoluteTime_W4.Checked == true)
            {
                TB_Pressure_W4.Enabled = true;
                TB_Time_W4.Enabled = true;

                UpdateLabelTimeM4BaseAbsoluteTime(m_ZoomStartTimeM4, m_ZoomEndTimeM4, m_ZoomyMaxM4);
            }
        }

        private void UpdateLabelTimeM4BaseAbsoluteTime(DateTime StartTime, DateTime EndTime, float yMax)
        {
            //y
            label72.Text = yMax.ToString() + "MPa";
            label66.Text = "0";
            int interVal = (int)yMax / 6;
            label67.Text = interVal.ToString() + "MPa";
            label68.Text = (2 * interVal).ToString() + "MPa";
            label69.Text = (3 * interVal).ToString() + "MPa";
            label70.Text = (4 * interVal).ToString() + "MPa";
            label71.Text = (5 * interVal).ToString() + "MPa";

            //x          
            float timeInterval = (float)(EndTime - StartTime).TotalSeconds;
            label65.Text = "0s";
     
            label64.Text = (timeInterval / 8).ToString() + "s";
            label63.Text = (timeInterval * 2 / 8).ToString() + "s";
            label62.Text = (timeInterval * 3 / 8).ToString() + "s";
            label61.Text = (timeInterval * 4 / 8).ToString() + "s";
            label60.Text = (timeInterval * 5 / 8).ToString() + "s";
            label59.Text = (timeInterval * 6 / 8).ToString() + "s";
            label58.Text = (timeInterval * 7 / 8).ToString() + "s";
            label57.Text = (timeInterval * 8 / 8).ToString() + "s";
        }

        private void panel_W1_MouseMove(object sender, MouseEventArgs e)
        {
            if (RB_StandardTime_W1.Checked == true)
            {
                return;
            }
            if (RB_AbsoluteTime_W1.Checked == true)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross;
                Point pt = default(Point);
                pt.X = e.X;
                pt.Y = e.Y;
                if (m_DrawCurveBench1Handle == null)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return;
                }
                float xMax = (float)(m_M1BaseEndTime - m_M1BaseStartTime).TotalMilliseconds;
                float yMax = m_M1yMax;
                float retXValue = 0;
                float retYValue = 0;
                m_DrawCurveBench1Handle.ChangePanelPointToValue(panel_W1, pt, xMax, yMax, ref retXValue, ref retYValue);
                TB_Pressure_W1.Text = retYValue.ToString() + "MPa";
                TB_Time_W1.Text = retXValue.ToString() + "s";
            }
        }

        private void panel_W2_MouseMove(object sender, MouseEventArgs e)
        {
            if (RB_StandardTime_W2.Checked == true)
            {
                return;
            }
            if (RB_AbsoluteTime_W2.Checked == true)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross;
                Point pt = default(Point);
                pt.X = e.X;
                pt.Y = e.Y;
                if (m_DrawCurveBench2Handle == null)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return;
                }
                float xMax = (float)(m_M2BaseEndTime - m_M2BaseStartTime).TotalMilliseconds;
                float yMax = m_M2yMax;
                float retXValue = 0;
                float retYValue = 0;
                m_DrawCurveBench2Handle.ChangePanelPointToValue(panel_W2, pt, xMax, yMax, ref retXValue, ref retYValue);
                TB_Pressure_W2.Text = retYValue.ToString() + "MPa";
                TB_Time_W2.Text = retXValue.ToString() + "s";
            }
        }

        private void panel_W3_MouseMove(object sender, MouseEventArgs e)
        {
            if (RB_StandardTime_W3.Checked == true)
            {
                return;
            }
            if (RB_AbsoluteTime_W3.Checked == true)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross;
                Point pt = default(Point);
                pt.X = e.X;
                pt.Y = e.Y;
                if (m_DrawCurveBench3Handle == null)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return;
                }
                float xMax = (float)(m_M3BaseEndTime - m_M3BaseStartTime).TotalMilliseconds;
                float yMax = m_M3yMax;
                float retXValue = 0;
                float retYValue = 0;
                m_DrawCurveBench3Handle.ChangePanelPointToValue(panel_W3, pt, xMax, yMax, ref retXValue, ref retYValue);
                TB_Pressure_W3.Text = retYValue.ToString() + "MPa";
                TB_Time_W3.Text = retXValue.ToString() + "s";
            }
        }

        private void panel_W4_MouseMove(object sender, MouseEventArgs e)
        {
            if (RB_StandardTime_W4.Checked == true)
            {
                return;
            }
            if (RB_AbsoluteTime_W4.Checked == true)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross;
                Point pt = default(Point);
                pt.X = e.X;
                pt.Y = e.Y;
                if (m_DrawCurveBench4Handle == null)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return;
                }
                float xMax = (float)(m_M4BaseEndTime - m_M4BaseStartTime).TotalMilliseconds;
                float yMax = m_M4yMax;
                float retXValue = 0;
                float retYValue = 0;
                m_DrawCurveBench4Handle.ChangePanelPointToValue(panel_W4, pt, xMax, yMax, ref retXValue, ref retYValue);
                TB_Pressure_W4.Text = retYValue.ToString() + "MPa";
                TB_Time_W4.Text = retXValue.ToString() + "s";
            }
        }

        private void panel_W4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void panel_W3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void panel_W2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void panel_W1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }


    }
}
