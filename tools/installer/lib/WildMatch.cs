using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resurgence
{
    /// <summary>
    /// Provides wildcard matching.
    /// </summary>
    /// <remarks>
    /// Code sourced from Untweaked.Code
    /// http://untweaked.wordpress.com/2008/06/03/wildcard-matching-in-c/
    /// Used without permission, code appears to be public domain.
    /// </remarks>
    public static class WildMatch
    {
        /// <summary>
        /// Tests if the given pattern matches the input.
        /// </summary>
        /// <param name="pattern">Pattern to test.</param>
        /// <param name="input">Input to run pattern test over.</param>
        /// <param name="caseInsensitive">Specifies whether the input will be tested case insensitive.</param>
        /// <returns>True if pattern matches the input, otherwise false.</returns>
        public static bool Test(string pattern, string input, bool caseInsensitive)
        {
            int offsetInput = 0;
            bool isAsterix = false;
            int i;

            while (true)
            {
                for (i = 0; i < pattern.Length; )
                {

                    switch (pattern[i])
                    {
                        case '?':
                            isAsterix = false;
                            offsetInput++;
                            break;
                        case '*':
                            isAsterix = true;
                            while (i < pattern.Length &&
                                        pattern[i] == '*')
                            {
                                i++;
                            }

                            if (i >= pattern.Length)
                                return true;
                            continue;
                        default:
                            if (offsetInput >= input.Length)
                                return false;

                            if ((caseInsensitive ?
                                   char.ToLower(input[offsetInput]) :
                                   input[offsetInput])
                                !=
                                (caseInsensitive ?
                                   char.ToLower(pattern[i]) :
                                   pattern[i]))
                            {
                                if (!isAsterix)
                                    return false;
                                offsetInput++;
                                continue;
                            }
                            offsetInput++;
                            break;
                    } // end switch
                    i++;
                } // end for

                // have we finished parsing our input?
                if (i > input.Length)
                    return false;

                // do we have any lingering asterixes we need to skip?
                while (i < pattern.Length && pattern[i] == '*')
                    ++i;

                // final evaluation. The index should be pointing at the
                // end of the string.
                return (offsetInput == input.Length);
            }

        }
    }
}
