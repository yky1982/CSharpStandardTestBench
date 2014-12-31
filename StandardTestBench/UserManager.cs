using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using DataBaseLib;
using System.Data;

namespace StandardTestBench
{ 
    class UserManager
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
        public event StatePageInfo ShowSystemInfo;
        private event StateSysInfo ShowDebugInfo;
        private string m_FilePath = Application.StartupPath + @"\SystemFile\UserConfig.mdb";
        //private string m_AdminFilePath = Application.StartupPath + @"\Config\SetPara";
        //private string m_GuestFilePath = Application.StartupPath + @"\Config\SetPara";
        private string m_UserFilePath = Application.StartupPath + @"\Config\SetPara";
        private string m_TableName = "UserConfig";
        private DataBaseLib.DataBaseInterface m_DBHandle = null;
        private Form1 m_MainFormHandle = null;

        private static UserManager m_SelfHandle = null;
        public string m_CurrenUser = null; //当前用户
        public string m_CurrenUserType = null;//当前用户权限;Config Admin Guest 三种类型
        public UserManager()
        {
            m_DBHandle = new DataBaseLib.DataBaseInterface();
            m_MainFormHandle = Form1.GetHandle();
            ShowSystemInfo += new StatePageInfo(m_MainFormHandle.ShowPageInfo);
            ShowDebugInfo += new StateSysInfo(m_MainFormHandle.ShowSystemInfo);
            bool code = false;
            if (!File.Exists(m_FilePath))
            {
                code = CreatUserDB();
                if (!code)
                {
                    SendDebugInfo("创建用户数据库失败");
                }
                code = AddDefaultUser();
                if (!code)
                {
                    SendDebugInfo("添加用户失败");
                }
            }
            else
            {
                code = OpenUserDB();
                if (!code)
                {
                    SendDebugInfo("数据库打开失败");
                }
            }
        }

        public static UserManager GetHandle()
        {
            if (m_SelfHandle == null)
            {
                m_SelfHandle = new UserManager();
            }
            return m_SelfHandle;
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns></returns>
        private bool OpenUserDB()
        {
            bool code;
            code = m_DBHandle.DBInit("Access", m_FilePath, "Open", m_TableName);
            if (!code)
            {
                return code;
            }
            return m_DBHandle.AddDBtoBuffer();
        }

        /// <summary>
        /// 生成用户数据库
        /// </summary>
        /// <returns></returns>
        public bool CreatUserDB()
        {
            bool code;
            code = m_DBHandle.DBInit("Access", m_FilePath, "Create", m_TableName);
            if(!code)
            {
                return code;
            }
            //用户名称
            code = m_DBHandle.AddColumn(m_TableName, "UserName", "VarChar");
            if(!code)
            {
                return code;
            }
            //用户密码
            code = m_DBHandle.AddColumn(m_TableName, "UserPWD", "VarChar");
            if (!code)
            {
                return code;
            }
            //用户类型
            code = m_DBHandle.AddColumn(m_TableName, "UserType", "VarChar");
            if (!code)
            {
                return code;
            }
            //用户数据存放地址
            code = m_DBHandle.AddColumn(m_TableName, "UserInfoPath", "VarChar");
            if (!code)
            {
                return code;
            }
            return true;
        }

        /// <summary>
        /// 添加默认用户
        /// </summary>
        /// <returns></returns>
        private bool AddDefaultUser()
        {
            m_DBHandle.AddDBtoBuffer();
            int index = 0;
            m_DBHandle.AddRows(m_TableName, ref index);
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserName", "Admin");
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserPWD", "8888");
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserType", "Admin");
            string sUserPath = m_UserFilePath;
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserInfoPath", sUserPath);
            m_DBHandle.SaveDataToBuffer();

            m_DBHandle.AddRows(m_TableName, ref index);
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserName", "Guest");
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserPWD", "9999");
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserType", "Guest");
            sUserPath = m_UserFilePath;
            m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserInfoPath", sUserPath);
            m_DBHandle.SaveDataToBuffer();
            m_DBHandle.SaveDateToDataBase();

            return true;
        }

        /// <summary>
        /// 添加用户名
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool AddUser(string UserName, string PWD)
        {
            DataSet ds = default(DataSet);
            bool code = m_DBHandle.QueryInDB("select * from " + m_TableName, out ds);
            if (!code)
            {
                SendDebugInfo("UserManage QueryDB失败");
            }
            DataTable dt = ds.Tables[0];
            int index = 0;
            if (dt.Rows.Count > 6)
            {
                return false;
            }

            code = m_DBHandle.ClearBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 清除缓存失败");
            }
            code = m_DBHandle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 将数据库数据添加至缓存失败");
            }

