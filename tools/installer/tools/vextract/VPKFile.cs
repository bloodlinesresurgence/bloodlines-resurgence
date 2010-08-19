using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Resurgence.tools.vextract
{
    /// <summary>
    /// Provides information about a VPK's files and method for
    /// retreiving files from inside the VPK file.
    /// </summary>
    public sealed class VPKFile
    {
        /// <summary>
        /// The directory information for the VPK file.
        /// </summary>
        private DirectoryInfo DirectoryInfo;

        /// <summary>
        /// The list of directory entries.
        /// </summary>
        private List<DirectoryEntry> DirectoryEntries;

        /// <summary>
        /// The filename being used.
        /// </summary>
        private string filepath;

        /// <summary>
        /// Creates a new instance of the VPKFile class and loads the
        /// directory information from the specified file.
        /// </summary>
        /// <param name="Path">Path to the VPK file to open.</param>
        public VPKFile(string Path)
        {
            FileStream VPKStream = new FileStream(Path, FileMode.Open);
            BinaryReader VPKReader = new BinaryReader(VPKStream);

            DirectoryInfo = new DirectoryInfo(VPKReader);
            DirectoryEntries = new List<DirectoryEntry>(DirectoryInfo.FileCount);

            VPKStream.Seek(DirectoryInfo.DirectoryOffset, SeekOrigin.Begin);

            // Read each directory entry.

            for (int i = 0; i < DirectoryInfo.FileCount; i++)
                DirectoryEntries.Add(new DirectoryEntry(VPKReader));

            VPKReader.Close();
            VPKStream.Close();

            filepath = Path;
        }

        /// <summary>
        /// Retreives an array of the entries located in the VPK file.
        /// </summary>
        /// <returns></returns>
        internal DirectoryEntry[] GetEntries()
        {
            return DirectoryEntries.ToArray();
        }

        /// <summary>
        /// Extracts the files or creates the directory structure of the given
        /// pattern.
        /// </summary>
        /// <param name="pattern">File/Directory pattern to match.</param>
        /// <param name="outputDirectory">Directory to place the files/directories.</param>
        /// <param name="createSkeleton">
        /// Determines whether files are extracted (false) 
        /// or if just the directories are created (true).
        /// </param>
        /// <returns>The number of files extracted.</returns>
        public int ExtractFiles(string pattern, string outputDirectory, bool createSkeleton)
        {
            int fileCount = 0;
            List<DirectoryEntry> entries = findMatches(pattern);

            if (entries.Count == 0)
                return 0;

            // Open VPK file.
            FileStream VPKStream = new FileStream(filepath, FileMode.Open);
            BinaryReader VPKReader = new BinaryReader(VPKStream);

            foreach (DirectoryEntry currentEntry in entries)
            {
                string directoryPart = getDirectory(currentEntry.FileName);
                string destDirectory = String.Format("{0}\\{1}", outputDirectory, directoryPart)
                    .Replace("/", @"\");

                if (Directory.Exists(destDirectory) == false)
                {
                    // Attempt to create the directory.
                    Directory.CreateDirectory(destDirectory);
                }

                if (createSkeleton == false)
                {
                    byte[] fileContents = readContents(currentEntry, VPKReader);

                    string outputFile = String.Format("{0}\\{1}", outputDirectory, currentEntry.FileName)
                        .Replace("/", @"\");
                    File.WriteAllBytes(outputFile, fileContents);

                    fileCount++;
                }
            }

            VPKReader.Close();
            VPKStream.Close();

            return fileCount;
        }

        /// <summary>
        /// Gets the directory from the given unix-style filespec.
        /// </summary>
        /// <param name="p">Filespec to retreive directory for.</param>
        /// <returns></returns>
        private string getDirectory(string p)
        {
            // Check for file
            if (p.Contains('.') == false)
                return p;

            // Find the last path filespec
            int lastPath = p.LastIndexOf('/');
            if (lastPath == -1)
                return p;

            return p.Substring(0, lastPath - 1);
        }

        /// <summary>
        /// Reads the contents of the file inside the VPK and returns then in a byte array.
        /// </summary>
        /// <param name="entry">Entry that specifies the file details.</param>
        /// <param name="VPKReader">BinaryReader stream to read from.</param>
        /// <returns></returns>
        private byte[] readContents(DirectoryEntry entry, BinaryReader VPKReader)
        {
            VPKReader.BaseStream.Seek(entry.FileOffset, SeekOrigin.Begin);
            byte[] contents = VPKReader.ReadBytes(entry.FileLength);

            return contents;
        }

        /// <summary>
        /// Searches for and returns all matches to the specified pattern in the VPK file.
        /// </summary>
        /// <param name="pattern">
        /// Pattern to search for.
        /// <example>*.tga, materials/*</example>
        /// </param>
        /// <returns></returns>
        private List<DirectoryEntry> findMatches(string pattern)
        {
            List<DirectoryEntry> entries = new List<DirectoryEntry>();

            foreach (DirectoryEntry entry in DirectoryEntries)
                if (WildMatch.Test(pattern, entry.FileName, true) == true)
                    entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Gets the filename of the VPK being used by this class.
        /// </summary>
        public string Filepath
        {
            get { return filepath; }
        }
    }
}
