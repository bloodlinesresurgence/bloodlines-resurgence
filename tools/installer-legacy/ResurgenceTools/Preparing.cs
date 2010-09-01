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
        private NodeCS.Callback callback;

        internal Preparing()
        {
            InitializeComponent();
        }

        internal Preparing(TranslationProvider Provider, NodeCS.Callback callback)
            : base(Provider)
        {
            this.callback = callback;
        }

        protected override void DoInitializeComponent()
        {
            InitializeComponent();
        }

        private void Preparing_Shown(object sender, EventArgs e)
        {
            this.callback();
        }

        
    }
}
