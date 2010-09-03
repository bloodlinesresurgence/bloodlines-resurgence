using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ErrorReportViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
            p.Add("id", ReportNumber.Text);
            Snowball.Common.PostSubmitter submitter = new Snowball.Common.PostSubmitter(
                "http://www.bloodlinesresurgence.com/error_reports/get.php", p);
            string content = submitter.Post();

            Report.Text = ResurgenceLib.Compression.StringDecompress(content);
        }
    }
}
