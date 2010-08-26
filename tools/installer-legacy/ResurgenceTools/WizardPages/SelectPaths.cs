using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;

namespace Resurgence.WizardPages
{
    internal partial class SelectPaths 
        : ResurgenceWizardPage
    {
        internal SelectPaths()
        {
            InitializeComponent();
        }

        internal SelectPaths(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            SteamDirectory.Text = Program.Settings.SteamDirectory;
            VampireDirectory.Text = Program.Settings.VampireDirectory;
            TemporaryDirectory.Text = Program.Settings.TemporaryDirectory;

            checkDirectorys();
        }

        private void BrowseVampireButton_Click(object sender, EventArgs e)
        {
            browseForDirectory(VampireDirectory, "!SelectVampireDirectoryMessage", false);
        }

        private void browseForDirectory(TextBox destinationTextbox, string description, bool showCreateDirectory)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            browser.Description = TranslationProvider.Translate(description);
            browser.ShowNewFolderButton = showCreateDirectory;
            browser.RootFolder = Environment.SpecialFolder.MyComputer;

            DialogResult result = browser.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                destinationTextbox.Text = browser.SelectedPath;
            }
        }

        private void BrowseSteamButton_Click(object sender, EventArgs e)
        {
            browseForDirectory(SteamDirectory, "!SelectSteamDirectoryMessage", false);
        }

        private void VampireDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.VampireDirectory = VampireDirectory.Text;

            checkDirectorys();
        }

        private void SteamDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.SteamDirectory = SteamDirectory.Text;

            checkDirectorys();
        }

        private void checkDirectorys()
        {
            bool steamFound = checkSteamDirectory();
            bool vamprFound = checkVampireDirectory();
            bool tempFound = true;// checkTempDirectory();
            NextButton.Enabled = (steamFound && vamprFound && tempFound);
        }

        private bool checkTempDirectory()
        {
            bool exists = (System.IO.Directory.Exists(TemporaryDirectory.Text));

            TempNotFound.Visible = !exists;
            CreateTempDirectory.Visible = !exists;
            return exists;
        }

        private bool checkSteamDirectory()
        {
            // Check for 'steam.exe' in the directory.

            string fullpath = Program.Settings.SteamDirectory + "\\Steam.exe";
            bool exists = System.IO.File.Exists(fullpath);

            SteamNotFound.Visible = !exists;

            return exists;
        }

        private bool checkVampireDirectory()
        {
            // Check for 'vampire.exe' in the directory.

            string fullpath = Program.Settings.VampireDirectory + "\\Vampire.exe";
            bool exists = System.IO.File.Exists(fullpath);

            BloodlinesNotFound.Visible = !exists;

            return exists;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            CreateTempDirectory_Click(null, null);
            Hide();
#if !FULL_TOOLSET
            Program.NextForm = new ModDetails(TranslationProvider);
#else
            (new ModDetails(TranslationProvider)).Show(Program.MainForm);
#endif
            Close();
        }

        private void BrowseTempDirectoryButton_Click(object sender, EventArgs e)
        {
            browseForDirectory(TemporaryDirectory, "!SelectTempDirectoryMessage", true);

            checkDirectorys();
        }

        private void TemporaryDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.TemporaryDirectory = TemporaryDirectory.Text;
        }

        private void CreateTempDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                if(System.IO.Directory.Exists(TemporaryDirectory.Text) == false)
                    System.IO.Directory.CreateDirectory(TemporaryDirectory.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, Translate("!CreateFailed") + " " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            checkDirectorys();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            Program.NextForm = new Welcome(TranslationProvider);
            Close();
        }
    }
}
