using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Resurgence
{
    /// <summary>
    /// Provides a named set of type-specific entries, identified via a key.
    /// </summary>
    /// <typeparam name="t"></typeparam>
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
        /// Gets the specified object from the base object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected object baseGet(object key)
        {
            return base[key];
        }

        /// <summary>
        /// Convert the element values to a flat list
        /// </summary>
        /// <returns></returns>
        public List<t> ToList()
        {
            List<t> list = new List<t>(this.Count);
            foreach(DictionaryEntry de in this) {
                list.Add((t)de.Value);
            }
            return list;
        }
    }
}
