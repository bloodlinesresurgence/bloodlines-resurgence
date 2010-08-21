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
    public partial class Intro : WizardPage
    {
        public Intro()
        {
            InitializeComponent();

            Program.cancel = this.Close;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.nextForm = new InstallType();
            this.Close();
        }
    }
}
