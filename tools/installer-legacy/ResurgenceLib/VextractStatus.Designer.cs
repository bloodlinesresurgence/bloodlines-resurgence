namespace ResurgenceLib
{
    partial class VextractStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.VextractWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StartButton = new System.Windows.Forms.Button();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.CancelButton = new System.Windows.Forms.Button();
            this.UpdateUITimer = new System.Windows.Forms.Timer(this.components);
            this.Log = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VextractWorker
            // 
            this.VextractWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.VextractWorker_DoWork);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.StartButton);
            this.panel1.Controls.Add(this.Progress);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 23);
            this.panel1.TabIndex = 2;
            // 
            // StartButton
            // 
            this.StartButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(150, 0);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "#Start#";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Visible = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Progress
            // 
            this.Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Progress.Location = new System.Drawing.Point(0, 0);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(225, 23);
            this.Progress.TabIndex = 3;
            // 
            // CancelButton
            // 
            this.CancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(225, 0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "#Cancel#";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Visible = false;
            this.CancelButton.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // UpdateUITimer
            // 
            this.UpdateUITimer.Interval = 300;
            this.UpdateUITimer.Tick += new System.EventHandler(this.UpdateUITimer_Tick);
            // 
            // Log
            // 
            this.Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log.Location = new System.Drawing.Point(0, 0);
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Log.Size = new System.Drawing.Size(300, 147);
            this.Log.TabIndex = 3;
            this.Log.Text = "";
            // 
            // VextractStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Log);
            this.Controls.Add(this.panel1);
            this.Name = "VextractStatus";
            this.Size = new System.Drawing.Size(300, 170);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker VextractWorker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Timer UpdateUITimer;
        private System.Windows.Forms.RichTextBox Log;
        private System.Windows.Forms.Button StartButton;
    }
}
