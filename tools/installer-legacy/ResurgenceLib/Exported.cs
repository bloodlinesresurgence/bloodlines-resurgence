using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides an interface for exported symbols.
    /// </summary>
    public abstract class Exported
    {
        /// <summary>
        /// The name of the object.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The description of the object.
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// Creates a new instance of the exported object.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="description">Description of the object.</param>
        protected Exported(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Gets the assembly's Company attribute.
        /// </summary>
        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        /// <summary>
        /// Gets the assembly's Description attribute.
        /// </summary>
        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the assembly's Title attribute.
        /// </summary>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets the assembly's Version attribute.
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private static string getHierachy(Type type)
        {
            if (type == typeof(Exported) || type == null)
                return "";

            return type.Name + "." + getHierachy(type.BaseType);
        }

        /// <summary>
        /// Determines whether the given target object is of the given type,
        /// going back through the hierachy of it's base classes.
        /// </summary>
        /// <param name="target">Object to test.</param>
        /// <param name="type">Type to match to.</param>
        /// <returns>True if the given type is found within the object hierachy.</returns>
        public static bool IsType(object target, Type type)
        {
            if (target == null)
                return false;

            if (target.GetType() == type)
                return true;

            return CompareHierachichalType(target.GetType().BaseType, type);
        }

        /// <summary>
        /// Determines whether the given type matches the target type,
        /// going back through the hierachy of the base classes.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        private static bool CompareHierachichalType(Type source, Type compareTo)
        {
            if (source == compareTo)
                return true;

            if (source.BaseType == null)
                return false;

            return CompareHierachichalType(source.BaseType, compareTo);
        }

        /// <summary>
        /// Determines whether the object is of the given type,
        /// going back through the hieracy of it's base classes.
        /// </summary>
        /// <param name="type">Type to match to.</param>
        /// <returns>True if the given type is found within the object hierachy.</returns>
        public bool IsType(Type type)
        {
            return Exported.IsType(this, type);
        }
    }
}
