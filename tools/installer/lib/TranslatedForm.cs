using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Resurgence.Translation;

namespace Resurgence
{
    /// <summary>
    /// A standardised translated form.
    /// This forms the base of all translated forms.
    /// </summary>
    public partial class TranslatedForm 
        : Form
    {
        /// <summary>
        /// The assigned translation provider.
        /// </summary>
        protected TranslationProvider TranslationProvider;

        /// <summary>
        /// Initializes a new instance of the TranslatedForm class.
        /// This initializer is primarily for use in the Form Designer.
        /// </summary>
        public TranslatedForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the TranslatedForm class.
        /// This is the primary initializer.
        /// </summary>
        /// <param name="Provider">Translation provider to use.</param>
        public TranslatedForm(TranslationProvider Provider)
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
            //string title = this.Text;

            DoInitializeComponent();

            TranslationProvider.TranslateControl(this, this.Name);
            //this.Text = title;

            PostTranslate();
        }

        /// <summary>
        /// Override with { InitializeComponent(); }
        /// </summary>
        protected virtual void DoInitializeComponent()
        {
            throw new NotImplementedException("Must override DoInitializeComponent!");
        }

        /// <summary>
        /// Occurs when translation is done on a form.
        /// </summary>
        protected virtual void PostTranslate()
        { }
    }
}
