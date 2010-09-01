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
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] content_b = encoding.GetBytes(content);
            System.IO.MemoryStream input = new System.IO.MemoryStream(content_b);
            System.IO.MemoryStream output = new System.IO.MemoryStream();
            System.IO.Compression.DeflateStream deflate = new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress);
            byte[] buffer = new byte[4096];
            int numRead = 0;
            while ((numRead = deflate.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, numRead);
            }
            output.Seek(0, System.IO.SeekOrigin.Begin);
            byte[] final_buffer = new byte[output.Length];
            string final = encoding.GetString(final_buffer);

            Report.Text = final;
        }
    }
}
