using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ResurgenceLib.Generics
{
    /// <summary>
    /// Provides a named set of type-specific entries, identified via a key.
    /// </summary>
    public class HashList<t>
        : Hashtable
    {
        /// <summary>
        /// Creates a new instance of the HashList class.
        /// </summary>
        public HashList()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the object via the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new t this[object key]
        {
            get
            {
                return (t)base[key];
            }
            set
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// Gets the specified object from the base (Hashtable) object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected object baseGet(object key)
        {
            return base[key];
        }
    }
}
