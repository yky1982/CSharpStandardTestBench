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
using DataBaseLib;

namespace StandardTestBench
{
    public partial class SetParaConfig : Form
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


        public SetParaConfig()
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
            public string m_isSave;//是否保存至数据库
            public string m_BenchNo;//工位号
            public SetParaList(string regName, string regNameCH, string dataType, object minValue, object maxValue, string paraUnit, string isSave, string BenchNo)
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
        public List<SetParaList> m_SetParaLists = new List<SetParaList>();   

        public class ReportParaList
        {
            public string m_ParaName;
            public string m_ParaNameCH;
            public string m_ParaUint;
            public string m_isSave;//是否保存至数据库
            public ReportParaList(string paraName, string paraNameCH, string paraUint, string isSave)
            {
                m_ParaName = paraName;
                m_ParaNameCH = paraNameCH;
                m_ParaUint = paraUint;
                m_isSave = isSave;
            }
        }
        public List<ReportParaList> m_ReportParaLists = new List<ReportParaList>();


        private Form1 m_MainFormHandle = null;
        private DataBaseLib.DataBaseInterface m_DBHandle = null;
        private string m_ConfigFilePath = Application.StartupPath + @"\SystemFile\SetPara\SetParaConfig.xml";
        private string m_ReportFilePath = Application.StartupPath + @"\SystemFile\SetPara\ReportXML.xml";
        private string m_INIFilePath = Application.StartupPath + @"\Config\SetPara\SetParaConfig.ini";
        private string m_INIReportFilePath = Application.StartupPath + @"\Config\SetPara\ReprotParaConfig.ini";
        private string m_MDBFilePath = Application.StartupPath + @"\Config\SetPara\SetParaTable.mdb";
        private string m_AdminINIFilePath = Application.StartupPath + @"\Config\SetPara\Admin.ini";
        private string m_AdminReportINIFilePath = Application.StartupPath + @"\Config\SetPara\AdminReport.ini";
        private string m_GuestINIFilePath = Application.StartupPath + @"\Config\SetPara\Guest.ini";
        private string m_GuestReportINIFilePath = Application.StartupPath + @"\Config\SetPara\GuestReport.ini";
        private string m_TableName = "SetParaTable";
        private event StateSysInfo ShowDebugInfo;

