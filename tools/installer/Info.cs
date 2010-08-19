using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resurgence
{
    /// <summary>
    /// Provides various information data.
    /// </summary>
    public static class Info
    {
        /// <summary>
        /// Gets the directory that tools should store their data and settings in.
        /// </summary>
        public static string ApplicationData
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                      "\\Bloodlines Resurgence";
            }
        }
    }
}
