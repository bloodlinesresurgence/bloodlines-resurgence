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
    public partial class SetupMod : WizardPage
    {
        private CheckBox[] checkboxes;

        public SetupMod()
        {
            InitializeComponent();

            this.checkboxes = new CheckBox[] { 
                SelectEngine,
                InstallSDK,
                LaunchSDK,
                CreateMod,
                StepModify,
                FirstTextbox,
                SecondTextbox,
                Wait,
            };

            Program.cancel = this.Close;
        }

        private void InstallSDK_CheckedChanged(object sender, EventArgs e)
        {
            // Check all checkboxes are checked
            bool allChecked = true;

            foreach (CheckBox check in this.checkboxes)
            {
                if (false == check.Checked)
                {
                    allChecked = false;
                    break;
                }
            }

            NextButton.Enabled = allChecked;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Program.nextForm = new InstallType();
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.nextForm = new LocationInformation();
            this.Close();
        }
    }
}
