using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RevivalTools.ToolWindows
{
    /// <summary>
    /// Provides a window for logging text to.
    /// </summary>
    internal partial class LogWindow : ToolWindow
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds the specified text to the window.
        /// </summary>
        /// <param name="text"></param>
        internal void AddText(string text)
        {
            if (LogText.InvokeRequired == false)
            {
                if (LogText.Text.Length > 10000)
                    LogText.Text = "";
                LogText.AddFormattedText(text, LogText.Text.Length);
                LogText.SelectionStart = LogText.Text.Length - 1;
                LogText.ScrollToCaret();
            }
            else
            {
                AddTextDelegate d = new AddTextDelegate(AddText);
                LogText.Invoke(d, new object[] { text });
            }
        }
        private delegate void AddTextDelegate(string text);

        /// <summary>
        /// Clears the log text.
        /// </summary>
        internal void ClearLog()
        {
            if (LogText.InvokeRequired == false)
            {
                LogText.Text = "";
            }
            else
            {
                ClearLogDelegate d = new ClearLogDelegate(ClearLog);
                LogText.Invoke(d);
            }
        }
        private delegate void ClearLogDelegate();
    }
}
