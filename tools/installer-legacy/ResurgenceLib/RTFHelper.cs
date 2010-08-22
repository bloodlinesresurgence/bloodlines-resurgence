using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides helper functions RTF-derived controls.
    /// </summary>
    public static class RTFHelper
    {
        const int SB_VERT = 1;
        const int EM_SETSCROLLPOS = 0x0400 + 222;

        [DllImport("user32", CharSet = CharSet.Auto)]
        //private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);
        private static extern bool GetScrollRange(IntPtr hWnd, Int32 nBar, out Int32 lpMinPos, out Int32 lpMaxPos);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern bool SetScrollPos(IntPtr hWnd, Int32 nBar, Int32 nPos, Int32 bRedraw);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, POINT lParam);

        [StructLayout(LayoutKind.Sequential)]
        class POINT
        {
            public int x;
            public int y;

            public POINT()
            {
            }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// Scrolls the given RTF box to the end.
        /// </summary>
        /// <param name="RichTextbox"></param>
        /// <remarks>
        /// The DLL call GetScrollRange returns only a 16 bit int - value maxes out at 65535.
        /// This causes the scrolling to stop.
        /// </remarks>
        public static void ScrollToEnd(System.Windows.Forms.RichTextBox RichTextbox)
        {
#if false
            int min, max;
            GetScrollRange(RichTextbox.Handle, SB_VERT, out min, out max);

            SetScrollPos(RichTextbox.Handle, SB_VERT, max, 1);
            RichTextbox.SelectionStart = RichTextbox.Text.Length;
            //SendMessage(RichTextbox.Handle, EM_SETSCROLLPOS, 0, new POINT(0, max));
#else
            RichTextbox.SelectionStart = RichTextbox.Text.Length;
            RichTextbox.ScrollToCaret();
#endif
        }
    }
}
