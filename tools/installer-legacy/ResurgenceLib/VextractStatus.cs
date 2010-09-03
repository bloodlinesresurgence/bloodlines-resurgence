using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib.Tools.Vextract;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides a visual interface to the status of a Vextract operation progress.
    /// </summary>
    public partial class VextractStatus
        : TranslatedControl
    {
        /// <summary>
        /// The Vextract class we will use.
        /// </summary>
        protected Vextract Vextract;

        /// <summary>
        /// The directory to store the extracted files.
        /// </summary>
        protected string outputDirectory = null;

        /// <summary>
        /// The directory containing the .vpk files.
        /// </summary>
        protected string vpkDirectory = null;

        /// <summary>
        /// Exemption patterns.
        /// </summary>
        protected string[] exemptionPatterns = null;

        /// <summary>
        /// The file pattern to match files to.
        /// </summary>
        protected string filePattern = null;

        /// <summary>
        /// Specifies whether only the skeleton is extracted.
        /// </summary>
        protected bool skeletonOnly = false;

        /// <summary>
        /// Indicates whether the Cancel button has been pressed.
        /// </summary>
        protected bool CancelPressed = false;

        /// <summary>
        /// Used to notify extraction is finished.
        /// </summary>
        /// <param name="filesExtracted"></param>
        public delegate void ExtractionFinishedDelegate(int filesExtracted);

        /// <summary>
        /// Used to notify generic events.
        /// </summary>
        public delegate void VextractEventDelegate();

        /// <summary>
        /// Occurs when extraction has completed.
        /// </summary>
        [Description("Occurs when extraction has comepleted.")]
        public ExtractionFinishedDelegate OnFinishedExtraction;

        /// <summary>
        /// Occurs when extraction begins.
        /// </summary>
        [Description("Occurs when extraction begins.")]
        public VextractEventDelegate OnStartExtraction;

        private string formatStr;

        private string filesBuffer = "";

        private int percentageValue = 0;

        /// <summary>
        /// Creates a new instance of the VextractStatus control.
        /// </summary>
        public VextractStatus()
        {
            InitializeComponent();

#if DEBUG
            Log.Visible = true;
            StartButton.Visible = true;
            CancelButton.Visible = true;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void DoBindProvider()
        {
            Vextract = new Vextract();
            Vextract.OnFileBeginExtraction += OnFileBeginExtraction;

            formatStr = "{0} (" + TranslationProvider.Translate("!VextractFilestoGo") + ")...{2}";
        }

        private bool OnFileBeginExtraction(VextractStatusData Status)
        {
            if (CancelPressed)
                return true;

            lock(lockFilesBufferObject)
            {
                //Log.AddLogText(Status.CurrentFile + "...");
                //SetProgressValue(Status.PercentComplete);
                filesBuffer += String.Format(formatStr, 
                    Status.CurrentFile, Status.FilesTotal - Status.FilesDone, Environment.NewLine);
                percentageValue = Status.PercentComplete;
            }

            return false;
        }

        private void SetProgressValue(int value)
        {
            if (Progress.InvokeRequired == false)
            {
                Progress.Value = value;
            }
            else
            {
                SetProgressValueDelegate d = new SetProgressValueDelegate(SetProgressValue);
                Progress.Invoke(d, new object[] { value });
            }
        }
        private delegate void SetProgressValueDelegate(int value);

        /// <summary>
        /// Gets or sets the directory to store the extracted files.
        /// </summary>
        [Description("The directory to store the extracted files.")]
        public string OutputDirectory
        {
            get { return outputDirectory; }
            set { outputDirectory = value; }
        }

        /// <summary>
        /// Gets or sets the directory that contains the .vpk files.
        /// </summary>
        [Description("The directory that contains the .vpk files.")]
        public string VPKDirectory
        {
            get { return vpkDirectory; }
            set { vpkDirectory = value; }
        }

        /// <summary>
        /// Gets or sets the file pattern to match files to.
        /// </summary>
        [Description("The file pattern to match files to.")]
        public string Pattern
        {
            get { return filePattern; }
            set { filePattern = value; }
        }

        /// <summary>
        /// Gets or sets the value that specifies whether only the skeleton is extracted.
        /// </summary>
        [Description("Specifies whether only the skeleton is extracted.")]
        public bool SkeletonOnly
        {
            get { return skeletonOnly; }
            set { skeletonOnly = value; }
        }

        /// <summary>
        /// Gets or sets the current exemption patterns array.
        /// </summary>
        [Description("The current exemption patterns array.")]
        public string[] ExemptionPatterns
        {
            get { return exemptionPatterns; }
            set { exemptionPatterns = value; }
        }

        /// <summary>
        /// Starts extraction.
        /// </summary>
        /// <returns>Returns true on success, false on failure.</returns>
        public bool StartNow()
        {
            if (Vextract == null)
                return false;

            if (OnStartExtraction != null)
                OnStartExtraction();

            if (VPKIndex.Exists == false)
            {
                if (vpkDirectory == null)
                {
                    return false;
                }

                AppendText(TranslationProvider.Translate("!Vextract_VPKIndex"));
                VPKIndex index = new VPKIndex(vpkDirectory);
                AppendText(TranslationProvider.Translate("!Vextract_VPKIndex_Done") + Environment.NewLine);
            }
            if (outputDirectory != null && filePattern != null)
            {
                StartButton.Visible = false;
                CancelButton.Visible = true;
                UpdateUITimer.Start();
                VextractWorker.RunWorkerAsync();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Readies the control for extraction.
        /// </summary>
        public void Start()
        {
            StartButton.Enabled = true;
            AppendText(TranslationProvider.Translate("!Vextract_Ready"));
        }

        /// <summary>
        /// Gets the value indicating whether the cancel button was pressed.
        /// </summary>
        public bool Cancelled
        {
            get { return CancelPressed; }
        }

        /// <summary>
        /// Cancels the current operation.
        /// </summary>
        public void Cancel()
        {
            CancelPressed = true;
        }

        private void VextractWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CancelEnabled = true;
            CancelPressed = false;
            int filesExtracted = Vextract.ExtractFiles(filePattern, outputDirectory, exemptionPatterns, skeletonOnly);
            CancelEnabled = false;
            UpdateUITimer.Enabled = false;
            SetProgressValue(100);

            filesBuffer += String.Format(TranslationProvider.Translate("!Vextract_Done"), filesExtracted);
            flushBuffer();

            if (OnFinishedExtraction != null)
                OnFinishedExtraction(filesExtracted);
        }

        private bool CancelEnabled
        {
            get { return CancelButton.Enabled; }
            set
            {
                if (CancelButton.InvokeRequired == false)
                    CancelButton.Enabled = value;
                else
                    CancelButton.Invoke(new SetCancelDelegate(SetCancel), new object[] { value });
            }
        }
        private delegate void SetCancelDelegate(bool value);
        private void SetCancel(bool value) { CancelEnabled = value; }

        private void Cancel_Click(object sender, EventArgs e)
        {
            CancelPressed = true;
            CancelButton.Enabled = false;
        }

        private object lockFilesBufferObject = new object();
        private void UpdateUITimer_Tick(object sender, EventArgs e)
        {
            flushBuffer();
        }

        private void flushBuffer()
        {
            lock (lockFilesBufferObject)
            {
                if (filesBuffer.Length > 0)
                {
                    AppendText(filesBuffer);
                    filesBuffer = "";
                    SetProgressValue(percentageValue);
                }
            }
        }

        private void AppendText(string text)
        {
#if DEBUG
            if (Log.InvokeRequired == false)
            {
                Log.AppendText(text);
                /*
                Log.SelectionStart = Log.Text.Length;
                Log.SelectionStart = Log.Text.Length;
                Log.SelectionLength = 0;
                Log.ScrollToCaret();*/
                RTFHelper.ScrollToEnd(Log);
                Lib.CommunicationsObject.AddLog(text);
            }
            else
                Log.Invoke(new AppendTextDelegate(AppendText), new object[] { text });
#else
            Lib.CommunicationsObject.AddLog(text);
#endif
        }
        private delegate void AppendTextDelegate(string text);

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartNow();
        }
    }
}
