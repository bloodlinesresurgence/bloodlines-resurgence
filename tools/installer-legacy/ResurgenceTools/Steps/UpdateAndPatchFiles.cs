using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Resurgence;
using ResurgenceLib;
using System.Diagnostics;
using ResurgenceLib;

namespace Resurgence.Steps
{
    internal partial class UpdateAndPatchFiles : GenericRunProcess
    {
        public UpdateAndPatchFiles()
        {
            InitializeComponent();
        }

        public UpdateAndPatchFiles(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();
        }

        protected override Result DoWork()
        {
            Status(TranslationProvider.Translate("!Starting", this.Name));
            prepareSmallgit();

            this.CaptureOutput = true;

            BinOutput output = new BinOutput();

            // Stage 1: git stash
            //          Store changes to tracked files
            Status(TranslationProvider.Translate("!Stash", this.Name));
            output = Git("stash");

            // Stage 2: git pull
            //          Download and apply any changes
            Status(TranslationProvider.Translate("!Pull", this.Name));
            output = Git("pull");

            if (output.StdOutput != "Already up-to-date.\n")
            {
                Program.SelectedSteps |= WizardSteps.View_Changelog;
                Program.ConstructSteps(Program.SelectedSteps);
            }

            // Stage 3: git stash pop
            //          Retrieve stored changes and reapply them
            Status(TranslationProvider.Translate("!Pop", this.Name));
            Git("stash pop");

            return Result.Success;
        }

        protected BinOutput Git(string p)
        {
            ProcessStartInfo info = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\smallgit\\bin\\git.exe", p);
            info.WorkingDirectory = Program.Settings.DestinationDirectory;
            info.EnvironmentVariables["PATH"] = Program.Settings.Tools_Directory + "\\smallgit\\bin";
            info.EnvironmentVariables["HOME"] = Program.Settings.Tools_Directory + "\\smallgit";
            this.CaptureOutput = true;

            return RunProcess(info);
        }

        protected void prepareSmallgit()
        {
            AppendText("Preparing updater...");//TranslationProvider.Translate("!StageOne", this.Name));

            ProcessStartInfo info = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\Unrar.exe",
                String.Format(Program.Settings.Generic_Unrar_Arguments, Program.Settings.smallgit_Rar));
            info.WorkingDirectory = Program.Settings.Tools_Directory;

            RunProcess(info);

            //AppendText(TranslationProvider.Translate("!StageOneDone", this.Name) + "\n");
        }
    }
}
