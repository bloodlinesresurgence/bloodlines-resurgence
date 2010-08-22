using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib
{
    /// <summary>
    /// Base class for Bloodlines Resurgence tools.
    /// </summary>
    public abstract class ResurgenceTool
        : Exported
    {
        /// <summary>
        /// Program-wide options.
        /// </summary>
        protected GlobalOptions GlobalOptions;

        /// <summary>
        /// Initializes a new instance of the ResurgenceTool base class.
        /// </summary>
        /// <param name="Name">The name of the tool.</param>
        /// <param name="Description">A description for the tool.</param>
        public ResurgenceTool(string Name, string Description)
            : base(Name, Description)
        {
        }

        /// <summary>
        /// Sets the options for the module.
        /// </summary>
        /// <param name="Options">Program-wide options to give.</param>
        public void SetOptions(GlobalOptions Options)
        {
            GlobalOptions = Options;
        }

        /// <summary>
        /// Starts the tool.
        /// </summary>
        public abstract void Start();
    }
}
