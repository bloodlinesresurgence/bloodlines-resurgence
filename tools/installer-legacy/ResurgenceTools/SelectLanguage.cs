using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using System.IO;

namespace ResurgenceTools
{
    /// <summary>
    /// Provides a Language Selection dialogue.
    /// This dialogue is NOT translated.
    /// </summary>
    internal partial class SelectLanguage 
        : Form
    {
        public SelectLanguage()
        {
            InitializeComponent();
        }

        private void SelectLanguage_Load(object sender, EventArgs e)
        {
            LanguageList.Items.Clear();
            RefreshList();
        }

        private void RefreshList()
        {
            DirectoryInfo dir_info = new DirectoryInfo(Program.Settings.LanguageDirectory);

            foreach (FileInfo file in dir_info.GetFiles("*.txt"))
            {
                string name = file.Name.Substring(0, file.Name.Length - file.Extension.Length);
                int index = LanguageList.Items.Add(name);
                if (name.Equals(Program.Settings.Language, StringComparison.InvariantCultureIgnoreCase))
                    LanguageList.SelectedIndex = index;
            }
        }

        private void Button_Change_Click(object sender, EventArgs e)
        {
            if (LanguageList.SelectedItem == null)
                return;

            Program.Settings.Language = LanguageList.SelectedItem as string;
            Close();
        }

        private void LanguageList_DoubleClick(object sender, EventArgs e)
        {
            Button_Change.PerformClick();
        }

        private void editFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(LanguageList.SelectedItem == null)
            {
                return;
            }

            string fileToEdit = Program.Settings.LanguageDirectory + "\\" + LanguageList.SelectedItem + ".txt";
            System.Diagnostics.Process.Start(fileToEdit);
        }

        private void newTranslationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LanguageList.SelectedItem == null)
            {
                return;
            }

            DaedalusLib.Inputbox input = new DaedalusLib.Inputbox("Create New Translation", "",
                "Enter translation name (eg language name):");
            input.ShowDialog(this);
            if (input.DialogResult == DialogResult.OK)
            {
                // Use English as source
                string source = Program.Settings.LanguageDirectory + "\\English.txt";
                string dest = Program.Settings.LanguageDirectory + "\\" + input.Value + ".txt";

                // Attempt to copy
                try
                {
                    File.Copy(source, dest);
                    RefreshList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error creating translation file: " + ex.Message, "Create New Translation",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void browseTranslationsDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Settings.LanguageDirectory);
        }
    }
}
