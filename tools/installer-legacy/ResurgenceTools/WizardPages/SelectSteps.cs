using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;

namespace Resurgence.WizardPages
{
    /// <summary>
    /// Provides a page for selecting steps to run.
    /// </summary>
    internal partial class SelectSteps 
        : ResurgenceWizardPage
    {
        public SelectSteps(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();
#if DEBUG
            IgnoreCancelExtraction.Visible = true;
            IgnoreCancelExtraction.Checked = true;
#endif

            AutoProceed.Checked = Program.Settings.AutoProceed;
        }

        protected override void PostTranslate()
        {
            // Add steps to list
            Steps.Items.Clear();



            foreach (string enumName in Enum.GetNames(typeof(WizardSteps)))
            {
                WizardStepItem listitem = new WizardStepItem(TranslationProvider, enumName, this.Name);

                switch (listitem.Step)
                {
                    case WizardSteps.All:
                    case WizardSteps.NONE:
                    case WizardSteps.INSTALL:
                        continue;
                }

                bool shouldSelect = (Program.SelectedSteps & listitem.Step) == listitem.Step;
                Steps.Items.Add(listitem, shouldSelect);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
#if !FULL_TOOLSET
            Program.NextForm = new ModDetails(TranslationProvider);
#else
            Hide();
            (new ModDetails(TranslationProvider)).Show(Program.MainForm);
#endif
            Close();
        }

        private void Steps_MouseMove(object sender, MouseEventArgs e)
        {
            // Find node under mouse
            int index = Steps.IndexFromPoint(e.X, e.Y);
            if(index == ListBox.NoMatches)
                return ;            // No item under mouse

            WizardStepItem step = IndexToWizardStep(index);
            if(step != null)
            {
                Description.Text = step.TranslatedDescription;
                CurrentItem.Text = step.TranslatedName;
            }
        }

        private WizardStepItem IndexToWizardStep(int index)
        {
            object node = Steps.Items[index];
            if (node.GetType() == typeof(WizardStepItem))
            {
                return (WizardStepItem)node;
            }
            else return null;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.Settings.AutoProceed = AutoProceed.Checked;
            
            WizardSteps stepsToRun = WizardSteps.NONE;

            // Build the steps
            foreach (WizardStepItem step in Steps.CheckedItems)
            {
                stepsToRun |= step.Step;
            }

            Program.ConstructSteps(stepsToRun);

            Hide();
            Program.NextStep();
            Close();
        }

        private void IgnoreCancelExtraction_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.IgnoreCancelExtraction = IgnoreCancelExtraction.Checked;
        }
    }

    /// <summary>
    /// Provides a Wizard Step item class.
    /// </summary>
    internal class WizardStepItem
    {
        internal WizardSteps Step;
        internal string TranslatedName;
        internal string TranslatedDescription;

        internal WizardStepItem(TranslationProvider Provider, string name, string Parent)
        {
            Step = (WizardSteps)Enum.Parse(typeof(WizardSteps), name);
            string trName = "!" + name;
            TranslatedName = Provider.Translate(trName, Parent);
            TranslatedDescription = Provider.Translate(trName + "_Description", Parent);

            if (TranslatedName == null)
                TranslatedName = "!!Missing: " + trName;
            if (TranslatedDescription == null)
                TranslatedDescription = "!!Missing: " + trName + "_Description";
        }

        public override string ToString()
        {
            return TranslatedName;
        }
    }
}
