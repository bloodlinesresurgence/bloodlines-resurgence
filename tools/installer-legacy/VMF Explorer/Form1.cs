using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VMF_Explorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Map Files|*.vmf|All Files|*.*";
            dlg.Title = "Open Map";

            DialogResult result = dlg.ShowDialog(this);
            if (result != DialogResult.OK)
            {
                return;
            }

            string file = dlg.FileName;
            
        }
    }
}
