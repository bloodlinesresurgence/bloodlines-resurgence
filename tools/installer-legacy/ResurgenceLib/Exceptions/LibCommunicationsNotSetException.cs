using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib.Exceptions
{
    /// <summary>
    /// Thrown when the LibCommunications object is not set.
    /// </summary>
    public class LibCommunicationsNotSetException
        : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        public LibCommunicationsNotSetException()
            : base("Lib Communications object not set!")
        { }
    }
}
