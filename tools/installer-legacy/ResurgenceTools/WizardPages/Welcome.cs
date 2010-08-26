using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using System.IO;

namespace ResurgenceTools.WizardPages
{
    public partial class Welcome 
        : ResurgenceWizardPage
    {
        public Welcome()
        {
            InitializeComponent();
        }

        public Welcome(TranslationProvider Provider)
            : base(Provider)
        { }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Dispose();
            Hide();
            Program.NextForm = new SelectPaths(TranslationProvider);
            Close();
        }
    }
}
