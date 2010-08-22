namespace ResurgenceTools
{
    partial class GenericRunProcess
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
            this.Log = new System.Windows.Forms.RichTextBox();
            this.StartStop = new System.Windows.Forms.Button();
            this.BackgroundProcessor = new System.ComponentModel.BackgroundWorker();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.Progress);
            this.ContentPanel.Controls.Add(this.Description);
            this.ContentPanel.Controls.Add(this.StartStop);
            this.ContentPanel.Controls.Add(this.Log);
            this.ContentPanel.Controls.SetChildIndex(this.Log, 0);
            this.ContentPanel.Controls.SetChildIndex(this.StartStop, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Description, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Progress, 0);
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
            this.Description.Location = new System.Drawing.Point(12, 13);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(69, 13);
            this.Description.TabIndex = 3;
            this.Description.Text = "Description...";
            // 
            // Log
            // 
            this.Log.Location = new System.Drawing.Point(15, 29);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(467, 177);
            this.Log.TabIndex = 4;
            this.Log.Text = "";
            // 
            // StartStop
            // 
            this.StartStop.Location = new System.Drawing.Point(396, 212);
            this.StartStop.Name = "StartStop";
            this.StartStop.Size = new System.Drawing.Size(86, 22);
            this.StartStop.TabIndex = 6;
            this.StartStop.Text = "#Start#";
            this.StartStop.UseVisualStyleBackColor = true;
            this.StartStop.Click += new System.EventHandler(this.StartStop_Click);
            // 
            // BackgroundProcessor
            // 
            this.BackgroundProcessor.WorkerReportsProgress = true;
            this.BackgroundProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundProcessor_DoWork);
            this.BackgroundProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundProcessor_RunWorkerCompleted);
            this.BackgroundProcessor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundProcessor_ProgressChanged);
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(15, 212);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(371, 22);
            this.Progress.TabIndex = 7;
            // 
            // GenericRunProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "GenericRunProcess";
            this.Text = "ConvertAndCopyMaterials";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.RichTextBox Log;
        private System.Windows.Forms.Button StartStop;
        protected System.ComponentModel.BackgroundWorker BackgroundProcessor;
        protected System.Windows.Forms.ProgressBar Progress;
    }
}