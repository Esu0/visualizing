using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualizing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void stk_que_Click(object sender, EventArgs e)
        {
            Video form = new Video("stk_que");
            form.ShowDialog();
        }

        private void sort_Click(object sender, EventArgs e)
        {
            Video form = new Video("sort");
            form.ShowDialog();
        }
    }
}
