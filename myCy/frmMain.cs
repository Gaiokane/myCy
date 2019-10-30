using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace myCy
{
    public partial class frmMain : Form
    {
        public string Names;
        public string Pow;
        public string Times;
        public SqlDataReader sdr;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            switch (Pow)
            {
                case "0": toolStripStatusLabel5.Text = "超级管理员"; break;
                case "1": toolStripStatusLabel5.Text = "经理"; break;
                case "2": toolStripStatusLabel5.Text = "一般用户"; break;
            }
            toolStripStatusLabel2.Text = Names;
            toolStripStatusLabel8.Text = Times;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void 日历ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //monthCalendar1.Visible = true;
        }

        private void 计数器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void 记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            lvdesk.Items.Clear();
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "select * from tb_Room";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string zt = sdr["RoomZT"].ToString().Trim();
                AddItems(zt);
            }
            conn.Close();
        }
        private void AddItems(string rzt)
        {
            if (rzt == "使用")
            {
                lvdesk.Items.Add(sdr["RoomName"].ToString(), 1);
            }
            else
            {
                lvdesk.Items.Add(sdr["RoomName"].ToString(), 0);
            }
        }

        private void 最大化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void 开台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvdesk.SelectedItems.Count != 0)
            {
                string names = lvdesk.SelectedItems[0].SubItems[0].Text.Trim();//取出桌台文本
                frmOpen fo = new frmOpen();
                fo.Rname = names;
                fo.Show();
            }
            else
            {
                MessageBox.Show("请选择桌台！");
            }
        }

        private void 取消开台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvdesk.SelectedItems.Count != 0)
            {
                string names = lvdesk.SelectedItems[0].SubItems[0].Text.Trim();//取出桌台文本
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();

                //更新tb_Room的信息
                string sqlstr = "update tb_Room set RoomZT='待用',RoomBZ='',GuestName='',zhangdanDate='',Num=0 where RoomName=@RoomName";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                cmd.Parameters.Add("@RoomName", SqlDbType.VarChar, 50).Value = names;
                cmd.ExecuteNonQuery();

                //删除tb_GurstFood的信息
                sqlstr = "delete from tb_GuestFood where zhuotai='" + names + "'";
                cmd = new SqlCommand(sqlstr, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                frmMain_Activated(sender, e);
            }
            else
            {
                MessageBox.Show("请选择桌台！");
            }
        }

        private void 点加菜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvdesk.SelectedItems.Count != 0)
            {
                string names = lvdesk.SelectedItems[0].SubItems[0].Text.Trim();//取出桌台文本
                frmDC dc = new frmDC();
                dc.RName = names;
                dc.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择桌台！");
            }
        }

        private void 结帐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvdesk.SelectedItems.Count != 0)
            {
                string names = lvdesk.SelectedItems[0].SubItems[0].Text.Trim();//取出桌台文本
                frmDC dc = new frmDC();
                dc.RName = names;
                dc.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择桌台！");
            }
        }
    }
}
