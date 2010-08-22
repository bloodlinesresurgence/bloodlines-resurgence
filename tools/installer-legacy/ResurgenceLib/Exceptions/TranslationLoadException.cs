using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib
{
    /// <summary>
    /// Occurs when unable to load a translation file.
    /// </summary>
    public class TranslationLoadException
        : Exception
    {
        /// <summary>
        /// Default exception message to report.
        /// </summary>
        protected const string DefaultExceptionMessage
            = "Unable to load translation file.";

        /// <summary>
        /// Creates a new instance of the TranslationLoadException
        /// using the default exception message.
        /// </summary>
        public TranslationLoadException()
            : base(DefaultExceptionMessage)
        {
        }

        /// <summary>
        /// Creates a new instance of the TranslationLoadException
        /// using the specified exception message.
        /// </summary>
        /// <param name="Message">Exception message to report.</param>
        public TranslationLoadException(string Message)
            : base(Message)
        {
        }

        /// <summary>
        /// Creates a new instance of the TranslationLoadException
        /// using the default exception message and the specified inner exception.
        /// </summary>
        /// <param name="InnerException">The inner exception to report.</param>
        public TranslationLoadException(Exception InnerException)
            : base(DefaultExceptionMessage, InnerException)
        {
        }
    }
}
