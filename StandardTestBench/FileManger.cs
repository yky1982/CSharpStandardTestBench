using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StandardTestBench
{
    class FileManger
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private string m_BaseFilePath = Application.StartupPath;

        public string m_SystemFileFold = Application.StartupPath + @"\SystemFile";
        public string m_ConfigFileFold = Application.StartupPath + @"\Config";
        //系统描述
        public string m_SysDesripFileFold = Application.StartupPath + @"\Config\Descrip";
        public string m_SysDesripINIFilePath = Application.StartupPath + @"\Config\Descrip\Descrip.ini";
        public string m_SysDesripTXTFilePath = Application.StartupPath + @"\Config\Descrip\Descrip.txt";

        //设置参数
        public string m_SetParaFileFold = Application.StartupPath + @"\Config\SetPara";

        //测试界面配置
        public string m_TestConfigFileFold = Application.StartupPath + @"\Config\TestConfig";

        //数据库
        private string m_DataBaseFilePath = Application.StartupPath + @"\DataBase";
        private string m_INIDataBaseFilePath = Application.StartupPath + @"\Config\QueryDB";

        //报表
        private string m_ReportFilePath = Application.StartupPath + @"\Report";
        private string m_M1ReportFilePath = Application.StartupPath + @"\Report\M1";
        private string m_M2ReportFilePath = Application.StartupPath + @"\Report\M2";
        private string m_M3ReportFilePath = Application.StartupPath + @"\Report\M3";
        private string m_M4ReportFilePath = Application.StartupPath + @"\Report\M4";

        //告警
        private string m_AlarmSysFilePath = Application.StartupPath + @"\SystemFile\Alarm";
        private string m_AlarmInfoFilePath = Application.StartupPath + @"\Config\AlarmInfo";

        //传感器校验
        private string m_AdjSensorSysFilePath = Application.StartupPath + @"\SystemFile\AdjSensor";
        private string m_AdjSensorINIFilePath = Application.StartupPath + @"\Config\AdjSensor";

        //PLC恢复出厂设置
        private string m_PLCINIFilePath = Application.StartupPath + @"\Config\PLCDefault";


        private static FileManger m_SelfHandle = null;
        public static FileManger GetHandle()
        {
            if (m_SelfHandle == null)
            {
                m_SelfHandle = new FileManger();
            }
            return m_SelfHandle;
        }

        public bool CreatFileFold()
        {
            if (!Directory.Exists(m_SystemFileFold))
            {
                System.IO.Directory.CreateDirectory(m_SystemFileFold);
            }
            if (!Directory.Exists(m_ConfigFileFold))
            {
                System.IO.Directory.CreateDirectory(m_ConfigFileFold);
            }
            if (!Directory.Exists(m_SysDesripFileFold))
            {
                System.IO.Directory.CreateDirectory(m_SysDesripFileFold);
            }
            if (!Directory.Exists(m_SysDesripFileFold))
            {
                System.IO.Directory.CreateDirectory(m_SysDesripFileFold);
            }
            if (!Directory.Exists(m_SetParaFileFold))
            {
                System.IO.Directory.CreateDirectory(m_SetParaFileFold);
            }
            if (!Directory.Exists(m_TestConfigFileFold))
            {
                System.IO.Directory.CreateDirectory(m_TestConfigFileFold);
            }
            if (!Directory.Exists(m_DataBaseFilePath))
            {
                System.IO.Directory.CreateDirectory(m_DataBaseFilePath);
            }
            if (!Directory.Exists(m_INIDataBaseFilePath))
            {
                System.IO.Directory.CreateDirectory(m_INIDataBaseFilePath);
            }
            if (!Directory.Exists(m_ReportFilePath))
            {
                System.IO.Directory.CreateDirectory(m_ReportFilePath);
            }
            if (!Directory.Exists(m_M1ReportFilePath))
            {
                System.IO.Directory.CreateDirectory(m_M1ReportFilePath);
            }
            if (!Directory.Exists(m_M2ReportFilePath))
            {
                System.IO.Directory.CreateDirectory(m_M2ReportFilePath);
            }
            if (!Directory.Exists(m_M3ReportFilePath))
            {
                System.IO.Directory.CreateDirectory(m_M3ReportFilePath);
            }
            if (!Directory.Exists(m_M4ReportFilePath))
            {
                System.IO.Directory.CreateDirectory(m_M4ReportFilePath);
            }
            if (!Directory.Exists(m_AlarmSysFilePath))
            {
                System.IO.Directory.CreateDirectory(m_AlarmSysFilePath);
            }
            if (!Directory.Exists(m_AlarmInfoFilePath))
            {
                System.IO.Directory.CreateDirectory(m_AlarmInfoFilePath);
            }

            if (!Directory.Exists(m_AdjSensorSysFilePath))
            {
                System.IO.Directory.CreateDirectory(m_AdjSensorSysFilePath);
            }
            if (!Directory.Exists(m_AdjSensorINIFilePath))
            {
                System.IO.Directory.CreateDirectory(m_AdjSensorINIFilePath);
            }

            if (!Directory.Exists(m_PLCINIFilePath))
            {
                System.IO.Directory.CreateDirectory(m_PLCINIFilePath);
            }
            return true;
        }

        public bool CreateUserFile(string userName)
        {
            //创建系统文件夹
            string SystemPath = m_BaseFilePath + @"\SystemFile";
            if (!Directory.Exists(SystemPath))
            {
                System.IO.Directory.CreateDirectory(SystemPath);
            }
            //Admin用户文件夹
            string userFilePath = SystemPath + @"\" + userName;
            if (!Directory.Exists(userFilePath))
            {
                System.IO.Directory.CreateDirectory(userFilePath);
            }

            return true;
        }

        public bool DeleteUserFiles(string userName)
        {
            string FilePath = m_BaseFilePath + @"\SystemFile\" + userName;     
            if (Directory.Exists(FilePath))
            {
                DirectoryInfo di = new DirectoryInfo(FilePath);
                di.Delete(true);
            }          
            return true;
        }

        /// <summary>
        /// 删除文件目录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CreateUserSetINIFile(string userName, bool isPermission)
        {
            string adminINIFilePath = Application.StartupPath + @"\Config\SetPara\Admin.ini";
            string INIFilePath = Application.StartupPath + @"\Config\SetPara\" + userName + ".ini";
            if (!File.Exists(INIFilePath))
            {
                File.Copy(adminINIFilePath, INIFilePath, true);
            }
            if (isPermission)
            {
                WritePrivateProfileString("SetPara", "Permission ", "Y", INIFilePath);
            }
            else
            {
                WritePrivateProfileString("SetPara", "Permission ", "N", INIFilePath);
            }
            

            string adminReportINIFilePath = Application.StartupPath + @"\Config\SetPara\AdminReport.ini";
            string reportINIFilePath = Application.StartupPath + @"\Config\SetPara\" + userName + "Report.ini";
            if (!File.Exists(reportINIFilePath))
            {
                File.Copy(adminReportINIFilePath, reportINIFilePath, true);
            }
            return true;
        }

        /// <summary>
        /// 删除配置INI文件
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUserSetINIFiles(string userName)
        {
            string INIFilePath = Application.StartupPath + @"\Config\SetPara\" + userName + ".ini";
            if (File.Exists(INIFilePath))
            {
                File.Delete(INIFilePath);
            }

            string reportINIFilePath = Application.StartupPath + @"\Config\SetPara\" + userName + "Report.ini";
            if (File.Exists(reportINIFilePath))
            {
                File.Delete(reportINIFilePath);
            }
            return true;
        }


    }
}
