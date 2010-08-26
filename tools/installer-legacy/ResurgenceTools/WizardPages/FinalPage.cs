using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Resurgence.WizardPages
{
    /// <summary>
    /// The final wizard page.
    /// </summary>
    internal partial class FinalPage 
        : ResurgenceWizardPage
    {
        public FinalPage()
        {
            InitializeComponent();
        }

        internal FinalPage (ResurgenceLib.TranslationProvider Provider)
            : base(Provider)
        {
            ConfirmUserQuit = false;
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            string doc = Program.Settings.FinishDocument;
            if (System.IO.File.Exists(doc) == true)
                Document.LoadFile(doc);
            else
                Document.Text = String.Format(Translate("!FinalDocNotFound"), doc);
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.NextForm = new SelectSteps(TranslationProvider);
            Close();
        }

        private void Document_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
