using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResurgenceLib.Tools.Vextract.VPKSupport
{
    /// <summary>
    /// Represents a directory information type.
    /// All VPK's, except for pack010.vpk, is type 1 (PATHS_AND_FILES), which
    /// means that both files and directories are stored.
    /// Type 0 (PATHS_ONLY) contains no files, just path entries.
    /// </summary>
    internal enum DirectoryInfoType
    {
        /// <summary>
        /// Only paths are contained in the VPK.
        /// </summary>
        PATHS_ONLY = 0,
        /// <summary>
        /// Paths and files are contained in the VPK.
        /// </summary>
        PATHS_AND_FILES = 1
    }

    /// <summary>
    /// The directory information structure, located at
    /// the end of the VPK file.
    /// </summary>
    internal struct DirectoryInfo
    {
        /// <summary>
        /// The number of files/directories contained in the VPK.
        /// </summary>
        internal readonly int FileCount;
        /// <summary>
        /// The location of the first directory entry struct.
        /// </summary>
        internal readonly UInt32 DirectoryOffset;
        /// <summary>
        /// The type of entries provided in the VPK.
        /// </summary>
        internal readonly DirectoryInfoType Type;

        /// <summary>
        /// Creates a new instance of the DirectoryInfo struct and
        /// reads the information from the VPK BinaryReader.
        /// </summary>
        /// <param name="VPKReader">Stream to read directory information from.</param>
        internal DirectoryInfo(BinaryReader VPKReader)
        {
            // The stream is located at the end of the file, minus the size of the struct.
            VPKReader.BaseStream.Seek(-9, SeekOrigin.End);

            FileCount = VPKReader.ReadInt32();
            DirectoryOffset = (UInt32)VPKReader.ReadInt32();
            char type = VPKReader.ReadChar();

            switch (type)
            {
                case (char)DirectoryInfoType.PATHS_ONLY:
                    Type = DirectoryInfoType.PATHS_ONLY;
                    break;

                case (char)DirectoryInfoType.PATHS_AND_FILES:
                    Type = DirectoryInfoType.PATHS_AND_FILES;
                    break;

                default:
                    // Invalid, but Type isn't really used so we'll silenty ignore it.
                    Type = DirectoryInfoType.PATHS_AND_FILES;
                    break;
            }
        }
    }
}
