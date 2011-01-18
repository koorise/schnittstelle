namespace Schnittstelle_NX_GUI
{
    partial class MainForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmdExtract = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grpPathSettings = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdBrowseSimFile = new System.Windows.Forms.Button();
            this.txtSimFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBrowsePartFile = new System.Windows.Forms.Button();
            this.txtPartFile = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panSelection = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDeselectAll = new System.Windows.Forms.LinkLabel();
            this.lblSelectAll = new System.Windows.Forms.LinkLabel();
            this.lstMethods = new System.Windows.Forms.CheckedListBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.grpPathSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "prt";
            this.openFileDialog1.Filter = "NX Part File|*.prt";
            this.openFileDialog1.ReadOnlyChecked = true;
            // 
            // cmdExtract
            // 
            this.cmdExtract.Enabled = false;
            this.cmdExtract.Location = new System.Drawing.Point(406, 391);
            this.cmdExtract.Name = "cmdExtract";
            this.cmdExtract.Size = new System.Drawing.Size(122, 23);
            this.cmdExtract.TabIndex = 1;
            this.cmdExtract.Text = "Extract selected...";
            this.cmdExtract.UseVisualStyleBackColor = true;
            this.cmdExtract.Click += new System.EventHandler(this.cmdExtract_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(278, 391);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(122, 23);
            this.cmdLoad.TabIndex = 5;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(538, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.infoToolStripMenuItem.Text = "&File";
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.browseToolStripMenuItem.Text = "&Browse";
            this.browseToolStripMenuItem.Click += new System.EventHandler(this.browseToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(106, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractSelectedToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.actionToolStripMenuItem.Text = "&Action";
            // 
            // extractSelectedToolStripMenuItem
            // 
            this.extractSelectedToolStripMenuItem.Enabled = false;
            this.extractSelectedToolStripMenuItem.Name = "extractSelectedToolStripMenuItem";
            this.extractSelectedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.extractSelectedToolStripMenuItem.Text = "&Extract selected...";
            this.extractSelectedToolStripMenuItem.Click += new System.EventHandler(this.extractSelectedToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(24, 20);
            this.helpToolStripMenuItem.Text = "?";
            // 
            // infoToolStripMenuItem1
            // 
            this.infoToolStripMenuItem1.Name = "infoToolStripMenuItem1";
            this.infoToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.infoToolStripMenuItem1.Text = "About";
            this.infoToolStripMenuItem1.Click += new System.EventHandler(this.infoToolStripMenuItem1_Click);
            // 
            // grpPathSettings
            // 
            this.grpPathSettings.Controls.Add(this.label3);
            this.grpPathSettings.Controls.Add(this.cmdBrowseSimFile);
            this.grpPathSettings.Controls.Add(this.txtSimFile);
            this.grpPathSettings.Controls.Add(this.label1);
            this.grpPathSettings.Controls.Add(this.cmdBrowsePartFile);
            this.grpPathSettings.Controls.Add(this.txtPartFile);
            this.grpPathSettings.Location = new System.Drawing.Point(7, 27);
            this.grpPathSettings.Name = "grpPathSettings";
            this.grpPathSettings.Size = new System.Drawing.Size(521, 116);
            this.grpPathSettings.TabIndex = 13;
            this.grpPathSettings.TabStop = false;
            this.grpPathSettings.Text = "Path Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Simulation File:";
            // 
            // cmdBrowseSimFile
            // 
            this.cmdBrowseSimFile.Location = new System.Drawing.Point(436, 77);
            this.cmdBrowseSimFile.Name = "cmdBrowseSimFile";
            this.cmdBrowseSimFile.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseSimFile.TabIndex = 23;
            this.cmdBrowseSimFile.Text = "Browse...";
            this.cmdBrowseSimFile.UseVisualStyleBackColor = true;
            this.cmdBrowseSimFile.Click += new System.EventHandler(this.cmdBrowseSimFile_Click);
            // 
            // txtSimFile
            // 
            this.txtSimFile.Location = new System.Drawing.Point(12, 79);
            this.txtSimFile.Name = "txtSimFile";
            this.txtSimFile.Size = new System.Drawing.Size(418, 20);
            this.txtSimFile.TabIndex = 22;
            this.txtSimFile.TextChanged += new System.EventHandler(this.txtSimFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Part file:";
            // 
            // cmdBrowsePartFile
            // 
            this.cmdBrowsePartFile.Location = new System.Drawing.Point(436, 35);
            this.cmdBrowsePartFile.Name = "cmdBrowsePartFile";
            this.cmdBrowsePartFile.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowsePartFile.TabIndex = 18;
            this.cmdBrowsePartFile.Text = "Browse...";
            this.cmdBrowsePartFile.UseVisualStyleBackColor = true;
            this.cmdBrowsePartFile.Click += new System.EventHandler(this.cmdBrowsePartFile_Click);
            // 
            // txtPartFile
            // 
            this.txtPartFile.Location = new System.Drawing.Point(12, 37);
            this.txtPartFile.Name = "txtPartFile";
            this.txtPartFile.Size = new System.Drawing.Size(418, 20);
            this.txtPartFile.TabIndex = 17;
            this.txtPartFile.TextChanged += new System.EventHandler(this.txtPartFile_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panSelection);
            this.groupBox2.Controls.Add(this.lstMethods);
            this.groupBox2.Location = new System.Drawing.Point(7, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(521, 236);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Information";
            // 
            // panSelection
            // 
            this.panSelection.Controls.Add(this.label2);
            this.panSelection.Controls.Add(this.lblDeselectAll);
            this.panSelection.Controls.Add(this.lblSelectAll);
            this.panSelection.Enabled = false;
            this.panSelection.Location = new System.Drawing.Point(381, 209);
            this.panSelection.Name = "panSelection";
            this.panSelection.Size = new System.Drawing.Size(130, 19);
            this.panSelection.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "/";
            // 
            // lblDeselectAll
            // 
            this.lblDeselectAll.AutoSize = true;
            this.lblDeselectAll.Location = new System.Drawing.Point(68, 2);
            this.lblDeselectAll.Name = "lblDeselectAll";
            this.lblDeselectAll.Size = new System.Drawing.Size(62, 13);
            this.lblDeselectAll.TabIndex = 15;
            this.lblDeselectAll.TabStop = true;
            this.lblDeselectAll.Text = "Deselect all";
            this.lblDeselectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDeselectAll_LinkClicked);
            // 
            // lblSelectAll
            // 
            this.lblSelectAll.AutoSize = true;
            this.lblSelectAll.Location = new System.Drawing.Point(4, 2);
            this.lblSelectAll.Name = "lblSelectAll";
            this.lblSelectAll.Size = new System.Drawing.Size(50, 13);
            this.lblSelectAll.TabIndex = 14;
            this.lblSelectAll.TabStop = true;
            this.lblSelectAll.Text = "Select all";
            this.lblSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSelectAll_LinkClicked);
            // 
            // lstMethods
            // 
            this.lstMethods.CheckOnClick = true;
            this.lstMethods.FormattingEnabled = true;
            this.lstMethods.Location = new System.Drawing.Point(12, 24);
            this.lstMethods.Name = "lstMethods";
            this.lstMethods.Size = new System.Drawing.Size(499, 184);
            this.lstMethods.TabIndex = 22;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.DefaultExt = "sim";
            this.openFileDialog2.Filter = "NX Simulation File|*.sim";
            this.openFileDialog2.ReadOnlyChecked = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 420);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpPathSettings);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.cmdExtract);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schnittstelle_NX";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpPathSettings.ResumeLayout(false);
            this.grpPathSettings.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panSelection.ResumeLayout(false);
            this.panSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cmdExtract;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem1;
        private System.Windows.Forms.GroupBox grpPathSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdBrowseSimFile;
        private System.Windows.Forms.TextBox txtSimFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdBrowsePartFile;
        private System.Windows.Forms.TextBox txtPartFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblDeselectAll;
        private System.Windows.Forms.LinkLabel lblSelectAll;
        private System.Windows.Forms.CheckedListBox lstMethods;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;

    }
}

