using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides an interface to get various data from the host program.
    /// </summary>
    public interface ILibCommunications
    {
        /// <summary>
        /// Gets full path to the specified file.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        string GetLanguageFile(string language);

        /// <summary>
        /// Add a log item (usually for debug logging, in case the user is having issues)
        /// </summary>
        /// <param name="text"></param>
        void AddLog(string text);
    }
}
