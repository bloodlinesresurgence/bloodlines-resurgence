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

            // Load the welcome document for the specified language.
            string doc = Program.Settings.WelcomeDocument;
            if (File.Exists(doc))
                Document.LoadFile(doc);
            else
                Document.Text = String.Format(Translate("!WelcomeDocNotFound"), doc);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.NextForm = new SelectPaths(TranslationProvider);
            Close();
        }

        private void Understand_CheckedChanged(object sender, EventArgs e)
        {
            NextButton.Enabled = Understand.Checked;
        }

        private void Document_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