            if (m_DBHandle.FindDataInDB(m_TableName, "UserName", UserName, ref index))
            {
                return false;
            }

            
            code = m_DBHandle.AddRows(m_TableName, ref index);
            if (!code)
            {
                SendDebugInfo("UserManage 添加行失败");
            }
            code = m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserName", UserName);
            if (!code)
            {
                SendDebugInfo("UserManage 写入用户名失败");
            }
            code  = m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserPWD", PWD);
            if (!code)
            {
                SendDebugInfo("UserManage 添加密码失败");
            }
            code = m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserType", "Guest");
            if (!code)
            {
                SendDebugInfo("UserManage 添加用户类型失败");
            }
            string sUserPath = m_UserFilePath + @"\" + UserName;
            code = m_DBHandle.WriteSigleDataToDB(m_TableName, index, "UserInfoPath", sUserPath);
            if (!code)
            {
                SendDebugInfo("UserManage 写入路径失败");
            }
            code = m_DBHandle.SaveDataToBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 保存数据至缓存失败");
            }
            code = m_DBHandle.SaveDateToDataBase();
            if (!code)
            {
                SendDebugInfo("UserManage 保存数据失败");
            }

            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool RemoveUser(string UserName)
        {
            if (UserName == "Admin" || UserName == "Guest")
            {
                return false;
            }
            int index = 0;

            bool code = m_DBHandle.ClearBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 清除缓存失败");
            }
            code = m_DBHandle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 添加数据至缓存失败");
            }

            if (!m_DBHandle.FindDataInDB(m_TableName, "UserName", UserName, ref index))
            {
                SendDebugInfo("UserManage 找寻用户名失败");
                return false;
            }

            code  = m_DBHandle.RemoveDataIndexInDB(index);
            if (!code)
            {
                SendDebugInfo("UserManage 移除数据库索引失败");
            }
            //string sPath = m_UserFilePath + @"\" + UserName;
            //Directory.Delete(sPath);
            return true;

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">密码</param>
        /// <returns></returns>
        public bool ModifyPWD(string UserName, string PWD)
        {
            bool code = m_DBHandle.ClearBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 清除缓存失败");
            }
            code = m_DBHandle.AddDBtoBuffer();
            if (!code)
            {
                SendDebugInfo("UserManage 添加数据至缓存失败");
            }
            int index = -1;

            if (!m_DBHandle.FindDataInDB(m_TableName, "UserName", UserName, ref index))
            {
                SendDebugInfo("UserManage 找寻用户名失败");
                return false;
            }

            code = m_DBHandle.OverwriteSigleDataToDB(m_TableName, index, "UserPWD", PWD);
            if (!code)
            {
                SendDebugInfo("UserManage 修改密码失败");
                return false;
            }
            code = m_DBHandle.SaveDateToDataBase();
            if (!code)
            {
                SendDebugInfo("UserManage 保存数据失败");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据用户名及密码，查询用户信息，此函数不用
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="isFind">是否找用户</param>
        /// <param name="returnCode">返回错误代码</param>
        /// <returns></returns>
        public bool GetUserInfo_b(string userName, string passWord, ref string userAuthority, ref bool isFind, ref int returnCode)
        {
            if (userName == "Config")
            {
                if (passWord == "Maximator8888")
                {
                    userAuthority = "Config";
                    isFind = true;
                    returnCode = 0;
                    return true;
                }
                else
                {
                    isFind = false;
                    returnCode = 2;
                    return true;
                }
            }

            else if (userName == "Admin")
            {
                if (passWord == "8888")
                {
                    userAuthority = "Admin";
                    isFind = true;
                    returnCode = 0;
                    return true;
                }
                else
                {
                    isFind = false;
                    returnCode = 2;
                    return true;
                }
            }
            else if (userName == "Guest")
            {
                userAuthority = "Guest";
                isFind = true;
                returnCode = 0;
                return true;
            }
            else
            {
                isFind = false;
                returnCode = 1;
                return true;
            }
        }

        /// <summary>
        /// 根据用户名及密码，查询用户信息，
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="isFind">是否找用户</param>
        /// <param name="returnCode">返回错误代码</param>
        /// <returns></returns>
        public bool CheckUserInfo(string userName, string passWord, ref string userAuthority, ref bool isFind, ref int returnCode)
        {
            if (userName == "Config")
            {
                if (passWord == "M8888")
                {
                    userAuthority = "Config";
                    isFind = true;
                    returnCode = 0;
                    m_CurrenUser = "Config";
                    m_CurrenUserType = "Config";
                    return true;
                }
                else
                {
                    isFind = false;
                    returnCode = 2;
                    return true;
                }
            }

            DataSet ds = default(DataSet);
            m_DBHandle.QueryInDB("select * from " + m_TableName, out ds);
            DataTable dt = ds.Tables[0];

            int count = dt.Rows.Count;
            int index = -1;
            for (int i = 0; i < count; i++)
            {
                if (dt.Rows[i]["UserName"].ToString() == userName)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                isFind = false;
                returnCode = 1;
                return true;
            }

            if (dt.Rows[index]["UserPWD"].ToString() != passWord)
            {
                isFind = false;
                returnCode = 2;
                return true;
            }
            m_CurrenUser = dt.Rows[index]["UserName"].ToString();
            m_CurrenUserType = dt.Rows[index]["UserType"].ToString();
            isFind = true;
            returnCode = 0;
            return true;
        }

        public bool GetALLUserName(out string[] userName)
        {
            DataSet ds = default(DataSet);
            bool code = m_DBHandle.QueryInDB("select * from " + m_TableName, out ds);
            if (!code)
            {
                SendDebugInfo("UserManage 查询数据失败");
            }
            DataTable dt = ds.Tables[0];

            int count = dt.Rows.Count;
            if (count < 2)
            {
                userName = new string[0];
                return false;
            }

            userName = new string[count];

            for (int i = 0; i < count; i++)
            {
                userName[i] = dt.Rows[i]["UserName"].ToString();
            }

            return true;
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
