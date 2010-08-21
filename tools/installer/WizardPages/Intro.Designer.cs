namespace Resurgence.WizardPages
{
    partial class Intro
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
            this.PatchNotes = new System.Windows.Forms.WebBrowser();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(52, 13);
            this.WizardDescription.Text = "Welcome";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.PatchNotes);
            this.ContentPanel.Controls.SetChildIndex(this.PatchNotes, 0);
            // 
            // NextButton
            // 
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Visible = false;
            // 
            // PatchNotes
            // 
            this.PatchNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PatchNotes.Location = new System.Drawing.Point(0, 10);
            this.PatchNotes.MinimumSize = new System.Drawing.Size(20, 20);
            this.PatchNotes.Name = "PatchNotes";
            this.PatchNotes.Size = new System.Drawing.Size(494, 230);
            this.PatchNotes.TabIndex = 3;
            this.PatchNotes.Url = new System.Uri("http://www.bloodlinesresurgence.com/patchnotes.html", System.UriKind.Absolute);
            // 
            // Wizard_Intro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "Wizard_Intro";
            this.Text = "Bloodlines Resurgence";
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser PatchNotes;
    }
}

