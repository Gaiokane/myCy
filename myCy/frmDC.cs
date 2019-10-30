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
    public partial class frmDC : Form
    {
        public string RName;

        public frmDC()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void frmDC_Load(object sender, EventArgs e)
        {
            BindData();

            TreeNode tr1 = new TreeNode("锅底", 0, 1);
            TreeNode tr2 = new TreeNode("配菜", 0, 1);
            TreeNode tr3 = new TreeNode("烟酒", 0, 1);
            TreeNode tr4 = new TreeNode("主食", 0, 1);

            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();

            string strsql = "select * from tb_food where foodty = 1";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                tr1.Nodes.Add("", sdr["foodname"].ToString().Trim(), 0, 1);
            }
            sdr.Close();

            strsql = "select * from tb_food where foodty = 2";
            cmd = new SqlCommand(strsql, conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tr2.Nodes.Add("", sdr["foodname"].ToString().Trim(), 0, 1);
            }
            sdr.Close();

            strsql = "select * from tb_food where foodty = 3";
            cmd = new SqlCommand(strsql, conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tr3.Nodes.Add("", sdr["foodname"].ToString().Trim(), 0, 1);
            }
            sdr.Close();

            strsql = "select * from tb_food where foodty = 4";
            cmd = new SqlCommand(strsql, conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tr4.Nodes.Add("", sdr["foodname"].ToString().Trim(), 0, 1);
            }
            sdr.Close();

            treeView1.Nodes.Add(tr1);
            treeView1.Nodes.Add(tr2);
            treeView1.Nodes.Add(tr3);
            treeView1.Nodes.Add(tr4);

            strsql = "select * from tb_Waiter";
            cmd = new SqlCommand(strsql, conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //cbNum.Items.Add(sdr["RoomName"].ToString());
                comboBox1.Items.Add(sdr[1].ToString());
            }
            sdr.Close();
            comboBox1.SelectedIndex = 0;//-1为空

            strsql = "select RoomZT from tb_Room where RoomName='" + RName + "'"; ;
            cmd = new SqlCommand(strsql, conn);

            string zt = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (zt == "待用")
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
            }
            
            conn.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //不允许输入字符
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar) && e.KeyChar != 13)
            {
                MessageBox.Show("请输入数字！");
                e.Handled = true;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            string foodname = treeView1.SelectedNode.Text;
            if (foodname == "锅底" || foodname == "配菜" || foodname == "烟酒" || foodname == "主食")
            {

            }
            else
            {
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();

                string strsql = "select * from tb_food where foodname = '" + foodname + "'";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();

                textBox1.Text = sdr[2].ToString();
                textBox2.Text = foodname;
                textBox3.Text = sdr[4].ToString();

                conn.Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("数量不能为空！");
            }
            else
            {
                textBox5.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) * Convert.ToInt32(textBox4.Text));
            }
        }

        private void BindData()
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string sqlstr = "select foodnum,foodallprice,waitername,beizhu,zhuotai,datatime,ID from tb_GuestFood where zhuotai='" + RName + "'order by id desc";
            SqlDataAdapter sda = new SqlDataAdapter(sqlstr, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("请选择菜品！");
                return;
            }
            else
            {
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();
                string strsql = "insert into tb_GuestFood(foodnum,foodallprice,foodsum,foodname,waitername,beizhu,zhuotai,datatime) values(@foodnum,@foodallprice,@foodsum,@foodname,@waitername,@beizhu,@zhuotai,@datatime) ";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.Add("@foodnum", SqlDbType.VarChar, 50).Value = textBox1.Text;
                cmd.Parameters.Add("@foodallprice", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox5.Text);
                cmd.Parameters.Add("@foodsum", SqlDbType.VarChar, 50).Value = textBox4.Text;
                cmd.Parameters.Add("@foodname", SqlDbType.VarChar, 50).Value = textBox2.Text;
                cmd.Parameters.Add("@waitername", SqlDbType.VarChar, 50).Value = comboBox1.Text;
                cmd.Parameters.Add("@beizhu", SqlDbType.VarChar, 50).Value = textBox6.Text;
                cmd.Parameters.Add("@zhuotai", SqlDbType.VarChar, 50).Value = RName;
                cmd.Parameters.Add("@datatime", SqlDbType.VarChar, 50).Value = DateTime.Now.ToString();

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("插入记录成功！");
                }
                else
                {
                    MessageBox.Show("插入记录失败！");
                }
                conn.Close();
                BindData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//判断是否选中某条记录
            {
                string names = dataGridView1.SelectedCells[0].Value.ToString();//选中要删除的菜品编号的名字
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();
                string strsql = "delete from tb_GuestFood where zhuotai='" + RName + "' and foodname='" + names + "'";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                int n = cmd.ExecuteNonQuery();
                conn.Close();
                BindData();
            }
        }
    }
}
