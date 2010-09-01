namespace Resurgence.WizardPages
{
    partial class SelectOperation
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
            this.InstallOption = new System.Windows.Forms.RadioButton();
            this.Install = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdateOption = new System.Windows.Forms.RadioButton();
            this.SelectSteps = new System.Windows.Forms.CheckBox();
            this.DebugLog = new System.Windows.Forms.CheckBox();
            this.DebugLogHelp = new System.Windows.Forms.Button();
            this.ContentPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(136, 13);
            this.WizardDescription.Text = "What would you like to do?";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.DebugLogHelp);
            this.ContentPanel.Controls.Add(this.DebugLog);
            this.ContentPanel.Controls.Add(this.SelectSteps);
            this.ContentPanel.Controls.Add(this.InstallOption);
            this.ContentPanel.Controls.Add(this.UpdateOption);
            this.ContentPanel.Controls.Add(this.Install);
            this.ContentPanel.Controls.Add(this.label1);
            this.ContentPanel.Controls.SetChildIndex(this.label1, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Install, 0);
            this.ContentPanel.Controls.SetChildIndex(this.UpdateOption, 0);
            this.ContentPanel.Controls.SetChildIndex(this.InstallOption, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SelectSteps, 0);
            this.ContentPanel.Controls.SetChildIndex(this.DebugLog, 0);
            this.ContentPanel.Controls.SetChildIndex(this.DebugLogHelp, 0);
            // 
            // NextButton
            // 
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // InstallOption
            // 
            this.InstallOption.AutoSize = true;
            this.InstallOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstallOption.Location = new System.Drawing.Point(15, 16);
            this.InstallOption.Name = "InstallOption";
            this.InstallOption.Size = new System.Drawing.Size(233, 20);
            this.InstallOption.TabIndex = 3;
            this.InstallOption.TabStop = true;
            this.InstallOption.Text = "Install Bloodlines Resurgence";
            this.InstallOption.UseVisualStyleBackColor = true;
            // 
            // Install
            // 
            this.Install.AutoSize = true;
            this.Install.Location = new System.Drawing.Point(34, 39);
            this.Install.Name = "Install";
            this.Install.Size = new System.Drawing.Size(248, 13);
            this.Install.TabIndex = 4;
            this.Install.Text = "Perform a full install. You must do this at least once.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Update your existing installation to the latest version.";
            // 
            // UpdateOption
            // 
            this.UpdateOption.AutoSize = true;
            this.UpdateOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateOption.Location = new System.Drawing.Point(15, 66);
            this.UpdateOption.Name = "UpdateOption";
            this.UpdateOption.Size = new System.Drawing.Size(243, 20);
            this.UpdateOption.TabIndex = 3;
            this.UpdateOption.TabStop = true;
            this.UpdateOption.Text = "Update Bloodlines Resurgence";
            this.UpdateOption.UseVisualStyleBackColor = true;
            // 
            // SelectSteps
            // 
            this.SelectSteps.AutoSize = true;
            this.SelectSteps.Location = new System.Drawing.Point(15, 140);
            this.SelectSteps.Name = "SelectSteps";
            this.SelectSteps.Size = new System.Drawing.Size(187, 17);
            this.SelectSteps.TabIndex = 5;
            this.SelectSteps.Text = "Advanced: Select the steps to run";
            this.SelectSteps.UseVisualStyleBackColor = true;
            // 
            // DebugLog
            // 
            this.DebugLog.AutoSize = true;
            this.DebugLog.Location = new System.Drawing.Point(15, 217);
            this.DebugLog.Name = "DebugLog";
            this.DebugLog.Size = new System.Drawing.Size(159, 17);
            this.DebugLog.TabIndex = 6;
            this.DebugLog.Text = "DEBUG: Enable Debug Log";
            this.DebugLog.UseVisualStyleBackColor = true;
            // 
            // DebugLogHelp
            // 
            this.DebugLogHelp.Location = new System.Drawing.Point(203, 211);
            this.DebugLogHelp.Name = "DebugLogHelp";
            this.DebugLogHelp.Size = new System.Drawing.Size(29, 23);
            this.DebugLogHelp.TabIndex = 7;
            this.DebugLogHelp.Text = "?";
            this.DebugLogHelp.UseVisualStyleBackColor = true;
            this.DebugLogHelp.Click += new System.EventHandler(this.DebugLogHelp_Click);
            // 
            // SelectOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "SelectOperation";
            this.Text = "SelectOperation";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton InstallOption;
        private System.Windows.Forms.RadioButton UpdateOption;
        private System.Windows.Forms.Label Install;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SelectSteps;
        private System.Windows.Forms.Button DebugLogHelp;
        private System.Windows.Forms.CheckBox DebugLog;
    }
}