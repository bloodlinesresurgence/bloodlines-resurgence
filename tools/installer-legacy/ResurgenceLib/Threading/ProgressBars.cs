using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResurgenceLib.Threading
{
    /// <summary>
    /// Provides thread-safe alteration of ProgressBars.
    /// </summary>
    public static class ProgressBars
    {
        delegate void SetProgressDelegate(ProgressBar Progress, int value);
        delegate void SetProgressStyleDelegate(ProgressBar Progress, ProgressBarStyle style);

        /// <summary>
        /// Sets the progress bar max value. Thread-safe.
        /// </summary>
        /// <param name="Progress"></param>
        /// <param name="value"></param>
        public static void SetProgressMax(ProgressBar Progress, int value)
        {
            if (Progress.InvokeRequired == false)
                Progress.Maximum = value;
            else
                Progress.Invoke(new SetProgressDelegate(SetProgressMax), value);
        }

        /// <summary>
        /// Sets the progress bar value. Thread-safe.
        /// It may help to use BackgroundProcesser.ReportProgress(0, (raw value)) and use
        /// SetProgressMax to the max raw value.
        /// </summary>
        /// <param name="Progress"></param>
        /// <param name="value"></param>
        public static void SetProgress(ProgressBar Progress, int value)
        {
            if (Progress.InvokeRequired == false)
                Progress.Value = value;
            else
                Progress.Invoke(new SetProgressDelegate(SetProgress), value);
        }

        /// <summary>
        /// Sets the progress bar style. Thread-safe.
        /// </summary>
        /// <param name="Progress"></param>
        /// <param name="style"></param>
        public static void SetProgressStyle(ProgressBar Progress, ProgressBarStyle style)
        {
            if (Progress.InvokeRequired == false)
                Progress.Style = style;
            else
                Progress.Invoke(new SetProgressStyleDelegate(SetProgressStyle), style);
        }
    }
}
