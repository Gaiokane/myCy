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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void BindData()
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string sqlstr = "select UserName,UserPwd,power,ID from tb_User order by power asc";
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
            
            button2.Enabled = true;
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "delete from tb_User where ID=@ID ";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = dataGridView1.SelectedCells[3].Value.ToString();

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;

            textBox1.ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            f.Show();


            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "select count (*) from tb_User where UserName=@UserName";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 20).Value = textBox1.Text.Trim();
            int n = Convert.ToInt32(cmd.ExecuteScalar());
            if (n > 0)
            {
                //修改
                strsql = "update tb_User set UserName=@UserName,UserPwd=@UserPwd,power=@power where ID=@ID";
                cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = textBox1.Text;
                cmd.Parameters.Add("@UserPwd", SqlDbType.VarChar, 50).Value = textBox2.Text;
                cmd.Parameters.Add("@power", SqlDbType.VarChar, 50).Value = textBox3.Text;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.SelectedCells[3].Value);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindData();
            }
            else
            {
                //添加
                strsql = "insert into tb_User(UserName,UserPwd,power) values(@UserName,@UserPwd,@power) ";
                cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = textBox1.Text;
                cmd.Parameters.Add("@UserPwd", SqlDbType.VarChar, 50).Value = textBox2.Text;
                cmd.Parameters.Add("@power", SqlDbType.VarChar, 50).Value = textBox3.Text;
                cmd.ExecuteNonQuery();
                conn.Close();
                BindData();
            }
            conn.Close();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
