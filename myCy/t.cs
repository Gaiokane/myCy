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
    public partial class t : Form
    {
        public t()
        {
            InitializeComponent();
        }

        private void t_Load(object sender, EventArgs e)
        {
            TreeNode tr = new TreeNode("公司职员", 0, 1);
            tr.Nodes.Add("", "小一", 0, 1);
            tr.Nodes.Add("", "小二", 0, 1);
            tr.Nodes.Add("", "小三", 0, 1);
            tr.Nodes.Add("", "小四", 0, 1);
            tr.Nodes.Add("", "小五", 0, 1);
            tr.Nodes.Add("", "小六", 0, 1);

            treeView1.Nodes.Add(tr);
            treeView1.ExpandAll();
        }
    }
}
