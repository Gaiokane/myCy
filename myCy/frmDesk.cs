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
    public partial class frmDesk : Form
    {
        public frmDesk()
        {
            InitializeComponent();
        }

        private void BindData()
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            string sqlstr = "select RoomName,RoomJC,RoomBJF,RoomWZ,RoomType,RoomBZ,ID from tb_Room order by ID desc";
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
            textBox2.Text = dataGridView1.SelectedCells[3].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[2].Value.ToString();
            textBox6.Text = dataGridView1.SelectedCells[5].Value.ToString();
        }
    }
}
