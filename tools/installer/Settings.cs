using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace Resurgence
{
    /// <summary>
    /// Program-wide settings
    /// </summary>
    internal class Settings
    {
        private XMLTable settings;

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Settings () {
            settings = new XMLTable();
        }

        private Settings(string path)
        {
            settings = XMLTable.Load(path);
        }

        public static string FilePath
        {
            get
            {
                return Path.Combine(Info.ApplicationData, "BloodlinesResurgence\\settings.xml");
            }
        }

        private string this[string key]
        {
            get
            {
                return settings[key] as string;
            }
            set
            {
                settings[key] = value;
            }
        }

        public static Settings GetInstance()
        {
            if (true == File.Exists(FilePath))
            {
                try
                {
                    return new Settings(FilePath);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error loading settings. Settings have been reset. Reason: " + 
                        ex.Message,
                        "Bloodlines Resurgence", System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return new Settings();
        }

        public string SteamDirectory
        {
            get
            {
                if (null == this["SteamDirectory"])
                {
                    this["SteamDirectory"] = detectSteamDirectory();
                }
                return this["SteamDirectory"];
            }
            set
            {
                this["SteamDirectory"] = value;
            }
        }
        private string detectSteamDirectory()
        {
            // Read registry to find install path
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
            string regEntry = key.GetValue("SteamPath") as string;

            if (regEntry != null)
            {
                return regEntry;
            }

            return null;
        }

        public string TemporaryDirectory
        {
            get
            {
                if (null == this["TempDirectory"])
                {
                    this["TempDirectory"] = detectTempDirectory();
                }
                return this["TempDirectory"];
            }
            set
            {
                this["TempDirectory"] = value;
            }
        }

        private string detectTempDirectory()
        {
            // Get it from the SpecialFolders enumeration.
            string directory = Path.GetTempPath();
            if (directory.EndsWith(@"\") == false)
                directory += @"\";
            directory += "ResurgenceTemp";
            return directory;
        }

        public string VampireDirectory
        {
            get
            {
                if (null == this["VampireDirectory"])
                {
                    this["VampireDirectory"] = "";
                }
                return this["VampireDirectory"];
            }
            set
            {
                this["VampireDirectory"] = value;
            }
        }
    }
}
