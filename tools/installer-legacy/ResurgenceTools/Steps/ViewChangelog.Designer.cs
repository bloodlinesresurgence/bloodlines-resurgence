namespace Resurgence.Steps
{
    partial class ViewChangelog
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
            this.Changelog = new System.Windows.Forms.RichTextBox();
            this.ContentPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Progress
            // 
            this.Progress.Visible = false;
            // 
            // Description
            // 
            this.Description.Visible = false;
            // 
            // StartStop
            // 
            this.StartStop.Visible = false;
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.Changelog);
            this.ContentPanel.Controls.SetChildIndex(this.StartStop, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Log, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Description, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Progress, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Changelog, 0);
            // 
            // Changelog
            // 
            this.Changelog.Location = new System.Drawing.Point(15, 17);
            this.Changelog.Name = "Changelog";
            this.Changelog.Size = new System.Drawing.Size(467, 217);
            this.Changelog.TabIndex = 8;
            this.Changelog.Text = "";
            // 
            // ViewChangelog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "ViewChangelog";
            this.Text = "ViewChangelog";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Changelog;
    }
}