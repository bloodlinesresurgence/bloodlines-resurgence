using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib
{
    /// <summary>
    /// Provides assembly-related helper functions.
    /// </summary>
    public class Assemblies
    {
        /// <summary>
        /// Determines whether the given type matches the target type,
        /// going back through the hierachy of the base classes.
        /// </summary>
        /// <param name="Source">Source type.</param>
        /// <param name="CompareTo">Type to compare to.</param>
        /// <returns>True on match, false if not.</returns>
        protected static bool compareHierachichalType(Type Source, Type CompareTo)
        {
            if (Source == CompareTo)
                return true;

            if (Source.BaseType == null)
                return false;

            return compareHierachichalType(Source.BaseType, CompareTo);
        }
        
        /// <summary>
        /// Determines whether the given target object is of the given type,
        /// going back through the hierachy of it's base classes.
        /// </summary>
        /// <param name="Target">Object to test.</param>
        /// <param name="Type">Type to match to.</param>
        /// <returns>True if the given type is found within the object hierachy.</returns>
        public static bool IsType(object Target, Type Type)
        {
            return compareHierachichalType(Target.GetType(), Type);
        }
    }
}
