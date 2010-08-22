using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ResurgenceLib.Generics;

namespace ResurgenceLib.Translation
{
    /// <summary>
    /// Provides loading and parsing of a language file.
    /// </summary>
    public class LanguageFile
    {
        /// <summary>
        /// The currently loaded file.
        /// </summary>
        protected string filename;

        /// <summary>
        /// The loaded file contents.
        /// </summary>
        protected string[] fileContents;

        /// <summary>
        /// Creates a new instance of the LanguageFile class and loads
        /// the specified file.
        /// </summary>
        /// <param name="LanguageFile">Language file to load.</param>
        public LanguageFile(string LanguageFile)
        {
            filename = LanguageFile;

            if (File.Exists(LanguageFile) == false)
                throw new FileNotFoundException(LanguageFile + " does not exist!");

            ReloadFile();
        }

        /// <summary>
        /// Reloads the current file.
        /// </summary>
        public void ReloadFile()
        {
            fileContents = File.ReadAllLines(filename);
        }

        /// <summary>
        /// Gets the filename of the currently loaded file.
        /// </summary>
        public string Filename
        {
            get { return filename; }
        }

        /// <summary>
        /// Gets the current contents of the file.
        /// </summary>
        public string[] Contents
        {
            get { return fileContents; }
        }

        /// <summary>
        /// Parses the translation file.
        /// </summary>
        /// <returns>An array containing TranslationItems.</returns>
        internal TranslationCollection ParseTranslations()
        {
            TranslationItemList controls = new TranslationItemList();
            TranslationItemList constants = new TranslationItemList();

            // For debug purposes, we add the first letter of the translation file to
            // the line.
            string suffix = ""; 
#if DEBUG
            FileInfo info = new FileInfo(Filename);
            suffix = " (" + info.Name[0] + ")";
#endif

            string owner = Translation.GlobalId;
            // Run through contents, finding [Owner] and parsing other lines

            foreach (string currentLine in fileContents)
            {
                string trimmedLine = currentLine.Trim();
                if (trimmedLine.StartsWith("#"))     // Comment, ignore
                    continue;
                if (trimmedLine.Equals(""))          // Empty line, ignore
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    // Owner specification, eg: [frmWizard]
                    owner = trimmedLine.Substring(1, trimmedLine.Length - 2);
                }
                else if (trimmedLine.StartsWith("!"))
                {
                    // Constant, eg: !Name = Value
                    try
                    {
                        TranslationItem constant =
                            TranslationItem.Parse(owner, currentLine + suffix);
                        constants.Add(constant.Key, constant);
                    }
                    catch(Exception ex)
                    {
                        // Ignore
                        System.Diagnostics.Debug.Assert(false, "Constant error: " + ex.Message);
                    }
                } /* else if (trimmedLine.StartsWith("!")) */
                else
                {
                    // Standard line, parse it
                    try
                    {
                        TranslationItem translationItem
                            = TranslationItem.Parse(owner, currentLine + suffix);
                        controls.Add(translationItem.Key, translationItem);
                    }
                    catch(Exception ex)
                    {
                        // Ignore errors
                        System.Diagnostics.Debug.Assert(false, "Constant error: " + ex.Message);
                    }
                } /* else */
            } /* foreach (string currentLine... */

            return new TranslationCollection(constants, controls);
        }
    }
}
