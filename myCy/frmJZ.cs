using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myCy
{
    public partial class frmJZ : Form
    {
        public string RNAme;
        public frmJZ()
        {
            InitializeComponent();
        }

        private void frmJZ_Load(object sender, EventArgs e)
        {
            this.Text = RNAme + "结帐";
            groupBox1.Text = "当前的桌台-" + RNAme;
        }
    }
}
