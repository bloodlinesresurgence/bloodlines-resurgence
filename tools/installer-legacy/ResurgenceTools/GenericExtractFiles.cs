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
    internal partial class GenericExtractFiles 
        : ResurgenceWizardPage
    {
        protected string extractPattern;
        protected string[] exemptPattern;
        protected string outputDirectory;

        public GenericExtractFiles()
        {
            InitializeComponent();
        }

        public GenericExtractFiles(TranslationProvider Provider)
            : base(Provider)
        {
            outputDirectory = Program.Settings.DestinationDirectory;
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            Vextract.BindProvider(TranslationProvider);

            AutoProceedStep = true;
        }

        protected override void AutoProceed_Start()
        {
            Vextract.StartNow();
        }

        protected void StartExtraction()
        {
            Vextract.OutputDirectory = outputDirectory;
            Vextract.VPKDirectory = Program.Settings.VPKDirectory;
            Vextract.Pattern = extractPattern;
            Vextract.ExemptionPatterns = exemptPattern;
            Vextract.OnFinishedExtraction += OnFinished;
            Vextract.OnStartExtraction += OnStartExtraction;
            Vextract.Start();
        }

        /*private void OnStartExtraction()
        {
            if (this.InvokeRequired == false)
            {
                BackButton.Enabled = false;
            }
            else
            {
                this.Invoke(new OnStartExtractionDelegate(OnStartExtraction));
            }
        }
        private delegate void OnStartExtractionDelegate();*/

        private void OnStartExtraction()
        {
            Step_Blocking();
        }

        /*private void OnFinished(int count)
        {
            if (this.InvokeRequired == false)
            {
                if (Vextract.Cancelled == false || Program.Settings.IgnoreCancelExtraction)
                    NextButton.Enabled = true;
                BackButton.Enabled = true;
                CancelWizardButton.Enabled = true;
            }
            else
            {
                this.Invoke(new OnFinishedDelegate(OnFinished), new object[] { count });
            }
        }
        private delegate void OnFinishedDelegate(int count);*/
        private void OnFinished(int count)
        {
            if (Vextract.Cancelled == false || Program.Settings.IgnoreCancelExtraction)
                Step_Success();
            else
                Step_Failure();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.PreviousStep();
            Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.NextStep();
            Close();
        }

        private void ExtractGeneralFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason != CloseReason.UserClosing)
                Vextract.Cancel();
        }
    }
}
