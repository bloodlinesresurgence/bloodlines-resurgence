using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RevivalLib;

namespace RevivalTools
{
    internal partial class MainForm 
        : TranslatedForm
    {
        private int childFormNumber = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        internal MainForm(RevivalLib.TranslationProvider provider)
            : base(provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            InitializeComponent();
        }

        protected override void PostTranslate()
        {
            this.Text = TranslationProvider.Translate("!AppTitle");
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void InstalltionWizardButton_Click(object sender, EventArgs e)
        {
            (new RevivalTools.WizardPages.SelectPaths(Program.Settings.TranslationProvider))
                .Show(Program.MainForm);
        }
    }
}
