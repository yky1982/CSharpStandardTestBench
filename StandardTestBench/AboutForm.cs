using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace StandardTestBench
{
    public partial class AboutForm : Form
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private string m_INISystemConfigFilePath = Application.StartupPath + @"\SystemFile\SystemConfig.ini";

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            string sLanguage = ContentValue("SystemCofig", "Language", m_INISystemConfigFilePath);

            if (sLanguage == "English")
            {
                LB_Address.Text = "Address: No.89, Donglu Road, Pudong, Shanghai, China ";
                LB_Name.Text = "Company: Maximator(Shanghai) Fluid Engineering Co.,Ltd";
                LB_Product.Text = "Product: Test Bench";
                LB_Editor.Text = "Editor: 1.0";
                LB_Phone.Text = "Phone: (+86)021-5868 2266";
                LB_Fax.Text = "Fax: (+86)021-5868 0926";
                LB_Internet.Text = "Internet: www.Maximator.cn";
                BT_Save.Text = "Confirm";
                this.Size = new System.Drawing.Size(550, 300);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ContentValue(string Section, string key, string strFilePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }


    }
}
