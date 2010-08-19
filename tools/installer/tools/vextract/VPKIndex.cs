using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaedalusLib;
using System.Collections;
using System.IO;

namespace Resurgence.tools.vextract
{
    /// <summary>
    /// Provides indexing for a number of VPK files.
    /// </summary>
    [Serializable]
    public class VPKIndex
        : BaseSerializable, IEnumerable
    {
        /// <summary>
        /// Array containing the vpk index entries.
        /// </summary>
        protected VPKIndexEntry[] vpkentries;

        /// <summary>
        /// Creates a new instance of the VPKIndex class and creates
        /// an index of all .vpk files found in the specified directory.
        /// </summary>
        /// <param name="VPKDirectory">Directory that contains the .vpk files to index.</param>
        internal VPKIndex(string VPKDirectory)
            : base(FormatterType.Binary)
        {
            List<VPKIndexEntry> vpkIndexEntries = new List<VPKIndexEntry>();
            string[] vpkFiles = Directory.GetFiles(VPKDirectory, "*.vpk", SearchOption.TopDirectoryOnly);

            foreach (string currentVPKFile in vpkFiles)
            {
                VPKFile VPKFile = new VPKFile(currentVPKFile);
                DirectoryEntry[] entries = VPKFile.GetEntries();
                foreach (DirectoryEntry entry in entries)
                {
                    vpkIndexEntries.Add(new VPKIndexEntry(entry, currentVPKFile));
                }
            }

            vpkentries = vpkIndexEntries.ToArray();

            Save();
        }

        /// <summary>
        /// Finds all files that match the specified file pattern.
        /// </summary>
        /// <param name="Pattern">Pattern to match to. (Eg, materials/*, *.wav)</param>
        /// <param name="ExemptionPatterns">A string array containing exemption patterns.</param>
        /// <returns></returns>
        public VPKIndexEntry[] FindFiles(string Pattern, string[] ExemptionPatterns)
        {
            List<VPKIndexEntry> foundFiles = new List<VPKIndexEntry>();

            foreach (VPKIndexEntry currentEntry in this)
            {
                if (currentEntry.IsMatch(Pattern) == true)
                {
                    bool exemptionMatch = false;
                    if (ExemptionPatterns != null)
                    {
                        foreach (string exemption in ExemptionPatterns)
                            if (currentEntry.IsMatch(exemption))
                            {
                                exemptionMatch = true;
                                break; /* breaks foreach (string exemption... */
                            }
                    }

                    if(exemptionMatch == false)
                        foundFiles.Add(currentEntry);
                }
            }

            return foundFiles.ToArray();
        }

        /// <summary>
        /// Serializes the VPKIndex class to file.
        /// </summary>
        private void Save()
        {
            ChildObjectRef = this;
            base.__Serialize(VPKIndexLocation);
        }

        /// <summary>
        /// Gets the location to store the VPKIndex file.
        /// </summary>
        internal static string VPKIndexLocation
        {
            get
            {
                return Info.ApplicationData + "\\vpkindex.bin";
            }
        }

        /// <summary>
        /// Checks whether the VPK index file exists.
        /// </summary>
        public static bool Exists
        {
            get
            {
                return File.Exists(VPKIndexLocation);
            }
        }

        /// <summary>
        /// Loads the VPKIndex from file.
        /// </summary>
        /// <returns>The loaded VPKIndex object.</returns>
        /// <exception cref="FileNotFoundException" />
        public static VPKIndex Load()
        {
            if(File.Exists(VPKIndexLocation) == false)
                throw new FileNotFoundException();

            // Attempt to load it
            try
            {
                VPKIndex index = (VPKIndex)VPKIndex._Deserialize(VPKIndexLocation, FormatterType.Binary);
                return index;
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Gets the number of VPKIndexEntry entries contained in the index.
        /// </summary>
        public int Count
        {
            get { return vpkentries.Length; }
        }

        /// <summary>
        /// Gets the VPKIndexEntry object from the given index.
        /// </summary>
        /// <param name="index">The zero-based index to retreive the entry from.</param>
        /// <returns>VPKIndexEntry.InvalidEntry if the index is invalid or does not exist.</returns>
        public VPKIndexEntry this[int index]
        {
            get
            {
                if (index < 0 || index > vpkentries.Length)
                    return VPKIndexEntry.InvalidEntry;

                return vpkentries[index];
            }
        }

        #region IEnumerable Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return new VPKIndexEnumerator(this);
        }

        /// <summary>
        /// Provides an enumerator for the VPKIndex class.
        /// </summary>
        public class VPKIndexEnumerator
            : IEnumerator
        {
            /// <summary>
            /// The VPKIndex object we're enumerating.
            /// </summary>
            /// <seealso cref="VPKIndex"/>
            protected VPKIndex indexObject;
            int position = -1;

            /// <summary>
            /// Creates a new instance of the VPKIndexEnumerator class.
            /// </summary>
            /// <param name="Index"></param>
            public VPKIndexEnumerator(VPKIndex Index)
            {
                indexObject = Index;
            }

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return indexObject[position]; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                position++;
                return (position < indexObject.vpkentries.Length);
            }

            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                position = -1;
            }

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// A VPKIndex index entry.
    /// </summary>
    [Serializable]
    public class VPKIndexEntry
    {
        /// <summary>
        /// Path and filename of the entry.
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// The path to the VPK that contains the file.
        /// </summary>
        public readonly string VPKPath;

        /// <summary>
        /// The offset (position) of the file within the VPK.
        /// </summary>
        public readonly int FileOffset;

        /// <summary>
        /// The length of the file in bytes.
        /// </summary>
        public readonly int FileLength;

        /// <summary>
        /// Provides an empty constructor.
        /// </summary>
        private VPKIndexEntry() { }

        /// <summary>
        /// Creates a new instance of the VPKIndexEntry class from the provided
        /// DirectoryEntry and VPK filespec.
        /// </summary>
        /// <param name="Entry">The entry to create index information from.</param>
        /// <param name="VPK">The path to the VPK file that contains this entry.</param>
        internal VPKIndexEntry(DirectoryEntry Entry, string VPK)
        {
            FileName = Entry.FileName;
            FileOffset = (int)Entry.FileOffset;
            FileLength = Entry.FileLength;
            VPKPath = VPK;
        }

        /// <summary>
        /// Tests if this index entry matches the given pattern.
        /// </summary>
        /// <param name="Pattern">Pattern to match.</param>
        /// <returns></returns>
        public bool IsMatch(string Pattern)
        {
            return WildMatch.Test(Pattern, FileName, true);
        }

        /// <summary>
        /// Reads the current entry from the vpk file that owns it and returns
        /// the file contents in a byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] GetContents()
        {
            if (CurrentVPKPath == null || CurrentVPKPath.Equals(VPKPath) == false)
            {
                // The current stream is not the VPK we need, open the one we need.
                if (VPKReader != null)
                    VPKReader.Close();
                if (VPKStream != null)
                    VPKStream.Close();

                CurrentVPKPath = VPKPath;
                VPKStream = new FileStream(VPKPath, FileMode.Open);
                VPKReader = new BinaryReader(VPKStream);
            }

            VPKStream.Seek(FileOffset, SeekOrigin.Begin);
            byte[] contents = VPKReader.ReadBytes(FileLength);

            return contents;
        }

        private static FileStream VPKStream = null;
        private static BinaryReader VPKReader = null;
        private static string CurrentVPKPath = "";

        /// <summary>
        /// Closes any open file handles.
        /// </summary>
        public static void Close()
        {
            if (VPKReader != null)
            {
                VPKReader.Close();
                VPKReader = null;
            }

            if (VPKStream != null)
            {
                VPKStream.Close();
                VPKStream = null;
            }

            CurrentVPKPath = null;
        }

        /// <summary>
        /// Constructs an empty (invalid) entry.
        /// </summary>
        /// <param name="unused">Unused.</param>
        private VPKIndexEntry(string unused)
        {
            FileName = null;
            VPKPath = null;
            FileOffset = -1;
            FileLength = -1;
        }

        /// <summary>
        /// Specifies that the VPKIndexEntry is invalid.
        /// </summary>
        internal static VPKIndexEntry InvalidEntry = new VPKIndexEntry("");
    }
}
