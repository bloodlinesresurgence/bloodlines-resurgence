namespace Resurgence.WizardPages
{
    partial class SelectSteps
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
            this.Instruction = new System.Windows.Forms.Label();
            this.Steps = new System.Windows.Forms.CheckedListBox();
            this.DescriptionBox = new System.Windows.Forms.GroupBox();
            this.CurrentItem = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.Label();
            this.IgnoreCancelExtraction = new System.Windows.Forms.CheckBox();
            this.AutoProceed = new System.Windows.Forms.CheckBox();
            this.ContentPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.extendedPanel3.SuspendLayout();
            this.DescriptionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardDescription
            // 
            this.WizardDescription.Size = new System.Drawing.Size(67, 13);
            this.WizardDescription.Text = "Select Steps";
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.AutoProceed);
            this.ContentPanel.Controls.Add(this.IgnoreCancelExtraction);
            this.ContentPanel.Controls.Add(this.DescriptionBox);
            this.ContentPanel.Controls.Add(this.Steps);
            this.ContentPanel.Controls.Add(this.Instruction);
            this.ContentPanel.Controls.SetChildIndex(this.Instruction, 0);
            this.ContentPanel.Controls.SetChildIndex(this.Steps, 0);
            this.ContentPanel.Controls.SetChildIndex(this.DescriptionBox, 0);
            this.ContentPanel.Controls.SetChildIndex(this.IgnoreCancelExtraction, 0);
            this.ContentPanel.Controls.SetChildIndex(this.AutoProceed, 0);
            // 
            // NextButton
            // 
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // Instruction
            // 
            this.Instruction.Location = new System.Drawing.Point(12, 3);
            this.Instruction.Name = "Instruction";
            this.Instruction.Size = new System.Drawing.Size(277, 32);
            this.Instruction.TabIndex = 0;
            this.Instruction.Text = "Select the steps to run from the list below:";
            this.Instruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Steps
            // 
            this.Steps.CheckOnClick = true;
            this.Steps.FormattingEnabled = true;
            this.Steps.Location = new System.Drawing.Point(15, 38);
            this.Steps.Name = "Steps";
            this.Steps.Size = new System.Drawing.Size(274, 169);
            this.Steps.TabIndex = 1;
            this.Steps.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Steps_MouseMove);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Controls.Add(this.CurrentItem);
            this.DescriptionBox.Controls.Add(this.Description);
            this.DescriptionBox.Location = new System.Drawing.Point(300, 31);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(182, 176);
            this.DescriptionBox.TabIndex = 2;
            this.DescriptionBox.TabStop = false;
            this.DescriptionBox.Text = "Step Description";
            // 
            // CurrentItem
            // 
            this.CurrentItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentItem.Location = new System.Drawing.Point(6, 16);
            this.CurrentItem.Name = "CurrentItem";
            this.CurrentItem.Size = new System.Drawing.Size(170, 33);
            this.CurrentItem.TabIndex = 1;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(6, 49);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(170, 113);
            this.Description.TabIndex = 0;
            this.Description.Text = "Move your mouse over an item to view its description.";
            // 
            // IgnoreCancelExtraction
            // 
            this.IgnoreCancelExtraction.Location = new System.Drawing.Point(300, 8);
            this.IgnoreCancelExtraction.Name = "IgnoreCancelExtraction";
            this.IgnoreCancelExtraction.Size = new System.Drawing.Size(191, 17);
            this.IgnoreCancelExtraction.TabIndex = 2;
            this.IgnoreCancelExtraction.Text = "Treat Cancel as Success (DBG)";
            this.IgnoreCancelExtraction.UseVisualStyleBackColor = true;
            this.IgnoreCancelExtraction.Visible = false;
            this.IgnoreCancelExtraction.CheckedChanged += new System.EventHandler(this.IgnoreCancelExtraction_CheckedChanged);
            // 
            // AutoProceed
            // 
            this.AutoProceed.AutoSize = true;
            this.AutoProceed.Location = new System.Drawing.Point(15, 213);
            this.AutoProceed.Name = "AutoProceed";
            this.AutoProceed.Size = new System.Drawing.Size(310, 17);
            this.AutoProceed.TabIndex = 3;
            this.AutoProceed.Text = "Proceed to next dialogue automatically as each step finishes";
            this.AutoProceed.UseVisualStyleBackColor = true;
            this.AutoProceed.Visible = false;
            // 
            // SelectSteps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 350);
            this.Name = "SelectSteps";
            this.Text = "SelectSteps";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.extendedPanel3.ResumeLayout(false);
            this.extendedPanel3.PerformLayout();
            this.DescriptionBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Instruction;
        private System.Windows.Forms.CheckedListBox Steps;
        private System.Windows.Forms.GroupBox DescriptionBox;
        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.Label CurrentItem;
        private System.Windows.Forms.CheckBox IgnoreCancelExtraction;
        private System.Windows.Forms.CheckBox AutoProceed;
    }
}