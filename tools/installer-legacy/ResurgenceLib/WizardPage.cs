using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using ResurgenceLib.Translation;

namespace ResurgenceLib
{
    /// <summary>
    /// A standardised wizard page.
    /// </summary>
    public partial class WizardPage
        : Form
    {
        /// <summary>
        /// Translation provider.
        /// </summary>
        protected TranslationProvider TranslationProvider;

        /// <summary>
        /// Initializes a new instance of the WizardPage class.
        /// This initializer is primarily for use in the Form Designer.
        /// </summary>
        public WizardPage()
            : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the WizardPage class.
        /// This is the primary initializer.
        /// </summary>
        /// <param name="Provider">Translation provider to use.</param>
        public WizardPage(TranslationProvider Provider)
        {
            Provider.OnLanguagedChanged += delegate()
            {
                this.Controls.Clear();
                RunTranslation();
            };

            TranslationProvider = Provider;

            RunTranslation();
        }

        private void RunTranslation()
        {
            InitializeComponent();

            TranslationProvider.TranslateControl(this);
            string title = this.Text;

            DoInitializeComponent();

            TranslationProvider.TranslateControl(this, this.Name);
            this.Text = title;

            PostTranslate();
        }

        /// <summary>
        /// Occurs after translation.
        /// </summary>
        protected virtual void PostTranslate()
        {
        }

        /// <summary>
        /// Override with { InitializeComponent(); }
        /// </summary>
        protected virtual void DoInitializeComponent()
        {
            throw new NotImplementedException("Must override DoInitializeComponent!");
        }
    }
}
