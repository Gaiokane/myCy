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
    public partial class CPCX : Form
    {
        public CPCX()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();

            string strsql = "select * from tb_Waiter where CardNum = '" + textBox1.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(strsql, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();

            textBox2.Text = sdr[1].ToString();
            textBox3.Text = sdr[4].ToString();
            textBox4.Text = sdr[5].ToString();
            textBox5.Text = sdr[6].ToString();

            conn.Close();
        }
    }
}
