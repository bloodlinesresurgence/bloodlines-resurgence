namespace Resurgence.WizardPages
{
    partial class LocationInformation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectVampireDirectory = new System.Windows.Forms.Label();
            this.VampireDirectory = new System.Windows.Forms.TextBox();
            this.BrowseSteamButton = new System.Windows.Forms.Button();
            this.BrowseVampireButton = new System.Windows.Forms.Button();
            this.SteamDirectory = new System.Windows.Forms.TextBox();
            this.SelectSteamDirectory = new System.Windows.Forms.Label();
            this.SupplyWizard = new System.Windows.Forms.Label();
            this.BloodlinesNotFound = new System.Windows.Forms.Label();
            this.SteamNotFound = new System.Windows.Forms.Label();
            this.TempNotFound = new System.Windows.Forms.Label();
            this.SelectTemporaryDirectory = new System.Windows.Forms.Label();
            this.BrowseTempDirectoryButton = new System.Windows.Forms.Button();
            this.TemporaryDirectory = new System.Windows.Forms.TextBox();
            this.CreateTempDirectory = new System.Windows.Forms.Button();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(139, 13);
            this.WizardDescription.Text = "Specify directory information";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.TempNotFound);
            this.ContentPanel.Controls.Add(this.SelectTemporaryDirectory);
            this.ContentPanel.Controls.Add(this.CreateTempDirectory);
            this.ContentPanel.Controls.Add(this.BrowseTempDirectoryButton);
            this.ContentPanel.Controls.Add(this.TemporaryDirectory);
            this.ContentPanel.Controls.Add(this.SupplyWizard);
            this.ContentPanel.Controls.Add(this.SteamNotFound);
            this.ContentPanel.Controls.Add(this.SelectVampireDirectory);
            this.ContentPanel.Controls.Add(this.BloodlinesNotFound);
            this.ContentPanel.Controls.Add(this.BrowseVampireButton);
            this.ContentPanel.Controls.Add(this.VampireDirectory);
            this.ContentPanel.Controls.Add(this.SelectSteamDirectory);
            this.ContentPanel.Controls.Add(this.BrowseSteamButton);
            this.ContentPanel.Controls.Add(this.SteamDirectory);
            this.ContentPanel.Controls.SetChildIndex(this.SteamDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.BrowseSteamButton, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SelectSteamDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.VampireDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.BrowseVampireButton, 0);
            this.ContentPanel.Controls.SetChildIndex(this.BloodlinesNotFound, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SelectVampireDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SteamNotFound, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SupplyWizard, 0);
            this.ContentPanel.Controls.SetChildIndex(this.TemporaryDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.BrowseTempDirectoryButton, 0);
            this.ContentPanel.Controls.SetChildIndex(this.CreateTempDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SelectTemporaryDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.TempNotFound, 0);
            // 
            // NextButton
            // 
            this.NextButton.Enabled = false;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // SelectVampireDirectory
            // 
            this.SelectVampireDirectory.AutoSize = true;
            this.SelectVampireDirectory.Location = new System.Drawing.Point(27, 43);
            this.SelectVampireDirectory.Name = "SelectVampireDirectory";
            this.SelectVampireDirectory.Size = new System.Drawing.Size(147, 13);
            this.SelectVampireDirectory.TabIndex = 7;
            this.SelectVampireDirectory.Text = "Vampire: Bloodlines Directory:";
            // 
            // VampireDirectory
            // 
            this.VampireDirectory.Location = new System.Drawing.Point(30, 59);
            this.VampireDirectory.Name = "VampireDirectory";
            this.VampireDirectory.ReadOnly = true;
            this.VampireDirectory.Size = new System.Drawing.Size(335, 20);
            this.VampireDirectory.TabIndex = 8;
            this.VampireDirectory.TextChanged += new System.EventHandler(this.VampireDirectory_TextChanged);
            // 
            // BrowseSteamButton
            // 
            this.BrowseSteamButton.Location = new System.Drawing.Point(371, 118);
            this.BrowseSteamButton.Name = "BrowseSteamButton";
            this.BrowseSteamButton.Size = new System.Drawing.Size(111, 23);
            this.BrowseSteamButton.TabIndex = 12;
            this.BrowseSteamButton.Text = "#Browse#";
            this.BrowseSteamButton.UseVisualStyleBackColor = true;
            this.BrowseSteamButton.Click += new System.EventHandler(this.BrowseSteamButton_Click);
            // 
            // BrowseVampireButton
            // 
            this.BrowseVampireButton.Location = new System.Drawing.Point(371, 57);
            this.BrowseVampireButton.Name = "BrowseVampireButton";
            this.BrowseVampireButton.Size = new System.Drawing.Size(111, 23);
            this.BrowseVampireButton.TabIndex = 9;
            this.BrowseVampireButton.Text = "#Browse#";
            this.BrowseVampireButton.UseVisualStyleBackColor = true;
            this.BrowseVampireButton.Click += new System.EventHandler(this.BrowseVampireButton_Click);
            // 
            // SteamDirectory
            // 
            this.SteamDirectory.Location = new System.Drawing.Point(30, 120);
            this.SteamDirectory.Name = "SteamDirectory";
            this.SteamDirectory.ReadOnly = true;
            this.SteamDirectory.Size = new System.Drawing.Size(335, 20);
            this.SteamDirectory.TabIndex = 11;
            this.SteamDirectory.TextChanged += new System.EventHandler(this.SteamDirectory_TextChanged);
            // 
            // SelectSteamDirectory
            // 
            this.SelectSteamDirectory.AutoSize = true;
            this.SelectSteamDirectory.Location = new System.Drawing.Point(27, 104);
            this.SelectSteamDirectory.Name = "SelectSteamDirectory";
            this.SelectSteamDirectory.Size = new System.Drawing.Size(85, 13);
            this.SelectSteamDirectory.TabIndex = 10;
            this.SelectSteamDirectory.Text = "Steam Directory:";
            // 
            // SupplyWizard
            // 
            this.SupplyWizard.AutoSize = true;
            this.SupplyWizard.Location = new System.Drawing.Point(12, 13);
            this.SupplyWizard.Name = "SupplyWizard";
            this.SupplyWizard.Size = new System.Drawing.Size(267, 13);
            this.SupplyWizard.TabIndex = 13;
            this.SupplyWizard.Text = "Please supply the Wizard with the following information:";
            // 
            // BloodlinesNotFound
            // 
            this.BloodlinesNotFound.AutoSize = true;
            this.BloodlinesNotFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BloodlinesNotFound.Location = new System.Drawing.Point(27, 82);
            this.BloodlinesNotFound.Name = "BloodlinesNotFound";
            this.BloodlinesNotFound.Size = new System.Drawing.Size(297, 13);
            this.BloodlinesNotFound.TabIndex = 14;
            this.BloodlinesNotFound.Text = "Vampire: Bloodlines was not found in this directory.";
            // 
            // SteamNotFound
            // 
            this.SteamNotFound.AutoSize = true;
            this.SteamNotFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SteamNotFound.Location = new System.Drawing.Point(27, 143);
            this.SteamNotFound.Name = "SteamNotFound";
            this.SteamNotFound.Size = new System.Drawing.Size(221, 13);
            this.SteamNotFound.TabIndex = 15;
            this.SteamNotFound.Text = "Steam was not found in this directory.";
            // 
            // TempNotFound
            // 
            this.TempNotFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TempNotFound.Location = new System.Drawing.Point(27, 204);
            this.TempNotFound.Name = "TempNotFound";
            this.TempNotFound.Size = new System.Drawing.Size(338, 33);
            this.TempNotFound.TabIndex = 19;
            this.TempNotFound.Text = "The temporary files directory was not found.";
            // 
            // SelectTemporaryDirectory
            // 
            this.SelectTemporaryDirectory.AutoSize = true;
            this.SelectTemporaryDirectory.Location = new System.Drawing.Point(27, 165);
            this.SelectTemporaryDirectory.Name = "SelectTemporaryDirectory";
            this.SelectTemporaryDirectory.Size = new System.Drawing.Size(126, 13);
            this.SelectTemporaryDirectory.TabIndex = 16;
            this.SelectTemporaryDirectory.Text = "Temporary files Directory:";
            // 
            // BrowseTempDirectoryButton
            // 
            this.BrowseTempDirectoryButton.Location = new System.Drawing.Point(371, 179);
            this.BrowseTempDirectoryButton.Name = "BrowseTempDirectoryButton";
            this.BrowseTempDirectoryButton.Size = new System.Drawing.Size(111, 23);
            this.BrowseTempDirectoryButton.TabIndex = 18;
            this.BrowseTempDirectoryButton.Text = "#Browse#";
            this.BrowseTempDirectoryButton.UseVisualStyleBackColor = true;
            this.BrowseTempDirectoryButton.Click += new System.EventHandler(this.BrowseTempDirectoryButton_Click);
            // 
            // TemporaryDirectory
            // 
            this.TemporaryDirectory.Location = new System.Drawing.Point(30, 181);
            this.TemporaryDirectory.Name = "TemporaryDirectory";
            this.TemporaryDirectory.ReadOnly = true;
            this.TemporaryDirectory.Size = new System.Drawing.Size(335, 20);
            this.TemporaryDirectory.TabIndex = 17;
            this.TemporaryDirectory.TextChanged += new System.EventHandler(this.TemporaryDirectory_TextChanged);
            // 
            // CreateTempDirectory
            // 
            this.CreateTempDirectory.Location = new System.Drawing.Point(371, 204);
            this.CreateTempDirectory.Name = "CreateTempDirectory";
            this.CreateTempDirectory.Size = new System.Drawing.Size(111, 23);
            this.CreateTempDirectory.TabIndex = 18;
            this.CreateTempDirectory.Text = "Create";
            this.CreateTempDirectory.UseVisualStyleBackColor = true;
            this.CreateTempDirectory.Click += new System.EventHandler(this.CreateTempDirectory_Click);
            // 
            // SelectPaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "SelectPaths";
            this.Text = "SelectPaths";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label SelectVampireDirectory;
        private System.Windows.Forms.TextBox VampireDirectory;
        private System.Windows.Forms.Button BrowseSteamButton;
        private System.Windows.Forms.Button BrowseVampireButton;
        private System.Windows.Forms.TextBox SteamDirectory;
        private System.Windows.Forms.Label SelectSteamDirectory;
        private System.Windows.Forms.Label SupplyWizard;
        private System.Windows.Forms.Label BloodlinesNotFound;
        private System.Windows.Forms.Label SteamNotFound;
        private System.Windows.Forms.Label TempNotFound;
        private System.Windows.Forms.Label SelectTemporaryDirectory;
        private System.Windows.Forms.Button BrowseTempDirectoryButton;
        private System.Windows.Forms.TextBox TemporaryDirectory;
        private System.Windows.Forms.Button CreateTempDirectory;
    }
}