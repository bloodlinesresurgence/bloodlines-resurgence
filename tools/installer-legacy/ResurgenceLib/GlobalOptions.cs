using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides options that are made available to all modules.
    /// This class can be serialized.
    /// </summary>
    [Serializable]
    public class GlobalOptions
    {
        /// <summary>
        /// The options to provide.
        /// </summary>
        protected Hashtable options;

        /// <summary>
        /// Initializes a new instance of the GlobalOptions class.
        /// The list of options will be empty.
        /// </summary>
        public GlobalOptions()
        {
            options = new Hashtable();
        }

        /// <summary>
        /// Gets or sets the option for the specified key.
        /// </summary>
        /// <param name="key">Option key (or name).</param>
        /// <returns></returns>
        public object this[object key]
        {
            get { return options[key]; }
            set { options[key] = value; }
        }
    }
}
