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

namespace StandardTestBench
{
    public partial class SysDescripConfig : Form
    {
        public const int WM_USER = 0x0400;
        public const int EM_GETPARAFORMAT = WM_USER + 61;
        public const int EM_SETPARAFORMAT = WM_USER + 71;
        public const long MAX_TAB_STOPS = 32;
        public const uint PFM_LINESPACING = 0x00000100;
        [StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam); 

        public SysDescripConfig()
        {
            InitializeComponent();
        }

        private string m_FileFoldPath = null;
        private string m_FileName = null; //文件名，绝对地址
        private string m_TXTFileName = null;
        private Form1 m_MainFormHandle = null;
        private void SysDescripConfig_Load(object sender, EventArgs e)
        {
            string FilePath = Application.StartupPath + @"\Config\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            FilePath = Application.StartupPath + @"\Config\Descrip";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            m_FileFoldPath = FilePath;
            m_FileName = m_FileFoldPath + @"\Descrip.ini";
            m_TXTFileName = m_FileFoldPath + @"\Descrip.txt";         

            CB_FontType.Items.Add("宋体");
            CB_FontType.Items.Add("仿宋");
            CB_FontType.Items.Add("楷体");
            CB_FontType.Items.Add("黑体");
            CB_FontType.Items.Add("隶书");
            CB_FontType.Items.Add("Times New Roman");
            CB_FontType.SelectedIndex = 0;

            CB_FontColor.Items.Add("黑色");
            CB_FontColor.Items.Add("红色");
            CB_FontColor.Items.Add("蓝色");
            CB_FontColor.Items.Add("绿色");
            CB_FontColor.Items.Add("紫色");
            CB_FontColor.SelectedIndex = 0;

            CB_LineSpaces.Items.Add("1");
            CB_LineSpaces.Items.Add("2");
            CB_LineSpaces.Items.Add("3");
            CB_LineSpaces.Items.Add("4");
            CB_LineSpaces.Items.Add("5");
            CB_LineSpaces.Items.Add("6");
            CB_LineSpaces.Items.Add("7");
            CB_LineSpaces.Items.Add("8");
            CB_LineSpaces.Items.Add("9");
            CB_LineSpaces.SelectedIndex = 0;

            TB_FontSize.Text = "9.0";

            CB_FontStyle.Items.Add("普通");
            CB_FontStyle.Items.Add("加粗");
            CB_FontStyle.Items.Add("斜体");
            CB_FontStyle.Items.Add("下划线");
            CB_LineSpaces.SelectedIndex = 0;

            m_MainFormHandle = Form1.GetHandle();

            LoadINI();


            string sContext = "";
            ReadTXT(m_TXTFileName, ref sContext);
            RTBox.Text = sContext;
            
        }

        private void LoadINI()
        {
            CB_FontType.Text = ContentValue("Descrip","FontType", m_FileName);
            TB_FontSize.Text = ContentValue("Descrip", "FontSize", m_FileName);
            CB_FontColor.Text = ContentValue("Descrip", "FontColor", m_FileName);
            CB_LineSpaces.Text = ContentValue("Descrip", "FontLineSpace", m_FileName);
            CB_FontStyle.Text = ContentValue("Descrip", "FontStyle", m_FileName);
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            string FontType = CB_FontType.Text;
            string fontSize = TB_FontSize.Text;
            string fontColor = CB_FontColor.Text;
            string lineSpace = CB_LineSpaces.Text;
            string FontStyle = CB_FontStyle.Text;

            WritePrivateProfileString("Descrip", "FontType", FontType, m_FileName);
            WritePrivateProfileString("Descrip", "FontSize", fontSize, m_FileName);
            WritePrivateProfileString("Descrip", "FontColor", fontColor, m_FileName);
            WritePrivateProfileString("Descrip", "FontLineSpace", lineSpace, m_FileName);
            WritePrivateProfileString("Descrip", "FontStyle", FontStyle, m_FileName);

            if (File.Exists(m_TXTFileName))
            {
                File.Delete(m_TXTFileName);
            }
            string sContext = RTBox.Text;
            WriteTXT(m_TXTFileName, sContext);
            MessageBox.Show("保存成功", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CB_FontType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sContext = CB_FontType.Text;
                float FontSize = Convert.ToSingle(TB_FontSize.Text);
                //float FontSize = 9.0f;
                RTBox.SelectAll();
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Regular);
            }
            catch (System.Exception)
            {
                return;
            }

        }

        private void TB_FontSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sContext = CB_FontType.Text;
                float FontSize = Convert.ToSingle(TB_FontSize.Text);
                RTBox.SelectAll();
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Regular);
            }
            catch (System.Exception)
            {
                return;
            }
        }

        private void CB_FontColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sContext = CB_FontColor.Text;
            RTBox.SelectAll();
            switch (sContext)
            {
                case "黑色":
                    RTBox.ForeColor = Color.Black;
                    break;
                case "红色":
                    RTBox.ForeColor = Color.Red;
                    break;
                case "蓝色":
                    RTBox.ForeColor = Color.Blue;
                    break;
                case "绿色":
                    RTBox.ForeColor = Color.Green;
                    break;
                case "紫色":
                    RTBox.ForeColor = Color.Violet;
                    break;
                default:
                    break;
            }

        }

        private void CB_LineSpaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lineSpace = Convert.ToInt32(CB_LineSpaces.Text) * 100;
            RTBox.SelectAll();

            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 4;
            fmt.dyLineSpacing = lineSpace;//((int)richTextBox1.Font.Size) * 20 * ((int)ud.Value);
            fmt.dwMask = PFM_LINESPACING;
            SendMessage(new HandleRef(this.RTBox, RTBox.Handle), EM_SETPARAFORMAT, 0, ref fmt);
        }

        private void CB_FontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sContext;
            float FontSize;
            try
            {
                sContext = CB_FontType.Text;
                FontSize = Convert.ToSingle(TB_FontSize.Text);
                RTBox.SelectAll();          
            }
            catch (System.Exception )
            {
                return;
            }
            if (CB_FontStyle.Text == "普通")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Regular);
            }
            if (CB_FontStyle.Text == "加粗")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Bold);
            }
            if (CB_FontStyle.Text == "斜体")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Italic);
            }
            if (CB_FontStyle.Text == "下划线")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Underline);
            }
        }


        private void WriteTXT(string LogFileName, string s)
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

        private void ReadTXT(string LogFileName, ref string s)
        {
            FileStream filestream = new FileStream(LogFileName, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(filestream, Encoding.GetEncoding("gb2312"));
            string sTem = sr.ReadToEnd();
            s = sTem;
            sr.Close();
            filestream.Close();
        }

        private void BT_Next_Click(object sender, EventArgs e)
        {
            this.Close();
            m_MainFormHandle.ShowSetParaConfigForm();
        }
    }
}
