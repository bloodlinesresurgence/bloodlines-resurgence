using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RevivalLib;
using System.Windows.Forms;

namespace TestTool
{
    /// <summary>
    /// Test tool. Does nothing.
    /// </summary>
    public class TestTool
        : RevivalTool
    {
        public TestTool()
            : base("Test Tool", "Does nothing.")
        {
        }

        public override void Start()
        {
            MessageBox.Show("Started. Will not quit.");
        }
    }
}
