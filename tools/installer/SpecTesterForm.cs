using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BloodlinesResurgenceInstaller
{
    public partial class SpecTesterForm : Form
    {
        private Type[] types = { typeof(EventEmitter) };

        public SpecTesterForm()
        {
            InitializeComponent();
        }

        private void addResults(string type, object r)
        {
            if (ResultsListView.InvokeRequired)
            {
                ResultsListView.BeginInvoke(new MethodInvoker(() => addResults(type, r)));
            }
            else
            {
                Hashtable results = r as Hashtable;
                foreach (DictionaryEntry de in results)
                {
                    ListViewItem item = new ListViewItem(new string[] { type, de.Key as string, de.Value.ToString() });
                    ResultsListView.Items.Add(item);
                }
            }
        }

        private void RunSpecTestsButton_Click(object sender, EventArgs e)
        {
            // Create an instance of each type and run their spec tests
            foreach (Type typeInfo in this.types)
            {
                Spec instance = Activator.CreateInstance(typeInfo) as Spec;
                instance.RunSpec((object results) => this.addResults(typeInfo.Name, results));
            }
        }
    }
}
