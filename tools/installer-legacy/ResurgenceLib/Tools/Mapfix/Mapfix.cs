using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResurgenceLib.Tools.Mapfix
{
    /// <summary>
    /// Provides a C# port of the C++ Mapfix program, originally written
    /// by myself (Daedalus) for the Bloodlines Resurgence Project.
    /// </summary>
    public class Mapfix
    {
        /// <summary>
        /// Gets or sets the value indicating if connection fixing is disabled.
        /// Default is FALSE.
        /// </summary>
        public static bool Disable_ConnectionFix = false;

        /// <summary>
        /// The last error that occured.
        /// </summary>
        public static string LastError = "";

        private enum Flags
        {
            NONE,
            GENERIC,
            ENTITY
        }

        /// <summary>
        /// Fixes the specified map, and writes the changes to the specified destination file.
        /// </summary>
        /// <param name="source">Source file to fix.</param>
        /// <returns>True on success, false on failure.</returns>
        public static Result Fix(string source)
        {
            return Fix(source, source);
        }

        /// <summary>
        /// Fixes the specified map, and writes the changes to the specified destination file.
        /// </summary>
        /// <param name="source">Source file to fix.</param>
        /// <param name="destination">Destination file, can be the same as source.</param>
        /// <returns>True on success, false on failure.</returns>
        public static Result Fix(string source, string destination)
        {
            string[] buffer = File.ReadAllLines(source);
            StreamWriter output = new StreamWriter(destination);
            Flags flags = Flags.NONE;
            int depth = 0;
            Entity entity = new Entity();
            LastError = "";

            foreach(string line in buffer)
            {
                switch (line)
                {
                    case "{":
                        depth++;
                        output.WriteLine(line);
                        break;

                    case "}":
                        depth--;
                        if (depth < 0)
                        {
                            LastError = "Error: Under depth!";
                            output.Close();
                            return Result.Failure;
                        }
                        else if (depth == 0)
                        {
                            // Back to 0 depth, flush entity
                            if (flags == Flags.ENTITY)
                            {
                                output.Write(entity.Generate());
                                entity.Reset();
                            }
                            flags = Flags.NONE;
                        }

                        output.WriteLine(line);

                        break;

                    default:
                        // Current operation depends on flags
                        switch (flags)
                        {
                            case Flags.NONE:
                                // About to move into either general data, or entity data
                                if (line == "entity")
                                    flags = Flags.ENTITY;
                                else
                                    flags = Flags.GENERIC;
                                output.WriteLine(line);
                                break;

                            case Flags.GENERIC:
                                // Generic data, just write it
                                output.WriteLine(line);
                                break;

                            case Flags.ENTITY:
                                // Entity data, let the entity class manage it
                                entity.Add_Line(line);
                                break;
                        } /* switch(flags) */
                        break;
                } /* switch(line) */
            } /* while (input.EndOfStream == false) */

            output.Close();

            return Result.Success;
        }
    }
}
