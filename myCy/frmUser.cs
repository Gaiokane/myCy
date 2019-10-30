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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void BindData()
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string sqlstr = "select waiterName,CardNum,WaiterNum,Sex,Age,Tel,ID from tb_Waiter order by ID desc";
            SqlDataAdapter sda = new SqlDataAdapter(sqlstr, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void btn_esc_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[2].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedCells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[5].Value.ToString();

            button2.Enabled = true;
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "delete from tb_Waiter where ID=@ID ";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = dataGridView1.SelectedCells[6].Value.ToString();

            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                MessageBox.Show("删除记录成功！");
            }
            else
            {
                MessageBox.Show("删除记录失败！");
            }
            conn.Close();
            BindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
            textBox5.Enabled = true;

            textBox1.ReadOnly = true;

            button1.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            textBox5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBox1.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;

            textBox1.ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "select count (*) from tb_Waiter where WaiterName=@WaiterName";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Parameters.Add("@WaiterName", SqlDbType.VarChar, 20).Value = textBox1.Text.Trim();
            int n = Convert.ToInt32(cmd.ExecuteScalar());
            if (n > 0)
            {
                //修改
                strsql = "update tb_Waiter set WaiterName=@WaiterName,CardNum=@CardNum,WaiterNum=@WaiterNum,Sex=@Sex,Age=@Age,Tel=@Tel where ID=@ID";
                cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.Add("@WaiterName", SqlDbType.VarChar, 50).Value = textBox1.Text;
                cmd.Parameters.Add("@CardNum", SqlDbType.VarChar, 50).Value = textBox2.Text;
                cmd.Parameters.Add("@WaiterNum", SqlDbType.VarChar, 50).Value = textBox3.Text;
                cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 50).Value = comboBox1.Text;
                cmd.Parameters.Add("@Age", SqlDbType.VarChar, 50).Value = textBox4.Text;
                cmd.Parameters.Add("@Tel", SqlDbType.VarChar, 50).Value = textBox5.Text;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.SelectedCells[6].Value);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindData();
            }
            else
            {
                //添加
                strsql = "insert into tb_Waiter(WaiterName,CardNum,WaiterNum,Sex,Age,Tel) values(@WaiterName,@CardNum,@WaiterNum,@Sex,@Age,@Tel) ";
                cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.Add("@WaiterName", SqlDbType.VarChar, 50).Value = textBox1.Text;
                cmd.Parameters.Add("@CardNum", SqlDbType.VarChar, 50).Value = textBox2.Text;
                cmd.Parameters.Add("@WaiterNum", SqlDbType.VarChar, 50).Value = textBox3.Text;
                cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 50).Value = comboBox1.Text;
                cmd.Parameters.Add("@Age", SqlDbType.VarChar, 50).Value = textBox4.Text;
                cmd.Parameters.Add("@Tel", SqlDbType.VarChar, 50).Value = textBox5.Text;
                cmd.ExecuteNonQuery();
                conn.Close();
                BindData();
            }
            conn.Close();
        }
    }
}
