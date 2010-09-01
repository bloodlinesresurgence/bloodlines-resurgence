namespace Resurgence
{
    partial class Preparing
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
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Location = new System.Drawing.Point(12, 9);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(165, 13);
            this.Description.TabIndex = 0;
            this.Description.Text = "Preparing data files, please wait...";
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(12, 25);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(275, 23);
            this.Progress.TabIndex = 1;
            // 
            // Preparing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 58);
            this.ControlBox = false;
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.Description);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preparing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bloodlines Revial Toolkit";
            this.Shown += new System.EventHandler(this.Preparing_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Description;
        internal System.Windows.Forms.ProgressBar Progress;
    }
}