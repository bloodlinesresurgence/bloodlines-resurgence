using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResurgenceLib;

namespace RunMapFix
{
    class Program
    {
        static void Main(string[] args)
        {
            ResurgenceLib.Tools.Mapfix.Mapfix.Fix(@"E:\BloodlinesSVN\maps\sm_bailbonds_1_d.vmf", @"E:\BloodlinesSVN\maps\bail_1_d.vmf");
        }
    }
}
