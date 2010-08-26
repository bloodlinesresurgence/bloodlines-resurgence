using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;

namespace Resurgence.Steps
{
    /// <summary>
    /// Extracts the general required files to the mod directory.
    /// </summary>
    internal partial class ExtractGeneralFiles 
        : GenericExtractFiles
    {
        public ExtractGeneralFiles()
        {
            InitializeComponent();
        }

        public ExtractGeneralFiles(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            outputDirectory = Program.Settings.DestinationDirectory;
            extractPattern = "*";
            exemptPattern = new string[] 
            {
                "models/*",
                "materials/*",
                "shaders/*",
                "resource/*"
            };

            StartExtraction();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.NextStep();
            Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.PreviousStep();
            Close();
        }


    }
}
