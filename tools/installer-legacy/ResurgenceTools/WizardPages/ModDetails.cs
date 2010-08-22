﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResurgenceLib;
using System.IO;

namespace ResurgenceTools.WizardPages
{
    internal partial class ModDetails 
        : ResurgenceWizardPage
    {
        public ModDetails()
        {
            InitializeComponent();
        }

        internal ModDetails(TranslationProvider Provider)
            : base(Provider)
        {
        }

        protected override void DoInitializeComponent()
        {
            base.DoInitializeComponent();
            InitializeComponent();

            SourceModDirectory.Text = Program.Settings.ModDirectory;
            updateInstallPath();

            ModVersion version = Program.Settings.ModVersion;
            EpisodeOneOption.Checked = (version == ModVersion.EPISODE_ONE_MOD);
            OrangeBoxOption.Checked = (version == ModVersion.ORANGE_BOX_MOD);
        }

        private void SourceModDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.ModDirectory = SourceModDirectory.Text;

            updateInstallPath();
        }

        private bool updateInstallPath()
        {
            string path = Program.Settings.DestinationDirectory;
            Destination.Text = path;

            bool pathExists = Directory.Exists(path);

            NextButton.Enabled = pathExists;
            ModNotFound.Visible = !pathExists;

            return pathExists;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
#if !FULL_TOOLSET
            Program.NextForm = new SelectPaths(TranslationProvider);
#else
            (new SelectPaths(TranslationProvider)).Show(Program.MainForm);
#endif
            Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (EpisodeOneOption.Checked)
                Program.Settings.ModVersion = ModVersion.EPISODE_ONE_MOD;
            else if (OrangeBoxOption.Checked)
                Program.Settings.ModVersion = ModVersion.ORANGE_BOX_MOD;

#if !FULL_TOOLSET
            Program.NextForm = new SelectSteps(TranslationProvider);
#else
            Hide();
            (new SelectSteps(TranslationProvider)).Show(Program.MainForm);
#endif
            Close();
        }
    }
}