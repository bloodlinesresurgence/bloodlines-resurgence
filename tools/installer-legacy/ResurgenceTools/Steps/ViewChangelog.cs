using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Resurgence;
using ResurgenceLib;
using RevivalLib;

namespace Resurgence.Steps
{
    internal partial class ViewChangelog : UpdateAndPatchFiles
    {
        public ViewChangelog()
        {
            InitializeComponent();
        }

        public ViewChangelog(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            AutoProceedStep = true;
            InitializeComponent();

            prepareSmallgit();

            BinOutput output = Git("log --pretty=medium --abbrev-commit");
            Changelog.Text = output.StdOutput;

            NextButton.Enabled = true;
        }

        protected new Result DoWork()
        {
            // To force user to click next
            return Result.Failure;
        }
    }
}
