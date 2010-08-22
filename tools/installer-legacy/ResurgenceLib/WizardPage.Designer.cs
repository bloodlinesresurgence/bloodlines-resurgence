namespace ResurgenceLib
{
    partial class WizardPage
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
            this.extendedPanel3 = new DaedalusLib.ExtendedPanel();
            this.WizardDescription = new System.Windows.Forms.Label();
            this.WizardTitle = new System.Windows.Forms.Label();
            this.ControlPanel = new DaedalusLib.ExtendedPanel();
            this.NextButton = new System.Windows.Forms.Button();
            this.FinishButton = new System.Windows.Forms.Button();
            this.LanguageButton = new System.Windows.Forms.Button();
            this.horizontalBar1 = new DaedalusLib.HorizontalBar();
            this.CancelWizardButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.ContentPanel = new DaedalusLib.ExtendedPanel();
            this.horizontalBar2 = new DaedalusLib.HorizontalBar();
            this.extendedPanel3.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // extendedPanel3
            // 
            this.extendedPanel3.BackColor = System.Drawing.Color.White;
            this.extendedPanel3.Controls.Add(this.WizardDescription);
            this.extendedPanel3.Controls.Add(this.WizardTitle);
            this.extendedPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.extendedPanel3.ExtendedBorder = false;
            this.extendedPanel3.ExtendedBorderColor = System.Drawing.Color.Black;
            this.extendedPanel3.Location = new System.Drawing.Point(0, 0);
            this.extendedPanel3.Name = "extendedPanel3";
            this.extendedPanel3.Size = new System.Drawing.Size(494, 64);
            this.extendedPanel3.TabIndex = 0;
            // 
            // WizardDescription
            // 
            this.WizardDescription.AutoSize = true;
            this.WizardDescription.Location = new System.Drawing.Point(48, 36);
            this.WizardDescription.Name = "WizardDescription";
            this.WizardDescription.Size = new System.Drawing.Size(72, 13);
            this.WizardDescription.TabIndex = 0;
            this.WizardDescription.Text = "Description ...";
            // 
            // WizardTitle
            // 
            this.WizardTitle.AutoSize = true;
            this.WizardTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WizardTitle.Location = new System.Drawing.Point(12, 9);
            this.WizardTitle.Name = "WizardTitle";
            this.WizardTitle.Size = new System.Drawing.Size(175, 18);
            this.WizardTitle.TabIndex = 0;
            this.WizardTitle.Text = "Bloodlines Resurgence";
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlPanel.Controls.Add(this.NextButton);
            this.ControlPanel.Controls.Add(this.FinishButton);
            this.ControlPanel.Controls.Add(this.LanguageButton);
            this.ControlPanel.Controls.Add(this.horizontalBar1);
            this.ControlPanel.Controls.Add(this.CancelWizardButton);
            this.ControlPanel.Controls.Add(this.BackButton);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ControlPanel.ExtendedBorder = false;
            this.ControlPanel.ExtendedBorderColor = System.Drawing.Color.Black;
            this.ControlPanel.Location = new System.Drawing.Point(0, 304);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(494, 46);
            this.ControlPanel.TabIndex = 1;
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.Location = new System.Drawing.Point(396, 12);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(86, 22);
            this.NextButton.TabIndex = 0;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // FinishButton
            // 
            this.FinishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FinishButton.Location = new System.Drawing.Point(396, 12);
            this.FinishButton.Name = "FinishButton";
            this.FinishButton.Size = new System.Drawing.Size(86, 22);
            this.FinishButton.TabIndex = 6;
            this.FinishButton.Text = "Finish";
            this.FinishButton.UseVisualStyleBackColor = true;
            this.FinishButton.Visible = false;
            // 
            // LanguageButton
            // 
            this.LanguageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LanguageButton.Location = new System.Drawing.Point(15, 12);
            this.LanguageButton.Name = "LanguageButton";
            this.LanguageButton.Size = new System.Drawing.Size(86, 22);
            this.LanguageButton.TabIndex = 5;
            this.LanguageButton.Text = "Language ...";
            this.LanguageButton.UseVisualStyleBackColor = true;
            // 
            // horizontalBar1
            // 
            this.horizontalBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.horizontalBar1.Location = new System.Drawing.Point(0, 0);
            this.horizontalBar1.Name = "horizontalBar1";
            this.horizontalBar1.Size = new System.Drawing.Size(494, 10);
            this.horizontalBar1.TabIndex = 4;
            // 
            // CancelWizardButton
            // 
            this.CancelWizardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelWizardButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelWizardButton.Location = new System.Drawing.Point(203, 12);
            this.CancelWizardButton.Name = "CancelWizardButton";
            this.CancelWizardButton.Size = new System.Drawing.Size(86, 22);
            this.CancelWizardButton.TabIndex = 2;
            this.CancelWizardButton.Text = "Cancel";
            this.CancelWizardButton.UseVisualStyleBackColor = true;
            // 
            // BackButton
            // 
            this.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BackButton.Location = new System.Drawing.Point(300, 12);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(86, 22);
            this.BackButton.TabIndex = 1;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.ContentPanel.Controls.Add(this.horizontalBar2);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.ExtendedBorder = false;
            this.ContentPanel.ExtendedBorderColor = System.Drawing.Color.Black;
            this.ContentPanel.Location = new System.Drawing.Point(0, 64);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(494, 240);
            this.ContentPanel.TabIndex = 5;
            // 
            // horizontalBar2
            // 
            this.horizontalBar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.horizontalBar2.Location = new System.Drawing.Point(0, 0);
            this.horizontalBar2.Name = "horizontalBar2";
            this.horizontalBar2.Size = new System.Drawing.Size(494, 10);
            this.horizontalBar2.TabIndex = 2;
            // 
            // WizardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelWizardButton;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.extendedPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WizardPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bloodlines Resurgence";
            this.extendedPanel3.ResumeLayout(false);
            this.extendedPanel3.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DaedalusLib.ExtendedPanel extendedPanel3;
        private DaedalusLib.ExtendedPanel ControlPanel;

        /// <summary>
        /// Title of the wizard, displayed at the top of the page.
        /// </summary>
        protected System.Windows.Forms.Label WizardTitle;

        /// <summary>
        /// Description for the wizard, displayed below the title.
        /// </summary>
        protected System.Windows.Forms.Label WizardDescription;

        /// <summary>
        /// Content panel. Wizard content should be placed here.
        /// </summary>
        protected DaedalusLib.ExtendedPanel ContentPanel;

        /// <summary>
        /// Next button.
        /// </summary>
        protected System.Windows.Forms.Button NextButton;

        /// <summary>
        /// Back button.
        /// </summary>
        protected System.Windows.Forms.Button BackButton;

        /// <summary>
        /// Cancel button.
        /// </summary>
        protected System.Windows.Forms.Button CancelWizardButton;

        private DaedalusLib.HorizontalBar horizontalBar1;
        private DaedalusLib.HorizontalBar horizontalBar2;

        /// <summary>
        /// Language button.
        /// </summary>
        protected System.Windows.Forms.Button LanguageButton;

        /// <summary>
        /// Finish button.
        /// </summary>
        protected System.Windows.Forms.Button FinishButton;
    }
}

