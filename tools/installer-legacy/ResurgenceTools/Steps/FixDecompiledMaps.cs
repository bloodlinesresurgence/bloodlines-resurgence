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
using ResurgenceLib.Tools.Mapfix;

namespace Resurgence.Steps
{
    internal partial class FixDecompiledMaps 
        : GenericRunProcess
    {
        public FixDecompiledMaps()
        {
            InitializeComponent();
        }

        public FixDecompiledMaps(TranslationProvider Provider)
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
            AppendText(Translate("!StageOne"));

            string src = Program.Settings.VampireDirectory + "\\vampire\\maps";
            string[] files = Directory.GetFiles(src, "*.vmf");
            SetProgressStyle(ProgressBarStyle.Marquee);

            if (files.Length == 0)
            {
                LibCommunications.gAddLog("No maps found to fix");
                AppendText(String.Format(Constant("!NoMapsFound"), src));
                return Result.Success;
            }

            LibCommunications.gAddLog(String.Format("Found {0} maps to fix", files.Length));

            AppendText(String.Format(Translate("!StageOneDone"), files.Length) + "\n");
            SetProgressMax(files.Length - 1);
            SetProgressStyle(ProgressBarStyle.Blocks);
            SetProgress(0);

            string destDirectory = Program.Settings.DestinationDirectory + "\\map_src\\";
            if (Directory.Exists(destDirectory) == false)
                Directory.CreateDirectory(destDirectory);

            for(int i = 0; i < files.Length; ++i)
            {
                if (CancelProcess)
                {
                    AppendText("\n" + Constant("!Cancelled") + "\n");
                    if (Program.Settings.IgnoreCancelExtraction)
                        return Result.Success;
                    else
                        return Result.Failure;
                }

                string current = files[i];

                FileInfo info = new FileInfo(current);

                AppendText(String.Format(Translate("!StageTwoFix"), info.Name));
                Result result = Mapfix.Fix(current);
                if (result == Result.Failure)
                {
                    AppendText(TranslationProvider.Translate(result) + ": " + Mapfix.LastError + "\n");
                    return result;
                }

                AppendText(String.Format(Translate("!StageTwoCopy")));
                try
                {
                    string destination = destDirectory + info.Name;
                    if (File.Exists(destination) == true)   // Delete old file
                        File.Delete(destination);

                    // Move source file to destination
                    File.Move(current, destination);
                }
                catch (Exception ex)
                {
                    AppendText(ex.Message);
                    return Result.Failure;
                }
                AppendText("\n");

                BackgroundProcessor.ReportProgress(0, i);
            }

            AppendText(Translate("!Done"));

            return Result.Success;
        }
    }
}
