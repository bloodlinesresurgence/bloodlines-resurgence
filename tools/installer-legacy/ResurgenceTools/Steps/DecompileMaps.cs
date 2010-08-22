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
using System.Threading;
using System.IO;

namespace ResurgenceTools.Steps
{
    /// <summary>
    /// Provides a map decompiler wizard step.
    /// </summary>
    public partial class DecompileMaps : GenericRunProcess
    {
        public DecompileMaps()
        {
            InitializeComponent();
        }

        public DecompileMaps(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            AutoProceedStep = true;
        }

        protected override Result DoWork()
        {
            prepareVmex();

            string mapdir = Program.Settings.VampireDirectory + "\\Vampire\\maps\\";

            string[] maps = Directory.GetFiles(mapdir, "*.bsp");
            SetProgressMax(maps.Length);

            ProcessStartInfo vmex = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\vmex.exe");
            vmex.WorkingDirectory = mapdir;
            vmex.UseShellExecute = false;
            vmex.CreateNoWindow = true;
            vmex.RedirectStandardOutput = true;

            int mapsDone = 0;

            foreach (string mapFile in maps)
            {
                if (CancelProcess == true)
                {
                    AppendText(TranslationProvider.Translate("!Cancelled") + "\n");
                    if (Program.Settings.IgnoreCancelExtraction)
                        return Result.Success;
                    else
                        return Result.Failure;
                }

                string pathStripped = mapFile.Replace(mapdir, "");
                //vmex.Arguments = pathStripped;
                vmex.Arguments = pathStripped;

                AppendText(String.Format(TranslationProvider.Translate("!Decompiling", this.Name) + "\n", pathStripped));

                RunProcess(vmex);

                ++mapsDone;
                ReportProgress(0, mapsDone);
            }

            AppendText(TranslationProvider.Translate("!DecompilingDone", this.Name) + "\n");

            return Result.Success;
        }

        /// <summary>
        /// Prepares Vmex for execution by unraring the supporting IKVM files.
        /// </summary>
        /// <remarks>
        /// This stage is required, as vmex has been provided in an IKVM-converted .net executable.
        /// This removes the need for Java to be installed on the end-users machine, but requires a
        /// rather large support file (IKVM's open source java library), which we rar to reduce size.
        /// </remarks>
        private void prepareVmex()
        {
            AppendText(TranslationProvider.Translate("!StageOne", this.Name));

            ProcessStartInfo info = new ProcessStartInfo(Program.Settings.Tools_Directory + "\\Unrar.exe",
                String.Format(Program.Settings.IKVM_Unrar_Arguments, Program.Settings.IKVM_Rar));
            info.WorkingDirectory = Program.Settings.Tools_Directory;

            RunProcess(info);

            AppendText(TranslationProvider.Translate("!StageOneDone", this.Name) + "\n");
        }
    }
}
