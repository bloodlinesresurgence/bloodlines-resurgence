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
    internal partial class SelectOperation : ResurgenceWizardPage
    {
        internal SelectOperation()
        {
            InitializeComponent();
        }

        internal SelectOperation(TranslationProvider Provider)
            : base(Provider)
        {
            
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            InstallOption.Checked = (false == Program.Settings.HasCompletedInstallation);
            UpdateOption.Checked = !InstallOption.Checked;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.Settings.AutoProceed = true;

            Program.Settings.DebugLog = DebugLog.Checked;

            // Always patch files
            WizardSteps steps = WizardSteps.Update_And_Patch_Files;

            if (InstallOption.Checked)
            {
                // For install, add the other required items
                steps = WizardSteps.INSTALL;
            }

            Program.ConstructSteps(steps);

            Hide();
            if (SelectSteps.Checked)
            {
                Program.NextForm = new SelectSteps(TranslationProvider);
            }
            else
            {
                Program.NextStep();
            }
            Close();
        }

        private void DebugLogHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Records various installation details so that you may send an error report to us",
                "Debug Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
