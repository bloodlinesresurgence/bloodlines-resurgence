namespace ResurgenceTools.WizardPages
{
    partial class ModDetails
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
            this.SelectSourceModDirectory = new System.Windows.Forms.Label();
            this.OrangeBoxOption = new System.Windows.Forms.RadioButton();
            this.ModType = new System.Windows.Forms.Label();
            this.SourceModDirectory = new System.Windows.Forms.TextBox();
            this.EpisodeOneOption = new System.Windows.Forms.RadioButton();
            this.Destination = new System.Windows.Forms.TextBox();
            this.InstallTo = new System.Windows.Forms.Label();
            this.ModNotFound = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.Label();
            this.ModDirectoryHint = new System.Windows.Forms.Label();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(136, 13);
            this.WizardDescription.Text = "Specify Mod-related Details";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.ModDirectoryHint);
            this.ContentPanel.Controls.Add(this.Info);
            this.ContentPanel.Controls.Add(this.ModNotFound);
            this.ContentPanel.Controls.Add(this.Destination);
            this.ContentPanel.Controls.Add(this.InstallTo);
            this.ContentPanel.Controls.Add(this.SelectSourceModDirectory);
            this.ContentPanel.Controls.Add(this.OrangeBoxOption);
            this.ContentPanel.Controls.Add(this.ModType);
            this.ContentPanel.Controls.Add(this.SourceModDirectory);
            this.ContentPanel.Controls.Add(this.EpisodeOneOption);
            this.ContentPanel.Controls.SetChildIndex(this.EpisodeOneOption, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SourceModDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.ModType, 0);
            this.ContentPanel.Controls.SetChildIndex(this.OrangeBoxOption, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SelectSourceModDirectory, 0);
            this.ContentPanel.Controls.SetChildIndex(this.InstallTo, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Destination, 0);
            this.ContentPanel.Controls.SetChildIndex(this.ModNotFound, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Info, 0);
            this.ContentPanel.Controls.SetChildIndex(this.ModDirectoryHint, 0);
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
            // SelectSourceModDirectory
            // 
            this.SelectSourceModDirectory.AutoSize = true;
            this.SelectSourceModDirectory.Location = new System.Drawing.Point(28, 46);
            this.SelectSourceModDirectory.Name = "SelectSourceModDirectory";
            this.SelectSourceModDirectory.Size = new System.Drawing.Size(128, 13);
            this.SelectSourceModDirectory.TabIndex = 20;
            this.SelectSourceModDirectory.Text = "SourceMod Directory Name:";
            // 
            // OrangeBoxOption
            // 
            this.OrangeBoxOption.AutoSize = true;
            this.OrangeBoxOption.Location = new System.Drawing.Point(51, 133);
            this.OrangeBoxOption.Name = "OrangeBoxOption";
            this.OrangeBoxOption.Size = new System.Drawing.Size(246, 17);
            this.OrangeBoxOption.TabIndex = 23;
            this.OrangeBoxOption.Text = "Orange Box (Latest, unstable, missing features)";
            this.OrangeBoxOption.UseVisualStyleBackColor = true;
            // 
            // ModType
            // 
            this.ModType.AutoSize = true;
            this.ModType.Location = new System.Drawing.Point(28, 94);
            this.ModType.Name = "ModType";
            this.ModType.Size = new System.Drawing.Size(145, 13);
            this.ModType.TabIndex = 22;
            this.ModType.Text = "Select the mod type to install:";
            // 
            // SourceModDirectory
            // 
            this.SourceModDirectory.Location = new System.Drawing.Point(43, 62);
            this.SourceModDirectory.Name = "SourceModDirectory";
            this.SourceModDirectory.Size = new System.Drawing.Size(223, 20);
            this.SourceModDirectory.TabIndex = 19;
            this.SourceModDirectory.Text = "BloodlinesResurgence";
            this.SourceModDirectory.TextChanged += new System.EventHandler(this.SourceModDirectory_TextChanged);
            // 
            // EpisodeOneOption
            // 
            this.EpisodeOneOption.AutoSize = true;
            this.EpisodeOneOption.Checked = true;
            this.EpisodeOneOption.Location = new System.Drawing.Point(51, 110);
            this.EpisodeOneOption.Name = "EpisodeOneOption";
            this.EpisodeOneOption.Size = new System.Drawing.Size(166, 17);
            this.EpisodeOneOption.TabIndex = 21;
            this.EpisodeOneOption.TabStop = true;
            this.EpisodeOneOption.Text = "Episode 1 (Older, more stable)";
            this.EpisodeOneOption.UseVisualStyleBackColor = true;
            // 
            // Destination
            // 
            this.Destination.Location = new System.Drawing.Point(31, 185);
            this.Destination.Name = "Destination";
            this.Destination.ReadOnly = true;
            this.Destination.Size = new System.Drawing.Size(446, 20);
            this.Destination.TabIndex = 25;
            // 
            // InstallTo
            // 
            this.InstallTo.AutoSize = true;
            this.InstallTo.Location = new System.Drawing.Point(28, 169);
            this.InstallTo.Name = "InstallTo";
            this.InstallTo.Size = new System.Drawing.Size(155, 13);
            this.InstallTo.TabIndex = 24;
            this.InstallTo.Text = "Bloodlines Resurgence will install to:";
            // 
            // ModNotFound
            // 
            this.ModNotFound.AutoSize = true;
            this.ModNotFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModNotFound.Location = new System.Drawing.Point(12, 215);
            this.ModNotFound.Name = "ModNotFound";
            this.ModNotFound.Size = new System.Drawing.Size(465, 13);
            this.ModNotFound.TabIndex = 26;
            this.ModNotFound.Text = "Error: Mod not found. Check directory name, and ensure you have created the mod.";
            // 
            // Info
            // 
            this.Info.Location = new System.Drawing.Point(12, 13);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(465, 33);
            this.Info.TabIndex = 27;
            this.Info.Text = "Some more details are required. Please ensure you have used the Create A Mod wiza" +
                "rd in Steam before continuing past this step.";
            // 
            // ModDirectoryHint
            // 
            this.ModDirectoryHint.AutoSize = true;
            this.ModDirectoryHint.Location = new System.Drawing.Point(272, 65);
            this.ModDirectoryHint.Name = "ModDirectoryHint";
            this.ModDirectoryHint.Size = new System.Drawing.Size(205, 13);
            this.ModDirectoryHint.TabIndex = 28;
            this.ModDirectoryHint.Text = "(This is the name of the mod you created.)";
            // 
            // ModDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "ModDetails";
            this.Text = "ModDetails";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label SelectSourceModDirectory;
        private System.Windows.Forms.RadioButton OrangeBoxOption;
        private System.Windows.Forms.Label ModType;
        private System.Windows.Forms.TextBox SourceModDirectory;
        private System.Windows.Forms.RadioButton EpisodeOneOption;
        private System.Windows.Forms.TextBox Destination;
        private System.Windows.Forms.Label InstallTo;
        private System.Windows.Forms.Label ModDirectoryHint;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.Label ModNotFound;
    }
}