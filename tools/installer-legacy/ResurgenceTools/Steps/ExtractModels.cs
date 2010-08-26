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
    internal partial class ExtractModels : GenericExtractFiles
    {
        public ExtractModels()
        {
            InitializeComponent();
        }

        public ExtractModels(TranslationProvider Provider)
            : base(Provider)
        { }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            extractPattern = "models/*";
            exemptPattern = new string[] { "materials/*" };

            outputDirectory = Program.Settings.DestinationDirectory + "\\models_raw";

            StartExtraction();
        }

        
    }
}
