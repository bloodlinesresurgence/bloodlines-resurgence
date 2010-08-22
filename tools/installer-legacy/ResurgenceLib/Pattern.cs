using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#if false
namespace RevivalLib
{
    /// <summary>
    /// Provides an easy interface to RegEx patterns.
    /// </summary>
    public class Pattern
    {
        /// <summary>
        /// The pattern created.
        /// </summary>
        protected readonly string regExPattern;

        /// <summary>
        /// Creates a new instance of the Pattern class using the
        /// given RegEx pattern.
        /// </summary>
        /// <param name="RegExPattern">The RegEx pattern to use.</param>
        public Pattern(string RegExPattern)
        {
            regExPattern = RegExPattern;
        }

        /// <summary>
        /// Tests if the current pattern returns a match on the given data.
        /// </summary>
        /// <param name="testAgainst">Data to test pattern against.</param>
        /// <returns></returns>
        public bool Test(string testAgainst)
        {
            return Regex.IsMatch(testAgainst, regExPattern);
        }

        /// <summary>
        /// Creates a pattern that matches a given directory path and file extension.
        /// </summary>
        /// <param name="matchPath"></param>
        /// <param name="matchExtension"></param>
        /// <returns></returns>
        public static Pattern CreatePattern (string matchPath, string matchExtension)
        {
            string path = PatternPath(matchPath);
            string extension = PatternExtension(matchExtension);
            return new Pattern(String.Format(@"^{0}[A-Za-z\d/]{1}", path, extension));
        }

        public static Pattern CreatePattern (string matchSpecific, PatternCreationMatch matchType)
        {
            switch (matchType)
            {
                case PatternCreationMatch.MATCH_DIRECTORY:
                    return new Pattern(String.Format(@"^{0}[A-Za-z\d/]*.*", PatternPath(matchSpecific)));

                case PatternCreationMatch.MATCH_FILE:
                    // Ensure *. is prefixed.
                    if (matchSpecific.StartsWith("*.") == false)
                        matchSpecific = "*." + matchSpecific;
                    return new Pattern(String.Format(@"[A-Za-z\d/]{0}", PatternExtension(matchSpecific)));

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Ensures the given path pattern is in the correct format.
        /// </summary>
        /// <param name="matchPath">The path pattern to check.</param>
        /// <returns>A valid pattern match patch specifier.</returns>
        private static string PatternPath(string matchPath)
        {
            if (matchPath.EndsWith("/") == false)
                matchPath += "/";
            return matchPath;
        }

        /// <summary>
        /// Ensures the given extension pattern match is in the correct format.
        /// </summary>
        /// <param name="matchExtension"></param>
        /// <returns></returns>
        protected static string PatternExtension(string matchExtension)
        {
            if (matchExtension.StartsWith("*.") == false && matchExtension.Contains("*") == false)
                matchExtension = "*." + matchExtension;
            return matchExtension;
        }
    }

    /// <summary>
    /// Specifies the type of Pattern to create.
    /// </summary>
    public enum PatternCreationMatch
    {
        /// <summary>
        /// Specifies a file match pattern should be created.
        /// </summary>
        MATCH_FILE,
        /// <summary>
        /// Specifies a directory match pattern should be created.
        /// </summary>
        MATCH_DIRECTORY
    }
}
#endif