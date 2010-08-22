using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RevivalLib;

namespace XMLTableTest
{
    class Program
    {
        [Flags]
        enum Mode
        {
            NOTHING = 0,
            SAVE = 1 << 1,
            LOAD = 1 << 2
        }

        static void Main(string[] args)
        {
            XMLTable table = new XMLTable();
            Mode mode = Mode.NOTHING
                      | Mode.LOAD
                      //| Mode.SAVE
                      ;

            if ((mode & Mode.SAVE) != 0)
            {
                for (int i = 0; i < 10; i++)
                    table["item" + i] = i;

                table["bool"] = (bool)false;
                table["point"] = System.Drawing.Point.Empty;

                table.Save("test.xml");
            }

            if ((mode & Mode.LOAD) != 0)
            {
                table = XMLTable.Load("test.xml");
                foreach (System.Collections.DictionaryEntry de in table)
                    Console.WriteLine("{0} = {1}", de.Key, de.Value);
            }

            Console.ReadLine();
        }
    }
}
