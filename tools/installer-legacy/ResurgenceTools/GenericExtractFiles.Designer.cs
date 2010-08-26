namespace Resurgence.Steps
{
    partial class GenericExtractFiles
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
            this.Description = new System.Windows.Forms.Label();
            this.Vextract = new ResurgenceLib.VextractStatus();
            this.ContentPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.Description);
            this.ContentPanel.Controls.Add(this.Vextract);
            this.ContentPanel.Controls.SetChildIndex(this.Vextract, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Description, 0);
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
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Location = new System.Drawing.Point(12, 81);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(122, 13);
            this.Description.TabIndex = 4;
            this.Description.Text = "Extracting general files...";
            // 
            // Vextract
            // 
            this.Vextract.ExemptionPatterns = null;
            this.Vextract.Location = new System.Drawing.Point(15, 97);
            this.Vextract.Name = "Vextract";
            this.Vextract.OutputDirectory = null;
            this.Vextract.Pattern = null;
            this.Vextract.Size = new System.Drawing.Size(467, 137);
            this.Vextract.SkeletonOnly = false;
            this.Vextract.TabIndex = 3;
            this.Vextract.VPKDirectory = null;
            // 
            // GenericExtractFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "GenericExtractFiles";
            this.Text = "GenericExtractFiles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtractGeneralFiles_FormClosing);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal ResurgenceLib.VextractStatus Vextract;
        protected internal System.Windows.Forms.Label Description;
    }
}