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
            this.Document = new System.Windows.Forms.RichTextBox();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.Document);
            this.ContentPanel.Controls.SetChildIndex(this.Document, 0);
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
            // Document
            // 
            this.Document.Location = new System.Drawing.Point(12, 6);
            this.Document.Name = "Document";
            this.Document.Size = new System.Drawing.Size(470, 228);
            this.Document.TabIndex = 6;
            this.Document.Text = "";
            this.Document.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.Document_LinkClicked);
            // 
            // FinalPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "FinalPage";
            this.Text = "FinalPage";
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Document;
    }
}