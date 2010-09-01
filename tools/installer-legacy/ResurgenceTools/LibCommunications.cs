using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResurgenceLib;

namespace Resurgence
{
    internal class LibCommunications
        : ILibCommunications
    {
        private static LibCommunications CurrentInstance = null;

        private StringBuilder DebugLog = new StringBuilder();

        internal static LibCommunications GetInstance()
        {
            if (CurrentInstance == null)
                CurrentInstance = new LibCommunications();
            return CurrentInstance;
        }

        /// <summary>
        /// Fast access to AddLog
        /// </summary>
        /// <param name="text"></param>
        internal static void gAddLog(string text)
        {
            LibCommunications.GetInstance().AddLog(text);
        }

        /// <summary>
        /// Get log contents
        /// </summary>
        /// <returns></returns>
        internal static string gLog
        {
            get
            {
                return LibCommunications.GetInstance().DebugLog.ToString();
            }
        }

        public string GetLanguageFile(string language)
        {
            return Program.Settings.LanguageDirectory + "\\" + language + ".txt";
        }

        public void AddLog(string text)
        {
            this.DebugLog.AppendLine(text);
        }
    }
}
