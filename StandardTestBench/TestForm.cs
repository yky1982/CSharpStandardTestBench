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
using DataBaseLib;

namespace StandardTestBench
{
    public partial class TestForm : Form
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

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private TabControl.TabPageCollection m_Pages;
        private TabPage m_MainPage;
        private TabPage m_Page1;
        private TabPage m_Page2;
        private TabPage m_Page3;
        private TabPage m_Page4;

        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench1Handle;
        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench2Handle;
        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench3Handle;
        private DrawCurveDLL.DrawCurveLib m_DrawCurveBench4Handle;

        public class RuntimeInfoList
        {
            public string m_RegName;
            public string m_RegNameCH;//中文名称
            public string m_DataType;
            public string m_DataNum;//数据数
            public string m_ParaUint;
            public string[] m_State;
            public string m_isSave;
            public RuntimeInfoList(string regName, string regNameCH, string dataType, string paraUint, string DataNum, string[] state, string isSave)
            {
                m_RegName = regName;
                m_RegNameCH = regNameCH;
                m_DataType = dataType;
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

        public class ReportParaList
        {
            public string m_ParaName;
            public string m_ParaNameCH;
            public string m_ParaUint;
            public string m_Value;
            public string m_isSave;
            public ReportParaList(string paraName, string paraNameCH, string paraUint, string value, string isSave)
            {
                m_ParaName = paraName;
                m_ParaNameCH = paraNameCH;
                m_ParaUint = paraUint;
                m_Value = value;
                m_isSave = isSave;
            }
        }
        public List<ReportParaList> m_ReportParaLists = new List<ReportParaList>();

        public class SetParaList
        {
            public string m_RegName;
            public string m_RegNameCh;//中文名称
            public string m_DataType;
            public object m_MinValue;//最小值
            public object m_MaxValue;//最大值
            public string m_ParaUint;//单位
            public object m_Value;
            public string m_isSave;
            public string m_BenchNo;
            public SetParaList(string regName, string regNameCH, string dataType, object minValue, object maxValue, string paraUnit, object value, string isSave, string BenchNo)
            {
                m_RegName = regName;
                m_RegNameCh = regNameCH;
                m_DataType = dataType;
                m_MinValue = minValue;
                m_MaxValue = maxValue;
                m_ParaUint = paraUnit;
                m_Value = value;
                m_isSave = isSave;
                m_BenchNo = BenchNo;
            }
        }
        public List<SetParaList> m_SetParaLists = new List<SetParaList>();

        private string m_XMLRuntimeInfoFilePath1 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench1.xml";
        private string m_XMLRuntimeInfoFilePath2 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench2.xml";
        private string m_XMLRuntimeInfoFilePath3 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench3.xml";
        private string m_XMLRuntimeInfoFilePath4 = Application.StartupPath + @"\SystemFile\TestConfig\RuntimeParaBench4.xml";
        private string m_XMLReportFilePath = Application.StartupPath + @"\SystemFile\SetPara\ReportXML.xml";
        private string m_XMLConfigFilePath = Application.StartupPath + @"\SystemFile\SetPara\SetParaConfig.xml";
        private string m_INIRuntimeParaConfigFilePath = Application.StartupPath + @"\Config\TestConfig\RuntimeParaConfig.ini";
        private string m_INIMachineFilePath = Application.StartupPath + @"\Config\SetPara\SetParaConfig.ini";//试验台数目
        private string m_INIMachineSelectFilePath = Application.StartupPath + @"\Config\SetPara\MSelect.ini";//试验台数目
        private string m_INIToolBar01FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar01.ini";
        private string m_INIToolBar02FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar02.ini";
        private string m_INIToolBar03FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar03.ini";
        private string m_INIToolBar04FilePath = Application.StartupPath + @"\Config\TestConfig\ToolBar04.ini";
        private string m_INICurveInfoFilePath = Application.StartupPath + @"\Config\TestConfig\CurveInfo.ini";
        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

        private DataBaseLib.DataBaseInterface m_DBM1Handle = null;
        private DataBaseLib.DataBaseInterface m_DBM2Handle = null;
        private DataBaseLib.DataBaseInterface m_DBM3Handle = null;
        private DataBaseLib.DataBaseInterface m_DBM4Handle = null;
        private string m_DBBench1FilePath = Application.StartupPath + @"\DataBase\M1.mdb";
        private string m_DBBench2FilePath = Application.StartupPath + @"\DataBase\M2.mdb";
        private string m_DBBench3FilePath = Application.StartupPath + @"\DataBase\M3.mdb";
        private string m_DBBench4FilePath = Application.StartupPath + @"\DataBase\M4.mdb";
        private string m_TableName = "TestResult";

        private int m_MachineNum = 0;   //拖机总数量
        private bool m_isM1Enable = false;
        private bool m_isM2Enable = false;
        private bool m_isM3Enable = false;
        private bool m_isM4Enable = false;
        private int m_MaxColumn = 1;
        private event StatePageInfo ShowPageInfo;
        private Form1 m_MainFormHandle = null;
        private event StateSysInfo ShowDebugInfo;
        private UserManager m_UserManagerHandle = null;

        private System.Windows.Forms.Timer m_TimerHandle = new System.Windows.Forms.Timer();
        private int m_TimerCount = 0;

        private Color m_LineColor = Color.Red;
        private float m_M1xMax = 30;
        private float m_M1yMax = 100;
        private float m_M2xMax = 30;
        private float m_M2yMax = 100;
        private float m_M3xMax = 30;
        private float m_M3yMax = 100;
        private float m_M4xMax = 30;
        private float m_M4yMax = 100;
        private float m_xSpan = 30;
        private DateTime m_M1BaseTime = default(DateTime);
        private DateTime m_M2BaseTime = default(DateTime);
        private DateTime m_M3BaseTime = default(DateTime);
        private DateTime m_M4BaseTime = default(DateTime);
        private DateTime m_M1EndTime = default(DateTime);
        private DateTime m_M2EndTime = default(DateTime);
        private DateTime m_M3EndTime = default(DateTime);
        private DateTime m_M4EndTime = default(DateTime);
        private string m_DrawCurveType = "压缩曲线";
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);

            m_UserManagerHandle = UserManager.GetHandle();

            InitPages();
            InitPagesDis();
            BenchSelect();
            ToolDisBench1();
            ToolDisBench2();
            ToolDisBench3();
            ToolDisBench4();

            LoadRuntimeInfo();

            LoadReportInfo();

            LoadSetParaInfo();

            DrawCurveInit();

            CurveAdjInit();

            AccessInit();

            UILanguageInit();

            m_TimerHandle.Interval = 100;
            m_TimerHandle.Tick += new EventHandler(TimerTick);
            return;
            m_TimerHandle.Start();

        }

