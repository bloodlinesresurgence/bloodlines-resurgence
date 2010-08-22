using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResurgenceLib.Generics;

namespace ResurgenceLib.Translation
{
    /// <summary>
    /// Provides storage for two type of TranslationItem collections:
    /// Constants, and Controls
    /// </summary>
    public struct TranslationCollection
    {
        /// <summary>
        /// List of constants.
        /// </summary>
        public TranslationItemList Constants;

        /// <summary>
        /// List of controls.
        /// </summary>
        public TranslationItemList Controls;

        /// <summary>
        /// Creates a new instance of the TranslationCollection class.
        /// </summary>
        public TranslationCollection(TranslationItemList ConstantsList,
            TranslationItemList ControlsList)
        {
            Constants = ConstantsList;
            Controls = ControlsList;
        }

        /// <summary>
        /// Generates a global name for the variable, adding appropriate
        /// prefixes.
        /// </summary>
        /// <param name="name">Name to globalenise.</param>
        /// <returns></returns>
        internal string GlobalName(string name)
        {
            return "!Globals.!" + name;
        }
    }
}
