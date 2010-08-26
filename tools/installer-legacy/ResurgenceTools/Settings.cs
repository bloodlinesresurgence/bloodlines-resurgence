using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using ResurgenceLib;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Drawing;

namespace ResurgenceTools
{
    /// <summary>
    /// Provides a serializable Settings class for various
    /// program settings.
    /// </summary>
    internal sealed class Settings
    {
        /// <summary>
        /// The default file to open.
        /// </summary>
        //internal const string DefaultFile = "$/Config.Soap";
        internal const string DefaultFile = ":$:/Config.Xml";

        /// <summary>
        /// The location to save to.
        /// </summary>
        internal string StorageFilename;

        /// <summary>
        /// Translation provider.
        /// </summary>
        internal TranslationProvider TranslationProvider;

        /// <summary>
        /// Holds the settings for the program.
        /// </summary>
        private XMLTable settings;

        /// <summary>
        /// Stack to hold "write on alter" values (can be pushed and popped.)
        /// </summary>
        private Stack<bool> writeOnAlterStack = new Stack<bool>();

        /// <summary>
        /// Provides a delegate for executing generic procedures (see the NoWriteCall function.)
        /// </summary>
        /// <param name="settingsObject">The settings object calling the method.</param>
        internal delegate void GenericProcedure(Settings settingsObject);

        /// <summary>
        /// Creates a new instance of the Settings class with default
        /// options.
        /// </summary>
        internal Settings()
        {
            StorageFilename = DefaultFile;

            settings = new XMLTable();

            Program.Settings = this;

            LoadTranslations();
        }

