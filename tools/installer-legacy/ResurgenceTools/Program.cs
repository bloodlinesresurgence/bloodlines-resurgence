using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ResurgenceLib;
using Resurgence.WizardPages;
using System.ComponentModel;
using Resurgence.Steps;
using System.IO;

namespace Resurgence
{
    /// <summary>
    /// Provides the startup routine for the program, as well as access to global
    /// variables and data.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The file to load settings from.
        /// </summary>
        internal static string SettingsFile = null;

        /// <summary>
        /// The program settings.
        /// </summary>
        internal static Settings Settings;

#if FULL_TOOLSET
        /// <summary>
        /// List of loaded modules.
        /// </summary>
        internal static List<AssemblyInfo> LoadedAssemblyModules;


        /// <summary>
        /// A list of available tools.
        /// </summary>
        internal static List<ToolInfo> ToolList;

        /// <summary>
        /// The application's main form.
        /// </summary>
        internal static MainForm MainForm;
#else   // #if FULL_TOOLSET
        /// <summary>
        /// The next form to display.
        /// </summary>
        internal static WizardPage NextForm;
#endif

        /// <summary>
        /// The steps to run.
        /// </summary>
        internal static Type[] StepsToRun;

        /// <summary>
        /// The selected steps, saved for later modification.
        /// </summary>
        internal static WizardSteps SelectedSteps = WizardSteps.NONE;

        /// <summary>
        /// The current step being run.
        /// </summary>
        internal static int CurrentStep = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            processArguments(arguments);

            MigrateFiles("Languages", "*.txt", "Languages", false);

            Settings = Settings.Load(SettingsFile);

            SelectedSteps = Settings.LastSelectedSteps;
            
            // Load the translations.
            Lib.SetCommunicationsObject(LibCommunications.GetInstance());
           
            Settings.LoadTranslations();

            MigrateDataFiles();
#if FULL_TOOLSET
            LoadedAssemblyModules = new List<AssemblyInfo>();
            string ToolsDirectory = Settings.MakeDirectory("Tools");
            ToolList = ModuleLoader<ToolInfo>.LoadAll(
                new string[] { ToolsDirectory, "Tools" }, "*.dll");
            MainForm = new MainForm(Settings.TranslationProvider);
            Application.Run(MainForm);
#else
            NextForm = new Welcome(Program.Settings.TranslationProvider);

