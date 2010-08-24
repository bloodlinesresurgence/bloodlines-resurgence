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
            StringBuilder output = new StringBuilder(); 
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
                        output.Append(line + Environment.NewLine);
                        break;

                    case "}":
                        depth--;
                        if (depth < 0)
                        {
                            LastError = "Error: Under depth!";
                            return Result.Failure;
                        }
                        else if (depth == 0)
                        {
                            // Back to 0 depth, flush entity
                            if (flags == Flags.ENTITY)
                            {
                                output.Append(entity.Generate());
                                entity.Reset();
                            }
                            flags = Flags.NONE;
                        }

                        output.Append(line + Environment.NewLine);

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
                                output.Append(line + Environment.NewLine);
                                break;

                            case Flags.GENERIC:
                                // Generic data, just write it
                                output.Append(line + Environment.NewLine);
                                break;

                            case Flags.ENTITY:
                                // Entity data, let the entity class manage it
                                entity.Add_Line(line);
                                break;
                        } /* switch(flags) */
                        break;
                } /* switch(line) */
            } /* while (input.EndOfStream == false) */

            StreamWriter sw = new StreamWriter(destination);
            sw.Write(output.ToString());
            sw.Close();

            return Result.Success;
        }
    }
}
