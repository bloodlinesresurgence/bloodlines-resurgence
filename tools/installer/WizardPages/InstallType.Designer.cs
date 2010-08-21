namespace Resurgence.WizardPages
{
    partial class InstallType
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
            this.StandardInstallationOption = new System.Windows.Forms.RadioButton();
            this.NormalSteamGame = new System.Windows.Forms.Label();
            this.DevelopmentInstallationOption = new System.Windows.Forms.RadioButton();
            this.DevelopmentInstallation = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(187, 13);
            this.WizardDescription.Text = "What type of installation do you want?";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.StandardInstallationOption);
            this.ContentPanel.Controls.Add(this.NormalSteamGame);
            this.ContentPanel.Controls.Add(this.label1);
            this.ContentPanel.Controls.Add(this.DevelopmentInstallation);
            this.ContentPanel.Controls.Add(this.DevelopmentInstallationOption);
            this.ContentPanel.Controls.Add(this.checkBox1);
            this.ContentPanel.Controls.SetChildIndex(this.checkBox1, 0);
            this.ContentPanel.Controls.SetChildIndex(this.DevelopmentInstallationOption, 0);
            this.ContentPanel.Controls.SetChildIndex(this.DevelopmentInstallation, 0);
            this.ContentPanel.Controls.SetChildIndex(this.label1, 0);
            this.ContentPanel.Controls.SetChildIndex(this.NormalSteamGame, 0);
            this.ContentPanel.Controls.SetChildIndex(this.StandardInstallationOption, 0);
            // 
            // NextButton
            // 
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // StandardInstallationOption
            // 
            this.StandardInstallationOption.AutoSize = true;
            this.StandardInstallationOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StandardInstallationOption.Location = new System.Drawing.Point(16, 16);
            this.StandardInstallationOption.Name = "StandardInstallationOption";
            this.StandardInstallationOption.Size = new System.Drawing.Size(168, 20);
            this.StandardInstallationOption.TabIndex = 3;
            this.StandardInstallationOption.TabStop = true;
            this.StandardInstallationOption.Text = "Standard Installation";
            this.StandardInstallationOption.UseVisualStyleBackColor = true;
            // 
            // NormalSteamGame
            // 
            this.NormalSteamGame.AutoSize = true;
            this.NormalSteamGame.Location = new System.Drawing.Point(34, 39);
            this.NormalSteamGame.Name = "NormalSteamGame";
            this.NormalSteamGame.Size = new System.Drawing.Size(345, 13);
            this.NormalSteamGame.TabIndex = 4;
            this.NormalSteamGame.Text = "Install as a normal Steam game. Development tools will not be available.";
            // 
            // DevelopmentInstallationOption
            // 
            this.DevelopmentInstallationOption.AutoSize = true;
            this.DevelopmentInstallationOption.Checked = true;
            this.DevelopmentInstallationOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DevelopmentInstallationOption.Location = new System.Drawing.Point(15, 64);
            this.DevelopmentInstallationOption.Name = "DevelopmentInstallationOption";
            this.DevelopmentInstallationOption.Size = new System.Drawing.Size(197, 20);
            this.DevelopmentInstallationOption.TabIndex = 3;
            this.DevelopmentInstallationOption.TabStop = true;
            this.DevelopmentInstallationOption.Text = "Development Installation";
            this.DevelopmentInstallationOption.UseVisualStyleBackColor = true;
            // 
            // DevelopmentInstallation
            // 
            this.DevelopmentInstallation.AutoSize = true;
            this.DevelopmentInstallation.Location = new System.Drawing.Point(31, 87);
            this.DevelopmentInstallation.Name = "DevelopmentInstallation";
            this.DevelopmentInstallation.Size = new System.Drawing.Size(238, 13);
            this.DevelopmentInstallation.TabIndex = 4;
            this.DevelopmentInstallation.Text = "Allows you to use the Source SDK with the game";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(15, 193);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(216, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Install experimental version";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Enable features that have not been tested extensively yet.";
            // 
            // InstallType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "InstallType";
            this.Text = "Bloodlines Resurgence :: Installation Type";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton StandardInstallationOption;
        private System.Windows.Forms.Label NormalSteamGame;
        private System.Windows.Forms.RadioButton DevelopmentInstallationOption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DevelopmentInstallation;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}