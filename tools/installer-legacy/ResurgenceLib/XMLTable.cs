using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using System.Reflection;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides an XML table - a hashtable stored to file in XML format.
    /// </summary>
    public class XMLTable
        : Hashtable
    {
        /// <summary>
        /// The last known filename.
        /// </summary>
        private string Filename = null;

        /// <summary>
        /// Whether to save each time a value is updated.
        /// </summary>
        public bool SaveOnUpdate = true;

        private const string RootNode = "XMLTable";
        private const string SaveOnUpdateNode = "SaveOnUpdate";
        private const string IsNull = "IsNull";
        private const double mOldestVersionSupported = 0.012f;

        /// <summary>
        /// Maps the oldest supported version number to a double.
        /// </summary>
        private static double OldestSupported
        {
            get
            {
                return Math.Round(mOldestVersionSupported, 5);
            }
        }
        /// <summary>
        /// Maps the oldest supported version number to a string.
        /// </summary>
        private static string VersionString
        {
            get
            {
                return OldestSupported.ToString();
            }
        }

        private const string ItemsNode = "Items";
        private const string InformationNode = "TableInformation";

        /// <summary>
        /// Creates a new instance of the XMLTable class.
        /// </summary>
        public XMLTable()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the value that corresponds to the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override object this[object key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;

                if (SaveOnUpdate && Filename != null)
                    Save();
            }
        }

        /// <summary>
        /// Saves the current table to the last known filename.
        /// </summary>
        /// <exception cref="MissingFieldException">Thrown if filename is not set.</exception>
        public void Save()
        {
            Save(Filename);
        }

        /// <summary>
        /// Saves the current table to the given filename.
        /// </summary>
        /// <param name="path">Path to save to.</param>
        /// <exception cref="MissingFieldException">Thrown if filename is not set.</exception>
        public void Save(string path)
        {
            if (path == null)
                throw new MissingFieldException("Missing Save path"); ;

            XmlTextWriter tw = new XmlTextWriter(path, Encoding.Unicode);
            tw.Formatting = Formatting.Indented;

            tw.WriteComment(this.GetType().ToString() + ", saved on " + System.DateTime.Now.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString());
            tw.WriteStartElement(RootNode);
            tw.WriteAttributeString("Version", VersionString);
            tw.WriteAttributeString(SaveOnUpdateNode, SaveOnUpdate.ToString());

            // Stage one - write items list
            tw.WriteStartElement(ItemsNode);
            foreach (DictionaryEntry de in this)
            {
                tw.WriteStartElement(de.Key.ToString());
                if (de.Value != null)
                {
                    Type type = de.Value.GetType();
                    Assembly assembly = Assembly.GetAssembly(type);
                    TypeConverter converter = TypeDescriptor.GetConverter(type);
                    if (converter.CanConvertTo(typeof(string)))
                    {
                        string converted = converter.ConvertToString(de.Value);
                        tw.WriteValue(converted);
                    }
                }
                else
                {
                    tw.WriteAttributeString(IsNull, "true");
                }
                tw.WriteEndElement();
            }
            tw.WriteEndElement();

            // Stage two - write element information list
            tw.WriteStartElement(InformationNode);
            foreach (DictionaryEntry de in this)
            {
                if (de.Value == null)
                    continue;
                tw.WriteStartElement(de.Key.ToString());
                Type type = de.Value.GetType();
                Assembly assembly = Assembly.GetAssembly(type);
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                tw.WriteAttributeString("Type", type.ToString());
                tw.WriteAttributeString("Assembly", assembly.ToString());
                tw.WriteEndElement();
            }
            tw.WriteEndElement();

            // End root node
            tw.WriteEndElement();
            tw.Close();

            Filename = path;
        }

        /// <summary>
        /// Loads the given XMLTable file.
        /// </summary>
        /// <param name="path">Path to load from.</param>
        /// <returns>An initialized XMLTable class with the loaded contents.</returns>
        /// <exception cref="InvalidXMLTableFileException">
        /// Thrown if the XMLTable file is not in a supported format or invalid.
        /// </exception>
        public static XMLTable Load(string path)
        {
            XMLTable loaded = new XMLTable();
            loaded.Filename = path;
            loaded.SaveOnUpdate = false;

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode root = doc.SelectSingleNode(RootNode);
            if (root == null)
                throw new InvalidXMLTableFileException();

            double version = -1f;
            if (root.Attributes["Version"] != null)
            {
                if (double.TryParse(root.Attributes["Version"].Value, out version) == false)
                    version = -1f;
            }

            if (version < OldestSupported)
                throw new InvalidXMLTableFileException();

            XmlNode itemsNode = root.SelectSingleNode(ItemsNode);
            XmlNode informationNode = root.SelectSingleNode(InformationNode);

            List<string> invalidItems = new List<string>();
            foreach (XmlNode item in itemsNode.ChildNodes)
            {
                string name = item.Name;
                if (item.Attributes[IsNull] != null)
                    loaded[name] = null;
                else
                {
                    try
                    {
                        // The following uses reflection to get the type of the item,
                        // load the relevant assembly, and then to run a converter over it.
                        // Any errors will be reported after loading is complete.
                        XmlNode currentInfoNode = informationNode.SelectSingleNode(name);
                        string typeName = currentInfoNode.Attributes["Type"].Value;
                        string assemblyName = currentInfoNode.Attributes["Assembly"].Value;

                        Assembly assembly = Assembly.Load(assemblyName);
                        Type type = assembly.GetType(typeName);
                        if (type == null)
                            throw new InvalidOperationException("Unable to load specified type " + typeName);
                        TypeConverter converter = TypeDescriptor.GetConverter(type);
                        if (converter.CanConvertFrom(typeof(string)))
                            loaded[name] = converter.ConvertFrom(item.InnerText);
                        else
                            invalidItems.Add(name + ": Cannot convert from string");
                    }
                    catch (Exception ex)
                    {
                        invalidItems.Add(name + ": " + ex.Message);
                    }
                }
            }

            if (root.Attributes[SaveOnUpdateNode] != null)
                bool.TryParse(root.Attributes[SaveOnUpdateNode].Value, out loaded.SaveOnUpdate);

            if (invalidItems.Count > 0)
            {
                string seperator = Environment.NewLine + Environment.NewLine;
                string joined = " " + String.Join(seperator + " ", invalidItems.ToArray());
                System.Windows.Forms.MessageBox.Show("The following items could not be loaded from the XMLTable file:" + seperator + joined,
                    "XMLTable.Load", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            return loaded;
        }
    }

    /// <summary>
    /// Thrown if the XMLTable file is not in a supported format or invalid.
    /// </summary>
    public class InvalidXMLTableFileException
        : Exception
    {
        /// <summary>
        /// Creates a new instance of the InvalidXMLTableFileException.
        /// </summary>
        public InvalidXMLTableFileException()
            : base("The specified file is not a valid XMLTable file.")
        { }
    }
}
