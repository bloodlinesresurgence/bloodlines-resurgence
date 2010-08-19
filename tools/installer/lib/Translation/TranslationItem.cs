using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resurgence.Translation
{
    /// <summary>
    /// A translation item, with owner and text contents.
    /// </summary>
    public class TranslationItem
    {
        /// <summary>
        /// The name of the owner object (eg, form or custom control.)
        /// </summary>
        public readonly string Owner;

        /// <summary>
        /// The name of the item to translate.
        /// </summary>
        public readonly string Object;

        /// <summary>
        /// The translated text.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Creates a new instance of the TranslationItem object.
        /// </summary>
        /// <param name="Item_Owner">The name of the owner object (eg, form or custom control.)</param>
        /// <param name="Item_Object">The name of the item to translate.</param>
        /// <param name="Item_Text">The translated text.</param>
        public TranslationItem(string Item_Owner, string Item_Object, string Item_Text)
        {
            Owner = Item_Owner;
            Object = Item_Object;
            Text = Item_Text;
        }

        /// <summary>
        /// A value indicating the translation item was not found.
        /// </summary>
        public static TranslationItem NotFound = new TranslationItem(null, null, null);

        /// <summary>
        /// Gets the key for the current item.
        /// </summary>
        public string Key
        {
            get
            {
                if (Owner != null)
                    return String.Format("{0}.{1}", Owner, Object);
                else
                    return Object;
            }
        }

        /// <summary>
        /// Parses and creates a new instance of the TranslationItem object.
        /// </summary>
        /// <param name="Owner">Owner object.</param>
        /// <param name="Line">Line to parse.</param>
        /// <returns>A new instance of TranslationItem containing the parsed object.</returns>
        public static TranslationItem Parse(string Owner, string Line)
        {
            // Format of a translation line:
            // [Owner]
            // Name = Text

            int assignmentPosition = Line.IndexOf("=");
            if (assignmentPosition == -1)           // Not found
                throw new InvalidTranslationLineException();

            TranslationItem translationItem;

            // Get parts
            try
            {
                string objectName = Line.Substring(0, assignmentPosition - 1).Trim();

                // Constant
                if (Owner == null && objectName.StartsWith("!"))
                    objectName = objectName.Substring(1);

                string objectText = Line.Substring(assignmentPosition + 1).Trim();

                translationItem = new TranslationItem(Owner, objectName, objectText);
            }
            catch(Exception ex)
            {
                throw new InvalidTranslationLineException(ex);
            }

            return translationItem;
        }
    }
}
