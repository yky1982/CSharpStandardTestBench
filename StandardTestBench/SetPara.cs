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

namespace StandardTestBench
{
    public partial class SetPara : Form
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

        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

        public SetPara()
        {
            InitializeComponent();
        }

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

        private Form1 m_MainFormHandle = null;
        private UserManager m_UserManagerHandle = null;
        private event StatePageInfo ShowPageInfo;
        private event StateSysInfo ShowDebugInfo;

        private string m_ConfigFilePath = Application.StartupPath + @"\SystemFile\SetPara\SetParaConfig.xml";
        private string m_ReportFilePath = Application.StartupPath + @"\SystemFile\SetPara\ReportXML.xml";
        private string m_INIFilePath = Application.StartupPath + @"\Config\SetPara\SetParaConfig.ini";
        private string m_INIReportFilePath = Application.StartupPath + @"\Config\SetPara\ReprotParaConfig.ini";
        private string m_MDBFilePath = Application.StartupPath + @"\Config\SetPara\SetParaTable.mdb";
        private string m_TableName = "SetParaTable";
        public DataBaseLib.DataBaseInterface m_DBHandle = null;
        private int m_SetParaRows = 0;
        private int m_ReportRows = 0;
        private int m_MachineCount = 0;
        public int m_SelectRecordId = -1;
        private void SetPara_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            m_UserManagerHandle = UserManager.GetHandle();
            InitForm();
            AccessInit();

            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);

            string user = m_UserManagerHandle.m_CurrenUser;
            string INIFilePath = Application.StartupPath + @"\Config\SetPara\" + user + ".ini";
            string permission = ContentValue("SetPara", "Permission ", INIFilePath);
            if (permission == "Y")
            {
                BT_LoadSetConfig.Enabled = true;
            }
            if (permission == "N")
            {
                BT_LoadSetConfig.Enabled = false;
            }

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                Grp_Tool.Text = "Tool Bar";
                Grp_Para.Text = "Parameters";
                Grp_SetPara_Style.Text = "Set Parameters";
                Grp_Report.Text = "Report Info";
                TB_Exit.Text = "Exit";
                TB_StartTest.Text = "Start Test";
            }
        }

        #region 界面初始化
        private void InitForm()
        {
            DGV_SetPara.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Report.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGV_SetPara.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Report.DefaultCellStyle = dataGridViewCellStyle1;

            string sSetParaRows = ContentValue("SetParaConfig", "Rows", m_INIFilePath);
            string sMcount = ContentValue("SetParaConfig", "Machine", m_INIFilePath);
            string sRrows = ContentValue("ReprotParaConfig", "Rows", m_INIReportFilePath);

            m_SetParaRows = Convert.ToInt32(sSetParaRows);
            m_ReportRows = Convert.ToInt32(sRrows);
            m_MachineCount = Convert.ToInt32(sMcount);

            switch (m_MachineCount)
            {
                case 1:
                    checkBox_M1.Visible = true;
                    checkBox_M2.Visible = false;
                    checkBox_M3.Visible = false;
                    checkBox_M4.Visible = false;
                    break;
                case 2:
                    checkBox_M1.Visible = true;
                    checkBox_M2.Visible = true;
                    checkBox_M3.Visible = false;
                    checkBox_M4.Visible = false;
                    break;
                case 3:
                    checkBox_M1.Visible = true;
                    checkBox_M2.Visible = true;
                    checkBox_M3.Visible = true;
                    checkBox_M4.Visible = false;
                    break;
                case 4:
                    checkBox_M1.Visible = true;
                    checkBox_M2.Visible = true;
                    checkBox_M3.Visible = true;
                    checkBox_M4.Visible = true;
                    break;
            }

            LoadSetXML();
            DisSetPara();

            LoadReportXML();
            DisReportPara();

            LoadINI();
            LoadReportINI();

            LoadMachineINI();
        }

        private void LoadSetXML()
        {
            string regName = "";
            string regNameCH = "";
            string dataType = "";
            object minValue = default(object);
            object maxValue = default(object);
            string paraUnit = "";
            string BenchNo = "";
            object value = "";
            string isSave = "";
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
                m_SetParaLists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, value, isSave, BenchNo));
            }
        }

        private void DisSetPara()
        {
            DGV_SetPara.Rows.Clear();
            int count = m_SetParaLists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = m_SetParaRows;
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
            DGV_SetPara.ColumnCount = MaxColoum;
            for (int i = 0; i < 2 * MaxRows - 1; i++)
            {
                DGV_SetPara.Rows.Add();
            }
            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColoum; j++)
                {
                    if (i * MaxColoum + j == count)
                    {
                        break;
                    }
                    DGV_SetPara.Rows[i * 2].Cells[j].Value = m_SetParaLists[i * MaxColoum + j].m_RegNameCh +
                                                                                                   " (" + m_SetParaLists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
            for (int i = 0; i < DGV_SetPara.Rows.Count; i += 2)
            {
                DGV_SetPara.Rows[i].ReadOnly = true;
            }

        }

        private void LoadReportXML()
        {
            string paraName = "";
            string paraNameCH = "";
            string paraUint = "";
            string value = "";
            string isSave = "";
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
                m_ReportParaLists.Add(new ReportParaList(paraName, paraNameCH, paraUint, value, isSave));
            }
        }

        private void DisReportPara()
        {
            DGV_Report.Rows.Clear();
            int count = m_ReportParaLists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = m_ReportRows;
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
                    DGV_Report.Rows[i * 2].Cells[j].Value = m_ReportParaLists[i * MaxColoum + j].m_ParaNameCH +
                                                                                                   " (" + m_ReportParaLists[i * MaxColoum + j].m_ParaUint + ")";
                }
            }
            for (int i = 0; i < DGV_Report.Rows.Count; i += 2)
            {
                DGV_Report.Rows[i].ReadOnly = true;
            }
        }

        private void LoadINI()
        {
            string UserName = m_UserManagerHandle.m_CurrenUser;
            string userINIFilePath = Application.StartupPath + @"\Config\SetPara\" + UserName + @".ini";
            TB_ConfigTable.Text = ContentValue("SetPara", "Table", userINIFilePath);
            int index = 0;
            for (int i = 1; i < DGV_SetPara.Rows.Count; i += 2)
            {
                for (int j = 0; j < m_SetParaRows; j++)
                {
                    if ((i - 1) * m_SetParaRows / 2 + j >= m_SetParaLists.Count)
                    {
                        break;
                    }
                    DGV_SetPara.Rows[i].Cells[j].Value = ContentValue("SetPara", m_SetParaLists[index].m_RegName + "Value", userINIFilePath);
                    index++;
                }
            }
        }

        private void LoadReportINI()
        {
            string UserName = m_UserManagerHandle.m_CurrenUser;
            string userINIFilePath = Application.StartupPath + @"\Config\SetPara\" + UserName + @"Report.ini";
            int index = 0;
            for (int i = 1; i < DGV_Report.Rows.Count; i += 2)
            {
                for (int j = 0; j < m_ReportRows; j++)
                {
                    if (((i - 1) * m_ReportRows / 2) + j >= m_ReportParaLists.Count)
                    {
                        break;
                    }
                    DGV_Report.Rows[i].Cells[j].Value = ContentValue("ReportPara", m_ReportParaLists[index].m_ParaName + "Value", userINIFilePath);
                    index++;
                }
            }
        }

        private void LoadMachineINI()
        {
            string machineSelectFilePath = Application.StartupPath + @"\Config\SetPara\MSelect.ini";
            string M1 = ContentValue("MSelect", "M1", machineSelectFilePath);
            string M2 = ContentValue("MSelect", "M2", machineSelectFilePath);
            string M3 = ContentValue("MSelect", "M3", machineSelectFilePath);
            string M4 = ContentValue("MSelect", "M4", machineSelectFilePath);
            if (M1 == "True")
            {
                checkBox_M1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox_M1.CheckState = CheckState.Unchecked;
            }
            if (M2 == "True")
            {
                checkBox_M2.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox_M2.CheckState = CheckState.Unchecked;
            }
            if (M3 == "True")
            {
                checkBox_M3.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox_M3.CheckState = CheckState.Unchecked;
            }
            if (M4 == "True")
            {
                checkBox_M4.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox_M4.CheckState = CheckState.Unchecked;
            }

        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        #endregion

        private void AccessInit()
        {
            m_DBHandle = new DataBaseLib.DataBaseInterface();
            bool code = false;
            code = m_DBHandle.DBInit("Access", m_MDBFilePath, "Open", m_TableName);
            if (!code)
            {
                SendDebugInfo("SetPara 打开数据库失败, 位置：Open");
                MessageBox.Show("打开失败, 位置：Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private object m_OringinData = default(object);
        private void DGV_SetPara_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_OringinData = DGV_SetPara.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void DGV_SetPara_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex % 2 == 0)
            {
                return;
            }
            string nameV = DGV_SetPara.Rows[rowIndex - 1].Cells[columnIndex].Value.ToString();
            char[] a = nameV.ToCharArray();
            string nameCH = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == ' ')
                {
                    break;
                }
                nameCH += a[i];
            }

            int index = m_SetParaLists.FindIndex(r => r.m_RegNameCh == nameCH);
            if (index < 0)
            {
                SendDebugInfo("SetPara 没有找到参数，设置失败");
                MessageBox.Show("没有找到参数，设置失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            object setValue = DGV_SetPara.Rows[rowIndex].Cells[columnIndex].Value;
            bool result = CheckValue(index, setValue);
            if (!result)
            {
                SendDebugInfo("SetPara 参数超范围，设置失败");
                MessageBox.Show("参数超范围，设置失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DGV_SetPara.Rows[rowIndex].Cells[columnIndex].Value = m_OringinData;
                return;
            }
            m_SetParaLists[index].m_Value = setValue;
            return;
            string dataType = m_SetParaLists[index].m_DataType;
            if (dataType == "bit" || dataType == "byte")
            {
                sbyte data = Convert.ToSByte(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }
            if (dataType == "ubyte")
            {
                byte data = Convert.ToByte(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }
            if (dataType == "word")
            {
                Int16 data = Convert.ToInt16(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }
            if (dataType == "uword")
            {
                UInt16 data = Convert.ToUInt16(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }
            if (dataType == "dword")
            {
                Int32 data = Convert.ToInt32(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }
            if (dataType == "udword")
            {
                UInt32 data = Convert.ToUInt32(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }
            if (dataType == "float")
            {
                float data = Convert.ToSingle(setValue);
                m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[index].m_RegName, data);
            }


        }

        private bool CheckValue(int index, object setValue)
        {
            string dataType = m_SetParaLists[index].m_DataType;
            try
            {

                if (dataType == "bit")
                {
                    if ((bool)setValue == true || (bool)setValue == false)
                    {
                        return true;
                    }
                }
                if (dataType == "byte")
                {
                    sbyte maxValue = Convert.ToSByte(m_SetParaLists[index].m_MaxValue);
                    sbyte minValue = Convert.ToSByte(m_SetParaLists[index].m_MinValue);
                    sbyte value = Convert.ToSByte(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
                if (dataType == "ubyte")
                {
                    byte maxValue = Convert.ToByte(m_SetParaLists[index].m_MaxValue);
                    byte minValue = Convert.ToByte(m_SetParaLists[index].m_MinValue);
                    byte value = Convert.ToByte(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
                if (dataType == "word")
                {
                    Int16 maxValue = Convert.ToInt16(m_SetParaLists[index].m_MaxValue);
                    Int16 minValue = Convert.ToInt16(m_SetParaLists[index].m_MinValue);
                    Int16 value = Convert.ToInt16(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
                if (dataType == "uword")
                {
                    UInt16 maxValue = Convert.ToUInt16(m_SetParaLists[index].m_MaxValue);
                    UInt16 minValue = Convert.ToUInt16(m_SetParaLists[index].m_MinValue);
                    UInt16 value = Convert.ToUInt16(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
                if (dataType == "dword")
                {
                    Int32 maxValue = Convert.ToInt32(m_SetParaLists[index].m_MaxValue);
                    Int32 minValue = Convert.ToInt32(m_SetParaLists[index].m_MinValue);
                    Int32 value = Convert.ToInt32(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
                if (dataType == "udword")
                {
                    UInt32 maxValue = Convert.ToUInt32(m_SetParaLists[index].m_MaxValue);
                    UInt32 minValue = Convert.ToUInt32(m_SetParaLists[index].m_MinValue);
                    UInt32 value = Convert.ToUInt32(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
                if (dataType == "float")
                {
                    float maxValue = Convert.ToSingle(m_SetParaLists[index].m_MaxValue);
                    float minValue = Convert.ToSingle(m_SetParaLists[index].m_MinValue);
                    float value = Convert.ToSingle(setValue);
                    if (value <= maxValue && value >= minValue)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        #region 保存设置参数
        private void BT_SaveSetPara_Click(object sender, EventArgs e)
        {
            SaveINI();
            SaveMachineSelectINI();
            bool code = SaveDataToDB();
            if (!code)
            {
                return;
            }
        }

        private void SaveINI()
        {
            string UserName = m_UserManagerHandle.m_CurrenUser;
            string userINIFilePath = Application.StartupPath + @"\Config\SetPara\" + UserName + @".ini";
            string tableName = TB_ConfigTable.Text;

            WritePrivateProfileString("SetPara", "Table", tableName, userINIFilePath);
            for (int i = 0; i < m_SetParaLists.Count; i++)
            {
                WritePrivateProfileString("SetPara", m_SetParaLists[i].m_RegName, m_SetParaLists[i].m_RegNameCh, userINIFilePath);
                WritePrivateProfileString("SetPara", m_SetParaLists[i].m_RegName + "Value", m_SetParaLists[i].m_Value.ToString(), userINIFilePath);
            }
        }

        private bool SaveDataToDB()
        {
            bool code = m_DBHandle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("SetPara 读取数据库内容至缓存失败");
            }
            int dataIndex = -1;
            code = m_DBHandle.FindDataInDB(m_TableName, "ConfigName", TB_ConfigTable.Text, ref dataIndex);
            //if (!code)
            //{
            //    MessageBox.Show("参数保存失败，定位：ConfigName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            int SaveIndex = -1;
            if (dataIndex < 0)
            {
                code = m_DBHandle.AddRows(m_TableName, ref SaveIndex);
                if (!code)
                {
                    SendDebugInfo("SetPara 参数保存失败，定位：AddRow");
                    MessageBox.Show("参数保存失败，定位：AddRow", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                code = m_DBHandle.WriteSigleDataToDB(m_TableName, SaveIndex, "ConfigName", TB_ConfigTable.Text);
                if (!code)
                {
                    SendDebugInfo("SetPara 参数保存失败，定位：ConfigName");
                    MessageBox.Show("参数保存失败，定位：ConfigName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                code = m_DBHandle.WriteSigleDataToDB(m_TableName, SaveIndex, "UserName", m_UserManagerHandle.m_CurrenUser);
                if (!code)
                {
                    SendDebugInfo("SetPara 参数保存失败，定位：UserName");
                    MessageBox.Show("参数保存失败，定位：UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                for (int i = 0; i < m_SetParaLists.Count; i++)
                {
                    code = m_DBHandle.WriteSigleDataToDB(m_TableName, SaveIndex, m_SetParaLists[i].m_RegName + "DType", m_SetParaLists[i].m_DataType);
                    if (!code)
                    {
                        SendDebugInfo("SetPara 参数保存失败，定位：DT");
                        MessageBox.Show("参数保存失败，定位：DT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    code = m_DBHandle.WriteSigleDataToDB(m_TableName, SaveIndex, m_SetParaLists[i].m_RegName, m_SetParaLists[i].m_Value);
                    if (!code)
                    {
                        SendDebugInfo("SetPara 参数保存失败，定位："+m_SetParaLists[i].m_RegName);
                        MessageBox.Show("参数保存失败，定位：" + m_SetParaLists[i].m_RegName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                m_DBHandle.SaveDataToBuffer();
                m_DBHandle.SaveDateToDataBase();
            }
            else
            {
                code = m_DBHandle.OverwriteSigleDataToDB(m_TableName, dataIndex, "ConfigName", TB_ConfigTable.Text);
                if (!code)
                {
                    SendDebugInfo("SetPara 参数保存失败，定位：ConfigName");
                    MessageBox.Show("参数保存失败，定位：ConfigName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                code = m_DBHandle.OverwriteSigleDataToDB(m_TableName, dataIndex, "UserName", m_UserManagerHandle.m_CurrenUser);
                if (!code)
                {
                    SendDebugInfo("SetPara 参数保存失败，定位：UserName");
                    MessageBox.Show("参数保存失败，定位：UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                for (int i = 0; i < m_SetParaLists.Count; i++)
                {
                    code = m_DBHandle.OverwriteSigleDataToDB(m_TableName, dataIndex, m_SetParaLists[i].m_RegName + "DType", m_SetParaLists[i].m_DataType);
                    if (!code)
                    {
                        SendDebugInfo("SetPara 参数保存失败，定位：" +  m_SetParaLists[i].m_RegName);
                        return false;
                    }
                    code = m_DBHandle.OverwriteSigleDataToDB(m_TableName, dataIndex, m_SetParaLists[i].m_RegName, m_SetParaLists[i].m_Value);
                    if (!code)
                    {
                        SendDebugInfo("SetPara 参数保存失败，定位：" + m_SetParaLists[i].m_RegName);
                        return false;
                    }
                }
                code = m_DBHandle.SaveDateToDataBase();
                if (!code)
                {
                    return false;
                }
            }
            m_DBHandle.ClearBuffer();
            return true;

        }

        private void SaveMachineSelectINI()
        {
            bool M1 = false;
            bool M2 = false;
            bool M3 = false;
            bool M4 = false;
            int d1 = 0;
            int d2 = 0;
            int d3 = 0;
            int d4 = 0;
            if (checkBox_M1.CheckState == CheckState.Checked)
            {
                M1 = true;
                d1 = 1;
            }
            else
            {
                M1 = false;
                d1 = 0;
            }

            if (checkBox_M2.CheckState == CheckState.Checked)
            {
                M2 = true;
                d2 = 1;
            }
            else
            {
                M2 = false;
                d2 = 0;
            }

            if (checkBox_M3.CheckState == CheckState.Checked)
            {
                M3 = true;
                d3 = 1;
            }
            else
            {
                M3 = false;
                d3 = 0;
            }

            if (checkBox_M4.CheckState == CheckState.Checked)
            {
                M4 = true;
                d4 = 1;
            }
            else
            {
                M4 = false;
                d4 = 0;
            }

            string machineSelectFilePath = Application.StartupPath + @"\Config\SetPara\MSelect.ini";
            WritePrivateProfileString("MSelect", "M1", M1.ToString(), machineSelectFilePath);
            WritePrivateProfileString("MSelect", "M2", M2.ToString(), machineSelectFilePath);
            WritePrivateProfileString("MSelect", "M3", M3.ToString(), machineSelectFilePath);
            WritePrivateProfileString("MSelect", "M4", M4.ToString(), machineSelectFilePath);

            string RegName = ContentValue("SetParaConfig", "RegName", m_INIFilePath);
            int data = 0;

            data = (d4 << 3) + (d3 << 2) + (d2 << 1) + d1;

            return;
            int code = m_MainFormHandle.m_DriveHandle.WriteData(RegName, data);
            if( code != 1)
            {
                SendDebugInfo("SetPara 工位设置失败");
                MessageBox.Show("工位设置失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        #endregion

        private void BT_LoadSetConfig_Click(object sender, EventArgs e)
        {
            LoadSetParaConfig Handle = new LoadSetParaConfig(this);
            Handle.ShowDialog();

            if (m_SelectRecordId < 0)
            {
                return;
            }

            int index = -1;
            bool code  = m_DBHandle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("SetPara 添加数据库至缓存失败");
            }
            code = m_DBHandle.GetIndexInDB(m_TableName, m_SelectRecordId, ref index);
            if (!code)
            {
                SendDebugInfo("SetPara 获取Index失败");
            }

            object data = default(object);
            code = m_DBHandle.ReadSigleDataInDB(m_TableName, index, "ConfigName", ref data);
            if (!code)
            {
                SendDebugInfo("SetPara 读取数据失败");
                MessageBox.Show("载入数据表失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TB_ConfigTable.Text = data.ToString();

            int ListCount = 0;
            for (int i = 1; i < DGV_SetPara.Rows.Count; i += 2)
            {
                for (int j = 0; j < DGV_SetPara.ColumnCount; j++)
                {
                    if ((i - 1) * DGV_SetPara.ColumnCount / 2 + j >= m_SetParaLists.Count)
                    {
                        break;
                    }
                    code = m_DBHandle.ReadSigleDataInDB(m_TableName, index, m_SetParaLists[ListCount].m_RegName, ref data);
                    if (!code)
                    {
                        SendDebugInfo("SetPara 载入数据表失败, 定位：" + m_SetParaLists[ListCount].m_RegName);
                        MessageBox.Show("载入数据表失败, 定位：" + m_SetParaLists[ListCount].m_RegName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DGV_SetPara.Rows[i].Cells[j].Value = data.ToString();
                    ListCount++;
                }
            }
            code = m_DBHandle.ClearBuffer();
            if (!code)
            {
                SendDebugInfo("SetPara 清除缓存失败");
            }
        }

        private void BT_SaveReport_Click(object sender, EventArgs e)
        {
            string UserName = m_UserManagerHandle.m_CurrenUser;
            string userINIFilePath = Application.StartupPath + @"\Config\SetPara\" + UserName + @"Report.ini";
            for (int i = 0; i < m_ReportParaLists.Count; i++)
            {
                WritePrivateProfileString("ReportPara", m_ReportParaLists[i].m_ParaName, m_ReportParaLists[i].m_ParaNameCH, userINIFilePath);
                WritePrivateProfileString("ReportPara", m_ReportParaLists[i].m_ParaName + "Value", m_ReportParaLists[i].m_Value.ToString(), userINIFilePath);
            }
        }

        private void DGV_Report_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex % 2 == 0)
            {
                return;
            }

            string nameV = DGV_Report.Rows[rowIndex - 1].Cells[columnIndex].Value.ToString();
            char[] a = nameV.ToCharArray();
            string nameCH = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == ' ')
                {
                    break;
                }
                nameCH += a[i];
            }

            int index = m_ReportParaLists.FindIndex(r => r.m_ParaNameCH == nameCH);
            if (index < 0)
            {
                SendDebugInfo("SetPara 没有找到参数，设置失败");                
                MessageBox.Show("没有找到参数，设置失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string para = DGV_Report.Rows[rowIndex].Cells[columnIndex].Value.ToString();
            m_ReportParaLists[index].m_Value = para;
        }

        private void TB_Exit_Click(object sender, EventArgs e)
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

        private void BT_SetPara_Click(object sender, EventArgs e)
        {
            int dgRows = DGV_SetPara.Rows.Count;
            int dgColumns = DGV_SetPara.ColumnCount;
            int listCount = 0;
            int  code = 0;

            for (int i = 0; i < dgRows; i += 2)
            {
                for (int j = 0; j < dgColumns; j++)
                {
                    if (((i - 1) * dgColumns + j) > m_SetParaLists.Count)
                    {
                        break;
                    }
                    string nameV = DGV_SetPara.Rows[i].Cells[j].Value.ToString();
                    char[] a = nameV.ToCharArray();
                    string nameCH = "";
                    for (int m = 0; m < a.Length; m++)
                    {
                        if (a[m] == ' ')
                        {
                            break;
                        }
                        nameCH += a[m];
                    }

                    object setValue = DGV_SetPara.Rows[i + 1].Cells[j].Value;
                    if (nameCH == m_SetParaLists[listCount].m_RegNameCh && setValue.ToString() == m_SetParaLists[listCount].m_Value.ToString())
                    {
                        listCount++;
                        continue;
                    }
                    else
                    {
                        string dataType = m_SetParaLists[listCount].m_DataType;
                        if (dataType == "bit" || dataType == "byte")
                        {
                            sbyte data = Convert.ToSByte(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        if (dataType == "ubyte")
                        {
                            byte data = Convert.ToByte(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        if (dataType == "word")
                        {
                            Int16 data = Convert.ToInt16(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        if (dataType == "uword")
                        {
                            UInt16 data = Convert.ToUInt16(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        if (dataType == "dword")
                        {
                            Int32 data = Convert.ToInt32(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        if (dataType == "udword")
                        {
                            UInt32 data = Convert.ToUInt32(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        if (dataType == "float")
                        {
                            float data = Convert.ToSingle(setValue);
                            code = m_MainFormHandle.m_DriveHandle.WriteData(m_SetParaLists[listCount].m_RegName, data);
                            if (code != 1)
                            {
                                SendDebugInfo("SetPara 设置失败" + m_SetParaLists[listCount].m_RegName);
                            }
                        }
                        listCount++;
                    }
                }
            }
            SaveINI();
            MessageBox.Show("设置成功，请启动试验", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TB_StartTest_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowTestingForm();
        }

        private void checkBox_M1_CheckStateChanged(object sender, EventArgs e)
        {
            SaveMachineSelectINI();
        }

        private void checkBox_M2_CheckStateChanged(object sender, EventArgs e)
        {
            SaveMachineSelectINI();
        }

        private void checkBox_M3_CheckStateChanged(object sender, EventArgs e)
        {
            SaveMachineSelectINI();
        }

        private void checkBox_M4_CheckStateChanged(object sender, EventArgs e)
        {
            SaveMachineSelectINI();
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
