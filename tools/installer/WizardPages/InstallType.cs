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
    public partial class InstallType : WizardPage
    {
        public InstallType()
        {
            InitializeComponent();

            Program.cancel = this.Close;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Program.nextForm = new Intro();
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (StandardInstallationOption.Checked)
            {
                Program.nextForm = new LocationInformation();
            }
            else if (DevelopmentInstallationOption.Checked)
            {
                Program.nextForm = new SetupMod();
            }
            else
            {
                // What now?
                throw new MissingMethodException();
            }

            this.Close();
        }
    }
}
