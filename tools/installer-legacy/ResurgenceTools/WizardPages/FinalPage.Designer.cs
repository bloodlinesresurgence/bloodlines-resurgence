namespace ResurgenceTools.WizardPages
{
    partial class FinalPage
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
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.ContentPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.Browser);
            this.ContentPanel.Controls.SetChildIndex(this.Browser, 0);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(111, 12);
            this.NextButton.Visible = false;
            // 
            // BackButton
            // 
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // CancelWizardButton
            // 
            this.CancelWizardButton.Visible = false;
            // 
            // FinishButton
            // 
            this.FinishButton.Visible = true;
            this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
            // 
            // Browser
            // 
            this.Browser.Location = new System.Drawing.Point(15, 14);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(467, 220);
            this.Browser.TabIndex = 3;
            this.Browser.Url = new System.Uri("http://www.bloodlinesresurgence.com/patchdone.html", System.UriKind.Absolute);
            // 
            // FinalPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "FinalPage";
            this.Text = "FinalPage";
            this.ContentPanel.ResumeLayout(false);
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser Browser;

    }
}