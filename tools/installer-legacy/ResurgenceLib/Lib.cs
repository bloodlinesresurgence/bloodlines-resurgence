using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides various library-wide settings.
    /// </summary>
    public static class Lib
    {
        /// <summary>
        /// Provides communication between the library and the hosting application.
        /// </summary>
        private static ILibCommunications Communications = null;

        /// <summary>
        /// Gets the current ILibCommunications object.
        /// </summary>
        internal static ILibCommunications CommunicationsObject
        {
            get
            {
                if(Communications == null)
                    throw new Exceptions.LibCommunicationsNotSetException();
                return Communications;
            }
        }

        /// <summary>
        /// Sets the library communications object.
        /// </summary>
        /// <param name="Obj"></param>
        public static void SetCommunicationsObject(ILibCommunications Obj)
        {
            Communications = Obj;
        }
    }
}
