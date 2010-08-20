using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Resurgence
{
    static class Program
    {
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

            Application.Run(new Form_Installer());
        }
    }
}
