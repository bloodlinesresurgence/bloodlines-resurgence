namespace Resurgence.WizardPages
{
    partial class SetupMod
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
            this.label1 = new System.Windows.Forms.Label();
            this.InstallSDK = new System.Windows.Forms.CheckBox();
            this.LaunchSDK = new System.Windows.Forms.CheckBox();
            this.SelectEngine = new System.Windows.Forms.CheckBox();
            this.CreateMod = new System.Windows.Forms.CheckBox();
            this.StepModify = new System.Windows.Forms.CheckBox();
            this.FirstTextbox = new System.Windows.Forms.CheckBox();
            this.SecondTextbox = new System.Windows.Forms.CheckBox();
            this.Wait = new System.Windows.Forms.CheckBox();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(59, 13);
            this.WizardDescription.Text = "Setup Mod";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.label1);
            this.ContentPanel.Controls.Add(this.SelectEngine);
            this.ContentPanel.Controls.Add(this.InstallSDK);
            this.ContentPanel.Controls.Add(this.CreateMod);
            this.ContentPanel.Controls.Add(this.StepModify);
            this.ContentPanel.Controls.Add(this.FirstTextbox);
            this.ContentPanel.Controls.Add(this.SecondTextbox);
            this.ContentPanel.Controls.Add(this.Wait);
            this.ContentPanel.Controls.Add(this.LaunchSDK);
            this.ContentPanel.Controls.SetChildIndex(this.LaunchSDK, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Wait, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SecondTextbox, 0);
            this.ContentPanel.Controls.SetChildIndex(this.FirstTextbox, 0);
            this.ContentPanel.Controls.SetChildIndex(this.StepModify, 0);
            this.ContentPanel.Controls.SetChildIndex(this.CreateMod, 0);
            this.ContentPanel.Controls.SetChildIndex(this.InstallSDK, 0);
            this.ContentPanel.Controls.SetChildIndex(this.SelectEngine, 0);
            this.ContentPanel.Controls.SetChildIndex(this.label1, 0);
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(445, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please perform the following steps manually, and tick each box as you complete th" +
                "em.";
            // 
            // InstallSDK
            // 
            this.InstallSDK.AutoSize = true;
            this.InstallSDK.Location = new System.Drawing.Point(15, 48);
            this.InstallSDK.Name = "InstallSDK";
            this.InstallSDK.Size = new System.Drawing.Size(133, 17);
            this.InstallSDK.TabIndex = 4;
            this.InstallSDK.Text = "Install the Source SDK";
            this.InstallSDK.UseVisualStyleBackColor = true;
            this.InstallSDK.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // LaunchSDK
            // 
            this.LaunchSDK.AutoSize = true;
            this.LaunchSDK.Location = new System.Drawing.Point(15, 71);
            this.LaunchSDK.Name = "LaunchSDK";
            this.LaunchSDK.Size = new System.Drawing.Size(142, 17);
            this.LaunchSDK.TabIndex = 4;
            this.LaunchSDK.Text = "Launch the Source SDK";
            this.LaunchSDK.UseVisualStyleBackColor = true;
            this.LaunchSDK.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // SelectEngine
            // 
            this.SelectEngine.AutoSize = true;
            this.SelectEngine.Location = new System.Drawing.Point(15, 94);
            this.SelectEngine.Name = "SelectEngine";
            this.SelectEngine.Size = new System.Drawing.Size(195, 17);
            this.SelectEngine.TabIndex = 5;
            this.SelectEngine.Text = "Select engine version: Source 2007";
            this.SelectEngine.UseVisualStyleBackColor = true;
            this.SelectEngine.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // CreateMod
            // 
            this.CreateMod.AutoSize = true;
            this.CreateMod.Location = new System.Drawing.Point(15, 117);
            this.CreateMod.Name = "CreateMod";
            this.CreateMod.Size = new System.Drawing.Size(90, 17);
            this.CreateMod.TabIndex = 5;
            this.CreateMod.Text = "Create a Mod";
            this.CreateMod.UseVisualStyleBackColor = true;
            this.CreateMod.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // StepModify
            // 
            this.StepModify.AutoSize = true;
            this.StepModify.Location = new System.Drawing.Point(15, 140);
            this.StepModify.Name = "StepModify";
            this.StepModify.Size = new System.Drawing.Size(168, 17);
            this.StepModify.TabIndex = 5;
            this.StepModify.Text = "Modify Half-Life 2 Singleplayer";
            this.StepModify.UseVisualStyleBackColor = true;
            this.StepModify.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // FirstTextbox
            // 
            this.FirstTextbox.AutoSize = true;
            this.FirstTextbox.Location = new System.Drawing.Point(15, 163);
            this.FirstTextbox.Name = "FirstTextbox";
            this.FirstTextbox.Size = new System.Drawing.Size(375, 17);
            this.FirstTextbox.TabIndex = 5;
            this.FirstTextbox.Text = "In the first text box, enter directory for mod files (eg, C:\\Mods\\Resurgence)";
            this.FirstTextbox.UseVisualStyleBackColor = true;
            this.FirstTextbox.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // SecondTextbox
            // 
            this.SecondTextbox.AutoSize = true;
            this.SecondTextbox.Location = new System.Drawing.Point(15, 186);
            this.SecondTextbox.Name = "SecondTextbox";
            this.SecondTextbox.Size = new System.Drawing.Size(373, 17);
            this.SecondTextbox.TabIndex = 5;
            this.SecondTextbox.Text = "In the second text box, enter the name of the mod: BloodlinesResurgence";
            this.SecondTextbox.UseVisualStyleBackColor = true;
            this.SecondTextbox.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // Wait
            // 
            this.Wait.AutoSize = true;
            this.Wait.Location = new System.Drawing.Point(15, 209);
            this.Wait.Name = "Wait";
            this.Wait.Size = new System.Drawing.Size(239, 17);
            this.Wait.TabIndex = 5;
            this.Wait.Text = "Proceed, and wait until the installer is finished";
            this.Wait.UseVisualStyleBackColor = true;
            this.Wait.CheckedChanged += new System.EventHandler(this.InstallSDK_CheckedChanged);
            // 
            // SetupMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "SetupMod";
            this.Text = "Bloodlines Resurgence :: Setup Mod";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SelectEngine;
        private System.Windows.Forms.CheckBox InstallSDK;
        private System.Windows.Forms.CheckBox LaunchSDK;
        private System.Windows.Forms.CheckBox CreateMod;
        private System.Windows.Forms.CheckBox StepModify;
        private System.Windows.Forms.CheckBox FirstTextbox;
        private System.Windows.Forms.CheckBox SecondTextbox;
        private System.Windows.Forms.CheckBox Wait;
    }
}