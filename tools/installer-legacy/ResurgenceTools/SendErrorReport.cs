using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using Resurgence;
using System.IO;

namespace ResurgenceTools
{
    public partial class SendErrorReport : TranslatedForm
    {
        private ReportContent content = new ReportContent();

        private static bool enumeratedVampire = false;

        public SendErrorReport()
        {
            InitializeComponent();
        }

        public SendErrorReport(TranslationProvider provider)
            : base(provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            InitializeComponent();
        }

        private void SetProgressState(string action, int percent)
        {
            if (Progress.InvokeRequired)
            {
                Progress.Invoke(new MethodInvoker(() => SetProgressState(action, percent)));
            }
            else
            {
                Progress.Visible = CurrentAction.Visible = (null != action);
                Progress.Value = percent % 100;
                CurrentAction.Text = action;
            }
        }

        private void BeginFindFiles_Tick(object sender, EventArgs e)
        {
            BeginFindFiles.Enabled = false;

            if (enumeratedVampire) return;
            enumeratedVampire = true;

            System.Threading.Thread thread = new System.Threading.Thread(delegate()
            {

                SetProgressState("Inspecting Vampire directory", 0);

                try
                {
                    int done = 0, total = 1;
                    Stack<string> Directories = new Stack<string>();

                    Directories.Push(Program.Settings.VampireDirectory);

                    while (Directories.Count > 0)
                    {
                        string currentDir = Directories.Pop();
                        string[] subDirectories = Directory.GetDirectories(currentDir);
                        string[] files = Directory.GetFiles(currentDir);
                        total += subDirectories.Length + files.Length;
                        
                        
                        foreach (string file in files)
                        {
                            done += 1;
                            FileInfo info = new FileInfo(file);
                            LibCommunications.gAddLog(String.Format(@"{0}\{1}   Size: {2}", 
                                currentDir,
                                info.Name,
                                info.Length));
                        }
                        int percent = (int)Math.Round(total / done * 100d, 0);
                        SetProgressState("Inspecting Vampire directory", percent);
                        foreach (string str in subDirectories)
                            Directories.Push(str);
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {
                    LibCommunications.gAddLog("Exception enumerating VampireDirectory: " + ex.ToString());
                }
                finally
                {
                    SetProgressState(null, 0);
                    UpdateContents();
                }
            });
            thread.Start();
        }

        private void UpdateContents()
        {
            content.Update(NameEntry.Text, EmailEntry.Text, AdditionalInformation.Text);
            Contents.BeginInvoke(new MethodInvoker(delegate()
            {
                Contents.Text = content.serializeToString();
                UpdateReports.Enabled = false;
            }));
        }

        private void AdditionalInformation_TextChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void UpdateReport()
        {
            // Reset timeout
            UpdateReports.Enabled = false;
            UpdateReports.Enabled = true;
        }

        private void EmailEntry_TextChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void NameEntry_TextChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void UpdateReports_Tick(object sender, EventArgs e)
        {
            UpdateContents();
        }
    }

    [Serializable]
    internal class ReportContent : DaedalusLib.BaseSerializable
    {
        private string submitterName;
        private string submitterEmail;
        private string settings;
        private string additional;
        private WizardSteps selectedSteps = Program.SelectedSteps;
        private string log;

        public ReportContent()
            : base(FormatterType.Soap)
        {
            this.ChildObjectRef = this;
        }

        public void Update(string name, string email, string additional)
        {
            submitterName = name;
            submitterEmail = email;
            settings = Program.Settings.ConvertToString();
            log = LibCommunications.gLog;
            this.additional = additional;
        }

        internal string serializeToString()
        {
            return this._SerializeToString();
        }
    }

}
