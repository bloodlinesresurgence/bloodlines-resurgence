using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides a tool window with automatic translation handling.
    /// </summary>
    public partial class ToolWindow : Form
    {
        /// <summary>
        /// The translation provider being used.
        /// </summary>
        protected TranslationProvider TranslationProvider;

        /// <summary>
        /// This constructor is provided for the Forms designer.
        /// </summary>
        public ToolWindow()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ToolWindow object.
        /// </summary>
        public ToolWindow(TranslationProvider Translation)
            : base()
        {
            RegisterTranslationProvider(Translation);
        }

        /// <summary>
        /// Registers the given translation provider.
        /// </summary>
        /// <param name="Translation"></param>
        protected void RegisterTranslationProvider(TranslationProvider Translation)
        {
            TranslationProvider = Translation;


            DoInitializeComponents();

            TranslationProvider.TranslateControl(this);

            TranslationProvider.OnLanguagedChanged +=
                new TranslationProvider.TranslationEventDelegate(delegate() { TranslationProvider.TranslateControl(this); });
        }

        /// <summary>
        /// Override with a call to InitializeComponents.
        /// </summary>
        protected virtual void DoInitializeComponents()
        {
        }

        /// <summary>
        /// Translates the given constant string.
        /// </summary>
        /// <param name="Constant">Constant string to translate.</param>
        /// <returns></returns>
        protected string Translate(string Constant)
        {
            return TranslationProvider.Translate(Constant);
        }
    }
}
