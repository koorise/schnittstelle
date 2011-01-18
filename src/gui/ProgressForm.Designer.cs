namespace Schnittstelle_NX_GUI
{
    partial class ProgressForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panPathSettings = new System.Windows.Forms.Panel();
            this.cmdDefaultOwlDir = new System.Windows.Forms.Button();
            this.cmdBrowseBaseOwl = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaseOwlPath = new System.Windows.Forms.TextBox();
            this.cmdDefaultOutputFile = new System.Windows.Forms.Button();
            this.cmdBrowseOutput = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.panBottom = new System.Windows.Forms.Panel();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.panPathSettings.SuspendLayout();
            this.panBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 248);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(580, 22);
            this.progressBar1.TabIndex = 12;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.CheckFileExists = false;
            this.openFileDialog2.DefaultExt = "owl";
            this.openFileDialog2.Filter = "OWL files|*.owl";
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 115);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(580, 121);
            this.lstStatus.TabIndex = 18;
            // 
            // panPathSettings
            // 
            this.panPathSettings.Controls.Add(this.cmdDefaultOwlDir);
            this.panPathSettings.Controls.Add(this.cmdBrowseBaseOwl);
            this.panPathSettings.Controls.Add(this.label1);
            this.panPathSettings.Controls.Add(this.txtBaseOwlPath);
            this.panPathSettings.Controls.Add(this.cmdDefaultOutputFile);
            this.panPathSettings.Controls.Add(this.cmdBrowseOutput);
            this.panPathSettings.Controls.Add(this.label2);
            this.panPathSettings.Controls.Add(this.txtOutputFile);
            this.panPathSettings.Location = new System.Drawing.Point(12, 12);
            this.panPathSettings.Name = "panPathSettings";
            this.panPathSettings.Size = new System.Drawing.Size(580, 84);
            this.panPathSettings.TabIndex = 21;
            // 
            // cmdDefaultOwlDir
            // 
            this.cmdDefaultOwlDir.Location = new System.Drawing.Point(505, 60);
            this.cmdDefaultOwlDir.Name = "cmdDefaultOwlDir";
            this.cmdDefaultOwlDir.Size = new System.Drawing.Size(75, 23);
            this.cmdDefaultOwlDir.TabIndex = 40;
            this.cmdDefaultOwlDir.Text = "Default";
            this.cmdDefaultOwlDir.UseVisualStyleBackColor = true;
            this.cmdDefaultOwlDir.Click += new System.EventHandler(this.cmdDefaultOwlDir_Click);
            // 
            // cmdBrowseBaseOwl
            // 
            this.cmdBrowseBaseOwl.Location = new System.Drawing.Point(424, 60);
            this.cmdBrowseBaseOwl.Name = "cmdBrowseBaseOwl";
            this.cmdBrowseBaseOwl.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseBaseOwl.TabIndex = 39;
            this.cmdBrowseBaseOwl.Text = "Browse...";
            this.cmdBrowseBaseOwl.UseVisualStyleBackColor = true;
            this.cmdBrowseBaseOwl.Click += new System.EventHandler(this.cmdBrowseBaseOwl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Base Model Directory:";
            // 
            // txtBaseOwlPath
            // 
            this.txtBaseOwlPath.Location = new System.Drawing.Point(0, 62);
            this.txtBaseOwlPath.Name = "txtBaseOwlPath";
            this.txtBaseOwlPath.Size = new System.Drawing.Size(418, 20);
            this.txtBaseOwlPath.TabIndex = 37;
            // 
            // cmdDefaultOutputFile
            // 
            this.cmdDefaultOutputFile.Location = new System.Drawing.Point(505, 16);
            this.cmdDefaultOutputFile.Name = "cmdDefaultOutputFile";
            this.cmdDefaultOutputFile.Size = new System.Drawing.Size(75, 23);
            this.cmdDefaultOutputFile.TabIndex = 36;
            this.cmdDefaultOutputFile.Text = "Default";
            this.cmdDefaultOutputFile.UseVisualStyleBackColor = true;
            this.cmdDefaultOutputFile.Click += new System.EventHandler(this.cmdDefaultOutputFile_Click);
            // 
            // cmdBrowseOutput
            // 
            this.cmdBrowseOutput.Location = new System.Drawing.Point(424, 16);
            this.cmdBrowseOutput.Name = "cmdBrowseOutput";
            this.cmdBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseOutput.TabIndex = 35;
            this.cmdBrowseOutput.Text = "Browse...";
            this.cmdBrowseOutput.UseVisualStyleBackColor = true;
            this.cmdBrowseOutput.Click += new System.EventHandler(this.cmdBrowseOutput_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Output File Path:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(0, 18);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(418, 20);
            this.txtOutputFile.TabIndex = 33;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.cmdOpen);
            this.panBottom.Controls.Add(this.cmdStart);
            this.panBottom.Controls.Add(this.cmdCancel);
            this.panBottom.Location = new System.Drawing.Point(12, 276);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(580, 28);
            this.panBottom.TabIndex = 22;
            // 
            // cmdOpen
            // 
            this.cmdOpen.AutoSize = true;
            this.cmdOpen.Location = new System.Drawing.Point(0, 3);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(75, 23);
            this.cmdOpen.TabIndex = 22;
            this.cmdOpen.Text = "Open";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Visible = false;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(375, 3);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(124, 23);
            this.cmdStart.TabIndex = 21;
            this.cmdStart.Text = "Start Extraction";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(505, 3);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 20;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 304);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.panPathSettings);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extract selected";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.panPathSettings.ResumeLayout(false);
            this.panPathSettings.PerformLayout();
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panPathSettings;
        private System.Windows.Forms.Button cmdDefaultOwlDir;
        private System.Windows.Forms.Button cmdBrowseBaseOwl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaseOwlPath;
        private System.Windows.Forms.Button cmdDefaultOutputFile;
        private System.Windows.Forms.Button cmdBrowseOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdCancel;
    }
}