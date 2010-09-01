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
using System.IO.Compression;
using System.Text.RegularExpressions;

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
                    SendReport.BeginInvoke(new MethodInvoker(delegate()
                    {
                        SendReport.Enabled = true;
                    }));
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

        private void SendReport_Click(object sender, EventArgs e)
        {
            NodeCS.Async.run(delegate()
            {
                // Disable report button
                this.BeginInvoke(new MethodInvoker(delegate() { this.Enabled = false; }));

                SetProgressState("Compressing report", 25);
                UTF8Encoding encoding = new UTF8Encoding();
                string report_content = content.serializeToString();
                MemoryStream input = new MemoryStream(encoding.GetBytes(report_content));
                MemoryStream output = new MemoryStream();
                DeflateStream deflate = new DeflateStream(output, CompressionMode.Compress);
                input.WriteTo(deflate);
                byte[] compressed = new byte[output.Length];
                output.Seek(0, SeekOrigin.Begin);
                output.Read(compressed, 0, (int)output.Length);
                report_content = encoding.GetString(compressed);
                deflate.Close();
                input.Close();

                SetProgressState("Preparing to upload", 50);
                System.Collections.Specialized.NameValueCollection parameters = new System.Collections.Specialized.NameValueCollection();
                parameters.Add("name", NameEntry.Text);
                parameters.Add("email", EmailEntry.Text);
                parameters.Add("version", Application.ProductVersion);
                parameters.Add("content", report_content);

                // Create the key needed
                string crcs = "";
                string KEY = "fitz and the fool";
                foreach (string pKey in parameters.AllKeys)
                {
                    crcs += Crc32.Calculate(parameters[pKey]+KEY).ToString();
                }
                string key = Crc32.Calculate(crcs).ToString();
                parameters.Add("key", key);

                // Post to error receiver
                SetProgressState("Uploading report", 70);
                Snowball.Common.PostSubmitter submitter = new Snowball.Common.PostSubmitter(
                    "http://www.bloodlinesresurgence.com/error_reports/receive.php", parameters);
                submitter.Type = Snowball.Common.PostSubmitter.PostTypeEnum.Post;
                string result = submitter.Post();
                MatchCollection parts = Regex.Matches(result, @"^(\d+),(.*)$");
                if (0 == parts.Count || 3 != parts[0].Groups.Count)
                {
                    this.BeginInvoke(new MethodInvoker(delegate()
                    {
                        MessageBox.Show("Error: Unrecognized return message: " + result);
                    }));
                }
                else
                {
                    int code = Int32.Parse(parts[0].Groups[1].Value);
                    string message = parts[0].Groups[2].Value;
                    NodeCS.Callback callback;
                    switch (code)
                    {
                        case 0:     // Success!
                            callback = delegate()
                            {
                                MessageBox.Show(this,
                                    "Success! Your error report number is: " + message + Environment.NewLine +
                                    "Use this number if you wish to contact us regarding the error report.",
                                    "Send Error Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            break;

                        case 1:     // Database error
                        case 2:     // Error inserting data
                            callback = delegate()
                            {
                                MessageBox.Show(this,
                                    "Error submitting error report, a database error occurred", "Send Error Report",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            break;

                        case 128:   // Key doesn't fit
                            callback = delegate()
                            {
                                MessageBox.Show(this,
                                    "This key doesn't fit ... could not send report.", "Send Error Report",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            break;

                        default:
                            callback = delegate()
                            {
                                MessageBox.Show(this,
                                    "Unknown error code: " + result, "Send Error Report",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            break;
                    }
                    this.BeginInvoke(new MethodInvoker(delegate()
                    {
                        callback();
                        this.Enabled = true;
                        SetProgressState(null, 0);
                    }));
                }
            });
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
