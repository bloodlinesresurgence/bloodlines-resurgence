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
using System.Text.RegularExpressions;

namespace Resurgence.Steps
{
    public partial class CopyOriginalFiles
         : GenericRunProcess
    {
        public CopyOriginalFiles()
        {
            InitializeComponent();
        }

        public CopyOriginalFiles(TranslationProvider Provider)
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
            // Stage one: Find all files to be copied
            AppendText(TranslationProvider.Translate("!StageOne", this.Name) + "\n");

            Regex exclusions = new Regex(
                "^cl_dlls|"+
                "^dlls|"+
                "^maps|"+
                "vpk$|"+
                "bsp$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            DirectoryInfo dir = new DirectoryInfo(Program.Settings.VampireDirectory+@"\Vampire");
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);

            // Filter the files for ones we don't want
            System.Collections.ArrayList toCopy = new System.Collections.ArrayList();
            foreach (FileInfo file in files)
            {
                if (false == exclusions.IsMatch(file.Name)) toCopy.Add(file);
            }

            SetProgressStyle(ProgressBarStyle.Blocks);
            AppendText(TranslationProvider.Translate("!StageTwo", this.Name) + " " + toCopy.Count);
            SetProgressMax(files.Length);

            if (0 == toCopy.Count)
            {
                LibCommunications.gAddLog("No original files found to copy");
                return Result.Success;
            }

            LibCommunications.gAddLog(String.Format("Copying {0} original files",
                toCopy.Count));

            int count = toCopy.Count;
            string copyStr = TranslationProvider.Translate("!StageTwoCopy", this.Name) + " ";
            for (int i = 0; i < count; i++)
            {
                if (true == CancelProcess)
                {
                    AppendText(TranslationProvider.Translate("!Cancelled") + "\n");

                    if (Program.Settings.IgnoreCancelExtraction)
                        return Result.Success;
                    else
                        return Result.Failure;
                }

                FileInfo source = (FileInfo)toCopy[i];
                string shortName = source.FullName.Replace(dir.FullName + "\\", "");
                FileInfo dest = new FileInfo(Program.Settings.DestinationDirectory + "\\" + shortName);
                dest.Directory.Create();

                AppendText(copyStr + shortName+"...");

                if (false == dest.Exists || dest.Length != source.Length)              source.CopyTo(dest.FullName);

                AppendText("\n");

                SetProgress(i);
            }

            return Result.Success;
        }
    }
}
