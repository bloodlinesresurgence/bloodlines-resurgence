using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Resurgence.tools.vextract
{
    /// <summary>
    /// Provides information for a Vextract delegate.
    /// </summary>
    public class VextractStatusData
    {
        /// <summary>
        /// If set to True, cancels the current Vextract operation.
        /// </summary>
        public bool CancelExtraction;
        /// <summary>
        /// Reports the percent complete.
        /// </summary>
        public int PercentComplete;
        /// <summary>
        /// The number of files done.
        /// </summary>
        public int FilesDone;
        /// <summary>
        /// The number of files to go.
        /// </summary>
        public int FilesTotal;
        /// <summary>
        /// Reports the current file being extracted.
        /// </summary>
        public string CurrentFile;
    }

    /// <summary>
    /// C# edition of Vextract.
    /// </summary>
    public class Vextract
    {
        /// <summary>
        /// A delegate for various file extraction details.
        /// </summary>
        /// <param name="Status">Information about the current extraction.</param>
        /// <returns>True on cancel, false to continue.</returns>
        public delegate bool FileExtractionDelegate(VextractStatusData Status);

        /// <summary>
        /// Occurs when a file is about to be extracted.
        /// </summary>
        public FileExtractionDelegate OnFileBeginExtraction;

        /// <summary>
        /// Creates a new instance of the Vextract class.
        /// </summary>
        public Vextract()
        {
        }

        /// <summary>
        /// Extracts all files from the VPK files that match the given pattern.
        /// </summary>
        /// <param name="Pattern">The filename pattern to match and extract.</param>
        /// <param name="OutputDirectory">The directory to extract the files to.</param>
        /// <param name="Exemptions">A string array of patterns to exempt.</param>
        /// <param name="SkeletonOnly">If true, creates directories instead of extracting files.</param>
        /// <returns>The number of files extracted.</returns>
        public int ExtractFiles(string Pattern, string OutputDirectory, string[] Exemptions, bool SkeletonOnly)
        {
            int filesExtracted = 0;
            VPKIndex index;
            VextractStatusData status = new VextractStatusData();

            try
            {
                index = VPKIndex.Load();
            }
            catch (FileNotFoundException)
            {
                throw new Exceptions.VPKIndexNotFound();
            }

            VPKIndexEntry[] files = index.FindFiles(Pattern, Exemptions);
            decimal length = Decimal.Round((decimal)files.Length, 0);   // Used to avoid re-casting from int during the percentage calculations
            int intLength = files.Length;

            status.FilesTotal = intLength;

            for(int i = 0; i < intLength; i++)
            {
                VPKIndexEntry currentEntry = files[i];

                if (OnFileBeginExtraction != null)
                {
                    // Determine percentage complete
                    status.PercentComplete = (int)Decimal.Round((decimal)i / length * 100M, 0);
                    status.FilesDone = filesExtracted;
                    status.CurrentFile = currentEntry.FileName;
                    bool cancel = OnFileBeginExtraction(status);
                    if (cancel)
                        break;
                }

                // Attempt to open output file.
                string directoryPart = getDirectory(currentEntry.FileName);
                string destDirectory = String.Format("{0}\\{1}", OutputDirectory, directoryPart)
                    .Replace("/", "\\");

                if (Directory.Exists(destDirectory) == false)
                {
                    // Attempt to create the directory.
                    try
                    {
                        Directory.CreateDirectory(destDirectory);
                    }
                    catch 
                    { 
                        // Creation failed, skip file
                        continue;
                    }
                }

                if (SkeletonOnly == false)
                {
                    byte[] fileContents = currentEntry.GetContents();
                    if (fileContents.Length == 0)
                        continue;

                    string outputFile = String.Format("{0}\\{1}", OutputDirectory, currentEntry.FileName)
                        .Replace("/", "\\");

                    if (Directory.Exists(outputFile))
                    {
                        // The destination was incorrectly created as a directory. Delete it.
                        if (Directory.GetFiles(outputFile).Length == 0)
                            Directory.Delete(outputFile);
                    }

                    File.WriteAllBytes(outputFile, fileContents);

                    filesExtracted++;
                }
            }

            VPKIndexEntry.Close();

            return filesExtracted;
        }

        /// <summary>
        /// Gets the directory from the given unix-style filespec.
        /// </summary>
        /// <param name="p">Filespec to retreive directory for.</param>
        /// <returns></returns>
        private string getDirectory(string p)
        {
            // Find the last path filespec
            int lastPath = p.LastIndexOf('/');
            if (lastPath == -1)
                return "";

            return p.Substring(0, lastPath);
        }

        /// <summary>
        /// Creates the VPK required to extract VPK files.
        /// </summary>
        /// <param name="VPKLocation">The location of the VPK files.</param>
        /// <returns>True on success, false on failure.</returns>
        public bool CreateIndex(string VPKLocation)
        {
            try
            {
                VPKIndex index = new VPKIndex(VPKLocation);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
