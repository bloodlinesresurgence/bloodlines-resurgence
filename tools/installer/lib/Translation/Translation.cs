using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Resurgence.Translation
{
    /// <summary>
    /// Provides automated translation support.
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// The language to use if the specified language is not found.
        /// </summary>
        protected const string FallBackLanguage = "English";

        /// <summary>
        /// The current language in use.
        /// </summary>
        protected string currentLanguage = FallBackLanguage;
        
        /// <summary>
        /// The directory containing available translations.
        /// </summary>
        protected string languagesDirectory = "Languages";

        /// <summary>
        /// The Globals section.
        /// </summary>
        public const string GlobalId = "!Globals";

        /// <summary>
        /// Array containing the translation items.
        /// </summary>
        protected TranslationCollection Translations;

        /// <summary>
        /// Creates a new instance of the Translation class.
        /// The default language of English will be used.
        /// </summary>
        public Translation()
        {
            ReloadTranslation();
        }

        /// <summary>
        /// Creates a new instance of the Translation class, using the
        /// specified language for translation.
        /// The default directory location will be used.
        /// </summary>
        /// <param name="Language">Language to load translation for.</param>
        public Translation(string Language)
        {
            currentLanguage = Language;

            ReloadTranslation();
        }

        /// <summary>
        /// Creates a new instance of the Translation class, using the
        /// specified directory as the source of translation files.
        /// The specified language will be used.
        /// </summary>
        /// <param name="CustomLanguagesDirectory">Directory containing the translation files.</param>
        /// <param name="Language">Language to use.</param>
        public Translation(string CustomLanguagesDirectory, string Language)
        {
            languagesDirectory = CustomLanguagesDirectory;
            currentLanguage = Language;

            ReloadTranslation();
        }

        /// <summary>
        /// Creates a new instance of a TranslationEngine object.
        /// </summary>
        /// <returns></returns>
        public TranslationEngine CreateEngineInstance()
        {
            return new TranslationEngine(Translations);
        }

        /// <summary>
        /// Reloads the translation file.
        /// </summary>
        public void ReloadTranslation()
        {
            LanguageFile langFile;

            try
            {
                string filename = TranslationFileName;
                langFile = new LanguageFile(filename);

                Translations = langFile.ParseTranslations();
            }
            catch (Exception ex)
            {
                throw new TranslationLoadException(ex);
            }
        }

        /// <summary>
        /// Gets the filename of the target translation file.
        /// </summary>
        public string TranslationFileName
        {
            get
            {
                string filename =
                    String.Format(@"{0}\{1}\{2}.txt",
                    Directory.GetCurrentDirectory(), languagesDirectory, currentLanguage);
                return filename;
                //return Lib.CommunicationsObject.GetLanguageFile(currentLanguage);
            }
        }

        /// <summary>
        /// Gets or sets the language currently being used.
        /// </summary>
        public string Language
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
                ReloadTranslation();
            }
        }
    }
}
