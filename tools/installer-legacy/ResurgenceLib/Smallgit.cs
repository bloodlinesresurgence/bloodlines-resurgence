using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ResurgenceLib
{
    /// <summary>
    /// Interface to Smallgit distribution
    /// </summary>
    public class Smallgit
    {
        /// <summary>
        /// Location of smallgit distribution
        /// </summary>
        protected readonly string home;

        /// <summary>
        /// Internal delegate used by runApp.
        /// </summary>
        /// <param name="errorcode"></param>
        /// <param name="output"></param>
        protected delegate void SmallgitProcessCallback(int errorcode, BinOutput output);

        /// <summary>
        /// Output from a program.
        /// </summary>
        protected struct BinOutput
        {
            /// <summary>
            /// Standard output.
            /// </summary>
            public string stdout;
            /// <summary>
            /// Standard input.
            /// </summary>
            public string stderr;
        }

        /// <summary>
        /// Creates a new instance of the Smallgit class.
        /// </summary>
        /// <param name="home">Location of smallgit distribution.</param>
        public Smallgit(string home)
        {
            this.home = home;
        }

        /// <summary>
        /// Run an application.
        /// </summary>
        /// <param name="working">Working directory to run in.</param>
        /// <param name="bin">Binary to execute.</param>
        /// <param name="arguments">Arguments to pass to binary.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        protected void runApp(string working, string bin, string arguments, SmallgitProcessCallback callback)
        {
            ProcessStartInfo info = new ProcessStartInfo(this.home + "\\bin\\" + bin, arguments);
            info.WorkingDirectory = working;
            info.EnvironmentVariables["PATH"] = this.home + "\\bin";
            info.EnvironmentVariables["HOME"] = this.home + "\\smallgit";

            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            BinOutput output = new BinOutput();
            Process process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo = info;
            process.Exited += delegate(object sender, EventArgs e)
            {
                callback(process.ExitCode, output);
            };
            process.OutputDataReceived += delegate(object sendingProcess, DataReceivedEventArgs outLine)
            {
                output.stdout += outLine.Data;
            };
            process.ErrorDataReceived += delegate(object sendingProcess, DataReceivedEventArgs outLine)
            {
                output.stderr += outLine.Data;
            };
            process.Start();
        }

        public delegate void StatusCallback(string branch, FileInfo[] modified, FileInfo[] untracked);
        /// <summary>
        /// Get the status according to git of the specified directory.
        /// </summary>
        /// <param name="workingdir"></param>
        /// <param name="callback"></param>
        public void Status(string workingdir, StatusCallback callback)
        {
            runApp(workingdir, "git.exe", "status", delegate(int status, BinOutput output)
            {
                if (0 != status)
                {
                    throw new Exception("Unknown error");
                }

                // Can match modified files pretty easily
                List<FileInfo> modified = new List<FileInfo>();
                Regex regex = new Regex(@"/modified:\s+(.*)$/", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                MatchCollection results = regex.Matches(output.stdout);
            });
        }
    }
}
