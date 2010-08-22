using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using RevivalLib;

namespace RevivalTools
{
    /// <summary>
    /// Provides a method for storing and retreiving module information.
    /// </summary>
    internal abstract class InternalFileInfo
    {
        /// <summary>
        /// Path to module.
        /// </summary>
        internal string filePath;

        /// <summary>
        /// Name to display.
        /// </summary>
        protected string displayName;

        /// <summary>
        /// Description to display.
        /// </summary>
        protected string displayDescription;

        /// <summary>
        /// Special constructor used for generics.
        /// </summary>
        internal InternalFileInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of the InternalFileInfo class.
        /// </summary>
        /// <param name="path">The file to provide information for.</param>
        internal InternalFileInfo(string path)
        {
            FileInfo info = new FileInfo(path);

            filePath = info.FullName;
            if (info.Extension.Length > 0)
                displayName = info.Name.Replace(info.Extension, "");
            else
                displayName = info.Name;
            displayDescription = "N/A";

            getInformation();
        }

        /// <summary>
        /// Creates a new instance of the InternalFileInfo class.
        /// </summary>
        /// <param name="path">The file to provide information for.</param>
        /// <param name="callGetInformation">When false, does not call the getInformation method.</param>
        protected InternalFileInfo(string path, bool callGetInformation)
        {
            filePath = new FileInfo(path).FullName;
            if (callGetInformation == true)
                getInformation();
        }

        /// <summary>
        /// Retreives information for the file.
        /// </summary>
        protected virtual void getInformation()
        {
        }

        /// <summary>
        /// Gets the display name for the file.
        /// </summary>
        internal string DisplayName
        {
            get { return displayName; }
        }

        /// <summary>
        /// Gets the display description for the file.
        /// </summary>
        internal string DisplayDescription
        {
            get { return displayDescription; }
        }

        /// <summary>
        /// Gets the path to the module.
        /// </summary>
        internal string FilePath
        {
            get { return filePath; }
        }

        /// <summary>
        /// Gets the value indicating whether this is a valid module.
        /// </summary>
        internal abstract bool IsValid
        {
            get;
        }

