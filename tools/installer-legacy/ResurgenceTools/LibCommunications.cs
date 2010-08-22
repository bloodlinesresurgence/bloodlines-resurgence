using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResurgenceLib;

namespace ResurgenceTools
{
    internal class LibCommunications
        : ILibCommunications
    {
        private static LibCommunications CurrentInstance = null;

        internal static LibCommunications GetInstance()
        {
            if (CurrentInstance == null)
                CurrentInstance = new LibCommunications();
            return CurrentInstance;
        }

        public string GetLanguageFile(string language)
        {
            return Program.Settings.LanguageDirectory + "\\" + language + ".txt";
        }
    }
}
