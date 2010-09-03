namespace Resurgence
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResurgenceWizardPage));
            this.CheckForUpdates = new System.Windows.Forms.Timer(this.components);
            this.UpdateCheck = new System.Windows.Forms.LinkLabel();
            this.SendErrorReport = new System.Windows.Forms.Button();
            this.ControlPanel.SuspendLayout();
            this.extendedPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelWizardButton
            // 
            this.CancelWizardButton.Click += new System.EventHandler(this.CancelWizardButton_Click);
            // 
            // LanguageButton
            // 
            this.LanguageButton.Location = new System.Drawing.Point(111, 12);
            this.LanguageButton.Visible = false;
            this.LanguageButton.Click += new System.EventHandler(this.LanguageButton_Click);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.UpdateCheck);
            this.ControlPanel.Controls.SetChildIndex(this.UpdateCheck, 0);
            this.ControlPanel.Controls.SetChildIndex(this.BackButton, 0);
            this.ControlPanel.Controls.SetChildIndex(this.CancelWizardButton, 0);
            this.ControlPanel.Controls.SetChildIndex(this.LanguageButton, 0);
            this.ControlPanel.Controls.SetChildIndex(this.FinishButton, 0);
            this.ControlPanel.Controls.SetChildIndex(this.NextButton, 0);
            // 
            // extendedPanel3
            // 
            this.extendedPanel3.Controls.Add(this.SendErrorReport);
            this.extendedPanel3.Controls.SetChildIndex(this.SendErrorReport, 0);
            this.extendedPanel3.Controls.SetChildIndex(this.WizardTitle, 0);
            this.extendedPanel3.Controls.SetChildIndex(this.WizardDescription, 0);
            // 
            // CheckForUpdates
            // 
            this.CheckForUpdates.Enabled = true;
            this.CheckForUpdates.Tick += new System.EventHandler(this.CheckForUpdates_Tick);
            // 
            // UpdateCheck
            // 
            this.UpdateCheck.AutoSize = true;
            this.UpdateCheck.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.UpdateCheck.Location = new System.Drawing.Point(8, 17);
            this.UpdateCheck.Name = "UpdateCheck";
            this.UpdateCheck.Size = new System.Drawing.Size(112, 13);
            this.UpdateCheck.TabIndex = 7;
            this.UpdateCheck.Text = "Checking for update...";
            this.UpdateCheck.Visible = false;
            this.UpdateCheck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateCheck_LinkClicked);
            // 
            // SendErrorReport
            // 
            this.SendErrorReport.Location = new System.Drawing.Point(389, 7);
            this.SendErrorReport.Name = "SendErrorReport";
            this.SendErrorReport.Size = new System.Drawing.Size(93, 42);
            this.SendErrorReport.TabIndex = 1;
            this.SendErrorReport.Text = "Send Error Report";
            this.SendErrorReport.UseVisualStyleBackColor = true;
            this.SendErrorReport.Visible = false;
            this.SendErrorReport.Click += new System.EventHandler(this.SendErrorReport_Click);
            // 
            // ResurgenceWizardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResurgenceWizardPage";
            this.Text = "ResurgenceWizardPage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ResurgenceWizardPage_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResurgenceWizardPage_FormClosing);
            this.LocationChanged += new System.EventHandler(this.ResurgenceWizardPage_LocationChanged);
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.extendedPanel3.ResumeLayout(false);
            this.extendedPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel UpdateCheck;
        private System.Windows.Forms.Timer CheckForUpdates;
        private System.Windows.Forms.Button SendErrorReport;
    }
}