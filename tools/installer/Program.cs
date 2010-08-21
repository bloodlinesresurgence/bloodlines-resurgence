using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Resurgence.WizardPages;

namespace Resurgence
{
    static class Program
    {
        /// <summary>
        /// The next form to run.
        /// </summary>
        internal static Form nextForm = null;

        internal delegate void CancelDelegate();
        internal static CancelDelegate cancel;

        internal static Settings Settings = Settings.GetInstance();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            new SpecBack.SpecTesterForm(new Type[] { typeof(NodeCS.EventEmitter), typeof(NodeCS.StateMachine) }).Show();
#endif

            nextForm = new Intro();

            while (null != nextForm)
            {
                Form form = nextForm;
                nextForm = null;
                Application.Run(form);
            }
        }
    }
}
