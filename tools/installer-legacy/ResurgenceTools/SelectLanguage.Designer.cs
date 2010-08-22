namespace ResurgenceTools
{
    partial class SelectLanguage
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
            this.LanguageList = new System.Windows.Forms.ListBox();
            this.Button_Change = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.LanguageMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTranslationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseTranslationsDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanguageMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LanguageList
            // 
            this.LanguageList.ContextMenuStrip = this.LanguageMenu;
            this.LanguageList.FormattingEnabled = true;
            this.LanguageList.Location = new System.Drawing.Point(12, 12);
            this.LanguageList.Name = "LanguageList";
            this.LanguageList.Size = new System.Drawing.Size(253, 95);
            this.LanguageList.TabIndex = 0;
            this.LanguageList.DoubleClick += new System.EventHandler(this.LanguageList_DoubleClick);
            // 
            // Button_Change
            // 
            this.Button_Change.Location = new System.Drawing.Point(12, 113);
            this.Button_Change.Name = "Button_Change";
            this.Button_Change.Size = new System.Drawing.Size(75, 23);
            this.Button_Change.TabIndex = 1;
            this.Button_Change.Text = "Change";
            this.Button_Change.UseVisualStyleBackColor = true;
            this.Button_Change.Click += new System.EventHandler(this.Button_Change_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(190, 113);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // LanguageMenu
            // 
            this.LanguageMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editFileToolStripMenuItem,
            this.newTranslationToolStripMenuItem,
            this.browseTranslationsDirectoryToolStripMenuItem});
            this.LanguageMenu.Name = "LanguageMenu";
            this.LanguageMenu.Size = new System.Drawing.Size(231, 92);
            // 
            // editFileToolStripMenuItem
            // 
            this.editFileToolStripMenuItem.Name = "editFileToolStripMenuItem";
            this.editFileToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.editFileToolStripMenuItem.Text = "Edit File";
            this.editFileToolStripMenuItem.Click += new System.EventHandler(this.editFileToolStripMenuItem_Click);
            // 
            // newTranslationToolStripMenuItem
            // 
            this.newTranslationToolStripMenuItem.Name = "newTranslationToolStripMenuItem";
            this.newTranslationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newTranslationToolStripMenuItem.Text = "New Translation";
            this.newTranslationToolStripMenuItem.Click += new System.EventHandler(this.newTranslationToolStripMenuItem_Click);
            // 
            // browseTranslationsDirectoryToolStripMenuItem
            // 
            this.browseTranslationsDirectoryToolStripMenuItem.Name = "browseTranslationsDirectoryToolStripMenuItem";
            this.browseTranslationsDirectoryToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.browseTranslationsDirectoryToolStripMenuItem.Text = "Browse Translations Directory";
            this.browseTranslationsDirectoryToolStripMenuItem.Click += new System.EventHandler(this.browseTranslationsDirectoryToolStripMenuItem_Click);
            // 
            // SelectLanguage
            // 
            this.AcceptButton = this.Button_Change;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(277, 144);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Change);
            this.Controls.Add(this.LanguageList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectLanguage";
            this.Load += new System.EventHandler(this.SelectLanguage_Load);
            this.LanguageMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LanguageList;
        private System.Windows.Forms.Button Button_Change;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.ContextMenuStrip LanguageMenu;
        private System.Windows.Forms.ToolStripMenuItem editFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTranslationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseTranslationsDirectoryToolStripMenuItem;
    }
}