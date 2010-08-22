namespace ResurgenceTools
{
    partial class ResurgenceWizardPage
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
            this.SuspendLayout();
            // 
            // CancelWizardButton
            // 
            this.CancelWizardButton.Click += new System.EventHandler(this.CancelWizardButton_Click);
            // 
            // LanguageButton
            // 
            this.LanguageButton.Click += new System.EventHandler(this.LanguageButton_Click);
            // 
            // ResurgenceWizardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "ResurgenceWizardPage";
            this.Text = "ResurgenceWizardPage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ResurgenceWizardPage_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResurgenceWizardPage_FormClosing);
            this.LocationChanged += new System.EventHandler(this.ResurgenceWizardPage_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}