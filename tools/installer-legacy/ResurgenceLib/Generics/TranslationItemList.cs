using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResurgenceLib.Translation;

namespace ResurgenceLib.Generics
{
    /// <summary>
    /// Provides keyed list of TranslationItem's.
    /// </summary>
    public class TranslationItemList
        : HashList<TranslationItem>
    {
        /// <summary>
        /// Creates a new instance of the TranslationItemList class.
        /// </summary>
        public TranslationItemList()
            : base()
        {
        }

        /// <summary>
        /// Attempts to find the a TranslationItem match to the given object details.
        /// </summary>
        /// <param name="Owner">Owner object.</param>
        /// <param name="Name">Name of the object</param>
        /// <returns>The item found, or null if not found.</returns>
        public TranslationItem FindTranslationItem(string Owner, string Name)
        {
            foreach (TranslationItem translationItem in base.Values)
            {
                if (translationItem.Owner.Equals(Owner, StringComparison.OrdinalIgnoreCase) &&
                    translationItem.Object.Equals(Name, StringComparison.OrdinalIgnoreCase))
                {
                    // Found it!
                    return translationItem;
                }
            }

            // Not found.
            return TranslationItem.NotFound;
        }
    }
}
