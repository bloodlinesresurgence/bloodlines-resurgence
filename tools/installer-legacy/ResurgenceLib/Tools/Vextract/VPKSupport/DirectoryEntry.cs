using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResurgenceLib.Tools.Vextract.VPKSupport
{
    /// <summary>
    /// Provides a VPK directory entry.
    /// </summary>
    internal class DirectoryEntry
    {
        /// <summary>
        /// The filename (or in the case of pack010.vpk, directory name.
        /// </summary>
        internal readonly string FileName;

        /// <summary>
        /// The offset location of the file.
        /// </summary>
        internal readonly UInt32 FileOffset;

        /// <summary>
        /// The length of the file.
        /// </summary>
        internal readonly int FileLength;

        /// <summary>
        /// Creates a new instance of the DirectoryEntry class.
        /// Each DirectoryEntry should be read in direct succession.
        /// </summary>
        /// <param name="VPKReader">BinaryReader to read from.</param>
        internal DirectoryEntry(BinaryReader VPKReader)
        {
            int NameLength = VPKReader.ReadInt32();
            char[] name = VPKReader.ReadChars(NameLength);
            FileName = new string(name);
            FileOffset = (UInt32)VPKReader.ReadInt32();
            FileLength = VPKReader.ReadInt32();
        }
    }
}
