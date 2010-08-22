using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RevivalTools
{
    /// <summary>
    /// Provides automated loading of modules of the specified type.
    /// </summary>
    /// <typeparam name="t">Type of the module (derived class from AssemblyInfo).</typeparam>
    internal class ModuleLoader<t>
        where t : AssemblyInfo, new()
    {
        /// <summary>
        /// Searches for assembly modules in the specified directories that match type t and returns them.
        /// </summary>
        /// <param name="directoriesToSearch">Directories to search for modules.</param>
        /// <param name="searchPattern">Search pattern for modules.</param>
        /// <returns>
        /// An list of type AssemblyInfo t containing the loaded info. A conversion to the derived class
        /// may be required.
        /// </returns>
        internal static List<t> LoadAll(string[] directoriesToSearch, string searchPattern)
        {
            List<t> validModules = new List<t>();

            foreach (string currentDirectory in directoriesToSearch)
            {
                if (Directory.Exists(currentDirectory) == false)
                {
                    // Attempt to create the directory
                    try { Directory.CreateDirectory(currentDirectory); }
                    catch { /* Ignore */ }
                    // Obivously no files in here ;)
                    continue;
                }

                // Get all files
                string[] files = Directory.GetFiles(currentDirectory, searchPattern);

                // Examine each file to see if it matches
                foreach (string currentFile in files)
                {
                    try
                    {
                        t info = new t();

                        // Load all the exposed modules
                        object[] modules = info.GetModules(currentFile);

                        // Go through each, and if valid add it to the list
                        foreach (object obj in modules)
                        {
                            t module = (t)obj;
                            if (module.IsValid)
                                validModules.Add(module);
                        }
                    }
                    catch { /* Ignore */ }
                }
            }

            return validModules;
        }
    }
}
