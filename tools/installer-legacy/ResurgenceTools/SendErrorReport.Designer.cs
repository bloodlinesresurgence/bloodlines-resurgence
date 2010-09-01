namespace ResurgenceTools
{
    partial class SendErrorReport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.AdditionalInformation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SendReport = new System.Windows.Forms.Button();
            this.EmailEntry = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NameEntry = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ReportIntro = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.CurrentAction = new System.Windows.Forms.Label();
            this.ReportContentsBox = new System.Windows.Forms.GroupBox();
            this.Contents = new System.Windows.Forms.TextBox();
            this.BeginFindFiles = new System.Windows.Forms.Timer(this.components);
            this.UpdateReports = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ReportContentsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AdditionalInformation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SendReport);
            this.panel1.Controls.Add(this.EmailEntry);
            this.panel1.Controls.Add(this.Email);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.NameEntry);
            this.panel1.Controls.Add(this.NameLabel);
            this.panel1.Controls.Add(this.ReportIntro);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 167);
            this.panel1.TabIndex = 0;
            // 
            // AdditionalInformation
            // 
            this.AdditionalInformation.Location = new System.Drawing.Point(131, 105);
            this.AdditionalInformation.Multiline = true;
            this.AdditionalInformation.Name = "AdditionalInformation";
            this.AdditionalInformation.Size = new System.Drawing.Size(320, 56);
            this.AdditionalInformation.TabIndex = 8;
            this.AdditionalInformation.TextChanged += new System.EventHandler(this.AdditionalInformation_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 56);
            this.label2.TabIndex = 7;
            this.label2.Text = "Additional information: (Optional)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SendReport
            // 
            this.SendReport.Location = new System.Drawing.Point(376, 41);
            this.SendReport.Name = "SendReport";
            this.SendReport.Size = new System.Drawing.Size(75, 23);
            this.SendReport.TabIndex = 6;
            this.SendReport.Text = "Send Report";
            this.SendReport.UseVisualStyleBackColor = true;
            // 
            // EmailEntry
            // 
            this.EmailEntry.Location = new System.Drawing.Point(58, 72);
            this.EmailEntry.Name = "EmailEntry";
            this.EmailEntry.Size = new System.Drawing.Size(173, 20);
            this.EmailEntry.TabIndex = 5;
            this.EmailEntry.TextChanged += new System.EventHandler(this.EmailEntry_TextChanged);
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(17, 75);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(35, 13);
            this.Email.TabIndex = 4;
            this.Email.Text = "Email:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "(Optional)";
            // 
            // NameEntry
            // 
            this.NameEntry.Location = new System.Drawing.Point(58, 43);
            this.NameEntry.Name = "NameEntry";
            this.NameEntry.Size = new System.Drawing.Size(173, 20);
            this.NameEntry.TabIndex = 2;
            this.NameEntry.TextChanged += new System.EventHandler(this.NameEntry_TextChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 46);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Name:";
            // 
            // ReportIntro
            // 
            this.ReportIntro.Location = new System.Drawing.Point(12, 9);
            this.ReportIntro.Name = "ReportIntro";
            this.ReportIntro.Size = new System.Drawing.Size(439, 30);
            this.ReportIntro.TabIndex = 0;
            this.ReportIntro.Text = "Sending an error report will help us track down problems you are having with the " +
                "installation process.";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.Progress);
            this.panel2.Controls.Add(this.CurrentAction);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 339);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(463, 37);
            this.panel2.TabIndex = 2;
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(170, 4);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(288, 25);
            this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Progress.TabIndex = 1;
            this.Progress.Visible = false;
            // 
            // CurrentAction
            // 
            this.CurrentAction.AutoSize = true;
            this.CurrentAction.Location = new System.Drawing.Point(3, 13);
            this.CurrentAction.Name = "CurrentAction";
            this.CurrentAction.Size = new System.Drawing.Size(71, 13);
            this.CurrentAction.TabIndex = 0;
            this.CurrentAction.Text = "CurrentAction";
            this.CurrentAction.Visible = false;
            // 
            // ReportContentsBox
            // 
            this.ReportContentsBox.Controls.Add(this.Contents);
            this.ReportContentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportContentsBox.Location = new System.Drawing.Point(0, 167);
            this.ReportContentsBox.Name = "ReportContentsBox";
            this.ReportContentsBox.Size = new System.Drawing.Size(463, 172);
            this.ReportContentsBox.TabIndex = 3;
            this.ReportContentsBox.TabStop = false;
            this.ReportContentsBox.Text = "Report Contents";
            // 
            // Contents
            // 
            this.Contents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Contents.Location = new System.Drawing.Point(3, 16);
            this.Contents.Multiline = true;
            this.Contents.Name = "Contents";
            this.Contents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Contents.Size = new System.Drawing.Size(457, 153);
            this.Contents.TabIndex = 0;
            // 
            // BeginFindFiles
            // 
            this.BeginFindFiles.Enabled = true;
            this.BeginFindFiles.Tick += new System.EventHandler(this.BeginFindFiles_Tick);
            // 
            // UpdateReports
            // 
            this.UpdateReports.Interval = 500;
            this.UpdateReports.Tick += new System.EventHandler(this.UpdateReports_Tick);
            // 
            // SendErrorReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 376);
            this.Controls.Add(this.ReportContentsBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SendErrorReport";
            this.Text = "SendErrorReport";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ReportContentsBox.ResumeLayout(false);
            this.ReportContentsBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SendReport;
        private System.Windows.Forms.TextBox EmailEntry;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameEntry;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label ReportIntro;
        private System.Windows.Forms.TextBox AdditionalInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox ReportContentsBox;
        private System.Windows.Forms.TextBox Contents;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label CurrentAction;
        private System.Windows.Forms.Timer BeginFindFiles;
        private System.Windows.Forms.Timer UpdateReports;
    }
}