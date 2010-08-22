using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib
{
    /// <summary>
    /// A class that provides translation capabilities.
    /// </summary>
    public class TranslationProvider
    {
        /// <summary>
        /// A generic delegate for translation events.
        /// </summary>
        public delegate void TranslationEventDelegate();

        /// <summary>
        /// Occurs when the translation language is changed.
        /// </summary>
        public TranslationEventDelegate OnLanguagedChanged;

        /// <summary>
        /// Occurs before the translation language is changed.
        /// </summary>
        public TranslationEventDelegate BeforeLanguageChanged;

        /// <summary>
        /// The translation engine currently being used.
        /// </summary>
        protected Translation.Translation Translation;

        /// <summary>
        /// The current translation engine.
        /// </summary>
        protected Translation.TranslationEngine Engine;

        /// <summary>
        /// Creates an empty translation provider.
        /// </summary>
        public TranslationProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TranslationProvider class
        /// with the given language.
        /// </summary>
        /// <param name="Language">Language to use.</param>
        public TranslationProvider(string Language)
        {
            Translation = new ResurgenceLib.Translation.Translation(Language);

            Engine = Translation.CreateEngineInstance();
        }

        /// <summary>
        /// Initializes a new instance of the TranslationProvider class
        /// with the given language.
        /// </summary>
        /// <param name="LanguagesDirectory">Directory to search for translation in.</param>
        /// <param name="Language">Language to use.</param>
        public TranslationProvider(string LanguagesDirectory, string Language)
        {
            Translation = new ResurgenceLib.Translation.Translation(LanguagesDirectory, Language);

            Engine = Translation.CreateEngineInstance();
        }

        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        public string Language
        {
            get { return Translation.Language; }
            set
            {
                if (BeforeLanguageChanged != null)
                    BeforeLanguageChanged();

                if (Translation == null)
                    Translation = new ResurgenceLib.Translation.Translation(value);
                else
                    Translation.Language = value;
                Engine = Translation.CreateEngineInstance();

                if (OnLanguagedChanged != null)
                    OnLanguagedChanged();
            }
        }

        /// <summary>
        /// Translates the target control and it's subcontrols.
        /// </summary>
        /// <param name="Target">Target control to translate.</param>
        public void TranslateControl(System.Windows.Forms.Control Target)
        {
            if(Engine != null)
                Engine.Translate(Target);
        }

        /// <summary>
        /// Translates the target control and it's subcontrols.
        /// </summary>
        /// <param name="Target">Target control to translate.</param>
        /// <param name="ParentName">Name of the parent control.</param>
        public void TranslateControl(System.Windows.Forms.Control Target, string ParentName)
        {
            if (Engine != null)
            {
                string previousParent = Engine.CurrentParent;
                Engine.CurrentParent = ParentName;
                Engine.Translate(Target);
                Engine.CurrentParent = previousParent;
            }
        }

        /// <summary>
        /// Translates the given constant string.
        /// </summary>
        /// <param name="Constant">The constant string to translate.</param>
        public string Translate(string Constant)
        {
            if(Engine != null)
                return Engine.TranslateConstant(Constant);

            return Constant;
        }

        /// <summary>
        /// Translates the given constant string.
        /// </summary>
        /// <param name="Constant">The constant string to translate.</param>
        /// <param name="ParentName">Name of the parent to read constants from.</param>
        /// <returns></returns>
        public string Translate(string Constant, string ParentName)
        {
            if (Engine != null)
            {
                string previousParent = Engine.CurrentParent;
                Engine.CurrentParent = ParentName;
                string result = Engine.TranslateConstant(Constant);
                Engine.CurrentParent = previousParent;

                return result;
            }
            else
            {
                return Constant;
            }
        }

        /// <summary>
        /// Translates the given result value.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public string Translate(Result result)
        {
            return Translate("!Result." + result.ToString());
        }
    }
}
