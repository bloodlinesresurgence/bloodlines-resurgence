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
    internal partial class ModDetails 
        : ResurgenceWizardPage
    {
        public ModDetails()
        {
            InitializeComponent();
        }

        internal ModDetails(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            SourceModDirectory.Text = Program.Settings.ModDirectory;
            updateInstallPath();

#if DEBUG
            Destination.ReadOnly = false;
#endif
        }

        private void SourceModDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.ModDirectory = SourceModDirectory.Text;

            updateInstallPath();
        }

        private bool updateInstallPath()
        {
            string path = Program.Settings.DestinationDirectory;
            Destination.Text = path;

            bool pathExists = Directory.Exists(path);

            NextButton.Enabled = pathExists;
            ModNotFound.Visible = !pathExists;

            return pathExists;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
#if !FULL_TOOLSET
            Program.NextForm = new SelectPaths(TranslationProvider);
#else
            (new SelectPaths(TranslationProvider)).Show(Program.MainForm);
#endif
            Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
#if DEBUG
            // Allow install override
            Program.Settings.DestinationDirectory = Destination.Text;
#endif

#if !FULL_TOOLSET
            Program.NextForm = new SelectOperation(TranslationProvider);
#else
            Hide();
            (new SelectSteps(TranslationProvider)).Show(Program.MainForm);
#endif
            Close();
        }
    }
}
