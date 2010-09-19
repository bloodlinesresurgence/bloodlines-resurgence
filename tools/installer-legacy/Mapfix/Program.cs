using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mapfix
{
    class Program
    {
        static void Main(string[] args)
        {
            int rotate_direction = -90;
            bool fix_connections = true;
            string filespec = null;
            ArgumentState state = ArgumentState.FILESPEC;
            SearchOption searchOption = SearchOption.TopDirectoryOnly;

            foreach (string argument in args)
            {
                switch (argument.ToLower())
                {
                    case "-help":
                    case "-h":
                    case "/?":
                    case "/help":
                        show_help();
                        return;

                    case "-rotate":
                    case "-rot":
                        state = ArgumentState.ROTATE_DIRECTION;
                        break;

                    case "-recursive":
                    case "-r":
                        searchOption = SearchOption.AllDirectories;
                        break;

                    case "-fix":
                        state = ArgumentState.FIX_CONNECTIONS;
                        break;

                    default:
                        switch (state)
                        {
                            case ArgumentState.FILESPEC:
                                filespec = argument;
                                break;

                            case ArgumentState.FIX_CONNECTIONS:
                                fix_connections = Boolean.Parse(argument);
                                break;

                            case ArgumentState.ROTATE_DIRECTION:
                                rotate_direction = Int32.Parse(argument);
                                break;

                            default:
                                Console.WriteLine("Unknown state!");
                                return;
                        }
                        break;
                }
            }

            if (filespec == null)
            {
                show_help();
                return;
            }

            ResurgenceLib.Tools.Mapfix.Mapfix.Disable_ConnectionFix = !fix_connections;
            ResurgenceLib.Tools.Mapfix.Mapfix.Rotate_Direction = rotate_direction;

            // Get files and fix them
            DirectoryInfo currdir = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = currdir.GetFiles(filespec, searchOption);

            if (files.Length == 0)
            {
                Console.WriteLine("No files found matching that spec");
                return;
            }

            foreach(FileInfo current in files) {
                Console.Write(current.Name + "...");
                ResurgenceLib.Tools.Mapfix.Mapfix.Fix(current.FullName, current.FullName);
                Console.WriteLine("success.");
            }

            Console.WriteLine("{0} files fixed", files.Length);
        }

        private static void show_help()
        {
            Console.WriteLine("MapFix");
            Console.WriteLine("Syntax: mapfix filespec [-recursive] [-fix true|false] [-rotate n]");
            Console.WriteLine("");
            Console.WriteLine("filespec            File spec (can use wildcards) of files to fix)");
            Console.WriteLine("-recursive          Convert all maps (including those in subdirectories");
            Console.WriteLine("-fix true | false   Whether to fix connections (default true)");
            Console.WriteLine("-rotate n           Rotate props n degrees (default -90)");
            Console.WriteLine("");
        }

        private enum ArgumentState
        {
            FILESPEC,
            FIX_CONNECTIONS,
            ROTATE_DIRECTION,
        };
    }
}
