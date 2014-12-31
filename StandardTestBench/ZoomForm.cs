using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StandardTestBench
{
    public partial class ZoomForm : Form
    {
        private QueryDB m_QueryDBHandle = null;
        private string m_BenchNo = "";
        public ZoomForm(QueryDB Handle, string BenchNo)
        {
            InitializeComponent();
            m_QueryDBHandle = Handle;
            m_BenchNo = BenchNo;
        }

        string m_yMax = "";
        string m_StartTime = "";
        string m_EndTime = "";
        private void ZoomForm_Load(object sender, EventArgs e)
        {
            if (m_BenchNo == "M1")
            {
                float yMax = m_QueryDBHandle.m_M1yMax;
                m_yMax = yMax.ToString();
                DateTime baseTime = m_QueryDBHandle.m_M1BaseStartTime;
                m_StartTime = baseTime.ToLongTimeString();
                DateTime endTime = m_QueryDBHandle.m_M1BaseEndTime;
                m_EndTime = endTime.ToLongTimeString();
            }
            if (m_BenchNo == "M2")
            {
                float yMax = m_QueryDBHandle.m_M2yMax;
                m_yMax = yMax.ToString();
                DateTime baseTime = m_QueryDBHandle.m_M2BaseStartTime;
                m_StartTime = baseTime.ToLongTimeString();
                DateTime endTime = m_QueryDBHandle.m_M2BaseEndTime;
                m_EndTime = endTime.ToLongTimeString();
            }
            if (m_BenchNo == "M3")
            {
                float yMax = m_QueryDBHandle.m_M3yMax;
                m_yMax = yMax.ToString();
                DateTime baseTime = m_QueryDBHandle.m_M3BaseStartTime;
                m_StartTime = baseTime.ToLongTimeString();
                DateTime endTime = m_QueryDBHandle.m_M3BaseEndTime;
                m_EndTime = endTime.ToLongTimeString();
            }
            if (m_BenchNo == "M4")
            {
                float yMax = m_QueryDBHandle.m_M4yMax;
                m_yMax = yMax.ToString();
                DateTime baseTime = m_QueryDBHandle.m_M4BaseStartTime;
                m_StartTime = baseTime.ToLongTimeString();
                DateTime endTime = m_QueryDBHandle.m_M4BaseEndTime;
                m_EndTime = endTime.ToLongTimeString();
            }

        }

        private void BT_Default_Click(object sender, EventArgs e)
        {
            if (m_BenchNo == "M1")
            {
                m_QueryDBHandle.m_ZoomyMaxM1 = Convert.ToSingle(m_yMax);
                TB_Set_MaxP.Text = m_yMax;
                m_QueryDBHandle.m_ZoomStartTimeM1 = Convert.ToDateTime(m_StartTime);
                TB_Set_StartTime.Text = m_StartTime;
                m_QueryDBHandle.m_ZoomEndTimeM1 = Convert.ToDateTime(m_EndTime);
                TB_Set_EndTime.Text = m_EndTime;
            }

            if (m_BenchNo == "M2")
            {
                m_QueryDBHandle.m_ZoomyMaxM2 = Convert.ToSingle(m_yMax);
                TB_Set_MaxP.Text = m_yMax;
                m_QueryDBHandle.m_ZoomStartTimeM2 = Convert.ToDateTime(m_StartTime);
                TB_Set_StartTime.Text = m_StartTime;
                m_QueryDBHandle.m_ZoomEndTimeM2 = Convert.ToDateTime(m_EndTime);
                TB_Set_EndTime.Text = m_EndTime;
            }

            if (m_BenchNo == "M3")
            {
                m_QueryDBHandle.m_ZoomyMaxM3 = Convert.ToSingle(m_yMax);
                TB_Set_MaxP.Text = m_yMax;
                m_QueryDBHandle.m_ZoomStartTimeM3 = Convert.ToDateTime(m_StartTime);
                TB_Set_StartTime.Text = m_StartTime;
                m_QueryDBHandle.m_ZoomEndTimeM3 = Convert.ToDateTime(m_EndTime);
                TB_Set_EndTime.Text = m_EndTime;
            }

            if (m_BenchNo == "M4")
            {
                m_QueryDBHandle.m_ZoomyMaxM4 = Convert.ToSingle(m_yMax);
                TB_Set_MaxP.Text = m_yMax;
                m_QueryDBHandle.m_ZoomStartTimeM4 = Convert.ToDateTime(m_StartTime);
                TB_Set_StartTime.Text = m_StartTime;
                m_QueryDBHandle.m_ZoomEndTimeM4 = Convert.ToDateTime(m_EndTime);
                TB_Set_EndTime.Text = m_EndTime;
            }
        }

        private void BT_Set_Click(object sender, EventArgs e)
        {
            float m_yMax = Convert.ToSingle(TB_Set_MaxP.Text);
            DateTime startTime = Convert.ToDateTime(TB_Set_StartTime.Text);
            DateTime endTime = Convert.ToDateTime(TB_Set_EndTime.Text);
            DateTime baseStartTime = Convert.ToDateTime(m_StartTime);
            DateTime baseEndTime = Convert.ToDateTime(m_EndTime);

            if ((startTime - baseStartTime).TotalSeconds < 0)
            {
                startTime = baseStartTime;
            }

            if ((endTime - baseEndTime).TotalSeconds < 0)
            {
                endTime = baseEndTime;
            }

            if ((startTime - baseEndTime).TotalSeconds >= 0)
            {
                startTime = baseStartTime;
                endTime = baseEndTime;
            }

            TB_Set_StartTime.Text = startTime.ToLongTimeString();
            TB_Set_EndTime.Text = endTime.ToLongTimeString();

            if (m_BenchNo == "M1")
            {
                m_QueryDBHandle.m_ZoomyMaxM1 = Convert.ToSingle(TB_Set_MaxP.Text);
                m_QueryDBHandle.m_ZoomStartTimeM1 = startTime;
                m_QueryDBHandle.m_ZoomEndTimeM1 = endTime;
            }
            if (m_BenchNo == "M2")
            {
                m_QueryDBHandle.m_ZoomyMaxM1 = Convert.ToSingle(TB_Set_MaxP.Text);
                m_QueryDBHandle.m_ZoomStartTimeM1 = startTime;
                m_QueryDBHandle.m_ZoomEndTimeM1 = endTime;
            }
            if (m_BenchNo == "M3")
            {
                m_QueryDBHandle.m_ZoomyMaxM1 = Convert.ToSingle(TB_Set_MaxP.Text);
                m_QueryDBHandle.m_ZoomStartTimeM1 = startTime;
                m_QueryDBHandle.m_ZoomEndTimeM1 = endTime;
            }
            if (m_BenchNo == "M4")
            {
                m_QueryDBHandle.m_ZoomyMaxM1 = Convert.ToSingle(TB_Set_MaxP.Text);
                m_QueryDBHandle.m_ZoomStartTimeM1 = startTime;
                m_QueryDBHandle.m_ZoomEndTimeM1 = endTime;
            }


        }

        private void BT_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
