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
    /// Extracts the materials so they can be converted in a later step.
    /// </summary>
    internal partial class ExtractMaterials 
        : GenericExtractFiles
    {
        public ExtractMaterials()
        {
            InitializeComponent();
        }

        public ExtractMaterials(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            extractPattern = "materials/*";
            outputDirectory = Program.Settings.TemporaryDirectory;

            StartExtraction();
        }
    }
}