        #region Page初始化
        private void InitPages()
        {
            m_Pages = tabControl1.TabPages;
            m_MainPage = m_Pages[0];
            m_Page1 = m_Pages[1];
            m_Page2 = m_Pages[2];
            m_Page3 = m_Pages[3];
            m_Page4 = m_Pages[4];
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
                tabControl1.TabPages.Add(m_MainPage);
                tabControl1.TabPages.Add(m_Page1);
                tabControl1.TabPages.Add(m_Page2);
            }
            if (Num == 3)
            {
                tabControl1.TabPages.Add(m_MainPage);
                tabControl1.TabPages.Add(m_Page1);
                tabControl1.TabPages.Add(m_Page2);
                tabControl1.TabPages.Add(m_Page3);
            }
            if (Num == 4)
            {
                tabControl1.TabPages.Add(m_MainPage);
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

        #region 主界面显示初始化
        private void InitPagesDis()
        {
            if (m_MachineNum == 1)
            {
                return;
            }
            if (m_MachineNum == 2)
            {
                Grp_W1.Visible = true;
                Grp_W1.Location = new System.Drawing.Point(6,20);
                Grp_W1.Size = new System.Drawing.Size(1180, 360);
                DG_Main_W1.Location = new System.Drawing.Point(10, 15);
                DG_Main_W1.Size = new System.Drawing.Size(1160, 330);

                Grp_W2.Visible = true;
                Grp_W2.Location = new System.Drawing.Point(6,395);
                Grp_W2.Size = new System.Drawing.Size(1180, 360);
                DG_Main_W2.Location = new System.Drawing.Point(10, 15);
                DG_Main_W2.Size = new System.Drawing.Size(1160, 330);

                Grp_W3.Visible = false;
                Grp_W4.Visible = false;
            }

            if (m_MachineNum == 3)
            {
                Grp_W1.Visible = true;
                Grp_W1.Location = new System.Drawing.Point(15, 20);
                Grp_W1.Size = new System.Drawing.Size(580, 360);
                DG_Main_W1.Location = new System.Drawing.Point(10, 15);
                DG_Main_W1.Size = new System.Drawing.Size(560, 335);

                Grp_W2.Visible = true;
                Grp_W2.Location = new System.Drawing.Point(610, 20);
                Grp_W2.Size = new System.Drawing.Size(580, 360);
                DG_Main_W2.Location = new System.Drawing.Point(10, 15);
                DG_Main_W2.Size = new System.Drawing.Size(560, 335);

                Grp_W3.Visible = true;
                Grp_W3.Location = new System.Drawing.Point(15, 390);
                Grp_W3.Size = new System.Drawing.Size(580, 360);
                DG_Main_W3.Location = new System.Drawing.Point(10, 15);
                DG_Main_W3.Size = new System.Drawing.Size(560, 335);

                Grp_W4.Visible = false;
            }

            if (m_MachineNum == 4)
            {
                Grp_W1.Visible = true;
                Grp_W1.Location = new System.Drawing.Point(15, 20);
                Grp_W1.Size = new System.Drawing.Size(580, 360);
                DG_Main_W1.Location = new System.Drawing.Point(10, 15);
                DG_Main_W1.Size = new System.Drawing.Size(560, 335);

                Grp_W2.Visible = true;
                Grp_W2.Location = new System.Drawing.Point(610, 20);
                Grp_W2.Size = new System.Drawing.Size(580, 360);
                DG_Main_W2.Location = new System.Drawing.Point(10, 15);
                DG_Main_W2.Size = new System.Drawing.Size(560, 335);

                Grp_W3.Visible = true;
                Grp_W3.Location = new System.Drawing.Point(15, 390);
                Grp_W3.Size = new System.Drawing.Size(580, 360);
                DG_Main_W3.Location = new System.Drawing.Point(10, 15);
                DG_Main_W3.Size = new System.Drawing.Size(560, 335);

                Grp_W4.Visible = true;
                Grp_W4.Location = new System.Drawing.Point(610, 390);
                Grp_W4.Size = new System.Drawing.Size(580, 360);
                DG_Main_W4.Location = new System.Drawing.Point(10, 15);
                DG_Main_W4.Size = new System.Drawing.Size(560, 335);
            }
        }
        #endregion

        #region 工作台选择
        private void BenchSelect()
        {
            string M1 = ContentValue("MSelect", "M1", m_INIMachineSelectFilePath);
            string M2 = ContentValue("MSelect", "M2", m_INIMachineSelectFilePath);
            string M3 = ContentValue("MSelect", "M3", m_INIMachineSelectFilePath);
            string M4 = ContentValue("MSelect", "M4", m_INIMachineSelectFilePath);

            if (M1 == "False")
            {
                Grp_Curve01.Enabled = false;
                Grp_Tool01.Enabled = false;
                Grp_W1.Enabled = false;
                Grp_Adj01.Enabled = false;
                Grp_Info01.Enabled = false;
            }
            else
            {
                Grp_Curve01.Enabled = true;
                Grp_Tool01.Enabled = true;
                Grp_W1.Enabled = true;
                Grp_Adj01.Enabled = true;
                Grp_Info01.Enabled = true;
            }

            if (M2 == "False")
            {
                Grp_Curve02.Enabled = false;
                Grp_Tool02.Enabled = false;
                Grp_W2.Enabled = false;
                Grp_Adj02.Enabled = false;
                Grp_Info02.Enabled = false;
            }
            else
            {
                Grp_Curve02.Enabled = true;
                Grp_Tool02.Enabled = true;
                Grp_W2.Enabled = true;
                Grp_Adj02.Enabled = true;
                Grp_Info02.Enabled = true;
            }

            if (M3 == "False")
            {
                Grp_Curve03.Enabled = false;
                Grp_Tool03.Enabled = false;
                Grp_W3.Enabled = false;
                Grp_Adj03.Enabled = false;
                Grp_Info03.Enabled = false;
            }
            else
            {
                Grp_Curve03.Enabled = true;
                Grp_Tool03.Enabled = true;
                Grp_W3.Enabled = true;
                Grp_Adj03.Enabled = true;
                Grp_Info03.Enabled = true;
            }

            if (M4 == "False")
            {
                Grp_Curve04.Enabled = false;
                Grp_Tool04.Enabled = false;
                Grp_W4.Enabled = false;
                Grp_Adj04.Enabled = false;
                Grp_Info04.Enabled = false;
            }
            else
            {
                Grp_Curve04.Enabled = true;
                Grp_Tool04.Enabled = true;
                Grp_W4.Enabled = true;
                Grp_Adj04.Enabled = true;
                Grp_Info04.Enabled = true;
            }

            if (m_MachineNum <= 4 && M1 == "True")
            {
                m_isM1Enable = true;
            }
            else
            {
                m_isM1Enable = false;
            }

            if (m_MachineNum <= 4 && M2 == "True")
            {
                m_isM2Enable = true;
            }
            else
            {
                m_isM2Enable = false;
            }

            if (m_MachineNum <= 4 && M3 == "True")
            {
                m_isM3Enable = true;
            }
            else
            {
                m_isM3Enable = false;
            }

            if (m_MachineNum <= 4 && M4 == "True")
            {
                m_isM4Enable = true;
            }
            else
            {
                m_isM4Enable = false;
            }
        }
        #endregion

        #region 工作台工具栏显示初始化
        private void ToolDisBench1()
        {
            //TB_Start
            string BName = "BTStart";
            string INIPath = m_INIToolBar01FilePath;
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);         
            if (sEnable == "False")
            {
                TB_Start.Visible = false;
            }
            else
            {
                TB_Start.Visible = true;
                TB_Start.Text = regNameCH;
                TB_Start.Image = Bitmap.FromFile(picPath);
            }


            //TB_End
            BName = "BTStop";
            INIPath = m_INIToolBar01FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_End.Visible = false;
            }
            else
            {
                TB_End.Visible = true;
                TB_End.Text = regNameCH;
                TB_End.Image = Bitmap.FromFile(picPath);
            }


