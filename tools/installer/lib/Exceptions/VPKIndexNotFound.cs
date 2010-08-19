using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resurgence.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the VPKIndex file is not found.
    /// Use the Vextract.CreateIndex function to create it.
    /// </summary>
    public class VPKIndexNotFound
        : System.IO.FileNotFoundException
    {
        /// <summary>
        /// Creates a new instance of the VPKIndexNotFound exception with the
        /// default error message.
        /// </summary>
        public VPKIndexNotFound()
            : base("VPK Index not found")
        {
        }
    }
}
