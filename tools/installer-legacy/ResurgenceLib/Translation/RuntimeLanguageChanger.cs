using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RevivalLib.Translation
{
    /// <summary>
    /// Provides a changer to record all applicable details for
    /// a form before re-applying the language, and restore those details
    /// afterwards.
    /// </summary>
    internal class RuntimeLanguageChanger
    {
        /// <summary>
        /// Collection of controls contained on the form.
        /// </summary>
        /// <remarks>This will be cloned from the target form.</remarks>
        Control[] collection;

        /// <summary>
        /// The target form.
        /// </summary>
        Control TargetControl;

        /// <summary>
        /// Creates a new instance of the RuntimeLanguageChanger class.
        /// </summary>
        /// <param name="Target">The target control which is to be re-translated.</param>
        internal RuntimeLanguageChanger(Control Target)
        {
            TargetControl = Target;
            List<Control> controlList = new List<Control>();

            controlList.AddRange(Recurse_GetSupportedControls(Target));

            collection = controlList.ToArray();
        }

        private Control[] Recurse_GetSupportedControls(Control Target)
        {
            List<Control> controls = new List<Control>();

            if (IsSupportedType(Target))
                controls.Add(Target);

            foreach (Control child in Target.Controls)
                controls.AddRange(Recurse_GetSupportedControls(child));

            return controls.ToArray();
        }

        private bool IsSupportedType(Control control)
        {
            Type type = control.GetType();
            return (type == typeof(TextBox) ||
                    type == typeof(CheckBox));
        }

        /// <summary>
        /// Re-applies the copied values of various controls to the original target form.
        /// </summary>
        internal void Reapply_Values()
        {
            foreach (Control control in collection)
                Recursive_ApplyValues(control);
        }

        private void Recursive_ApplyValues(Control control)
        {
            // Apply to control
            Type type = control.GetType();
            Control target = TargetControl.Controls[control.Name];

            if(target != null)
            {
                if (type == typeof(TextBox))
                    (target as TextBox).Text = control.Text;
                else if (type == typeof(CheckBox))
                    (target as CheckBox).CheckState = (control as CheckBox).CheckState;
            }
                
            // Apply to children
            foreach (Control child in control.Controls)
                Recursive_ApplyValues(child);
        }
    }
}
