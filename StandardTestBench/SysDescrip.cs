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
    public partial class SysDescrip : Form
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

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam); 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

        private event StatePageInfo ShowPageInfo;
        private event StateSysInfo ShowDebugInfo;
        public SysDescrip()
        {
            InitializeComponent();
        }

        private string m_FileName = Application.StartupPath + @"\Config\Descrip\Descrip.ini";
        private string m_TXTFileName = Application.StartupPath + @"\Config\Descrip\Descrip.txt";
        private Form1 m_MainFormHandle = null;
        private void SysDescrip_Load(object sender, EventArgs e)
        {
            LoadINI();

            RTBox.Enabled = false;
            string sContext = "";
            ReadTXT(m_TXTFileName, ref sContext);
            RTBox.Text = sContext;

            m_MainFormHandle = Form1.GetHandle();
            ShowPageInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);

            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                TB_Exit.Text = "Exit";
            }
        }

        private void LoadINI()
        {
            if(!File.Exists(m_FileName))
            {
                SendDebugInfo("SysDescrip INI文件不存在");
                return ;
            }
            string fontType = ContentValue("Descrip", "FontType", m_FileName);
            string fontSize = ContentValue("Descrip", "FontSize", m_FileName);
            string fontColor = ContentValue("Descrip", "FontColor", m_FileName);
            string fontLineSpace= ContentValue("Descrip", "FontLineSpace", m_FileName);
            string fontStyle= ContentValue("Descrip", "FontStyle", m_FileName);

            SetTXTStyle(fontType, fontSize, fontColor, fontLineSpace, fontStyle);
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }

        private void SetTXTStyle(string fontType, string fontSize, string fontColor, string fontLineSpace, string fontStyle)
        {
            string sContext;
            float FontSize;
            try
            {
                sContext = fontType;
                FontSize = Convert.ToSingle(fontSize);
                RTBox.SelectAll();
            }
            catch (System.Exception)
            {
                return;
            }
            if (fontStyle == "普通")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Regular);
            }
            if (fontStyle == "加粗")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Bold);
            }
            if (fontStyle == "斜体")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Italic);
            }
            if (fontStyle == "下划线")
            {
                RTBox.Font = new System.Drawing.Font(sContext, FontSize, FontStyle.Underline);
            }

            switch (fontColor)
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

            int lineSpace = Convert.ToInt32(fontLineSpace) * 100;
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 4;
            fmt.dyLineSpacing = lineSpace;//((int)richTextBox1.Font.Size) * 20 * ((int)ud.Value);
            fmt.dwMask = PFM_LINESPACING;
            SendMessage(new HandleRef(this.RTBox, RTBox.Handle), EM_SETPARAFORMAT, 0, ref fmt);
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

        private void SendDebugInfo(string s)
        {
            if (ShowDebugInfo != null)
            {
                ShowDebugInfo(s);
            }
        }
    }
}
