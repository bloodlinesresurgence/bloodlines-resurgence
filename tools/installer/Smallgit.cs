using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Resurgence.smallgit
{
    public enum GitResult
    {
        SUCCESS = 0,
        ERROR = 1,
    }

    /// <summary>
    /// Provides an interface to Smallgit
    /// </summary>
    public class Smallgit
    {
        protected static readonly string PATH_SMALLGIT;

        static Smallgit()
        {
            PATH_SMALLGIT = Path.Combine(Application.StartupPath, @"smallgit\");
        }

        /// <summary>
        /// Clone the master branch of a repository to the specified directory
        /// </summary>
        /// <param name="repo">Git Repo Address / Location</param>
        /// <param name="destination">Destination to clone to</param>
        /// <returns>GitResult</returns>
        public static GitResult Clone(string repo, string destination)
        {
            return Clone(repo, destination, "master");
        }

        /// <summary>
        /// Clone the given branch of a repository to the specified directory
        /// </summary>
        /// <param name="repo">Git Repo Address / Location</param>
        /// <param name="destination">Destination to clone to</param>
        /// <param name="branch">Branch to clone</param>
        /// <returns></returns>
        public static GitResult Clone(string repo, string destination, string branch)
        {
            throw new NotImplementedException();
        }
    }
}