        private void SetParaConfig_Load(object sender, EventArgs e)
        {
            m_MainFormHandle = Form1.GetHandle();
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
            DGV_SetPara.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Report.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGV_SetPara.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Report.DefaultCellStyle = dataGridViewCellStyle1;

            TB_SetPara_Row.Text = ContentValue("SetParaConfig", "Rows", m_INIFilePath);
            TB_SetPara_MachineNum.Text = ContentValue("SetParaConfig", "Machine", m_INIFilePath);

            TB_Report.Text = ContentValue("ReprotParaConfig", "Rows", m_INIReportFilePath);

            LoadSetXML();
            DisSetPara();

            LoadReportXML();
            DisReportPara();

            m_DBHandle = new DataBaseLib.DataBaseInterface();
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
                SendDebugInfo("SetParaConfig XML文件不存在");
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
                m_SetParaLists.Add(new SetParaList(regName, regNameCH, dataType, minValue, maxValue, paraUnit, isSave, BenchNo));
            }
        }

        private void DisSetPara()
        {
            DGV_SetPara.Rows.Clear();
            int count = m_SetParaLists.Count;
            int MaxColoum = 0;
            try
            {
                MaxColoum = Convert.ToInt32(TB_SetPara_Row.Text);
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
            for(int i = 0; i < 2 * MaxRows; i++)
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
                    DGV_SetPara.Rows[i  * 2].Cells[j].Value = m_SetParaLists[i * MaxColoum + j].m_RegNameCh +
                                                                                                   " (" + m_SetParaLists[i * MaxColoum + j].m_ParaUint + ")";
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
                SendDebugInfo("SetParaConfig ReportXML 文件不存在");
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
                m_ReportParaLists.Add(new ReportParaList(paraName,paraNameCH,paraUint, isSave));
            }
        }

        private void DisReportPara()
        {
            DGV_Report.Rows.Clear();
            int count = m_ReportParaLists.Count;
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
            for (int i = 0; i < 2 * MaxRows; i++)
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
        }

        private void TB_SetPara_Row_TextChanged(object sender, EventArgs e)
        {
            int RowCount = 0;
            try
            {
                RowCount = Convert.ToInt32(TB_SetPara_Row.Text);
            }
            catch (Exception)
            {
                return;
            }
            DisSetPara();
        }

        private void TB_SetPara_MachineNum_TextChanged(object sender, EventArgs e)
        {
            int machineCount = 0;
            try
            {
                machineCount = Convert.ToInt32(TB_SetPara_MachineNum.Text); 
            }
            catch (System.Exception)
            {
                TB_SetPara_MachineNum.Text = "1";
                return;
            }
            if (machineCount > 4 || machineCount <= 0)
            {
                SendDebugInfo("SetParaConfig 拖机台数输入错误，请输入1-4台");
                MessageBox.Show("拖机台数输入错误，请输入1-4台！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TB_SetPara_MachineNum.Text = "1";
            }

        }

        private void BT_SavePara_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("SetParaConfig", "Rows", TB_SetPara_Row.Text, m_INIFilePath);
            WritePrivateProfileString("SetParaConfig", "Machine", TB_SetPara_MachineNum.Text, m_INIFilePath);
            WritePrivateProfileString("SetParaConfig", "RegName", TB_SetPara_RegName.Text, m_INIFilePath);
            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void TB_Report_TextChanged(object sender, EventArgs e)
        {
            int RowCount = 0;
            try
            {
                RowCount = Convert.ToInt32(TB_Report.Text);
            }
            catch (Exception)
            {
                return;
            }
            DisReportPara();
        }

        private void BT_CreatMDB_Click(object sender, EventArgs e)
        {
            if (File.Exists(m_MDBFilePath))
            {
                File.Delete(m_MDBFilePath);
            }
            bool code;
            code = m_DBHandle.DBInit("Access", m_MDBFilePath, "Create", m_TableName);
            if (!code)
            {
                SendDebugInfo("SetParaConfig 创建失败, 位置：Create");
                MessageBox.Show("创建失败, 位置：Create", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //配置表名称
            code = m_DBHandle.AddColumn(m_TableName, "ConfigName", "VarChar");
            if (!code)
            {
                SendDebugInfo("SetParaConfig 创建失败, 位置：ConfigName");
                MessageBox.Show("创建失败，位置：ConfigName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //用户名称
            code = m_DBHandle.AddColumn(m_TableName, "UserName", "VarChar");
            if (!code)
            {
                SendDebugInfo("SetParaConfig 创建失败, 位置：UserName");
                MessageBox.Show("创建失败，位置：UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = m_SetParaLists.Count;
            for (int i = 0; i < count; i++)
            {
                code = m_DBHandle.AddColumn(m_TableName, m_SetParaLists[i].m_RegName + "DType", "VarChar");
                if (!code)
                {
                    SendDebugInfo("SetParaConfig 创建失败,位置：" + m_SetParaLists[i].m_RegName);
                    MessageBox.Show("创建失败，位置：" + m_SetParaLists[i].m_RegName + " DataType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                code = m_DBHandle.AddColumn(m_TableName, m_SetParaLists[i].m_RegName, "VarChar");
                if (!code)
                {
                    SendDebugInfo("SetParaConfig 创建失败，位置：" + m_SetParaLists[i].m_RegName);
                    MessageBox.Show("创建失败，位置：" + m_SetParaLists[i].m_RegName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            CreateSetParaINI();
            MessageBox.Show("生成配置表成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CreateSetParaINI()
        {
            if (File.Exists(m_AdminINIFilePath))
            {
                File.Delete(m_AdminINIFilePath);
            }
            if (File.Exists(m_GuestINIFilePath))
            {
                File.Delete(m_GuestINIFilePath);
            }
            WritePrivateProfileString("SetPara", "Permission ", "Y", m_AdminINIFilePath);
            int Count = m_SetParaLists.Count;
            for (int i = 0; i < Count; i++)
            {
                WritePrivateProfileString("SetPara", m_SetParaLists[i].m_RegName, m_SetParaLists[i].m_RegNameCh, m_AdminINIFilePath);
                WritePrivateProfileString("SetPara", m_SetParaLists[i].m_RegName + "Value", m_SetParaLists[i].m_MinValue.ToString(), m_AdminINIFilePath);
            }

            File.Copy(m_AdminINIFilePath, m_GuestINIFilePath, true);
            WritePrivateProfileString("SetPara", "Permission ", "N", m_GuestINIFilePath);
        }

        private void BT_SaveReport_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("ReprotParaConfig", "Rows", TB_Report.Text, m_INIReportFilePath);
            CreateReportINI();
            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateReportINI()
        {
            if (File.Exists(m_AdminReportINIFilePath))
            {
                File.Delete(m_AdminReportINIFilePath);
            }
            if (File.Exists(m_GuestReportINIFilePath))
            {
                File.Delete(m_GuestReportINIFilePath);
            }
            int Count = m_ReportParaLists.Count;
            for (int i = 0; i < Count; i++)
            {
                WritePrivateProfileString("ReportPara", m_ReportParaLists[i].m_ParaName, m_ReportParaLists[i].m_ParaNameCH, m_AdminReportINIFilePath);
                WritePrivateProfileString("ReportPara", m_ReportParaLists[i].m_ParaName + "Value", "None", m_AdminReportINIFilePath);
            }
            File.Copy(m_AdminReportINIFilePath, m_GuestReportINIFilePath, true);
        }
        private void BT_Pre_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowSysDescripConfigForm();
        }

        private void BT_Next_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowTestConfigForm();
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
