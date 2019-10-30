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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("请输入用户名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtPwd.Text == "")
                {
                    MessageBox.Show("请输入密码！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlConnection conn = BaseClass.DBConn.CyCon();
                    conn.Open();
                    //MessageBox.Show("ok");
                    //string sqlstr = "SELECT * FROM tb_User WHERE UserName='" + txtName.Text + "' AND UserPwd='" + txtPwd.Text + "'";
                    string sqlstr = "SELECT * FROM tb_User WHERE UserName=@UserName AND UserPwd=@UserPwd";
                    SqlCommand cmd = new SqlCommand(sqlstr, conn);
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = txtName.Text;
                    cmd.Parameters.Add("@UserPwd", SqlDbType.VarChar, 50).Value = txtPwd.Text;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    if (sdr.HasRows)
                    {
                        //label3.Text = sdr["UserName"].ToString().Trim();
                        string qx = sdr[3].ToString().Trim();
                        //MessageBox.Show("登录成功！");
                        frmMain main = new frmMain();
                        main.Names = txtName.Text;
                        main.Pow = qx;
                        main.Times = DateTime.Now.ToShortTimeString();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    conn.Close();
                }
            }
            //if (txtName.Text == "admin" && txtPwd.Text == "q")
            //{
            //    MessageBox.Show("admin登陆成功", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //MessageBox.Show("请输入用户名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("请输入密码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("用户名或密码错误", "", MessageBoxButtons.OK);
            //MessageBox.Show("mr登陆成功", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //MessageBox.Show("确定退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //this.Close();
            if (MessageBox.Show("确定退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSubmit_Click(sender, e);
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "SELECT * FROM tb_User";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //textBox1.Text = textBox1.Text + "用户名：" + sdr["UserName"].ToString().Trim() + "密码：" + sdr["UserPwd"].ToString().Trim();
                listBox1.Items.Add("用户名：" + sdr["UserName"].ToString().Trim() + "密码：" + sdr["UserPwd"].ToString().Trim());
            }
            conn.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "INSERT into tb_User(UserName,UserPwd,power) values(@UserName,@UserPwd,@power)";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = txtName.Text;
            cmd.Parameters.Add("@UserPwd", SqlDbType.VarChar, 50).Value = txtPwd.Text;
            cmd.Parameters.Add("@power", SqlDbType.Int).Value = textBox1.Text;
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
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string strsql = "delete from tb_User where UserName=@UserName)";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = txtName.Text;
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
        }
    }
}
