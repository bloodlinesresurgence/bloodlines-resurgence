using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;

namespace Resurgence
{
    /// <summary>
    /// Provides a Preparing progress dialogue.
    /// The control 'Progress' is public and available for modification.
    /// </summary>
    internal partial class Preparing 
        : TranslatedForm
    {
        internal Preparing()
        {
            InitializeComponent();
        }

        internal Preparing(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            InitializeComponent();
        }

        
    }
}