        /// <summary>
        /// Creates a new instance of the Settings class with default
        /// options and a specified storage location.
        /// </summary>
        /// <param name="storageFilename">Location to store the settings file.</param>
        internal Settings(string storageFilename)
        {
            StorageFilename = storageFilename;

            Program.Settings = this;

            try
            {
                settings = XMLTable.Load(storageFilename);
            }
            catch (InvalidXMLTableFileException)
            {
                MessageBox.Show("Unable to load settings file - the format is no longer support, or the file is corrupt.", "Load Settings",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                settings = new XMLTable();
            }

            //LoadTranslations();
        }

        /// <summary>
        /// Sets the new WriteOnAlter value, saving the old one.
        /// </summary>
        /// <param name="newValue"></param>
        internal void WriteOnAlter_Push(bool newValue)
        {
            writeOnAlterStack.Push(WriteOnAlter);
            WriteOnAlter = newValue;
        }

        /// <summary>
        /// Resets the previous WriteOnAlter value.
        /// </summary>
        internal void WriteOnAlter_Pop()
        {
            bool resetValue;

            try
            {
                resetValue = writeOnAlterStack.Pop();
            }
            catch
            {
                resetValue = true;  // Fall-back
            }

            WriteOnAlter = resetValue;
        }

        /// <summary>
        /// Gets or sets the value indicating whether the settings are written
        /// each time they are altered.
        /// </summary>
        internal bool WriteOnAlter
        {
            get { return settings.SaveOnUpdate; }
            set { settings.SaveOnUpdate = value; }
        }

        /// <summary>
        /// Calls the given procedure, whilst enabling or disabling the WriteOnAlter attribute.
        /// </summary>
        /// <param name="procedure">Procedure to call.</param>
        internal void WriteOnAlterFunc(bool write, GenericProcedure procedure)
        {
            WriteOnAlter_Push(write);
            if (procedure != null)
                procedure(this);
            WriteOnAlter_Pop();
        }

        /// <summary>
        /// Forces a save of the settings.
        /// </summary>
        internal void Save ()
        {
            settings.Save(StorageFilename);
        }

        /// <summary>
        /// Gets the directory that the application started from.
        /// </summary>
        internal string ApplicationDirectory
        {
            get
            {
                return Application.StartupPath;
            }
        }

        /// <summary>
        /// Attempts to load the selected translations.
        /// Defaults to English if not yet specified, and falls back to an empty provider if unable to load.
        /// </summary>
        internal void LoadTranslations()
        {
            Lib.SetCommunicationsObject(LibCommunications.GetInstance());

            try
            {
                TranslationProvider = new TranslationProvider(Language);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error loading language: " + Ex.Message, "Load Translations Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                TranslationProvider = new TranslationProvider();
            }
        }

        /// <summary>
        /// Gets the languages directory.
        /// </summary>
        internal string LanguageDirectory
        {
            get
            {
                return ProgramData + "\\Languages";
            }
        }

        /// <summary>
        /// Gets or sets the setting under the specified key.
        /// </summary>
        /// <param name="key">Key that specifies the option.</param>
        /// <returns>The value of the given option.</returns>
        internal object this[object key]
        {
            get {
                if (settings.ContainsKey(key))
                    return settings[key];
                else
                    return null;
            }
            set
            {
                settings[key] = value;
#if false
                Serialize();
#endif
            }
        }

        /// <summary>
        /// Gets or sets the setting under the specified key, returning the specified
        /// default value if the option does not exist.
        /// </summary>
        /// <param name="key">Key that specifies the option.</param>
        /// <param name="defaultValue">Default value returned if the setting does not exist.</param>
        /// <returns>The value of the given option.</returns>
        internal object this[object key, object defaultValue]
        {
            get
            {
                if (settings.ContainsKey(key) == false)
                {
                    // Write default value
                    this[key] = defaultValue;
                    return defaultValue;
                }
                else
                    return settings[key];
            }
        }

#if false
        /// <summary>
        /// Serializes (saves) the current settings to file.
        /// </summary>
        public void Serialize()
        {
            settings.Save(GetStoragePath(StorageFilename));
        }
#endif

        /// <summary>
        /// Loads program settings from the specified file.
        /// Creates new settings if file does not exist.
        /// </summary>
        /// <returns>A Settings object containing program settings.</returns>
        internal static Settings Load()
        {
            return Load(GetStoragePath(DefaultFile));
        }

        /// <summary>
        /// Loads program settings from the specified file.
        /// Creates new settings if file does not exist.
        /// </summary>
        /// <param name="filename">File to load settings from.</param>
        /// <returns>A Settings object containing program settings.</returns>
        internal static Settings Load(string filename)
        {
            if (filename == null)
                filename = DefaultFile;

            string adjustedFilename = GetStoragePath(filename);

            if (File.Exists(adjustedFilename) == false)
                return createDefault(adjustedFilename);

            try
            {
                /*Settings settings = Settings._Deserialize(adjustedFilename, FormatterType.Soap) as Settings;
                settings.StorageFilename = adjustedFilename;

                return settings;*/
                Settings settings = new Settings(adjustedFilename);

                return settings;
            }
            catch
            {
                // Error loading settings, create new one.
                return createDefault(adjustedFilename);
            }
        }

        private static string GetStoragePath(string filename)
        {
            // Replace $ with program root.
            string adjustedFilename = filename.Replace(":$:", ProgramData);
            return adjustedFilename;
        }

        private static Settings createDefault(string adjustedFilename)
        {
            Settings settings = new Settings();
            settings.StorageFilename = GetStoragePath(adjustedFilename);
            return settings;
        }

        /// <summary>
        /// Gets the directory which will store program data.
        /// </summary>
        internal static string ProgramData
        {
            get
            {
                return Info.ApplicationData + "\\Resurgence Tools";
            }
        }

        /// <summary>
        /// Generates a path to the specified directory, which is 
        /// a directory in the program data directory.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        internal string MakeDirectory(string directory)
        {
            string thedirectory = ProgramData + "\\" + directory + "\\";
            if(Directory.Exists(thedirectory) == false)
                try
                {
                    // Attempt to create it
                    Directory.CreateDirectory(thedirectory);
                }
                catch { /* Ignore */ }

            return thedirectory;
        }

        /// <summary>
        /// Gets or sets the language to use.
        /// </summary>
        internal string Language
        {
            get { return this["Language", "English"] as string; }
            set
            {
                this["Language"] = value;
                TranslationProvider.Language = value;
            }
        }

        /// <summary>
        /// Gets or sets the path to the Vampire: Bloodlines directory.
        /// </summary>
        internal string VampireDirectory
        {
            get { return this["VampireDirectory", null] as string; }
            set { this["VampireDirectory"] = value; }
        }

        /// <summary>
        /// Gets the VPK directory.
        /// </summary>
        internal string VPKDirectory
        {
            get { return VampireDirectory + "\\Vampire"; }
        }

        /// <summary>
        /// Gets the destination directory.
        /// </summary>
        internal string DestinationDirectory
        {
            get
            {
                string def = String.Format(@"{0}\SteamApps\SourceMods\{1}", Program.Settings.SteamDirectory, 
                    Program.Settings.ModDirectory);
#if DEBUG
                return this["DebugInstallationDirectory", def] as string;
#else
                return def;
#endif
            }
            set
            {
#if DEBUG
                this["DebugInstallationDirectory"] = value;
#endif
            }
        }

        /// <summary>
        /// Gets or sets the path to Steam, checking the registry if
        /// the value is not found.
        /// </summary>
        internal string SteamDirectory
        {
            get
            {
                string directory = this["SteamDirectory", null] as string;
                if (directory != null)
                {
                    return directory;
                }

                // Read registry to find install path
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                if (null == key) return null;

                string regEntry = key.GetValue("SteamPath") as string;

                //string regEntry = Microsoft.Win32.Registry.CurrentUser.GetValue(
                //    @"Software\Valve\Steam\SourceModInstallPath") as string;
                if (regEntry != null)
                {
                    // Update setting
                    SteamDirectory = regEntry;
                    return regEntry;
                }
                
                return null;
            }
            set { this["SteamDirectory"] = value; }
        }

        /// <summary>
        /// Gets or sets the temporary files directory.
        /// </summary>
        public string TemporaryDirectory
        {
            get
            {
                string directory = this["TempDirectory", null] as string;
                if (directory != null)
                    return directory;

                // Get it from the SpecialFolders enumeration.
                directory = Path.GetTempPath();
                if (directory.EndsWith(@"\") == false)
                    directory += @"\";
                directory += "BloodlinesTemp";
                TemporaryDirectory = directory;
                return directory;
            }
            set { this["TempDirectory"] = value; }
        }

        /// <summary>
        /// Gets or sets the mod directory name.
        /// </summary>
        internal string ModDirectory
        {
            get { return this["ModDirectory", "BloodlinesResurgence"] as string; }
            set { this["ModDirectory"] = value; }
        }

        /// <summary>
        /// Gets or sets the mod version to use.
        /// </summary>
        internal ModVersion ModVersion
        {
            get { return (ModVersion)this["ModVersion", ModVersion.EPISODE_ONE_MOD]; }
            set { this["ModVersion"] = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if we ignore an extraction being cancelled,
        /// and treat it as success.
        /// </summary>
        internal bool IgnoreCancelExtraction
        {
            get { return this["IgnoreCancelExtraction", null] != null; }
            set { this["IgnoreCancelExtraction"] = (value == true ? (object)true : null); }
        }

        /// <summary>
        /// Gets the string indicating the IKVM rar file.
        /// </summary>
        internal string IKVM_Rar
        {
            get { return this["IKVM_Rar", "ikvm-0.38.0.2-small.rar"] as string; }
        }

        /// <summary>
        /// Gets the smallgit rar filename.
        /// </summary>
        internal string smallgit_Rar
        {
            get { return this["smallgit_Rar", "smallgit.rar"] as string; }
        }

        /// <summary>
        /// Gets the string indicating the Unrar arguments for unraring the required files.
        /// </summary>
        internal string IKVM_Unrar_Arguments
        {
            get { return this["IKVM_Unrar_Args", "e -inul -y {0}"] as string; }
        }

        /// <summary>
        /// Generic unraring.
        /// </summary>
        internal string Generic_Unrar_Arguments
        {
            get { return this["Generic_Unrar_Args", "x -y -idp {0}"] as string; }
        }

        /// <summary>
        /// Gets the string indicating the arguments to be supplied to vmex for decompiling a map.
        /// </summary>
        internal string Vmex_Arguments
        {
            get { return this["Vmex_Arguments", "{0}"] as string; }
        }

        /// <summary>
        /// Gets or sets the tools directory.
        /// </summary>
        internal string Tools_Directory
        {
            get { return this["Tools_Directory", Settings.ProgramData + "\\Tools"] as string; }
            set { this["Tools_Directory"] = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating whether wizard steps wait for the user to click next
        /// or move to their next step automatically.
        /// </summary>
        internal bool AutoProceed
        {
            get { return (bool)this["AutoProceed", false]; }
            set { this["AutoProceed"] = value; }
        }

        /// <summary>
        /// Gets or sets the last selected steps.
        /// </summary>
        internal WizardSteps LastSelectedSteps
        {
            get
            {
                return (WizardSteps)this["SelectedSteps", 
                    // Default is all steps minus extracting original models.
                    WizardSteps.All ^ WizardSteps.Extract_Original_Models];
            }
            set { this["SelectedSteps"] = value; }
        }

        /// <summary>
        /// Gets the path to the welcome document for the current language.
        /// </summary>
        internal string WelcomeDocument
        {
            get
            {
                return String.Format("{0}\\Documents\\Welcome-{1}.rtf",
                    LanguageDirectory, Language);
            }
        }

        /// <summary>
        /// Gets the path to the finish document for the current language.
        /// </summary>
        internal string FinishDocument
        {
            get
            {
                return String.Format("{0}\\Documents\\Finish-{1}.rtf",
                    LanguageDirectory, Language);
            }
        }

        internal System.Drawing.Point WindowPosition
        {
            get
            {
                /*int x = WinPos_X, y = WinPos_Y;
                if(x == -1 || y == -1)
                    return System.Drawing.Point.Empty;
                return new System.Drawing.Point(x, y);*/
                return (Point)this["WindowPosition", Point.Empty];
            }
            set {
                this["WindowPosition"] = value;
            }
        }

        /// <summary>
        /// Stores whether an installation has been completed or not.
        /// </summary>
        internal bool HasCompletedInstallation
        {
            get
            {
                if (null == this["hasCompletedInstallation"])
                {
                    this["hasCompletedInstallation"] = false;
                }
                return (bool)this["hasCompletedInstallation"];
            }
            set
            {
                this["hasCompletedInstallation"] = value;
            }
        }

        /*private int WinPos_X
        {
            get { return (int)this["WinPos_X", -1]; }
            set { this["WinPos_X"] = value; }
        }

        private int WinPos_Y
        {
            get { return (int)this["WinPos_Y", -1]; }
            set { this["WinPos_Y"] = value; }
        }*/
    }
}
