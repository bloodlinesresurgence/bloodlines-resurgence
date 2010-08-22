using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using RevivalLib;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RevivalTools
{
    /// <summary>
    /// Provides a serializable Settings class for various
    /// program settings.
    /// </summary>
    [Serializable]
    internal sealed class Settings
        : DaedalusLib.BaseSerializable
    {
        /// <summary>
        /// The default file to open.
        /// </summary>
        internal const string DefaultFile = "$/Config.Soap";

        /// <summary>
        /// The location to save to.
        /// </summary>
        [NonSerialized]
        internal string StorageFilename;

        /// <summary>
        /// Translation provider.
        /// </summary>
        [NonSerialized]
        internal TranslationProvider TranslationProvider;

        /// <summary>
        /// Holds the settings for the program.
        /// </summary>
        private Hashtable settings;

        /// <summary>
        /// Creates a new instance of the Settings class with default
        /// options.
        /// </summary>
        internal Settings()
            : base(FormatterType.Soap)
        {
            StorageFilename = DefaultFile;

            settings = new Hashtable();

            LoadTranslations();
        }

        /// <summary>
        /// Creates a new instance of the Settings class with default
        /// options and a specified storage location.
        /// </summary>
        /// <param name="storageFilename">Location to store the settings file.</param>
        internal Settings(string storageFilename)
            : base(FormatterType.Soap)
        {
            StorageFilename = storageFilename;

            settings = new Hashtable();

            LoadTranslations();
        }

        internal string ApplicationDirectory
        {
            get
            {
                return Application.StartupPath;
            }
        }

        /// <summary>
        /// Called when the object is deserialized. Fixes any issues, and reloads
        /// any required data.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
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
                TranslationProvider = new TranslationProvider(this["Language", "English"] as string);
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
                Serialize();
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

        /// <summary>
        /// Serializes (saves) the current settings to file.
        /// </summary>
        public void Serialize()
        {
            ChildObjectRef = this;
            __Serialize(StorageFilename);
        }

        /// <summary>
        /// Loads program settings from the specified file.
        /// Creates new settings if file does not exist.
        /// </summary>
        /// <returns>A Settings object containing program settings.</returns>
        internal static Settings Load()
        {
            return Load();
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

            // Replace $ with program root.
            string adjustedFilename = filename.Replace("$", ProgramData);

            if (File.Exists(adjustedFilename) == false)
                return createDefault(adjustedFilename);

            try
            {
                Settings settings = Settings._Deserialize(adjustedFilename, FormatterType.Soap) as Settings;
                settings.StorageFilename = adjustedFilename;

                return settings;
            }
            catch
            {
                // Error loading settings, create new one.
                return createDefault(adjustedFilename);
            }
        }

        private static Settings createDefault(string adjustedFilename)
        {
            Settings settings = new Settings(adjustedFilename);
            return settings;
        }

        /// <summary>
        /// Gets the directory which will store program data.
        /// </summary>
        internal static string ProgramData
        {
            get
            {
                return Info.ApplicationData + "\\Revival Tools";
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
            get { return 
                String.Format(@"{0}\SteamApps\SourceMods\{1}", Program.Settings.SteamDirectory, Program.Settings.ModDirectory); }
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

        public string TemporaryDirectory
        {
            get
            {
                string directory = this["TempDirectory", null] as string;
                if (directory != null)
                    return directory;

                // Get it from the SpecialFolders enumeration.
                directory = Path.GetTempPath() + "\\BloodlinesTemp";
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
            get { return this["ModDirectory", "BloodlinesRevival"] as string; }
            set { this["ModDirectory"] = value; }
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
            get { return this["Generic_Unrar_Args", "e -y {0}"] as string; }
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
    }
}
