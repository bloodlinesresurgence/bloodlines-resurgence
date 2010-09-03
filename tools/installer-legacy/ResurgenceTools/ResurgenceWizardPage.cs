using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Resurgence
{
    /// <summary>
    /// Provides an extended base for Wizard Pages that more directly
    /// ties into the Resurgence Wizard.
    /// </summary>
    public partial class ResurgenceWizardPage 
        : ResurgenceLib.WizardPage
    {
        /// <summary>
        /// Controls displaying of the confirm quit / cancel dialogue.
        /// </summary>
        protected bool ConfirmUserQuit = true;
        
        /// <summary>
        /// Controls allowing the user to quit the wizard. Use with care.
        /// </summary>
        protected bool AllowUserQuit = true;

        /// <summary>
        /// When true, uses AutoProceeding once success is reached if enabled.
        /// </summary>
        private bool mAutoProceedStep = false;

        /// <summary>
        /// Prevents multiple auto proceeds from occuring.
        /// </summary>
        private bool autoProceedOccurred = false;

        internal ResurgenceWizardPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of the ResurgenceWizardPage dialog.
        /// Used by base classes and does not do anything.
        /// </summary>
        /// <param name="Provider"></param>
        public ResurgenceWizardPage(ResurgenceLib.TranslationProvider Provider)
            : base(Provider)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void DoInitializeComponent()
        {
            InitializeComponent();

            LoadPosition();

            if (Program.Settings.DebugLog)
                SendErrorReport.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelWizardButton_Click(object sender, EventArgs e)
        {
            ConfirmQuit();
        }

        /// <summary>
        /// Confirms if the user wants to quit.
        /// </summary>
        /// <returns>True if user wishes to quit, otherwise false.</returns>
        private bool ConfirmQuit()
        {
#if !FULL_TOOLSET
            if (Program.NextForm == null)       // Already quitting
                return true;

            if (AllowUserQuit == false)
                return false;
            
            if (ConfirmUserQuit == false)
                return true;

            string message = TranslationProvider.Translate("!ConfirmQuit");
            string title = TranslationProvider.Translate("!ConfirmTitle");
            if (MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Program.NextForm = null;
                Close();
                return true;
            }
#endif
            return false;
        }

        private void ResurgenceWizardPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.WriteOnAlterFunc(true, delegate(Settings target)
                {
                    target.Save();
                });
        }

        private void ResurgenceWizardPage_FormClosing(object sender, FormClosingEventArgs e)
        {
#if !FULL_TOOLSET
            if (Program.NextForm != this)
                return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (ConfirmQuit() == false)
                {
                    e.Cancel = true;
                    return;
                }
            }
            Program.NextForm = null;
#endif
        }

        private void LanguageButton_Click(object sender, EventArgs e)
        {
            Form form = new SelectLanguage();
            form.ShowDialog(this);
        }

        /// <summary>
        /// Notifies the wizard page that success has occured and that it
        /// can proceed.
        /// </summary>
        protected void Step_Success()
        {
            if (this.InvokeRequired == false)
            {
                NextButton.Enabled = true;
                CancelWizardButton.Enabled = true;
                BackButton.Enabled = true;
                AllowUserQuit = true;
                LanguageButton.Enabled = true;

                if (AutoProceedStep && Program.Settings.AutoProceed)
                    NextButton.PerformClick();
            }
            else
                this.Invoke(new StepDelegate(Step_Success));
        }

        /// <summary>
        /// Notifies the wizard page that the step failed.
        /// </summary>
        protected void Step_Failure()
        {
            if (this.InvokeRequired == false)
            {
                CancelWizardButton.Enabled = true;
                NextButton.Enabled = false;
                BackButton.Enabled = true;
                LanguageButton.Enabled = true;
                AllowUserQuit = true;
            }
            else
                this.Invoke(new StepDelegate(Step_Failure));
        }

        /// <summary>
        /// Notifies the wizard page that it shall execute in BLOCKING mode,
        /// which disables the various buttons to control flow.
        /// </summary>
        protected void Step_Blocking()
        {
            if (this.InvokeRequired == false)
            {
                NextButton.Enabled = false;
                CancelWizardButton.Enabled = false;
                BackButton.Enabled = false;
                LanguageButton.Enabled = false;
                AllowUserQuit = false;
            }
            else
                this.Invoke(new StepDelegate(Step_Blocking));
        }

        private delegate void StepDelegate ();

        /// <summary>
        /// Gets the translation for the given constant for the current wizard step.
        /// The same as TranslationProvider.Translate(Constant, this.Name);
        /// </summary>
        /// <param name="Constant">Constant to retrieve. Ensure ! is prepended, as required.</param>
        /// <returns></returns>
        protected string Translate(string Constant)
        {
            return TranslationProvider.Translate(Constant, this.Name);
        }

        /// <summary>
        /// Gets the constant for the given constant name.
        /// The same as TranslationProvider.Translate(Constant);
        /// </summary>
        /// <param name="Constant">Constant to retrieve. Ensure ! is prepended, as required.</param>
        /// <returns></returns>
        protected string Constant(string Constant)
        {
            return TranslationProvider.Translate(Constant);
        }

        /// <summary>
        /// Gets or sets the value indicating if the current step can
        /// be automatically proceeded.
        /// </summary>
        protected bool AutoProceedStep
        {
            get { return mAutoProceedStep; }
            set
            {
                mAutoProceedStep = value;
                if (value == true && Program.Settings.AutoProceed)
                {
                    // Create thread to run autoproceed.
                    Thread autoProceedThread = new Thread(autoProceedThreadFunction);
                    autoProceedThread.Start(this);
                }
            }
        }

        private void autoProceedThreadFunction(object ownerForm)
        {
            Form form = (Form)ownerForm;
            if (form.InvokeRequired == false)
            {
                Thread.Sleep(200);
                Application.DoEvents();
                // Value may have changed, check again.
                if (AutoProceedStep && Program.Settings.AutoProceed && autoProceedOccurred == false)
                {
                    try
                    {
                        form.Invoke(new AutoProceed_Start_Delegate(AutoProceed_Start));
                        autoProceedOccurred = true;
                    }
                    catch (Exception)
                    {
                        autoProceedThreadFunction(ownerForm);
                    }
                }
            }
            else
                form.Invoke(new autoProceedThreadDelegate(autoProceedThreadFunction), ownerForm);
        }
        private delegate void autoProceedThreadDelegate(object unused);

        /// <summary>
        /// Override this method to contain the code to start your processing.
        /// </summary>
        protected virtual void AutoProceed_Start()
        {
            System.Diagnostics.Debug.Assert(false, "AutoProceed_Start() not overidden.");
        }
        private delegate void AutoProceed_Start_Delegate();

        #region Hackity code to save window position ... doesn't really work.
#if false
        private void ResurgenceWizardPage_LocationChanged(object sender, EventArgs e)
        {
            // For the form designer - it chokes on this.
            if (Program.Settings != null)
            {
                Program.Settings.WriteOnAlterFunc(false, delegate(Settings target)
                {
                    target.WindowPosition = this.Location;
                });
            }
        }

        private void LoadPosition()
        {
            Point loc = Program.Settings.WindowPosition;
            if (loc != Point.Empty)
            {
                Thread moveThread = new Thread(moveThreadFunction);
                moveThread.Start(loc);
            }
        }

        private void moveThreadFunction(object par)
        {
            Thread.Sleep(50);
            Application.DoEvents();
            moveThreadDelegatedFunction((Point)par);
        }
        private void moveThreadDelegatedFunction(Point to)
        {
            if (this.InvokeRequired == false)
                this.Location = to;
            else
                this.Invoke(new moveThreadDelegate(moveThreadDelegatedFunction), to);
        }
        private delegate void moveThreadDelegate(Point to);
#else
        // Dummy replacements
        private void ResurgenceWizardPage_LocationChanged(object sender, EventArgs e) { }
        private void LoadPosition() { }
#endif
        #endregion

        private void CheckForUpdates_Tick(object sender, EventArgs e)
        {
            CheckForUpdates.Enabled = false;

            Program.checkForUpdate(delegate(string updateURL)
            {
                UpdateCheck.BeginInvoke(new MethodInvoker(delegate()
                {
                    UpdateCheck.Text = "An update is available.";
                    UpdateCheck.Tag = updateURL;
                    UpdateCheck.LinkArea = new LinkArea(0, UpdateCheck.Text.Length);
                    UpdateCheck.Visible = true;
                }));
            });
        }

        private void UpdateCheck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(UpdateCheck.Tag as string);
        }

        private void SendErrorReport_Click(object sender, EventArgs e)
        {
            Resurgence.SendErrorReport form = new Resurgence.SendErrorReport(TranslationProvider);
            form.ShowDialog(this);
        }

        private void ResurgenceWizardPage_Shown(object sender, EventArgs e)
        {
            CheckForUpdates.Enabled = true;
        }
    }
}
