namespace Resurgence.WizardPages
{
    partial class Readme
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
            this.Understand = new System.Windows.Forms.CheckBox();
            this.Document = new System.Windows.Forms.RichTextBox();
            this.ContentPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.extendedPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.Document);
            this.ContentPanel.Controls.Add(this.Understand);
            this.ContentPanel.Controls.SetChildIndex(this.Understand, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Document, 0);
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
            // Understand
            // 
            this.Understand.AutoSize = true;
            this.Understand.Location = new System.Drawing.Point(12, 218);
            this.Understand.Name = "Understand";
            this.Understand.Size = new System.Drawing.Size(187, 17);
            this.Understand.TabIndex = 4;
            this.Understand.Text = "I understand the above conditions";
            this.Understand.UseVisualStyleBackColor = true;
            this.Understand.CheckedChanged += new System.EventHandler(this.Understand_CheckedChanged);
            // 
            // Document
            // 
            this.Document.Location = new System.Drawing.Point(13, 11);
            this.Document.Name = "Document";
            this.Document.ReadOnly = true;
            this.Document.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Document.Size = new System.Drawing.Size(469, 196);
            this.Document.TabIndex = 5;
            this.Document.Text = "";
            this.Document.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.Document_LinkClicked);
            // 
            // Readme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "Readme";
            this.Text = "Readme";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.extendedPanel3.ResumeLayout(false);
            this.extendedPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Document;
        private System.Windows.Forms.CheckBox Understand;
    }
}