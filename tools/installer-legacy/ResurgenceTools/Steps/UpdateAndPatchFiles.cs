using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceTools;
using ResurgenceLib;
using System.Diagnostics;

namespace ResurgenceTools.Steps
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
            prepareSmallgit();

            // Stage 1: git stash
            //          Store changes to tracked files
            AppendText("Saving any changes...");
            git("stash");

            // Stage 2: git pull
            //          Download and apply any changes
            AppendText("Downloading and applying changes...");
            git("pull");

            // Stage 3: git stash pop
            //          Retrieve stored changes and reapply them
            AppendText("Restoring changes made prior to update");
            git("stash pop");

            return Result.Success;
        }

        private void git(string p)
        {
            ProcessStartInfo info = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\smallgit\\bin\\git.exe", p);
            info.WorkingDirectory = Program.Settings.DestinationDirectory;
            info.EnvironmentVariables["PATH"] = Program.Settings.Tools_Directory + "\\smallgit\\bin";
            info.EnvironmentVariables["HOME"] = Program.Settings.Tools_Directory + "\\smallgit";

            RunProcess(info);
        }

        private void prepareSmallgit()
        {
            AppendText("Preparing updater...");//TranslationProvider.Translate("!StageOne", this.Name));

            ProcessStartInfo info = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\Unrar.exe",
                String.Format(Program.Settings.Generic_Unrar_Arguments, Program.Settings.smallgit_Rar));
            info.WorkingDirectory = Program.Settings.Tools_Directory;

            RunProcess(info);

            AppendText(TranslationProvider.Translate("!StageOneDone", this.Name) + "\n");
        }
    }
}
