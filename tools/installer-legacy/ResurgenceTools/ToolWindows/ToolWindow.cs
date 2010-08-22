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
    internal partial class ToolWindow 
        : DockContent
    {

        /// <summary>
        /// Holds the current window type.
        /// </summary>
        private WindowType windowType;

        public ToolWindow()
        {
            InitializeComponent();

            windowType = WindowType.Tool;
        }

        private void ToolWindow_DockStateChanged(object sender, EventArgs e)
        {
            if (DockState == DockState.Document)
            {
                this.TabPageContextMenuStrip = DocumentContextMenu;
                windowType = WindowType.Document;
            }
            else
            {
                this.TabPageContextMenuStrip = ToolWindowContextMenu;
                windowType = WindowType.Tool;
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void ToolWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Take action based on the window type
                switch(WindowType)
                {
                    // For Tool windows, we simply hide the window.
                    case WindowType.Tool:
                        e.Cancel = true;
                        Hide();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the current window type.
        /// </summary>
        internal WindowType WindowType
        {
            get { return windowType; }
        }
    }
    
    /// <summary>
    /// The type of the tool window.
    /// </summary>
    internal enum WindowType
    {
        Document,
        Tool
    }
}