            while (NextForm != null)
            {
                Application.Run(NextForm);
            }
#endif
        }

        /// <summary>
        /// Copies all the relevant files from the application directory to the
        /// data directory if they don't exist or are newer than the ones in the
        /// data directory.
        /// </summary>
        private static void MigrateDataFiles()
        {
            MigrateFiles("Languages\\Documents", "*.*", "Languages\\Documents", true);
            MigrateFiles("Tools", "*.*", "Tools", true);
        }

        /// <summary>
        /// Migrates the files from the given source path to the application data folder.
        /// </summary>
        /// <param name="path">Source to copy files from.</param>
        /// <param name="filespec">File pattern to match.</param>
        /// <param name="destination">(Relative) destination path.</param>
        /// <param name="dialog">Sets whether to show the progress dialog.</param>
        private static void MigrateFiles(string path, string filespec, string destination, bool dialog)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles(filespec);
            string destinationDirectory = Settings.ProgramData + "\\" + destination + "\\";
            List<string> failedCopies = new List<string>();

            if (Directory.Exists(destinationDirectory) == false)
            {
                try
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                catch (Exception ex)
                {
                    migrationError("Failed to create destination directory: ", destinationDirectory + Environment.NewLine + ex.Message);
                    return;
                }
            }

            Preparing preparing = null;
            NodeCS.Callback callback = delegate()
            {
                foreach (FileInfo sourceInfo in files)
                {
                    string destinationFile = destinationDirectory + sourceInfo.Name;
                    if (File.Exists(destinationFile))
                    {
                        // Check file times - skips file if the destination is newer than the source.
                        FileInfo destInfo = new FileInfo(destinationFile);
                        if (destInfo.LastWriteTimeUtc > sourceInfo.LastWriteTimeUtc)
                            continue;
                    }

                    try
                    {
                        File.Copy(sourceInfo.FullName, destinationFile, true);
                    }
                    catch (Exception ex)
                    {
                        failedCopies.Add(sourceInfo.Name + ": " + ex.Message);
                    }

                    if (dialog)
                    {
                        ResurgenceLib.Threading.ProgressBars.SetProgress(preparing.Progress, preparing.Progress.Value + 1);
                    }
                }

                if (failedCopies.Count > 0)
                {
                    string fileList = "";
                    foreach (string currentFailed in failedCopies)
                        fileList += currentFailed + Environment.NewLine;
                    migrationError("Failed to copy the following files to the data directory:", fileList);
                }

                if (dialog)
                {
                    preparing.Close();
                }
            };
            if (dialog)
            {
                preparing = new Preparing(Program.Settings.TranslationProvider, callback);
                preparing.Show();

                ResurgenceLib.Threading.ProgressBars.SetProgressStyle(preparing.Progress, ProgressBarStyle.Continuous);
                ResurgenceLib.Threading.ProgressBars.SetProgressMax(preparing.Progress, files.Length);
            }
            else
            {
                callback();
            }

            
        }

        /// <summary>
        /// Displays an error that has been generated during the migration of data files to the data directory.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="parameters"></param>
        private static void migrationError(string title, string parameters)
        {
            MessageBox.Show(title + Environment.NewLine + parameters, "Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Argument states for processing program arguments.
        /// </summary>
        private enum ArgumentState
        {
            NONE = 0,
            SETTINGS_FILE
        }

        /// <summary>
        /// Process program argument directives.
        /// </summary>
        /// <param name="arguments">Arguments to process.</param>
        private static void processArguments(string[] arguments)
        {
            ArgumentState state = ArgumentState.NONE;

            foreach (string argument in arguments)
            {
                switch (state)
                {
                    case ArgumentState.NONE:
                        switch (argument.ToLower())
                        {
                            case "-settings":
                            case "-setting":
                            case "-s":
                                state = ArgumentState.SETTINGS_FILE;
                                break;
                        }
                        break;

                    case ArgumentState.SETTINGS_FILE:
                        SettingsFile = argument;
                        break;
                }
            }
        }

        /// <summary>
        /// Constructs the list of wizard steps to be run.
        /// </summary>
        /// <param name="steps"></param>
        internal static void ConstructSteps(WizardSteps steps)
        {
            SelectedSteps = steps;
            Program.Settings.LastSelectedSteps = steps;

            CurrentStep = -1;
            List<Type> constructedSteps = new List<Type>();

            // Using flags, generate the steps to run
            if ((steps & WizardSteps.Extract_General_Files) != 0)
                constructedSteps.Add(typeof(ExtractGeneralFiles));
            if ((steps & WizardSteps.Copy_Original_Files) != 0)
                constructedSteps.Add(typeof(CopyOriginalFiles));
            if ((steps & WizardSteps.Extract_Materials) != 0)
                constructedSteps.Add(typeof(ExtractMaterials));
            if ((steps & WizardSteps.Convert_And_Copy_Materials) != 0)
                constructedSteps.Add(typeof(ConvertAndCopyMaterials));
            if ((steps & WizardSteps.Decompile_Maps) != 0)
                constructedSteps.Add(typeof(DecompileMaps));
            if ((steps & WizardSteps.Fix_Decompiled_Maps) != 0)
                constructedSteps.Add(typeof(FixDecompiledMaps));
            if ((steps & WizardSteps.Extract_Base_Files) != 0)
                constructedSteps.Add(typeof(CopyBaseFiles));
            if ((steps & WizardSteps.Extract_Original_Models) != 0)
                constructedSteps.Add(typeof(ExtractModels));
            if ((steps & WizardSteps.Update_And_Patch_Files) != 0)
                constructedSteps.Add(typeof(UpdateAndPatchFiles));
            if ((steps & WizardSteps.View_Changelog) != 0)
                constructedSteps.Add(typeof(ViewChangelog));

            StepsToRun = constructedSteps.ToArray();
        }

        /// <summary>
        /// Displays the next wizard dialog.
        /// </summary>
        internal static void NextStep()
        {
            Form form;
            CurrentStep++;
            if (CurrentStep == StepsToRun.Length)
            {
                form = new FinalPage(Program.Settings.TranslationProvider);
#if !FULL_TOOLSET
                Program.NextForm = form as WizardPage;
#else
                form.Show(MainForm);
#endif
                return;
            }

            form = CreateStep();
#if !FULL_TOOLSET
            Program.NextForm = form as WizardPage;
#else
            form.Show(Program.MainForm);
#endif
        }

        /// <summary>
        /// Creates a new wizard page with the current step.
        /// </summary>
        /// <returns></returns>
        private static WizardPage CreateStep()
        {
            return (WizardPage)
                        Activator.CreateInstance(StepsToRun[CurrentStep], Settings.TranslationProvider);
        }

        /// <summary>
        /// Displays the previous wizard step.
        /// </summary>
        internal static void PreviousStep()
        {
            CurrentStep--;
            if (CurrentStep < 0)
            {
#if !FULL_TOOLSET
                Program.NextForm = new SelectSteps(Settings.TranslationProvider);
#else
                (new SelectSteps(Settings.TranslationProvider)).Show(MainForm);
#endif
                return;
            }

            WizardPage form = CreateStep();
#if !FULL_TOOLSET
            Program.NextForm = form;
#else
            form.Show(MainForm);
#endif
        }

        internal delegate void UpdateCheckCallback(string updateURL);
        private static string updateURL = null;
        internal static void checkForUpdate(UpdateCheckCallback callback)
        {
            // Check for updates :)
            if (null != updateURL)
            {
                if ("" != updateURL) callback(updateURL);
                return;
            }

            NodeCS.Async.run(delegate()
            {
                string url = "http://www.bloodlinesresurgence.com/patcher.txt";
                System.Net.WebClient client = new System.Net.WebClient();
                client.Proxy = null;

                string contents;

                try
                {
                    contents = client.DownloadString(url);
                }
                catch (Exception ex)
                {
                    return;
                }

                string[] parts = contents.Split(',');
                if (parts.Length == 2)
                {
                    if (Application.ProductVersion != parts[0])
                    {
                        updateURL = parts[1];
                        callback(parts[1]);
                    }
                    else
                    {
                        updateURL = "";
                    }
                }
            });
        }
    }

    /// <summary>
    /// Specifies the wizard steps to run.
    /// </summary>
    [Serializable]
    [Flags]
    internal enum WizardSteps
    {
        NONE = 0,
        //Recreate_VPK_Indexes = 1 << 1,
        Extract_General_Files = 1 << 2,
        Copy_Original_Files = 1 << 3,
        Extract_Materials = 1 << 4,
        Convert_And_Copy_Materials = 1 << 5,
        Decompile_Maps = 1 << 6,
        Fix_Decompiled_Maps = 1 << 7,
        Extract_Base_Files = 1 << 8,
        Extract_Original_Models = 1 << 9,
        //Copy_New_Models = 1 << 9,
        Update_And_Patch_Files = 1 << 10,
        View_Changelog = 1 << 11,

        All = NONE
            | Extract_General_Files
            | Copy_Original_Files 
            | Extract_Materials
            | Convert_And_Copy_Materials
            | Decompile_Maps
            | Fix_Decompiled_Maps
            | Extract_Base_Files
            | Extract_Original_Models
            // | Copy_New_Models
            | Update_And_Patch_Files,
            

        INSTALL = NONE
            | Extract_General_Files
            | Copy_Original_Files
            | Extract_Materials
            | Convert_And_Copy_Materials
            | Decompile_Maps
            | Fix_Decompiled_Maps
            | Extract_Base_Files
            | Update_And_Patch_Files,
    }

    /// <summary>
    /// The different mod versions.
    /// </summary>
    internal enum ModVersion
    {
        /// <summary>
        /// HL2: Episode One engine mod.
        /// </summary>
        EPISODE_ONE_MOD,
        /// <summary>
        /// HL2: Orange Box engine mod.
        /// </summary>
        ORANGE_BOX_MOD
    }
}