        /// <summary>
        /// Represents the the object as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return displayName;
        }
    }

    /// <summary>
    /// Provides a base class for information on assembly-based modules.
    /// </summary>
    internal class AssemblyInfo
        : InternalFileInfo
    {
        /// <summary>
        /// Stores information about the assembly.
        /// </summary>
        protected Assembly Assembly;

        /// <summary>
        /// Stores the exposed type of the assembly.
        /// </summary>
        protected Type AssemblyType;

        /// <summary>
        /// Special constructor for generics.
        /// </summary>
        public AssemblyInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of the AssemblyInfo class.
        /// </summary>
        /// <param name="path">Path to file.</param>
        public AssemblyInfo(string path)
            : base(path, false)
        {
        }

        /// <summary>
        /// Gets the value indicating whether the module is valid. Should be overridden.
        /// </summary>
        internal override bool IsValid
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Late-initialization, used by generics.
        /// </summary>
        /// <param name="path">Path to file.</param>
        internal void genericInit(string path)
        {
            filePath = new FileInfo(path).FullName;
            Assembly = Assembly.LoadFile(filePath);
            getInformation();
        }

        /// <summary>
        /// Gets the exposed type of the assembly object.
        /// </summary>
        internal Type Type
        {
            get { return AssemblyType; }
        }

        /// <summary>
        /// Retreives a list of modules from the specified file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>A list of AssemblyInfo generic derived objects, converted to object.</returns>
        internal virtual object[] GetModules(string file) { throw new NotImplementedException(); }

        /// <summary>
        /// Retreives the AssemblyObject being used.
        /// </summary>
        /// <returns></returns>
        public virtual object GetAssemblyObject() { return null; }

        /// <summary>
        /// Gets the assembly's Company attribute.
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
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
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
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
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.CodeBase);
            }
        }

        /// <summary>
        /// Gets the assembly's Version attribute.
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the type hierachy of the object.
        /// </summary>
        public string AssemblyHierachy
        {
            get
            {
                return getHierachy(Type);
            }
        }

        private string getHierachy(Type type)
        {
            if (type == typeof(Exported))
                return "";

            return type.Name + "." + getHierachy(type.BaseType);
        }
    }

    /// <summary>
    /// Provides a base class for retreiving information and loading assembly (.DLL) objects.
    /// </summary>
    /// <typeparam name="t">Class of the assembly.</typeparam>
    internal class AssemblyInfo<t>
        : AssemblyInfo
    {
        /// <summary>
        /// Exposes the entire framework object.
        /// </summary>
        public t AssemblyObject = default(t);

        /// <summary>
        /// Special constructor for generics.
        /// </summary>
        protected AssemblyInfo()
        {
        }

        public override object GetAssemblyObject()
        {
            return AssemblyObject;
        }

        /// <summary>
        /// Creates a new instance of the AssemblyInfo class.
        /// </summary>
        /// <param name="path">Path to file.</param>
        protected AssemblyInfo(string path)
            : base(path)
        {
            Assembly = Assembly.LoadFile(filePath);
            getInformation();
        }

        /// <summary>
        /// Creates a new instance of the AssemblyInfo class using the specified
        /// class type.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <param name="type">Type to use.</param>
        protected AssemblyInfo(string path, Type type)
            : base(path)
        {
            AssemblyType = type;
            Assembly = type.Assembly;
            AssemblyObject = (t)Activator.CreateInstance(type);
            getFrameworkInformation();
        }

        /// <summary>
        /// Retreives the information for the loaded module.
        /// </summary>
        protected override void getInformation()
        {
            Type[] types = Assembly.GetExportedTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == null)
                    continue;
                // Test if it is a BrainWork class
                if (type.BaseType.Equals(typeof(t)))
                {
                    // Yes, load it
                    AssemblyType = type;
                    AssemblyObject = (t)Activator.CreateInstance(type);
                    getFrameworkInformation();

                    return;
                }
            }

            // Not found
            displayName = "(Invalid library)";
            displayDescription = "(Could not locate an object with the required framework)";
        }

        /// <summary>
        /// Creates an array of Type objects containing the different exported types
        /// the given assembly.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        internal Type[] GetTypes(string file)
        {
            List<Type> types = new List<Type>();

            // Fix up path, as .LoadFile requires full path
            FileInfo info = new FileInfo(file);

            Assembly = Assembly.LoadFile(info.FullName);
            Type[] internalTypes = Assembly.GetExportedTypes();
            foreach (Type type in internalTypes)
            {
                // Determine if it is a valid type
                if (IsValidType(type))
                {
                    // Yes, add it
                    types.Add(type);
                }
            }

            return types.ToArray();
        }

        /// <summary>
        /// Recursively determines if the given Type object has the correct BaseType by
        /// checking every BaseType of the hierachy.
        /// </summary>
        /// <param name="type">Current type to examine.</param>
        /// <returns>True if a match is found, false if not.</returns>
        protected bool IsValidType(Type type)
        {
            if (type.BaseType == null && type.Equals(typeof(t)) == false)
                return false;

            if (type.BaseType.Equals(typeof(t)) || type.Equals(typeof(t)))
                return true;

            return IsValidType(type.BaseType);
        }

        /// <summary>
        /// Retreives the specified information from the loaded module.
        /// </summary>
        protected void getFrameworkInformation()
        {
            // This doesn't look pretty, but C# doesn't like converting a generic to a class.
            // Workaround is to convert it to object then to desired class.
            displayName = ((Exported)(object)AssemblyObject).Name;
            displayDescription = ((Exported)(object)AssemblyObject).Description;
            Program.LoadedAssemblyModules.Add(this);
        }

        /// <summary>
        /// Gets the value indicating whether the module is valid.
        /// </summary>
        internal override bool IsValid
        {
            get { return (AssemblyObject != null); }
        }

        /// <summary>
        /// Creates an instance of the current type.
        /// </summary>
        /// <returns></returns>
        internal t CreateInstance()
        {
            object obj = Activator.CreateInstance(this.Type);
            return (t)obj;
        }
    }

    /// <summary>
    /// Provides a method of obtaining information from RevivalTool modules.
    /// </summary>
    internal class ToolInfo
        : AssemblyInfo<RevivalLib.RevivalTool>
    {
        /// <summary>
        /// Generic constructor used by generics.
        /// </summary>
        public ToolInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of the ModuleInfo class.
        /// </summary>
        /// <param name="path">The module to provide information for.</param>
        public ToolInfo(string path)
            : base(path)
        {
        }

        public ToolInfo(string path, Type type)
            : base(path, type)
        {
        }

        internal override object[] GetModules(string file)
        {
            List<object> modules = new List<object>();

            foreach (Type type in GetTypes(file))
            {
                // Create new instance of type
                ToolInfo info = new ToolInfo(file, type);
                modules.Add(info);
            }

            return modules.ToArray();
        }
    }
}
