using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Resurgence
{
    /// <summary>
    /// Provides a base user control that can be translated.
    /// </summary>
    public partial class TranslatedControl : UserControl
    {
        /// <summary>
        /// Translation provider.
        /// </summary>
        protected TranslationProvider TranslationProvider;

        /// <summary>
        /// Initializes a new instance of the TranslatedControl class.
        /// This initializer is primarily for use in the Form Designer.
        /// </summary>
        public TranslatedControl()
            : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binds the given translation provider to the control, and runs initialization.
        /// </summary>
        /// <param name="Provider"></param>
        public void BindProvider(TranslationProvider Provider)
        {
            TranslationProvider = Provider;

            TranslationProvider.TranslateControl(this);

            DoBindProvider();
        }

        /// <summary>
        /// Occurs when the translation provider is bound to the control.
        /// </summary>
        protected virtual void DoBindProvider()
        {
            throw new NotImplementedException("Must override DoBindProvider!");
        }
    }
}
