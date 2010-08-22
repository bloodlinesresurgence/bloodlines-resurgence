using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ResurgenceTools
{
    /// <summary>
    /// Provides a wizard page to convert materials and copy them to the
    /// mod folder.
    /// </summary>
    public partial class GenericRunProcess 
        : ResurgenceWizardPage
    {
        /// <summary>
        /// 
        /// </summary>
        protected volatile bool CancelProcess = false;
        private volatile bool Running = false;
        
        /// <summary>
        /// 
        /// </summary>
        public GenericRunProcess()
        {
            InitializeComponent();
        }

        public GenericRunProcess(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            AutoProceedStep = true;
        }

        protected override void AutoProceed_Start()
        {
            StartStop.PerformClick();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.PreviousStep();
            Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.NextStep();
            Close();
        }

        protected virtual Result DoWork()
        {
            AppendText("DoWork() function not overridden!");
            return Result.Failure;
        }

        private void BackgroundProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            Step_Blocking();
            SetProgress(0);
            SetProgressStyle(ProgressBarStyle.Blocks);
            Result result = DoWork();

            switch (result)
            {
                case Result.Failure:
                    Step_Failure();
                    break;

                case Result.Success:
                    Step_Success();
                    break;
            }
        }

        /// <summary>
        /// Executes the specified ProcessStartInfo class, capturing output.
        /// </summary>
        /// <param name="info">ProcessStartInfo to start.</param>
        /// <returns></returns>
        protected void RunProcess(ProcessStartInfo info)
        {
            // Set standard options
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            //info.RedirectStandardOutput = true;

            AppendText(info.FileName + " " + info.Arguments);

            Process p = Process.Start(info);

            while (p.HasExited == false)
            {
                Thread.Sleep(1);
                checkOutput(p, false);

                // Allow cancelling of process
                if (CancelProcess == true)
                {
                    AppendText(TranslationProvider.Translate("!Terminating") + "\n");

                    try
                    {
                        p.Kill();
                    }
                    catch (Exception) 
                    { 
                        /* Ignore, process is already killed */
                    }
                }
            }

            checkOutput(p, true);
        }

        /// <summary>
        /// Checks the buffered output stream for the given process and adds any
        /// waiting text to the log.
        /// </summary>
        /// <param name="p">Process to check.</param>
        /// <param name="processAll">Specifies whether all waiting data is read, or a small chunk.</param>
        protected void checkOutput(Process p, bool processAll)
        {
            // Only works if the RedirectStandardOutput flag is on.
            if (p.StartInfo.RedirectStandardOutput == false)
                return;

            if (p.StandardOutput.EndOfStream == false)
            {
                string buffer = "";
                // Loops from 0 to 1023, or if processAll is true, indefinately (will break if
                // Read() informs there are no more characters.)
                for (int i = 0; processAll || i < 1024; ++i)
                {
                    int b = p.StandardOutput.Read();
                    if (b == -1)
                        break;
                    buffer += (char)b;
                }
                if (buffer.Length > 0) ;
                    //AppendText(buffer);
            }
        }

        protected int CopyFiles(string source, string destination, string filter)
        {
            string[] files = Directory.GetFiles(source, filter);
            int filesCopied = 0;

            if (Directory.Exists(destination) == false)
            {
                try
                {
                    Directory.CreateDirectory(destination);
                }
                catch (Exception ex)
                {
                    AppendText(String.Format(TranslationProvider.Translate("!ErrorCreatingDir") + "\n", destination, ex.Message));
                    return 0;
                }
            }

            foreach (string currentFile in files)
            {
                FileInfo info = new FileInfo(currentFile);
                try
                {
                    string dst = destination + "\\" + info.Name;

                    File.Copy(currentFile, dst, true);
                    filesCopied++;
                }
                catch (Exception ex)
                {
                    AppendText(String.Format(TranslationProvider.Translate("!ErrorCopyingFile") + "\n", info.Name, ex.Message));
                }
            }

            return filesCopied;
        }

        delegate void SetProgressDelegate(int value);
        delegate void SetProgressStyleDelegate(ProgressBarStyle style);
        /// <summary>
        /// Sets the progress bar max value. Thread-safe.
        /// </summary>
        /// <param name="value"></param>
        protected void SetProgressMax(int value)
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
        /// <param name="value"></param>
        protected void SetProgress(int value)
        {
            if (Progress.InvokeRequired == false)
                Progress.Value = value;
            else 
                Progress.Invoke(new SetProgressDelegate(SetProgress), value);
        }
        /// <summary>
        /// Sets the progress bar style. Thread-safe.
        /// </summary>
        /// <param name="style"></param>
        protected void SetProgressStyle(ProgressBarStyle style)
        {
            if (Progress.InvokeRequired == false)
                Progress.Style = style;
            else
                Progress.Invoke(new SetProgressStyleDelegate(SetProgressStyle), style);
        }

        /// <summary>
        /// Appends the given text to the Log window. Thread-safe.
        /// </summary>
        /// <param name="text"></param>
        protected void AppendText(string text)
        {
            if (Log.InvokeRequired == false)
            {
                Log.AppendText(text);
                RTFHelper.ScrollToEnd(Log);
            }
            else
                Log.Invoke(new AppendTextDelegate(AppendText), new object[] { text });
        }
        delegate void AppendTextDelegate(string text);

        /// <summary>
        /// Gets a list of directories contained in the given directory, recursively.
        /// </summary>
        /// <param name="baseDirectory">Directory to start in.</param>
        /// <returns>An array of strings containing the full path to each directory.</returns>
        protected string[] FindDirectories(string baseDirectory)
        {
            List<string> directoriesFound = new List<string>();

            try
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(baseDirectory);
                foreach (DirectoryInfo subDirectory in currentDirectory.GetDirectories())
                {
                    if (CancelProcess == true)
                        return new string[0];

                    directoriesFound.Add(subDirectory.FullName);

                    string[] subDirectories = FindDirectories(subDirectory.FullName);
                    directoriesFound.AddRange(subDirectories);
                }
            }
            catch (Exception ex)
            {
                AppendText("FindDirectories: " + ex.Message + "\n");
                // Will return found directories thus far.
            }

            return directoriesFound.ToArray();
        }

        private void StartStop_Click(object sender, EventArgs e)
        {
            if (Running == false)
            {
                Running = true;
                CancelProcess = false;

                BackButton.Enabled = false;
                AllowUserQuit = false;
                CancelWizardButton.Enabled = false;

                StartStop.Text = TranslationProvider.Translate("!Cancel");

                BackgroundProcessor.RunWorkerAsync();
            }
            else
            {
                AppendText(TranslationProvider.Translate("!Cancelling") + "\n");
                CancelProcess = true;
            }
        }

        private void BackgroundProcessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StartStop.Text = TranslationProvider.Translate("!Start");
            Running = false;

            /*
             NextButton.Enabled = true;
            BackButton.Enabled = true;
            AllowUserQuit = true;
            CancelWizardButton.Enabled = true;
             */
        }

        private void BackgroundProcessor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != 0)
                Progress.Invoke(new SetProgressDelegate(SetProgress), e.ProgressPercentage);
            else if(e.ProgressPercentage == 0 && e.UserState != null)
                Progress.Invoke(new SetProgressDelegate(SetProgress), (int)e.UserState);
        }

        protected void ReportProgress(int percent, int raw)
        {
            BackgroundProcessor_ProgressChanged(null, new ProgressChangedEventArgs(percent, raw));
        }
    }
}