            //BTCustom01
            BName = "BTCustom01";
            INIPath = m_INIToolBar01FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom01.Visible = false;
            }
            else
            {
                BTCustom01.Visible = true;
                BTCustom01.Text = regNameCH;
                BTCustom01.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom02
            BName = "BTCustom02";
            INIPath = m_INIToolBar01FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom02.Visible = false;
            }
            else
            {
                BTCustom02.Visible = true;
                BTCustom02.Text = regNameCH;
                BTCustom02.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom03
            BName = "BTCustom03";
            INIPath = m_INIToolBar01FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom03.Visible = false;
            }
            else
            {
                BTCustom03.Visible = true;
                BTCustom03.Text = regNameCH;
                BTCustom03.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom04
            BName = "BTCustom04";
            INIPath = m_INIToolBar01FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom04.Visible = false;
            }
            else
            {
                BTCustom04.Visible = true;
                BTCustom04.Text = regNameCH;
                BTCustom04.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom05
            BName = "BTCustom05";
            INIPath = m_INIToolBar01FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom05.Visible = false;
            }
            else
            {
                BTCustom05.Visible = true;
                BTCustom05.Text = regNameCH;
                BTCustom05.Image = Bitmap.FromFile(picPath);
            }
        }

        private void ToolDisBench2()
        {
            //TB_Start
            string BName = "BTStart";
            string INIPath = m_INIToolBar02FilePath;
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_Start_2.Visible = false;
            }
            else
            {
                TB_Start_2.Visible = true;
                TB_Start_2.Text = regNameCH;
                TB_Start_2.Image = Bitmap.FromFile(picPath);
            }

            //TB_End
            BName = "BTStop";
            INIPath = m_INIToolBar02FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_End_2.Visible = false;
            }
            else
            {
                TB_End_2.Visible = true;
                TB_End_2.Text = regNameCH;
                TB_End_2.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom01
            BName = "BTCustom01";
            INIPath = m_INIToolBar02FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom01_2.Visible = false;
            }
            else
            {
                BTCustom01_2.Visible = true;
                BTCustom01_2.Text = regNameCH;
                BTCustom01_2.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom02
            BName = "BTCustom02";
            INIPath = m_INIToolBar02FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom02_2.Visible = false;
            }
            else
            {
                BTCustom02_2.Visible = true;
                BTCustom02_2.Text = regNameCH;
                BTCustom02_2.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom03
            BName = "BTCustom03";
            INIPath = m_INIToolBar02FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom03_2.Visible = false;
            }
            else
            {
                BTCustom03_2.Visible = true;
                BTCustom03_2.Text = regNameCH;
                BTCustom03_2.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom04
            BName = "BTCustom04";
            INIPath = m_INIToolBar02FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom04_2.Visible = false;
            }
            else
            {
                BTCustom04_2.Visible = true;
                BTCustom04_2.Text = regNameCH;
                BTCustom04_2.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom05
            BName = "BTCustom05";
            INIPath = m_INIToolBar02FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom05_2.Visible = false;
            }
            else
            {
                BTCustom05_2.Visible = true;
                BTCustom05_2.Text = regNameCH;
                BTCustom05_2.Image = Bitmap.FromFile(picPath);
            }
        }

        private void ToolDisBench3()
        {
            //TB_Start
            string BName = "BTStart";
            string INIPath = m_INIToolBar03FilePath;
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_Start_3.Visible = false;
            }
            else
            {
                TB_Start_3.Visible = true;
                TB_Start_3.Text = regNameCH;
                TB_Start_3.Image = Bitmap.FromFile(picPath);
            }

            //TB_End
            BName = "BTStop";
            INIPath = m_INIToolBar03FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_End_3.Visible = false;
            }
            else
            {
                TB_End_3.Visible = true;
                TB_End_3.Text = regNameCH;
                TB_End_3.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom01
            BName = "BTCustom01";
            INIPath = m_INIToolBar03FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom01_3.Visible = false;
            }
            else
            {
                BTCustom01_3.Visible = true;
                BTCustom01_3.Text = regNameCH;
                BTCustom01_3.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom02
            BName = "BTCustom02";
            INIPath = m_INIToolBar03FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom02_3.Visible = false;
            }
            else
            {
                BTCustom02_3.Visible = true;
                BTCustom02_3.Text = regNameCH;
                BTCustom02_3.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom03
            BName = "BTCustom03";
            INIPath = m_INIToolBar03FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom03_3.Visible = false;
            }
            else
            {
                BTCustom03_3.Visible = true;
                BTCustom03_3.Text = regNameCH;
                BTCustom03_3.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom04
            BName = "BTCustom04";
            INIPath = m_INIToolBar03FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom04_3.Visible = false;
            }
            else
            {
                BTCustom04_3.Visible = true;
                BTCustom04_3.Text = regNameCH;
                BTCustom04_3.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom05
            BName = "BTCustom05";
            INIPath = m_INIToolBar03FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom05_3.Visible = false;
            }
            else
            {
                BTCustom05_3.Visible = true;
                BTCustom05_3.Text = regNameCH;
                BTCustom05_3.Image = Bitmap.FromFile(picPath);
            }
        }

        private void ToolDisBench4()
        {
            //TB_Start
            string BName = "BTStart";
            string INIPath = m_INIToolBar04FilePath;
            string sEnable = ContentValue(BName, "Enable", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            string picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_Start_4.Visible = false;
            }
            else
            {
                TB_Start_4.Visible = true;
                TB_Start_4.Text = regNameCH;
                TB_Start_4.Image = Bitmap.FromFile(picPath);
            }

            //TB_End
            BName = "BTStop";
            INIPath = m_INIToolBar04FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                TB_End_4.Visible = false;
            }
            else
            {
                TB_End_4.Visible = true;
                TB_End_4.Text = regNameCH;
                TB_End_4.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom01
            BName = "BTCustom01";
            INIPath = m_INIToolBar04FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom01_4.Visible = false;
            }
            else
            {
                BTCustom01_4.Visible = true;
                BTCustom01_4.Text = regNameCH;
                BTCustom01_4.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom02
            BName = "BTCustom02";
            INIPath = m_INIToolBar04FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom02_4.Visible = false;
            }
            else
            {
                BTCustom02_4.Visible = true;
                BTCustom02_4.Text = regNameCH;
                BTCustom02_4.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom03
            BName = "BTCustom03";
            INIPath = m_INIToolBar04FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom03_4.Visible = false;
            }
            else
            {
                BTCustom03_4.Visible = true;
                BTCustom03_4.Text = regNameCH;
                BTCustom03_4.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom04
            BName = "BTCustom04";
            INIPath = m_INIToolBar04FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom04_4.Visible = false;
            }
            else
            {
                BTCustom04_4.Visible = true;
                BTCustom04_4.Text = regNameCH;
                BTCustom04_4.Image = Bitmap.FromFile(picPath);
            }

            //BTCustom05
            BName = "BTCustom05";
            INIPath = m_INIToolBar04FilePath;
            sEnable = ContentValue(BName, "Enable", INIPath);
            regNameCH = ContentValue(BName, "RegNameCH", INIPath);
            picPath = ContentValue(BName, "FilePath", INIPath);
            if (sEnable == "False")
            {
                BTCustom05_4.Visible = false;
            }
            else
            {
                BTCustom05_4.Visible = true;
                BTCustom05_4.Text = regNameCH;
                BTCustom05_4.Image = Bitmap.FromFile(picPath);
            }
        }
        #endregion

        #region 加载实时信息XML表格
        private void LoadRuntimeInfo()
        {
            string sColumn = ContentValue("RuntimeParaConfig", "Rows", m_INIRuntimeParaConfigFilePath);
            m_MaxColumn = Convert.ToInt32(sColumn);

            DG_Main_W1.ReadOnly = true;
            DG_Main_W2.ReadOnly = true;
            DG_Main_W3.ReadOnly = true;
            DG_Main_W4.ReadOnly = true;
            DG_W1.ReadOnly = true;
            DG_W2.ReadOnly = true;
            DG_W3.ReadOnly = true;
            DG_W4.ReadOnly = true;
            DG_Main_W1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_Main_W2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_Main_W3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_Main_W4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_W1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_W2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_W3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DG_W4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DG_Main_W1.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_Main_W2.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_Main_W3.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_Main_W4.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_W1.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_W2.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_W3.DefaultCellStyle = dataGridViewCellStyle1;
            this.DG_W4.DefaultCellStyle = dataGridViewCellStyle1;

            AddTimeInfoToList();
            switch (m_MachineNum)
            {
                case 1:
                    LoadRuntimeInfoXMLBench1();
                    InitDisRuntimeInfoBench1();
                    break;
                case 2:
                    LoadRuntimeInfoXMLBench1();
                    InitDisRuntimeInfoBench1();
                    LoadRuntimeInfoXMLBench2();
                    InitDisRuntimeInfoBench2();
                    break;
                case 3:
                    LoadRuntimeInfoXMLBench1();
                    InitDisRuntimeInfoBench1();
                    LoadRuntimeInfoXMLBench2();
                    InitDisRuntimeInfoBench2();
                    LoadRuntimeInfoXMLBench3();
                    InitDisRuntimeInfoBench3();
                    break;
                case 4:
                    LoadRuntimeInfoXMLBench1();
                    InitDisRuntimeInfoBench1();
                    LoadRuntimeInfoXMLBench2();
                    InitDisRuntimeInfoBench2();
                    LoadRuntimeInfoXMLBench3();
                    InitDisRuntimeInfoBench3();
                    LoadRuntimeInfoXMLBench4();
                    InitDisRuntimeInfoBench4();
                    break;
            }
        }

        private void AddTimeInfoToList()
        {
            string RegName = "StartTime";
            string RegNameCH = "开始时间";//中文名称
            string dataType = "DateTime";
            string DataNum = "1";//数据数
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

        private void LoadRuntimeInfoXMLBench1()
        {
            string regName = "";
            string regNameCH = "";
            string dataNum = "";
            string dataType = "";
            string paraUnit = "";
            string[] state = new string[0];
            string isSave = "";
            if (!File.Exists(m_XMLRuntimeInfoFilePath1))
            {
                SendDebugInfo("TestForm 实时信息配置文件1不存在");
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
            string dataType = "";
            string paraUnit = "";
            string[] state = new string[0];
            string isSave = "";
            if (!File.Exists(m_XMLRuntimeInfoFilePath2))
            {
                SendDebugInfo("TestForm 实时信息配置文件2不存在");
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
                SendDebugInfo("TestForm 实时信息配置文件3不存在");
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
                SendDebugInfo("TestForm 实时信息配置文件4不存在");
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

        private void InitDisRuntimeInfoBench1()
        {
            DG_Main_W1.Rows.Clear();
            DG_W1.Rows.Clear();
            int count = m_RuntimeInfoBench1Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = m_MaxColumn;
            }
            catch (System.Exception)
            {
                MaxColoum = 5;
                m_MaxColumn = 5;
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
            DG_Main_W1.ColumnCount = MaxColoum;
            DG_W1.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DG_Main_W1.Rows.Add();
                DG_W1.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DG_Main_W1.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench1Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench1Lists[i * MaxColoum + j].m_ParaUint + ")";
                    DG_W1.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench1Lists[i * MaxColoum + j].m_RegNameCH +
                                                                               " (" + m_RuntimeInfoBench1Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void InitDisRuntimeInfoBench2()
        {
            DG_Main_W2.Rows.Clear();
            DG_W2.Rows.Clear();
            int count = m_RuntimeInfoBench2Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = m_MaxColumn;
            }
            catch (System.Exception)
            {
                MaxColoum = 5;
                m_MaxColumn = 5;
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
            DG_Main_W2.ColumnCount = MaxColoum;
            DG_W2.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DG_Main_W2.Rows.Add();
                DG_W2.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DG_Main_W2.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench2Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench2Lists[i * MaxColoum + j].m_ParaUint + ")";
                    DG_W2.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench2Lists[i * MaxColoum + j].m_RegNameCH +
                                                                               " (" + m_RuntimeInfoBench2Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void InitDisRuntimeInfoBench3()
        {
            DG_Main_W3.Rows.Clear();
            DG_W3.Rows.Clear();
            int count = m_RuntimeInfoBench3Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = m_MaxColumn;
            }
            catch (System.Exception)
            {
                MaxColoum = 5;
                m_MaxColumn = 5;
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
            DG_Main_W3.ColumnCount = MaxColoum;
            DG_W3.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DG_Main_W3.Rows.Add();
                DG_W3.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DG_Main_W3.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench3Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench3Lists[i * MaxColoum + j].m_ParaUint + ")";
                    DG_W3.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench3Lists[i * MaxColoum + j].m_RegNameCH +
                                                                               " (" + m_RuntimeInfoBench3Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }

        private void InitDisRuntimeInfoBench4()
        {
            DG_Main_W4.Rows.Clear();
            DG_W4.Rows.Clear();
            int count = m_RuntimeInfoBench4Lists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = m_MaxColumn;
            }
            catch (System.Exception)
            {
                MaxColoum = 5;
                m_MaxColumn = 5;
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
            DG_Main_W4.ColumnCount = MaxColoum;
            DG_W4.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DG_Main_W4.Rows.Add();
                DG_W4.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DG_Main_W4.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench4Lists[i * MaxColoum + j].m_RegNameCH +
                                                                                                   " (" + m_RuntimeInfoBench4Lists[i * MaxColoum + j].m_ParaUint + ")";
                    DG_W4.Rows[i * 2].Cells[j].Value = m_RuntimeInfoBench4Lists[i * MaxColoum + j].m_RegNameCH +
                                                                               " (" + m_RuntimeInfoBench4Lists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
        }
        #endregion

        #region 加载报表信息
        private void LoadReportInfo()
        {
            string paraName = "";
            string paraNameCH = "";
            string paraUint = "";
            string value = "";
            string isSave = "";
            if (!File.Exists(m_XMLReportFilePath))
            {
                SendDebugInfo("TestForm 报表信息配置文件不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLReportFilePath);
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
                string UserName = m_UserManagerHandle.m_CurrenUser;
                string userINIFilePath = Application.StartupPath + @"\Config\SetPara\" + UserName + @"Report.ini";
                value = ContentValue("ReportPara", paraNameCH, userINIFilePath);
                m_ReportParaLists.Add(new ReportParaList(paraName, paraNameCH, paraUint, value, isSave));
            }
        }
        #endregion

        #region 加载设置参数信息
        private void LoadSetParaInfo()
        {
            string regName = "";
            string regNameCH = "";
            string dataType = "";
            object minValue = default(object);
            object maxValue = default(object);
            string paraUnit = "";
            string BenchNo = "";
            object value = default(object);
            string isSave = "";
            if (!File.Exists(m_XMLConfigFilePath))
            {
                SendDebugInfo("TestForm 设置参数配置表不存在");
                return;
            }
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(m_XMLConfigFilePath);
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
                string UserName = m_UserManagerHandle.m_CurrenUser;
                string userINIFilePath = Application.StartupPath + @"\Config\SetPara\" + UserName + @".ini";
                value = ContentValue("SetPara", regNameCH, userINIFilePath);
                m_SetParaLists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, value, isSave, BenchNo));
            }
        }
        #endregion


        #region 曲线初始化
        private void DrawCurveInit()
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
            if(sDisType == "压缩曲线")
            {
                m_DrawCurveBench1Handle.LibInit("Compress");
                m_DrawCurveBench2Handle.LibInit("Compress");
                m_DrawCurveBench3Handle.LibInit("Compress");
                m_DrawCurveBench4Handle.LibInit("Compress");
            }
            if(sDisType == "平移曲线")
            {
                m_DrawCurveBench1Handle.LibInit("Shift");
                m_DrawCurveBench2Handle.LibInit("Shift");
                m_DrawCurveBench3Handle.LibInit("Shift");
                m_DrawCurveBench4Handle.LibInit("Shift");
            }
            //绘图方式
            m_DrawCurveBench1Handle.SetDrawInitMode(sCurveMethod);
            m_DrawCurveBench2Handle.SetDrawInitMode(sCurveMethod);
            m_DrawCurveBench3Handle.SetDrawInitMode(sCurveMethod);
            m_DrawCurveBench4Handle.SetDrawInitMode(sCurveMethod);

            //设置参数
            if(sCurveMethod == "GDI")
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
            if(sCurveMethod == "OpenGL")
            {
                Point startP = panel_W1.PointToScreen(panel_W1.Location);
                int startX = startP.X;
                int startY = startP.Y;
                Point endP = default(Point);
                endP.X = startX + panel_W1.Width;
                endP.Y = startY +panel_W1.Height;
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
                endP.Y = startY +panel_W2.Height;
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
                endP.Y = startY +panel_W3.Height;
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
                endP.Y = startY +panel_W4.Height;
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
            m_M1xMax = Convert.ToSingle(sxMax);
            m_M1yMax = Convert.ToSingle(syMax);
            m_M2xMax = m_M1xMax;
            m_M3xMax = m_M1xMax;
            m_M4xMax = m_M1xMax;
            m_M2yMax = m_M1yMax;
            m_M3yMax = m_M1yMax;
            m_M4yMax = m_M1yMax;
            m_xSpan = Convert.ToSingle(sInterval);

        }
        #endregion

        #region 曲线调节初始化
        private void CurveAdjInit()
        {
            HP_AdjPre_M1.Maximum = (int)m_M1yMax * 2;
            HP_AdjPre_M2.Maximum = (int)m_M1yMax * 2;
            HP_AdjPre_M3.Maximum = (int)m_M1yMax * 2;
            HP_AdjPre_M4.Maximum = (int)m_M1yMax * 2;

            if (m_DrawCurveType == "压缩曲线")
            {
                HP_AdjTime_M1.Maximum = 120;
                HP_AdjTime_M1.Value = 60;
                HP_AdjTime_M2.Maximum = 120;
                HP_AdjTime_M2.Value = 60;
                HP_AdjTime_M3.Maximum = 120;
                HP_AdjTime_M3.Value = 60;
                HP_AdjTime_M4.Maximum = 120;
                HP_AdjTime_M4.Value = 60;
            }

            if (m_DrawCurveType == "平移曲线")
            {
                HP_AdjTime_M1.Maximum = 3600;
                HP_AdjTime_M1.Value = 60;
                HP_AdjTime_M2.Maximum = 3600;
                HP_AdjTime_M2.Value = 60;
                HP_AdjTime_M3.Maximum = 3600;
                HP_AdjTime_M3.Value = 60;
                HP_AdjTime_M4.Maximum = 3600;
                HP_AdjTime_M4.Value = 60;
            }

            TB_DisPre_M1.Enabled = false;
            TB_DisTime_M1.Enabled = false;
            TB_DisPre_M2.Enabled = false;
            TB_DisTime_M2.Enabled = false;
            TB_DisPre_M3.Enabled = false;
            TB_DisPre_M3.Enabled = false;
            TB_DisTime_M4.Enabled = false;
            TB_DisTime_M4.Enabled = false;

            TB_DisPre_M1.Text = m_M1yMax.ToString() + "MPa";
            TB_DisPre_M2.Text = m_M1yMax.ToString() + "MPa";
            TB_DisPre_M3.Text = m_M1yMax.ToString() + "MPa";
            TB_DisPre_M4.Text = m_M1yMax.ToString() + "MPa";

            TB_DisTime_M1.Text = HP_AdjTime_M1.Value.ToString() + "s";
            TB_DisTime_M2.Text = HP_AdjTime_M1.Value.ToString() + "s";
            TB_DisTime_M3.Text = HP_AdjTime_M1.Value.ToString() + "s";
            TB_DisTime_M4.Text = HP_AdjTime_M1.Value.ToString() + "s";
        }
        #endregion

        #region 数据库初始化
        private void AccessInit()
        {
            m_DBM1Handle = new DataBaseLib.DataBaseInterface();
            m_DBM2Handle = new DataBaseLib.DataBaseInterface();
            m_DBM3Handle = new DataBaseLib.DataBaseInterface();
            m_DBM4Handle = new DataBaseLib.DataBaseInterface();

            bool code = m_DBM1Handle.DBInit("Access", m_DBBench1FilePath, "Open", m_TableName);
            if (!code)
            {
                MessageBox.Show("打开数据表1失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM2Handle.DBInit("Access", m_DBBench2FilePath, "Open", m_TableName);
            if (!code)
            {
                MessageBox.Show("打开数据表2失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM3Handle.DBInit("Access", m_DBBench3FilePath, "Open", m_TableName);
            if (!code)
            {
                MessageBox.Show("打开数据表3失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            code = m_DBM4Handle.DBInit("Access", m_DBBench4FilePath, "Open", m_TableName);
            if (!code)
            {
                MessageBox.Show("打开数据表4失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region 语言初始化
        private void UILanguageInit()
        {
            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);
            if (sLanguage == "English")
            {
                TestBenchLanguageInitMain();
                TestBenchLanguageInitM1();
                TestBenchLanguageInitM2();
                TestBenchLanguageInitM3();
                TestBenchLanguageInitM4();
            }
        }

        private void TestBenchLanguageInitMain()
        {
            Grp_Main_ToolBar.Text = "ToolBar";
            Grp_DisInfo.Text = "Information";
            Grp_W1.Text = "Test No1";
            Grp_W2.Text = "Test No2";
            Grp_W3.Text = "Test No3";
            Grp_W4.Text = "Test No4";
            TB_Exit.Text = "Exit";
        }

        private void TestBenchLanguageInitM1()
        {
            Grp_Tool01.Text = "ToolBar";
            Grp_Curve01.Text = "Curve";
            Grp_Adj01.Text = "Curve Adjust";
            Grp_Info01.Text = "Information";
            TB_Exit_01.Text = "Exit";
            TB_Start.Text = "Start";
            TB_End.Text = "Stop";
            TB_Save.Text = "Save";
            TB_CancelSave.Text = "unSave";
        }

        private void TestBenchLanguageInitM2()
        {
            Grp_Tool02.Text = "ToolBar";
            Grp_Curve02.Text = "Curve";
            Grp_Adj02.Text = "Curve Adjust";
            Grp_Info02.Text = "Information";
            TB_Exit_2.Text = "Exit";
            TB_Start_2.Text = "Start";
            TB_End_2.Text = "Stop";
            TB_Save_2.Text = "Save";
            TB_CancelSave_2.Text = "unSave";
        }

        private void TestBenchLanguageInitM3()
        {
            Grp_Tool03.Text = "ToolBar";
            Grp_Curve03.Text = "Curve";
            Grp_Adj03.Text = "Curve Adjust";
            Grp_Info03.Text = "Information";
            TB_Exit_3.Text = "Exit";
            TB_Start_3.Text = "Start";
            TB_End_3.Text = "Stop";
            TB_Save_3.Text = "Save";
            TB_CancelSave_3.Text = "unSave";
        }

        private void TestBenchLanguageInitM4()
        {
            Grp_Tool04.Text = "ToolBar";
            Grp_Curve04.Text = "Curve";
            Grp_Adj04.Text = "Curve Adjust";
            Grp_Info04.Text = "Information";
            TB_Exit_4.Text = "Exit";
            TB_Start_4.Text = "Start";
            TB_End_4.Text = "Stop";
            TB_Save_4.Text = "Save";
            TB_CancelSave_4.Text = "unSave";
        }
        #endregion

        private bool m_isStartSample = false; //开始采集数据
        private void TimerTick(object o, EventArgs e)
        {
            m_TimerCount++;
            //采集
            SampleData();

            //采集开始、停止标志
            if (m_TimerCount % 4 == 0)
            {
                SampleTestFlag();
            }

            //采集实时信息
            if (m_TimerCount % 6 == 0)
            {
                SampleRuntimeInfo();
            }

            //绘图
            if (m_TimerCount % 10 == 0)
            {
                DrawRuntimeCurve();
            }
            if (m_TimerCount >= 10)
            {
                m_TimerCount = 0;
            }
            
        }

        #region 曲线数据采集       
        private void SampleData()
        {
            if (!m_isStartSample)
            {
                return;
            }
            if (m_isM1Enable)
            {
                SampleM1Data();
            }
            if (m_isM2Enable)
            {
                SampleM2Data();
            }
            if (m_isM3Enable)
            {
                SampleM3Data();
            }
            if (m_isM4Enable)
            {
                SampleM4Data();
            }
        }

        private bool m_isM1FirstSample = true;
        private PointF[] m_M1revData = new PointF[5];
        private int m_M1DataCount = 0;
        private void SampleM1Data()
        {
            string regName = ContentValue("CurveInfo", "RegNameM1", m_INICurveInfoFilePath);
            float revData = 0.0f;
            DateTime dt = default(DateTime);
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref revData, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                return;
            }

            if (m_isM1FirstSample)
            {
                revData = 0.3f;
                m_isM1FirstSample = false;
            }

            float timeSpan = (float)(dt - m_M1BaseTime).TotalSeconds;
            m_M1revData[m_M1DataCount].Y = revData;
            m_M1revData[m_M1DataCount].X = timeSpan;
            //string sDisType = ContentValue("CurveInfo", "DisType", m_INICurveInfoFilePath);
            if (m_DrawCurveType == "压缩曲线")
            {
                if (timeSpan > m_M1xMax)
                {
                    m_M1xMax += m_xSpan;
                }
            }

            m_M1DataCount++;
            if (m_M1DataCount >= 5)
            {
                bool ret = m_DrawCurveBench1Handle.SavePointToList(m_M1revData);
                if (!ret)
                {
                    SendDebugInfo("TestForm 向绘图控件保存数据失败，工位一");
                }
                m_M1DataCount = 0;
            }
        }

        private bool m_isM2FirstSample = true;
        private PointF[] m_M2revData = new PointF[5];
        private int m_M2DataCount = 0;
        private void SampleM2Data()
        {
            string regName = ContentValue("CurveInfo", "RegNameM2", m_INICurveInfoFilePath);
            float revData = 0.0f;
            DateTime dt = default(DateTime);
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref revData, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                return;
            }

            if (m_isM2FirstSample)
            {
                revData = 0.3f;
                m_isM2FirstSample = false;
            }

            float timeSpan = (float)(dt - m_M2BaseTime).TotalSeconds;
            m_M2revData[m_M2DataCount].Y = revData;
            m_M2revData[m_M2DataCount].X = timeSpan;
            string sDisType = ContentValue("CurveInfo", "DisType", m_INICurveInfoFilePath);
            if (sDisType == "压缩曲线")
            {
                if (timeSpan > m_M2xMax)
                {
                    m_M2xMax += m_xSpan;
                }
            }
            m_M2DataCount++;
            if (m_M2DataCount >= 5)
            {
                bool ret = m_DrawCurveBench2Handle.SavePointToList(m_M2revData);
                if (!ret)
                {
                    SendDebugInfo("TestForm 向绘图控件保存数据失败，工位二");
                }
                m_M2DataCount = 0;
            }
        }

        private bool m_isM3FirstSample = true;
        private PointF[] m_M3revData = new PointF[5];
        private int m_M3DataCount = 0;
        private void SampleM3Data()
        {
            string regName = ContentValue("CurveInfo", "RegNameM3", m_INICurveInfoFilePath);
            float revData = 0.0f;
            DateTime dt = default(DateTime);
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref revData, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                return;
            }

            if (m_isM3FirstSample)
            {
                revData = 0.3f;
                m_isM3FirstSample = false;
            }

            float timeSpan = (float)(dt - m_M3BaseTime).TotalSeconds;
            m_M3revData[m_M3DataCount].Y = revData;
            m_M3revData[m_M3DataCount].X = timeSpan;
            string sDisType = ContentValue("CurveInfo", "DisType", m_INICurveInfoFilePath);
            if (sDisType == "压缩曲线")
            {
                if (timeSpan > m_M3xMax)
                {
                    m_M3xMax += m_xSpan;
                }
            }
            m_M3DataCount++;
            if (m_M3DataCount >= 5)
            {
                bool ret = m_DrawCurveBench3Handle.SavePointToList(m_M3revData);
                if (!ret)
                {
                    SendDebugInfo("TestForm 向绘图控件保存数据失败，工位三");
                }
                m_M3DataCount = 0;
            }
        }

        private bool m_isM4FirstSample = true;
        private PointF[] m_M4revData = new PointF[5];
        private int m_M4DataCount = 0;
        private void SampleM4Data()
        {
            string regName = ContentValue("CurveInfo", "RegNameM4", m_INICurveInfoFilePath);
            float revData = 0.0f;
            DateTime dt = default(DateTime);
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref revData, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                return;
            }

            if (m_isM4FirstSample)
            {
                revData = 0.3f;
                m_isM4FirstSample = false;
            }

            float timeSpan = (float)(dt - m_M4BaseTime).TotalSeconds;
            m_M4revData[m_M4DataCount].Y = revData;
            m_M4revData[m_M4DataCount].X = timeSpan;
            string sDisType = ContentValue("CurveInfo", "DisType", m_INICurveInfoFilePath);
            if (sDisType == "压缩曲线")
            {
                if (timeSpan > m_M4xMax)
                {
                    m_M4xMax += m_xSpan;
                }
            }
            m_M4DataCount++;
            if (m_M4DataCount >= 5)
            {
                bool ret = m_DrawCurveBench4Handle.SavePointToList(m_M4revData);
                if (!ret)
                {
                    SendDebugInfo("TestForm 向绘图控件保存数据失败，工位四");
                }
                m_M4DataCount = 0;
            }
        }
        #endregion

        #region 采集启动、停止标志
        private void SampleTestFlag()
        {
            if (m_isM1Enable)
            {
                SampleM1Flag();
            }
            if (m_isM2Enable)
            {
                SampleM2Flag();
            }
            if (m_isM3Enable)
            {
                SampleM3Flag();
            }
            if (m_isM4Enable)
            {
                SampleM4Flag();
            }

            if (!m_isM1Testing && !m_isM2Testing && !m_isM3Testing && m_isM4Testing)
            {
                m_isStartSample = false;
                m_MainFormHandle.m_isTesting = false;
            }
        }

        private bool m_isM1Testing = false;
        private void SampleM1Flag()
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);

            DateTime dt = default(DateTime);
            byte startFlag = 0;
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref startFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                return;
            }
            if (startFlag == 1)
            {
                m_isM1Testing = true;
                m_MainFormHandle.m_isTesting = true;
                m_isStartSample = true;
                startFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, startFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位一");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench1Lists[0].m_State[0] = state[0];
                TB_Save.Enabled = false;
                TB_CancelSave.Enabled = false;
                m_M1BaseTime = DateTime.Now;
                m_DrawCurveBench1Handle.ClearPointsInList();
            }

            BName = "BTStop";
            INIPath = m_INIToolBar01FilePath;
            regName = ContentValue(BName, "RegName", INIPath);
            byte stopFlag = 0;
            code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref stopFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                return;
            }
            if (stopFlag == 1)
            {
                m_isM1Testing = false;
                stopFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, stopFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位一");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench1Lists[1].m_State[0] = state[0];
                TB_Save.Enabled = true;
                TB_CancelSave.Enabled = true;
                m_M1EndTime = DateTime.Now;
            }
        }

        private bool m_isM2Testing = false;
        private void SampleM2Flag()
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);

            DateTime dt = default(DateTime);
            byte startFlag = 0;
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref startFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                return;
            }
            if (startFlag == 1)
            {
                m_isM2Testing = true;
                m_MainFormHandle.m_isTesting = true;
                m_isStartSample = true;
                startFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, startFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位二");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench2Lists[0].m_State[0] = state[0];
                TB_Save_2.Enabled = false;
                TB_CancelSave_2.Enabled = false;
                m_M2BaseTime = DateTime.Now;
                m_DrawCurveBench2Handle.ClearPointsInList();
            }

            BName = "BTStop";
            INIPath = m_INIToolBar02FilePath;
            regName = ContentValue(BName, "RegName", INIPath);
            byte stopFlag = 0;
            code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref stopFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                return;
            }
            if (stopFlag == 1)
            {
                m_isM2Testing = false;
                stopFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, stopFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位二");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench2Lists[1].m_State[0] = state[0];
                TB_Save_2.Enabled = true;
                TB_CancelSave_2.Enabled = true;
                m_M2EndTime = DateTime.Now;
            }
        }

        private bool m_isM3Testing = false;
        private void SampleM3Flag()
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);

            DateTime dt = default(DateTime);
            byte startFlag = 0;
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref startFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                return;
            }
            if (startFlag == 1)
            {
                m_isM3Testing = true;
                m_MainFormHandle.m_isTesting = true;
                m_isStartSample = true;
                startFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, startFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位三");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench3Lists[0].m_State[0] = state[0];
                TB_Save_3.Enabled = false;
                TB_CancelSave_3.Enabled = false;
                m_M3BaseTime = DateTime.Now;
                m_DrawCurveBench3Handle.ClearPointsInList();
            }

            BName = "BTStop";
            INIPath = m_INIToolBar03FilePath;
            regName = ContentValue(BName, "RegName", INIPath);
            byte stopFlag = 0;
            code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref stopFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                return;
            }
            if (stopFlag == 1)
            {
                m_isM3Testing = false;
                stopFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, stopFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位三");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench3Lists[1].m_State[0] = state[0];
                TB_Save_3.Enabled = true;
                TB_CancelSave_3.Enabled = true;
                m_M3EndTime = DateTime.Now;
            }
        }

        private bool m_isM4Testing = false;
        private void SampleM4Flag()
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);

            DateTime dt = default(DateTime);
            byte startFlag = 0;
            int code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref startFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                return;
            }
            if (startFlag == 1)
            {
                m_isM4Testing = true;
                m_MainFormHandle.m_isTesting = true;
                m_isStartSample = true;
                startFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, startFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位四");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench4Lists[0].m_State[0] = state[0];
                TB_Save_4.Enabled = false;
                TB_CancelSave_4.Enabled = false;
                m_M4BaseTime = DateTime.Now;
                m_DrawCurveBench4Handle.ClearPointsInList();
            }

            BName = "BTStop";
            INIPath = m_INIToolBar04FilePath;
            regName = ContentValue(BName, "RegName", INIPath);
            byte stopFlag = 0;
            code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref stopFlag, ref dt);
            if (code != 1)
            {
                SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                return;
            }
            if (stopFlag == 1)
            {
                m_isM4Testing = false;
                stopFlag = 0;
                code = m_MainFormHandle.m_DriveHandle.WriteData(regName, stopFlag);
                if (code != 1)
                {
                    SendDebugInfo("TestForm 向PLC设置参数失败，定位：" + regName + "工位四");
                }
                string[] state = new string[1];
                state[0] = dt.ToLongTimeString();
                m_RuntimeInfoBench4Lists[1].m_State[0] = state[0];
                TB_Save_4.Enabled = true;
                TB_CancelSave_4.Enabled = true;
                m_M4EndTime = DateTime.Now;
            }
        }
        #endregion

        #region 采集实时信息
        private void SampleRuntimeInfo()
        {
            if (!m_isStartSample)
            {
                return;
            }
            if (m_isM1Enable)
            {
                SampleM1RuntimeInfo();
            }
            if (m_isM2Enable)
            {
                SampleM2RuntimeInfo();
            }
            if (m_isM3Enable)
            {
                SampleM3RuntimeInfo();
            }
            if (m_isM4Enable)
            {
                SampleM4RuntimeInfo();
            }
        }

        private void SampleM1RuntimeInfo()
        {
            string regName = "";
            DateTime dt = default(DateTime);

            int rows = DG_W1.RowCount;
            int columns = DG_W1.ColumnCount;
            int count = m_RuntimeInfoBench1Lists.Count;
            int code = 0;
            for(int i = 0; i < rows; i+=2)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i * columns + j) > count)
                    {
                        break;
                    }
                    string regNameCH = DG_W1.Rows[i].Cells[j].Value.ToString();
                    int index = m_RuntimeInfoBench1Lists.FindIndex(r => r.m_RegNameCH == regNameCH);
                    if (index < 0)
                    {
                        continue;
                    }
                    int Num = Convert.ToInt32(m_RuntimeInfoBench1Lists[index].m_DataNum);
                    regName = m_RuntimeInfoBench1Lists[index].m_RegName;

                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "DateTime")
                    {
                        DG_W1.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench1Lists[index].m_State[0];
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench1Lists[index].m_State[0];
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "bit" || m_RuntimeInfoBench1Lists[index].m_DataType == "byte")
                    {
                        sbyte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        if (Num > 1)
                        {
                            DG_Main_W1.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench1Lists[index].m_State[data];
                            DG_W1.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench1Lists[index].m_State[data];
                        }
                        if (Num == 1)
                        {
                            DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                            DG_W1.Rows[i + 1].Cells[j].Value = data;
                        }
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "ubyte")
                    {
                        byte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                        DG_W1.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "word")
                    {
                        Int16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                        DG_W1.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "uword")
                    {
                        UInt16 data = 0;
                        code  = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                        DG_W1.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "dword")
                    {
                        Int32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                        DG_W1.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "udword")
                    {
                        UInt32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                        DG_W1.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench1Lists[index].m_DataType == "float")
                    {
                        float data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位一");
                        }
                        DG_Main_W1.Rows[i + 1].Cells[j].Value = data;
                        DG_W1.Rows[i + 1].Cells[j].Value = data;
                    }
                }
            }
        }

        private void SampleM2RuntimeInfo()
        {
            string regName = "";
            DateTime dt = default(DateTime);

            int rows = DG_W2.RowCount;
            int columns = DG_W2.ColumnCount;
            int count = m_RuntimeInfoBench2Lists.Count;
            int code = 0;
            for (int i = 0; i < rows; i += 2)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i * columns + j) > count)
                    {
                        break;
                    }
                    string regNameCH = DG_W2.Rows[i].Cells[j].Value.ToString();
                    int index = m_RuntimeInfoBench2Lists.FindIndex(r => r.m_RegNameCH == regNameCH);
                    if (index < 0)
                    {
                        continue;
                    }
                    int Num = Convert.ToInt32(m_RuntimeInfoBench2Lists[index].m_DataNum);
                    regName = m_RuntimeInfoBench2Lists[index].m_RegName;

                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "DateTime")
                    {
                        DG_W2.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench2Lists[index].m_State[0];
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench2Lists[index].m_State[0];
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "bit" || m_RuntimeInfoBench2Lists[index].m_DataType == "byte")
                    {
                        sbyte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        if (Num > 1)
                        {
                            DG_Main_W2.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench2Lists[index].m_State[data];
                            DG_W2.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench2Lists[index].m_State[data];
                        }
                        if (Num == 1)
                        {
                            DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                            DG_W2.Rows[i + 1].Cells[j].Value = data;
                        }
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "ubyte")
                    {
                        byte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                        DG_W2.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "word")
                    {
                        Int16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                        DG_W2.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "uword")
                    {
                        UInt16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                        DG_W2.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "dword")
                    {
                        Int32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                        DG_W2.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "udword")
                    {
                        UInt32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                        DG_W2.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench2Lists[index].m_DataType == "float")
                    {
                        float data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位二");
                        }
                        DG_Main_W2.Rows[i + 1].Cells[j].Value = data;
                        DG_W2.Rows[i + 1].Cells[j].Value = data;
                    }
                }
            }
        }

        private void SampleM3RuntimeInfo()
        {
            string regName = "";
            DateTime dt = default(DateTime);

            int rows = DG_W3.RowCount;
            int columns = DG_W3.ColumnCount;
            int count = m_RuntimeInfoBench3Lists.Count;
            int code = 0;
            for (int i = 0; i < rows; i += 2)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i * columns + j) > count)
                    {
                        break;
                    }
                    string regNameCH = DG_W3.Rows[i].Cells[j].Value.ToString();
                    int index = m_RuntimeInfoBench3Lists.FindIndex(r => r.m_RegNameCH == regNameCH);
                    if (index < 0)
                    {
                        continue;
                    }
                    int Num = Convert.ToInt32(m_RuntimeInfoBench3Lists[index].m_DataNum);
                    regName = m_RuntimeInfoBench3Lists[index].m_RegName;

                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "DateTime")
                    {
                        DG_W3.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench3Lists[index].m_State[0];
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench3Lists[index].m_State[0];
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "bit" || m_RuntimeInfoBench3Lists[index].m_DataType == "byte")
                    {
                        sbyte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        if (Num > 1)
                        {
                            DG_Main_W3.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench3Lists[index].m_State[data];
                            DG_W3.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench3Lists[index].m_State[data];
                        }
                        if (Num == 1)
                        {
                            DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                            DG_W3.Rows[i + 1].Cells[j].Value = data;
                        }
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "ubyte")
                    {
                        byte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "word")
                    {
                        Int16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "uword")
                    {
                        UInt16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "dword")
                    {
                        Int32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "udword")
                    {
                        UInt32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "float")
                    {
                        float data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位三");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                }
            }
        }

        private void SampleM4RuntimeInfo()
        {
            string regName = "";
            DateTime dt = default(DateTime);

            int rows = DG_W4.RowCount;
            int columns = DG_W4.ColumnCount;
            int count = m_RuntimeInfoBench4Lists.Count;
            int code = 0;
            for (int i = 0; i < rows; i += 2)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i * columns + j) > count)
                    {
                        break;
                    }
                    string regNameCH = DG_W4.Rows[i].Cells[j].Value.ToString();
                    int index = m_RuntimeInfoBench4Lists.FindIndex(r => r.m_RegNameCH == regNameCH);
                    if (index < 0)
                    {
                        continue;
                    }
                    int Num = Convert.ToInt32(m_RuntimeInfoBench4Lists[index].m_DataNum);
                    regName = m_RuntimeInfoBench4Lists[index].m_RegName;

                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "DateTime")
                    {
                        DG_W4.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench4Lists[index].m_State[0];
                        DG_Main_W4.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench4Lists[index].m_State[0];
                    }
                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "bit" || m_RuntimeInfoBench4Lists[index].m_DataType == "byte")
                    {
                        sbyte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        if (Num > 1)
                        {
                            DG_Main_W4.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench4Lists[index].m_State[data];
                            DG_W4.Rows[i + 1].Cells[j].Value = m_RuntimeInfoBench4Lists[index].m_State[data];
                        }
                        if (Num == 1)
                        {
                            DG_Main_W4.Rows[i + 1].Cells[j].Value = data;
                            DG_W4.Rows[i + 1].Cells[j].Value = data;
                        }
                    }
                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "ubyte")
                    {
                        byte data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        DG_Main_W4.Rows[i + 1].Cells[j].Value = data;
                        DG_W4.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "word")
                    {
                        Int16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        DG_Main_W4.Rows[i + 1].Cells[j].Value = data;
                        DG_W4.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "uword")
                    {
                        UInt16 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        DG_Main_W4.Rows[i + 1].Cells[j].Value = data;
                        DG_W4.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench3Lists[index].m_DataType == "dword")
                    {
                        Int32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        DG_Main_W3.Rows[i + 1].Cells[j].Value = data;
                        DG_W3.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "udword")
                    {
                        UInt32 data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        DG_Main_W4.Rows[i + 1].Cells[j].Value = data;
                        DG_W4.Rows[i + 1].Cells[j].Value = data;
                    }
                    if (m_RuntimeInfoBench4Lists[index].m_DataType == "float")
                    {
                        float data = 0;
                        code = m_MainFormHandle.m_DriveHandle.ReadData(regName, ref data, ref dt);
                        if (code != 1)
                        {
                            SendDebugInfo("TestForm 向PLC读取参数失败，定位：" + regName + "工位四");
                        }
                        DG_Main_W4.Rows[i + 1].Cells[j].Value = data;
                        DG_W4.Rows[i + 1].Cells[j].Value = data;
                    }
                }
            }
        }

        #endregion

        #region 绘图
        private void DrawRuntimeCurve()
        {
            if (!m_isStartSample)
            {
                return;
            }
            if (m_isM1Enable)
            {
                DrawM1RuntimeCurve();
            }

            if (m_isM2Enable)
            {
                DrawM2RuntimeCurve();
            }

            if (m_isM3Enable)
            {
                DrawM3RuntimeCurve();
            }

            if (m_isM4Enable)
            {
                DrawM4RuntimeCurve();
            }
        }

        private void DrawM1RuntimeCurve()
        {
            m_DrawCurveBench1Handle.DrawCurve(panel_W1, m_LineColor, m_M1xMax, m_M1yMax);
            FlashM1Lable();
        }

        private void FlashM1Lable()
        {
            //y
            label1.Text = m_M1yMax.ToString() + "MPa";
            label7.Text = "0";
            int interVal = (int)m_M1yMax / 6;
            label6.Text = interVal.ToString() + "MPa";
            label5.Text = (2 * interVal).ToString() + "MPa";
            label4.Text = (3 * interVal).ToString() + "MPa";
            label3.Text = (4 * interVal).ToString() + "MPa";
            label2.Text = (5 * interVal).ToString() + "MPa";

            //x          
            if (m_DrawCurveType == "压缩曲线")
            {
                label10.Text = m_M1BaseTime.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M1xMax / 8, m_M1BaseTime, ref H, ref M, ref S);
                label11.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 2 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label12.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 3 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label13.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 4 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label14.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 5 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label15.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 6 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label16.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 7 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label17.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 8 / 8, m_M1BaseTime, ref H, ref M, ref S);
                label18.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            }
            if (m_DrawCurveType == "平移曲线")
            {
                DateTime dt = DateTime.Now;
                label18.Text = dt.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M1xMax / 8, dt, ref H, ref M, ref S);
                label17.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 2 / 8, dt, ref H, ref M, ref S);
                label16.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 3 / 8, dt, ref H, ref M, ref S);
                label15.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 4 / 8, dt, ref H, ref M, ref S);
                label14.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 5 / 8, dt, ref H, ref M, ref S);
                label13.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 6 / 8, dt, ref H, ref M, ref S);
                label12.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 7 / 8, dt, ref H, ref M, ref S);
                label11.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M1xMax * 8 / 8, dt, ref H, ref M, ref S);
                label10.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();

            }
        }

        private void DrawM2RuntimeCurve()
        {
            m_DrawCurveBench1Handle.DrawCurve(panel_W2, m_LineColor, m_M2xMax, m_M2yMax);
            FlashM2Lable();
        }

        private void FlashM2Lable()
        {
            //y
            label36.Text = m_M2yMax.ToString() + "MPa";
            label30.Text = "0";
            int interVal = (int)m_M2yMax / 6;
            label31.Text = interVal.ToString() + "MPa";
            label32.Text = (2 * interVal).ToString() + "MPa";
            label33.Text = (3 * interVal).ToString() + "MPa";
            label34.Text = (4 * interVal).ToString() + "MPa";
            label35.Text = (5 * interVal).ToString() + "MPa";

            //x          
            if (m_DrawCurveType == "压缩曲线")
            {
                label29.Text = m_M2BaseTime.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M2xMax / 8, m_M2BaseTime, ref H, ref M, ref S);
                label28.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 2 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label27.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 3 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label26.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 4 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label25.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 5 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label24.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 6 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label23.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 7 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label22.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 8 / 8, m_M2BaseTime, ref H, ref M, ref S);
                label21.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            }
            if (m_DrawCurveType == "平移曲线")
            {
                DateTime dt = DateTime.Now;
                label21.Text = dt.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M2xMax / 8, dt, ref H, ref M, ref S);
                label22.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 2 / 8, dt, ref H, ref M, ref S);
                label23.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 3 / 8, dt, ref H, ref M, ref S);
                label24.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 4 / 8, dt, ref H, ref M, ref S);
                label25.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 5 / 8, dt, ref H, ref M, ref S);
                label26.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 6 / 8, dt, ref H, ref M, ref S);
                label27.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 7 / 8, dt, ref H, ref M, ref S);
                label28.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M2xMax * 8 / 8, dt, ref H, ref M, ref S);
                label29.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();

            }
        }

        private void DrawM3RuntimeCurve()
        {
            m_DrawCurveBench1Handle.DrawCurve(panel_W3, m_LineColor, m_M3xMax, m_M3yMax);
        }

        private void FlashM3Lable()
        {
            //y
            label54.Text = m_M3yMax.ToString() + "MPa";
            label48.Text = "0";
            int interVal = (int)m_M3yMax / 6;
            label49.Text = interVal.ToString() + "MPa";
            label50.Text = (2 * interVal).ToString() + "MPa";
            label51.Text = (3 * interVal).ToString() + "MPa";
            label52.Text = (4 * interVal).ToString() + "MPa";
            label53.Text = (5 * interVal).ToString() + "MPa";

            //x          
            if (m_DrawCurveType == "压缩曲线")
            {
                label47.Text = m_M3BaseTime.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M3xMax / 8, m_M3BaseTime, ref H, ref M, ref S);
                label46.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 2 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label45.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 3 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label44.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 4 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label43.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 5 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label42.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 6 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label41.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 7 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label40.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 8 / 8, m_M3BaseTime, ref H, ref M, ref S);
                label39.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            }
            if (m_DrawCurveType == "平移曲线")
            {
                DateTime dt = DateTime.Now;
                label39.Text = dt.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M3xMax / 8, dt, ref H, ref M, ref S);
                label40.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 2 / 8, dt, ref H, ref M, ref S);
                label41.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 3 / 8, dt, ref H, ref M, ref S);
                label42.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 4 / 8, dt, ref H, ref M, ref S);
                label43.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 5 / 8, dt, ref H, ref M, ref S);
                label44.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 6 / 8, dt, ref H, ref M, ref S);
                label45.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 7 / 8, dt, ref H, ref M, ref S);
                label46.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M3xMax * 8 / 8, dt, ref H, ref M, ref S);
                label47.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();

            }
        }

        private void DrawM4RuntimeCurve()
        {
            m_DrawCurveBench1Handle.DrawCurve(panel_W4, m_LineColor, m_M4xMax, m_M4yMax);
        }

        private void FlashM4Lable()
        {
            //y
            label72.Text = m_M4yMax.ToString() + "MPa";
            label66.Text = "0";
            int interVal = (int)m_M4yMax / 6;
            label67.Text = interVal.ToString() + "MPa";
            label68.Text = (2 * interVal).ToString() + "MPa";
            label69.Text = (3 * interVal).ToString() + "MPa";
            label70.Text = (4 * interVal).ToString() + "MPa";
            label71.Text = (5 * interVal).ToString() + "MPa";

            //x          
            if (m_DrawCurveType == "压缩曲线")
            {
                label65.Text = m_M4BaseTime.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M4xMax / 8, m_M4BaseTime, ref H, ref M, ref S);
                label64.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 2 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label63.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 3 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label62.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 4 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label61.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 5 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label60.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 6 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label59.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 7 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label58.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 8 / 8, m_M4BaseTime, ref H, ref M, ref S);
                label57.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
            }
            if (m_DrawCurveType == "平移曲线")
            {
                DateTime dt = DateTime.Now;
                label57.Text = dt.ToLongDateString();
                int H = 0;
                int M = 0;
                int S = 0;
                GetLableTimeBaseCompress((int)m_M4xMax / 8, dt, ref H, ref M, ref S);
                label58.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 2 / 8, dt, ref H, ref M, ref S);
                label59.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 3 / 8, dt, ref H, ref M, ref S);
                label60.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 4 / 8, dt, ref H, ref M, ref S);
                label61.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 5 / 8, dt, ref H, ref M, ref S);
                label62.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 6 / 8, dt, ref H, ref M, ref S);
                label63.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 7 / 8, dt, ref H, ref M, ref S);
                label64.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();
                GetLableTimeBaseCompress((int)m_M4xMax * 8 / 8, dt, ref H, ref M, ref S);
                label65.Text = H.ToString() + ":" + M.ToString() + ":" + S.ToString();

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

        private void GetLableTimeBaseShift(int t, DateTime EndTime, ref int H, ref int M, ref int S)
        {
            H = (int)(t / 3600);
            M = (int)((t - H * 3600) / 60);
            S = (int)((t - H * 3600) % 60);

            S = EndTime.Second - S;
            if (S < 0)
            {
                M--;
                S += 60;
            }
            M = EndTime.Minute - M;
            if (M < 0)
            {
                H--;
                M += 60;
            }
            H = EndTime.Hour - H;
            if (H < 0)
            {
                H += 24;
            }
        }

        #endregion

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void TB_Exit_Click(object sender, EventArgs e)
        {
            if (m_isStartSample)
            {
                MessageBox.Show("请先停止实验！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

        #region 工位一Button
        private void TB_Exit_01_Click(object sender, EventArgs e)
        {
            if (m_isStartSample)
            {
                MessageBox.Show("请先停止实验！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Start_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save.Enabled = false;
            TB_CancelSave.Enabled = false;
            m_M1BaseTime = DateTime.Now;
            m_DrawCurveBench1Handle.ClearPointsInList();
        }

        private void TB_End_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save.Enabled = true;
            TB_CancelSave.Enabled = true;
            m_M1EndTime = DateTime.Now;

        }

        private void BTCustom01_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom02_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom03_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom04_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom05_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar01FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_Save_Click(object sender, EventArgs e)
        {
            bool code = m_DBM1Handle.AddDBtoBuffer();
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddDBToBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index = -1;
            code = m_DBM1Handle.AddRows(m_TableName, ref index);
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddRows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_DBM1Handle.ClearBuffer();
                return;
            }

            string userName = m_UserManagerHandle.m_CurrenUser;
            code = m_DBM1Handle.WriteSigleDataToDB(m_TableName, index, "UserName", userName);
            if (!code)
            {
                m_DBM1Handle.ClearBuffer();
                MessageBox.Show("写入数据库失败失败，定位：用户名保存", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string regName = "";
            //存储实时信息
            int count = m_RuntimeInfoBench1Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_RuntimeInfoBench1Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_RuntimeInfoBench1Lists[i].m_RegName;
                string dataType = m_RuntimeInfoBench1Lists[i].m_DataType;
                code = m_DBM1Handle.WriteSigleDataToDB(m_TableName, index, regName, m_RuntimeInfoBench1Lists[i].m_State[0]);
                if (!code)
                {
                    m_DBM1Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：存储实时信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //报表信息
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM1Handle.WriteSigleDataToDB(m_TableName, index, regName, m_ReportParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM1Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：报表信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //设置参数
            count = m_SetParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaLists[i].m_RegName;
                code = m_DBM1Handle.WriteSigleDataToDB(m_TableName, index, regName, m_SetParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM1Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //曲线参数
            string isSave = ContentValue("CurveInfo", "SaveM1", m_INICurveInfoFilePath);
            if (isSave == "True")
            {
                PointF[] pt;
                m_DrawCurveBench1Handle.GetPointFromList(out pt);
                if (pt.Length <= 1)
                {
                    m_DBM1Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：曲线参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int length = pt.Length;
                float[] pt_X = new float[length];
                float[] pt_Y = new float[length];
                for (int i = 0; i < length; i++)
                {
                    pt_X[i] = pt[i].X;
                    pt_Y[i] = pt[i].Y;
                }

                regName = ContentValue("CurveInfo", "RegNameM1", m_INICurveInfoFilePath);
                code = m_DBM1Handle.WriteFloatsToDB(m_TableName, index, regName, pt_X);
                if (!code)
                {
                    m_DBM1Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                code = m_DBM1Handle.WriteFloatsToDB(m_TableName, index, regName, pt_Y);
                if (!code)
                {
                    m_DBM1Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            m_DBM1Handle.SaveDataToBuffer();
            m_DBM1Handle.SaveDateToDataBase();
            m_DBM1Handle.ClearBuffer();
            MessageBox.Show("写入数据库成功。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_CancelSave_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            MsgBoxResult = MessageBox.Show("确定不保存本组数据吗？",//对话框的显示内容
            "提示",//对话框的标题
            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
            MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                m_DrawCurveBench1Handle.ClearPointsInList();
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }
        #endregion      

        #region 工位二Button
        private void TB_Exit_2_Click(object sender, EventArgs e)
        {
            if (m_isStartSample)
            {
                MessageBox.Show("请先停止实验！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Start_2_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save_2.Enabled = false;
            TB_CancelSave_2.Enabled = false;
            m_M2BaseTime = DateTime.Now;
            m_DrawCurveBench2Handle.ClearPointsInList();
        }

        private void TB_End_2_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save_2.Enabled = true;
            TB_CancelSave_2.Enabled = true;
            m_M2EndTime = DateTime.Now;
        }

        private void BTCustom01_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BTCustom02_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom03_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom04_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom05_2_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar02FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_Save_2_Click(object sender, EventArgs e)
        {
            bool code = m_DBM2Handle.AddDBtoBuffer();
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddDBToBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index = -1;
            code = m_DBM2Handle.AddRows(m_TableName, ref index);
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddRows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_DBM2Handle.ClearBuffer();
                return;
            }

            string userName = m_UserManagerHandle.m_CurrenUser;
            code = m_DBM2Handle.WriteSigleDataToDB(m_TableName, index, "UserName", userName);
            if (!code)
            {
                m_DBM2Handle.ClearBuffer();
                MessageBox.Show("写入数据库失败失败，定位：用户名", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string regName = "";
            //存储实时信息
            int count = m_RuntimeInfoBench2Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_RuntimeInfoBench2Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_RuntimeInfoBench2Lists[i].m_RegName;
                string dataType = m_RuntimeInfoBench2Lists[i].m_DataType;
                code = m_DBM2Handle.WriteSigleDataToDB(m_TableName, index, regName, m_RuntimeInfoBench2Lists[i].m_State[0]);
                if (!code)
                {
                    m_DBM2Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：存储实时信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //报表信息
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM2Handle.WriteSigleDataToDB(m_TableName, index, regName, m_ReportParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM2Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：报表信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //设置参数
            count = m_SetParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaLists[i].m_RegName;
                code = m_DBM2Handle.WriteSigleDataToDB(m_TableName, index, regName, m_SetParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM2Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //曲线参数
            string isSave = ContentValue("CurveInfo", "SaveM2", m_INICurveInfoFilePath);
            if (isSave == "True")
            {
                PointF[] pt;
                m_DrawCurveBench2Handle.GetPointFromList(out pt);
                if (pt.Length <= 1)
                {
                    m_DBM2Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：曲线参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int length = pt.Length;
                float[] pt_X = new float[length];
                float[] pt_Y = new float[length];
                for (int i = 0; i < length; i++)
                {
                    pt_X[i] = pt[i].X;
                    pt_Y[i] = pt[i].Y;
                }

                regName = ContentValue("CurveInfo", "RegNameM2", m_INICurveInfoFilePath);
                code = m_DBM2Handle.WriteFloatsToDB(m_TableName, index, regName, pt_X);
                if (!code)
                {
                    m_DBM2Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                code = m_DBM2Handle.WriteFloatsToDB(m_TableName, index, regName, pt_Y);
                if (!code)
                {
                    m_DBM2Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            m_DBM2Handle.SaveDataToBuffer();
            m_DBM2Handle.SaveDateToDataBase();
            m_DBM2Handle.ClearBuffer();
            MessageBox.Show("写入数据库成功。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_CancelSave_2_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            MsgBoxResult = MessageBox.Show("确定不保存本组数据吗？",//对话框的显示内容
            "提示",//对话框的标题
            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
            MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                m_DrawCurveBench2Handle.ClearPointsInList();
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }
        #endregion

        #region 工位三Button
        private void TB_Exit_3_Click(object sender, EventArgs e)
        {
            if (m_isStartSample)
            {
                MessageBox.Show("请先停止实验！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Start_3_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save_3.Enabled = false;
            TB_CancelSave_3.Enabled = false;
            m_M3BaseTime = DateTime.Now;
            m_DrawCurveBench3Handle.ClearPointsInList();
        }

        private void TB_End_3_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save_3.Enabled = true;
            TB_CancelSave_3.Enabled = true;
            m_M3EndTime = DateTime.Now;
        }

        private void BTCustom01_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom02_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom03_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom04_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom05_3_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar03FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_Save_3_Click(object sender, EventArgs e)
        {
            bool code = m_DBM3Handle.AddDBtoBuffer();
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddDBToBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index = -1;
            code = m_DBM3Handle.AddRows(m_TableName, ref index);
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddRows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_DBM3Handle.ClearBuffer();
                return;
            }

            string userName = m_UserManagerHandle.m_CurrenUser;
            code = m_DBM3Handle.WriteSigleDataToDB(m_TableName, index, "UserName", userName);
            if (!code)
            {
                m_DBM3Handle.ClearBuffer();
                MessageBox.Show("写入数据库失败失败，定位：用户名", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string regName = "";
            //存储实时信息
            int count = m_RuntimeInfoBench3Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_RuntimeInfoBench3Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_RuntimeInfoBench3Lists[i].m_RegName;
                string dataType = m_RuntimeInfoBench3Lists[i].m_DataType;
                code = m_DBM3Handle.WriteSigleDataToDB(m_TableName, index, regName, m_RuntimeInfoBench3Lists[i].m_State[0]);
                if (!code)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：存储实时信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //报表信息
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM3Handle.WriteSigleDataToDB(m_TableName, index, regName, m_ReportParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：报表信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //设置参数
            count = m_SetParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaLists[i].m_RegName;
                code = m_DBM3Handle.WriteSigleDataToDB(m_TableName, index, regName, m_SetParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //曲线参数
            string isSave = ContentValue("CurveInfo", "SaveM3", m_INICurveInfoFilePath);
            if (isSave == "True")
            {
                PointF[] pt;
                m_DrawCurveBench3Handle.GetPointFromList(out pt);
                if (pt.Length <= 1)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：曲线参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int length = pt.Length;
                float[] pt_X = new float[length];
                float[] pt_Y = new float[length];
                for (int i = 0; i < length; i++)
                {
                    pt_X[i] = pt[i].X;
                    pt_Y[i] = pt[i].Y;
                }

                regName = ContentValue("CurveInfo", "RegNameM3", m_INICurveInfoFilePath);
                code = m_DBM3Handle.WriteFloatsToDB(m_TableName, index, regName, pt_X);
                if (!code)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                code = m_DBM3Handle.WriteFloatsToDB(m_TableName, index, regName, pt_Y);
                if (!code)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            m_DBM3Handle.SaveDataToBuffer();
            m_DBM3Handle.SaveDateToDataBase();
            m_DBM3Handle.ClearBuffer();
            MessageBox.Show("写入数据库成功。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_CancelSave_3_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            MsgBoxResult = MessageBox.Show("确定不保存本组数据吗？",//对话框的显示内容
            "提示",//对话框的标题
            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
            MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                m_DrawCurveBench3Handle.ClearPointsInList();
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }
        #endregion

        #region 工位四Button

        private void TB_Exit_4_Click(object sender, EventArgs e)
        {
            if (m_isStartSample)
            {
                MessageBox.Show("请先停止实验！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
            UpdateMainForm("主界面");
        }

        private void TB_Start_4_Click(object sender, EventArgs e)
        {
            string BName = "BTStart";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save_4.Enabled = false;
            TB_CancelSave_4.Enabled = false;
            m_M4BaseTime = DateTime.Now;
            m_DrawCurveBench4Handle.ClearPointsInList();
        }

        private void TB_End_4_Click(object sender, EventArgs e)
        {
            string BName = "BTStop";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_Save_4.Enabled = true;
            TB_CancelSave_4.Enabled = true;
            m_M4EndTime = DateTime.Now;
        }

        private void BTCustom01_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom01";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom02_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom02";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom03_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom03";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom04_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom04";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTCustom05_4_Click(object sender, EventArgs e)
        {
            string BName = "BTCustom05";
            string INIPath = m_INIToolBar04FilePath;
            string regName = ContentValue(BName, "RegName", INIPath);
            string regNameCH = ContentValue(BName, "RegNameCH", INIPath);

            byte data = 1;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(regName, data);
            if (code != 1)
            {
                MessageBox.Show(regNameCH + "失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(regNameCH + "设置成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_Save_4_Click(object sender, EventArgs e)
        {
            bool code = m_DBM4Handle.AddDBtoBuffer();
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddDBToBuffer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index = -1;
            code = m_DBM4Handle.AddRows(m_TableName, ref index);
            if (!code)
            {
                MessageBox.Show("加载数据库失败失败，定位：AddRows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_DBM4Handle.ClearBuffer();
                return;
            }

            string userName = m_UserManagerHandle.m_CurrenUser;
            code = m_DBM4Handle.WriteSigleDataToDB(m_TableName, index, "UserName", userName);
            if (!code)
            {
                m_DBM4Handle.ClearBuffer();
                MessageBox.Show("写入数据库失败失败，定位：用户名", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string regName = "";
            //存储实时信息
            int count = m_RuntimeInfoBench4Lists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_RuntimeInfoBench4Lists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_RuntimeInfoBench4Lists[i].m_RegName;
                string dataType = m_RuntimeInfoBench4Lists[i].m_DataType;
                code = m_DBM4Handle.WriteSigleDataToDB(m_TableName, index, regName, m_RuntimeInfoBench4Lists[i].m_State[0]);
                if (!code)
                {
                    m_DBM4Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：存储实时信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //报表信息
            count = m_ReportParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_ReportParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_ReportParaLists[i].m_ParaName;
                code = m_DBM4Handle.WriteSigleDataToDB(m_TableName, index, regName, m_ReportParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM4Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：报表信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //设置参数
            count = m_SetParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_SetParaLists[i].m_isSave != "y")
                {
                    continue;
                }
                regName = m_SetParaLists[i].m_RegName;
                code = m_DBM4Handle.WriteSigleDataToDB(m_TableName, index, regName, m_SetParaLists[i].m_Value);
                if (!code)
                {
                    m_DBM4Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //曲线参数
            string isSave = ContentValue("CurveInfo", "SaveM3", m_INICurveInfoFilePath);
            if (isSave == "True")
            {
                PointF[] pt;
                m_DrawCurveBench4Handle.GetPointFromList(out pt);
                if (pt.Length <= 1)
                {
                    m_DBM3Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：曲线参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int length = pt.Length;
                float[] pt_X = new float[length];
                float[] pt_Y = new float[length];
                for (int i = 0; i < length; i++)
                {
                    pt_X[i] = pt[i].X;
                    pt_Y[i] = pt[i].Y;
                }

                regName = ContentValue("CurveInfo", "RegNameM4", m_INICurveInfoFilePath);
                code = m_DBM4Handle.WriteFloatsToDB(m_TableName, index, regName, pt_X);
                if (!code)
                {
                    m_DBM4Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                code = m_DBM4Handle.WriteFloatsToDB(m_TableName, index, regName, pt_Y);
                if (!code)
                {
                    m_DBM4Handle.ClearBuffer();
                    MessageBox.Show("写入数据库失败失败，定位：设置参数", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            m_DBM4Handle.SaveDataToBuffer();
            m_DBM4Handle.SaveDateToDataBase();
            m_DBM4Handle.ClearBuffer();
            MessageBox.Show("写入数据库成功。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_CancelSave_4_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;//设置对话框的返回值
            MsgBoxResult = MessageBox.Show("确定不保存本组数据吗？",//对话框的显示内容
            "提示",//对话框的标题
            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮
            MessageBoxIcon.Exclamation,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号
            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
            {
                m_DrawCurveBench4Handle.ClearPointsInList();
            }
            if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
            {

            }
        }
        #endregion

        #region 曲线调节
        private void HP_AdjPre_M1_Scroll(object sender, ScrollEventArgs e)
        {
            m_M1yMax = e.NewValue;
            TB_DisPre_M1.Text = m_M1yMax.ToString() + "MPa";
        }

        private void HP_AdjTime_M1_Scroll(object sender, ScrollEventArgs e)
        {
            m_M1xMax = e.NewValue;
            TB_DisTime_M1.Text = m_M1xMax.ToString() + "s";
        }

        private void HP_AdjPre_M2_Scroll(object sender, ScrollEventArgs e)
        {
            m_M2yMax = e.NewValue;
            TB_DisPre_M2.Text = m_M2yMax.ToString() + "MPa";
        }

        private void HP_AdjTime_M2_Scroll(object sender, ScrollEventArgs e)
        {
            m_M2xMax = e.NewValue;
            TB_DisTime_M2.Text = m_M2xMax.ToString() + "s";
        }

        private void HP_AdjPre_M3_Scroll(object sender, ScrollEventArgs e)
        {
            m_M3yMax = e.NewValue;
            TB_DisPre_M3.Text = m_M3yMax.ToString() + "MPa";
        }

        private void HP_AdjTime_M3_Scroll(object sender, ScrollEventArgs e)
        {
            m_M3xMax = e.NewValue;
            TB_DisTime_M3.Text = m_M3xMax.ToString() + "s";
        }

        private void HP_AdjPre_M4_Scroll(object sender, ScrollEventArgs e)
        {
            m_M4yMax = e.NewValue;
            TB_DisPre_M4.Text = m_M4yMax.ToString() + "MPa";
        }

        private void HP_AdjTime_M4_Scroll(object sender, ScrollEventArgs e)
        {
            m_M4xMax = e.NewValue;
            TB_DisTime_M4.Text = m_M4xMax.ToString() + "s";
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
