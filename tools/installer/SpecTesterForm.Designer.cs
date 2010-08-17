namespace BloodlinesResurgenceInstaller
{
    partial class SpecTesterForm
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
            this.ResultsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.RunSpecTestsButton = new System.Windows.Forms.Button();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // ResultsListView
            // 
            this.ResultsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.ResultsListView.Location = new System.Drawing.Point(12, 22);
            this.ResultsListView.Name = "ResultsListView";
            this.ResultsListView.Size = new System.Drawing.Size(445, 194);
            this.ResultsListView.TabIndex = 0;
            this.ResultsListView.UseCompatibleStateImageBehavior = false;
            this.ResultsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Test Name";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Result";
            this.columnHeader2.Width = 600;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Spec Test Results:";
            // 
            // RunSpecTestsButton
            // 
            this.RunSpecTestsButton.Location = new System.Drawing.Point(12, 222);
            this.RunSpecTestsButton.Name = "RunSpecTestsButton";
            this.RunSpecTestsButton.Size = new System.Drawing.Size(97, 23);
            this.RunSpecTestsButton.TabIndex = 2;
            this.RunSpecTestsButton.Text = "Run Spec Tests";
            this.RunSpecTestsButton.UseVisualStyleBackColor = true;
            this.RunSpecTestsButton.Click += new System.EventHandler(this.RunSpecTestsButton_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Module";
            this.columnHeader3.Width = 100;
            // 
            // SpecTesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 273);
            this.Controls.Add(this.RunSpecTestsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResultsListView);
            this.Name = "SpecTesterForm";
            this.Text = "SpecTesterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ResultsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RunSpecTestsButton;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}