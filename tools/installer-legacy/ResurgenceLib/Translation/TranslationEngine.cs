using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib.Generics;
using System.Diagnostics;

namespace ResurgenceLib.Translation
{
    /// <summary>
    /// Performs translation actions on UI elements.
    /// </summary>
    public class TranslationEngine
    {
        /// <summary>
        /// The control we started from.
        /// </summary>
        protected Control parentControl;

        /// <summary>
        /// Translation objects to use.
        /// </summary>
        protected TranslationCollection Translations;

        /// <summary>
        /// Specifies the current parent to read translations from.
        /// </summary>
        public string CurrentParent = null;

        /// <summary>
        /// Creates a new instance of the TranslationEngine class.
        /// </summary>
        /// <param name="TranslationCollection">Translation to use.</param>
        public TranslationEngine(TranslationCollection TranslationCollection)
        {
            Translations = TranslationCollection;
        }

        /// <summary>
        /// Creates a new instance of the TranslationEngine class with
        /// the specified target control.
        /// </summary>
        /// <param name="TranslationCollection">Translations to use.</param>
        /// <param name="Target"></param>
        public TranslationEngine(TranslationCollection TranslationCollection, Control Target)
        {
            Translations = TranslationCollection;
            parentControl = Target;
        }

        /// <summary>
        /// Performs translation on the current control.
        /// </summary>
        public void Translate()
        {
            if(parentControl != null)
                Translate(parentControl);
        }

        /// <summary>
        /// Performs translation on the given control.
        /// </summary>
        /// <param name="Target">Control to translate.</param>
        public void Translate(Control Target)
        {
            translateControl(Target);

            if (Assemblies.IsType(Target, typeof(Form)))
            {
                // Find special form constants
                TranslationItem item = Translations.Constants.FindTranslationItem(Target.Name, "!Title");
                if (item != TranslationItem.NotFound)
                    (Target as Form).Text = item.Text;
            }
        }

        /// <summary>
        /// Performs recursive translation on the control.
        /// </summary>
        /// <param name="Target">Control to translate.</param>
        private void translateControl(Control Target)
        {
            // Recurse!
            // Run through each subcontrol and translate.
            foreach (Control subControl in Target.Controls)
                translateControl(subControl);

            // Don't match Textbox objects
            if (Assemblies.IsType(Target, typeof(TextBox))) return;

            // Don't match Form objects (these are handled seperately)
            if (Assemblies.IsType(Target, typeof(Form))) return;

            string owner;
            string name;
            TranslationItemList list;

            if (Target.Text.StartsWith("#") && Target.Text.EndsWith("#"))
            {
                // Items surrounded in # (eg, #Browse#) are replaced using globals.
                if (CurrentParent == null)
                {
                    owner = Translation.GlobalId;
                }
                else
                {
                    owner = CurrentParent;
                }

                name = "!" + Target.Text.Substring(1, Target.Text.Length - 2);
                list = Translations.Constants;
            }
            else
            {
                owner = topMostOwner(Target);
                name = Target.Name;
                list = Translations.Controls;
            }
            
            TranslationItem translationItem = list.FindTranslationItem(owner, name);

            if (translationItem == TranslationItem.NotFound)
            {
                // Check globals
                owner = Translation.GlobalId;
                translationItem = list.FindTranslationItem(owner, name);
            }

            if (translationItem != TranslationItem.NotFound)
            {
                // Translation found!
                Target.Text = translationItem.Text;
            }
#if false
            else
            {
                // Not found :(
                // Check constants
                translationItem = Translations.Constants.FindTranslationItem(null, Target.Text);
                if (translationItem != TranslationItem.NotFound)
                    Target.Text = translationItem.Text;
            }
#endif
        }

        /// <summary>
        /// Finds the topmost owner of the given control.
        /// </summary>
        /// <param name="Target">Control to find the topmost owner of.</param>
        /// <returns>The name of the topmost owner of the control.</returns>
        private string topMostOwner(Control Target)
        {
            // Note: An Owner is only considered to be a Form type

            Control ownerControl = findForm(Target);
            if (ownerControl == null)
                ownerControl = findUserControl(Target);

            return ownerControl.Name;
        }

        /// <summary>
        /// Finds the form that owns the specified control.
        /// </summary>
        /// <param name="control">Control to find the owner of.</param>
        /// <returns>The form that owns the control.</returns>
        private Control findForm(Control control)
        {
            return findControl(control, typeof(Form));
        }

        /// <summary>
        /// Finds the UserControl that owns the specified control.
        /// </summary>
        /// <param name="control">Control to find the owner of.</param>
        /// <returns>The UserControl that owns the control.</returns>
        private Control findUserControl(Control control)
        {
            return findControl(control, typeof(UserControl));
        }

        /// <summary>
        /// Finds the control that owns the specified control, and is of
        /// the given type.
        /// </summary>
        /// <param name="target">Control to find the owner of.</param>
        /// <param name="type">The type of control to find.</param>
        /// <returns>The control that owns the target.</returns>
        private Control findControl(Control target, Type type)
        {
            if (Assemblies.IsType(target, type))
                return target;

            if (target.Parent == null)
                return null;

            return findControl(target.Parent, type);
        }

        /// <summary>
        /// Translates the given constant and returns the definition.
        /// </summary>
        /// <param name="Constant"></param>
        /// <returns></returns>
        internal string TranslateConstant(string Constant)
        {
            string parent;
            if (CurrentParent == null)
                parent = Translation.GlobalId;
            else
                parent = CurrentParent;

            string key = parent + "." + Constant;
            TranslationItem item = Translations.Constants[key];
            if (item == null)
            {
                Debug.Print("Missing constant: " + key);
                return String.Format("!!Missing: {0}", key);
            }

            return item.Text;
        }
    }
}
