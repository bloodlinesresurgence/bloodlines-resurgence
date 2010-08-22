using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using System.Diagnostics;

namespace ResurgenceTools.Steps
{
    internal partial class CopyReplacementFiles 
        : GenericRunProcess
    {
        public CopyReplacementFiles()
        {
            InitializeComponent();
        }

        public CopyReplacementFiles(TranslationProvider Provider)
            : base(Provider)
        { }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();
            AutoProceedStep = true;
        }

        protected override Result DoWork()
        {
            ExtractRar("replacements.rar");

            switch (Program.Settings.ModVersion)
            {
                case ModVersion.EPISODE_ONE_MOD:
                    ExtractRar("content-ep1.rar");
                    break;

                case ModVersion.ORANGE_BOX_MOD:
                    ExtractRar("content-ob.rar");
                    break;
            }

            return Result.Success;
        }

        private void ExtractRar(string rarFile)
        {
            AppendText(Translate("!Starting") + "\n");

            string rar = "\"" + Program.Settings.Tools_Directory + "\\" + rarFile + "\"";

            ProcessStartInfo info = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\Unrar.exe");
            info.WorkingDirectory = Program.Settings.DestinationDirectory;
            info.Arguments = String.Format(Program.Settings.Generic_Unrar_Arguments, rar);

            SetProgressStyle(ProgressBarStyle.Marquee);

            RunProcess(info);

            SetProgressStyle(ProgressBarStyle.Blocks);
        }
    }
}
