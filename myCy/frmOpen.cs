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
    public partial class frmOpen : Form
    {
        public string Rname;

        public frmOpen()
        {
            InitializeComponent();
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //不允许输入字符
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar) && e.KeyChar != 13)
            {
                MessageBox.Show("请输入数字！");
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "select * from tb_Room";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //cbNum.Items.Add(sdr["RoomName"].ToString());
                cbNum.Items.Add(sdr[1].ToString());
            }
            sdr.Close();
            //cbNum.SelectedIndex = 0;//-1为空
            cbNum.SelectedText = Rname;

            strsql = "select * from tb_Waiter";
            cmd = new SqlCommand(strsql, conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //cbNum.Items.Add(sdr["RoomName"].ToString());
                cbWaiter.Items.Add(sdr[1].ToString());
            }
            sdr.Close();
            cbWaiter.SelectedIndex = 0;//-1为空

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtNum.Text == "") || Convert.ToInt32(txtNum.Text) <= 0)
            {
                MessageBox.Show("请输入用餐人数！");
            }
            else
            {
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();
                string sqlstr = "update tb_Room set GuestName=@GuestName,zhangdanDate=@zhangdanDate,Num=@Num,WaiterName=@WaiterName,RoomBZ=@RoomBZ,RoomZT='使用'where RoomName=@RoomName";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                cmd.Parameters.Add("@GuestName", SqlDbType.VarChar, 50).Value = txtName.Text.ToString();
                cmd.Parameters.Add("@zhangdanDate", SqlDbType.VarChar, 50).Value = dateTimePicker1.Value.ToString();
                cmd.Parameters.Add("@Num", SqlDbType.VarChar, 50).Value = Convert.ToInt32(txtNum.Text);
                cmd.Parameters.Add("@WaiterName", SqlDbType.VarChar, 50).Value = cbWaiter.SelectedItem.ToString();
                cmd.Parameters.Add("@RoomBZ", SqlDbType.VarChar, 50).Value = txtBZ.Text.ToString();
                cmd.Parameters.Add("@RoomName", SqlDbType.VarChar, 50).Value = cbNum.Text;

                int n = cmd.ExecuteNonQuery();
                if (n >= 0)
                {
                    MessageBox.Show("添加成功！");
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
                conn.Close();
            }
            this.Close();
        }
    }
}
