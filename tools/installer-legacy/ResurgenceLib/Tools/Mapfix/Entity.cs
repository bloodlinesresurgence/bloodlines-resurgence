using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ResurgenceLib.Tools.Mapfix
{
    /// <summary>
    /// A VMF entity section.
    /// </summary>
    internal class Entity
    {
        private StringBuilder GenericData = new StringBuilder();
        private StringBuilder Outputs = new StringBuilder();
        /// <summary>
        /// Class name.
        /// </summary>
        /// Hammer crashes if no class name is specified.
        private string ClassName = "unknown_entity";

        private string id = null;

        /// <summary>
        /// Whether we are in a connections block.
        /// </summary>
        public bool in_connections = false;

        private Regex keyValue = new Regex("\"([^\"]+)\"\\W+\"([^\"]+)\"", RegexOptions.Compiled);

        static string[] to_rotate = {
                                        "prop_dynamic",
                                        //"prop_doorknob",
                                        "prop_static",
                                    };

        public Entity()
        {
        }

        public void Reset()
        {
            ClassName = "unknown_entity";
            GenericData.Length = 0;
            Outputs.Length = 0;
            id = null;
        }

        private bool is_output(string line)
        {
            if (Mapfix.Disable_ConnectionFix) return false;

            if (in_connections) return false;

            // If line contains: "On
            if (line.Contains("\"On"))
                return true;

            // Further checks?

            return false;
        }

        public void Add_Line(string line)
        {
            if (is_output(line))
                this.Outputs.AppendLine("\t" + line + "");
            else
            {
                // Probably in the format: "key" "value"
                MatchCollection pair = keyValue.Matches(line);
                if (pair.Count > 0)
                {
                    var parts = pair[0].Groups;
                    if (parts.Count > 1)
                    {
                        // Key
                        switch (parts[1].Value.ToLower())
                        {
                            case "id":
                                if (this.id == null)
                                {
                                    this.id = parts[2].Value;
                                }
                                else
                                {
                                    this.GenericData.AppendLine(line);
                                }
                                break;

                            case "classname":
                                this.ClassName = parts[2].Value;
                                break;

                            case "angles":
                                // If of a certain class, rotate the angles
                                if (Mapfix.Rotate_Direction != 0 && to_rotate.Contains(ClassName))
                                {
                                    // Adjust angles accordingly
                                    string[] angles_parts = parts[2].Value.Split(' ');
                                    if (angles_parts[0] == "0" && angles_parts[2] == "0")
                                    {
                                        float angle = float.Parse(angles_parts[1]) + Mapfix.Rotate_Direction;
                                        if (angle < -360)
                                            angle = 360 - (-angle);
                                        else if (angle > 360)
                                            angle -= 360;
                                        angles_parts[1] = angle.ToString();
                                    }
                                    this.GenericData.AppendLine("\t\"angles\" \"" + String.Join(" ", angles_parts) + "\"");
                                }
                                else
                                {
                                    this.GenericData.AppendLine(line);
                                }
                                break;

                            default:
                                this.GenericData.AppendLine(line);
                                break;
                        }
                    }
                }
                else
                {
                    this.GenericData.AppendLine(line);
                }
            }
        }
            
        public string Generate()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("\t\"id\" \"" + this.id + "\"");
            output.AppendLine("\t\"classname\" \"" + this.ClassName + "\"");
            output.Append(this.GenericData.ToString());

            if (Mapfix.Disable_ConnectionFix == false && this.Outputs.Length > 0)
            {
                output.AppendLine("        connections");
                output.AppendLine("        {");
                output.AppendLine(this.Outputs.ToString());
                output.AppendLine("        }");
            }

            return output.ToString();
        }
    }
}
