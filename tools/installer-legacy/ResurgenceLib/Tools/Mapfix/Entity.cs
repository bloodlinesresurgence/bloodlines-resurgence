using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResurgenceLib.Tools.Mapfix
{
    /// <summary>
    /// A VMF entity section.
    /// </summary>
    internal class Entity
    {
        private string GenericData = "";
        private string Outputs = "";

        public Entity()
        {
        }

        public void Reset()
        {
            GenericData = "";
            Outputs = "";
        }

        private bool is_output(string line)
        {
            if (Mapfix.Disable_ConnectionFix) return false;

            // If line contains: "On
            if (line.Contains("\"On"))
                return true;

            // Further checks?

            return false;
        }

        private void checkEntityData()
        {
            // Fix missing "classname" error - crashes Hammer if an entity does not define their classname
            if (this.GenericData.Contains("\"classname\"") == false)
                this.GenericData += "		\"classname\"	\"unknown_entity\"" + System.Environment.NewLine;
        }

        public void Add_Line(string line)
        {
            if (is_output(line))
                this.Outputs += "\t" + line + "" + System.Environment.NewLine;
            else
                this.GenericData += line + "" + System.Environment.NewLine;
        }

        public string Generate()
        {
            checkEntityData();
            string output = this.GenericData;

            if (Mapfix.Disable_ConnectionFix == false)
            {
                output += "        connections" + System.Environment.NewLine;
                output += "        {" + System.Environment.NewLine;
                output += this.Outputs;
                output += "        }" + System.Environment.NewLine;
            }

            return output;
        }
    }
}
