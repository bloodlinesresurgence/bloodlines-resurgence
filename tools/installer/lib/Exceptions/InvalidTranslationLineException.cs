using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resurgence
{
    /// <summary>
    /// Occurs when a translation line is being parsed and
    /// found to be invalid.
    /// </summary>
    public class InvalidTranslationLineException
        : Exception
    {
        /// <summary>
        /// Default exception message to report.
        /// </summary>
        protected const string DefaultExceptionMessage
            = "Invalid translation line found.";

        /// <summary>
        /// Creates a new instance of the InvalidTranslationLineException
        /// using the default exception message.
        /// </summary>
        public InvalidTranslationLineException()
            : base(DefaultExceptionMessage)
        {
        }

        /// <summary>
        /// Creates a new instance of the InvalidTranslationLineException
        /// using the specified exception message.
        /// </summary>
        /// <param name="Message">Exception message to report.</param>
        public InvalidTranslationLineException(string Message)
            : base(Message)
        {
        }

        /// <summary>
        /// Creates a new instance of the InvalidTranslationLineException
        /// using the default exception message and the specified inner exception.
        /// </summary>
        /// <param name="InnerException">The inner exception to report.</param>
        public InvalidTranslationLineException(Exception InnerException)
            : base(DefaultExceptionMessage, InnerException)
        {
        }
    }
}
