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

namespace ResurgenceTools.Steps
{
    /// <summary>
    /// Provides a wizard page to convert materials and copy them to the
    /// mod folder.
    /// </summary>
    public partial class ConvertAndCopyMaterials 
        : GenericRunProcess
    {
        public ConvertAndCopyMaterials()
        {
            InitializeComponent();
        }

        public ConvertAndCopyMaterials(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            AutoProceedStep = true;
            InitializeComponent();
        }

        protected override Result DoWork()
        {
            // Stage one: Find all directories in the extracted materials folder
            AppendText(TranslationProvider.Translate("!StageOne", this.Name) + "\n");

            string baseDirectory = Program.Settings.TemporaryDirectory + "\\materials";

            SetProgressStyle(ProgressBarStyle.Marquee);
            string[] DirectoryList = FindDirectories(baseDirectory);
            SetProgressStyle(ProgressBarStyle.Blocks);

            if (DirectoryList.Length == 0)
            {
                if (Program.Settings.IgnoreCancelExtraction == true)
                    return Result.Success;
                return Result.Failure;
            }

            AppendText(String.Format(TranslationProvider.Translate("!StageOneDone", this.Name) + "\n", DirectoryList.Length));
            SetProgressMax(DirectoryList.Length);

            // Stage two: Convert all materials and copy them to their destination
            AppendText(TranslationProvider.Translate("!StageTwo", this.Name) + "\n");
            ProcessStartInfo startInfo = new ProcessStartInfo(Settings.ProgramData + "\\Tools\\ttz2vtf.exe", "*.ttz");
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            int directoriesDone = 0, filesCopied = 0;

            foreach (string currentDirectory in DirectoryList)
            {
                if (CancelProcess == true)
                {
                    AppendText(TranslationProvider.Translate("!Cancelled") + "\n");

                    if (Program.Settings.IgnoreCancelExtraction)
                        return Result.Success;
                    else
                        return Result.Failure;
                }

                string shortName = currentDirectory.Replace(baseDirectory + "\\", "");

                AppendText(String.Format(TranslationProvider.Translate("!StageTwoConvert", this.Name) + "\n", shortName));

                startInfo.WorkingDirectory = currentDirectory;
                Process app = Process.Start(startInfo);
                // Pause whilst it executes
                while (app.HasExited == false)
                    /* Do nothing */
                    Thread.Sleep(1);
                    ;

                string destination = currentDirectory.Replace(baseDirectory, Program.Settings.DestinationDirectory + "\\materials");
                filesCopied += CopyFiles(currentDirectory, destination, "*.vtf");
                               CopyFiles(currentDirectory, destination, "*.vmt");

                directoriesDone++;
                ReportProgress(0, directoriesDone);
            }

            AppendText(String.Format(TranslationProvider.Translate("!StageTwoDone", this.Name) + "\n", filesCopied, directoriesDone));

            return Result.Success;
        }
    }
}
