using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Schnittstelle_NX;
using System.Diagnostics;

namespace Schnittstelle_NX_GUI
{
    public partial class ProgressForm : Form
    {
        //========================================================================================

        #region Fields

        private static Config s_Config = new Config();
        private InformationModelAccess m_InformationModelAccess;
        private List<string> m_MethodList;
        private Process m_Proc;

        #endregion

        //========================================================================================

        #region Construction

        public ProgressForm(InformationModelAccess informationModelAccess, List<string> methodList)
        {
            InitializeComponent();

            m_InformationModelAccess = informationModelAccess;
            m_MethodList = methodList;
        }

        #endregion

        //========================================================================================

        #region ProgressForm

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            BringToFront();

            string outputFilePath = Properties.Settings.Default.outputFilePath;
            string baseOwlDir = Properties.Settings.Default.baseOwlDir;

            if (string.IsNullOrEmpty(outputFilePath))
                txtOutputFile.Text = getDefaultOutputFilePath();
            else txtOutputFile.Text = outputFilePath;

            if (string.IsNullOrEmpty(baseOwlDir))
                txtBaseOwlPath.Text = getDefaultOwlDir();
            else txtBaseOwlPath.Text = baseOwlDir;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.outputFilePath = txtOutputFile.Text;
            Properties.Settings.Default.Save();
        }

        #endregion

        //========================================================================================

        #region Buttons

        private void cmdBrowseOutput_Click(object sender, EventArgs e)
        {
            browseOutputFile();
        }

        private void cmdBrowseBaseOwl_Click(object sender, EventArgs e)
        {
            browseOwlFolder();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            startExtraction();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            m_Proc = new Process();

            m_Proc.StartInfo = new ProcessStartInfo(txtOutputFile.Text);
            m_Proc.Start();
        }

        private void cmdDefaultOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = getDefaultOutputFilePath();
        }

        private void cmdDefaultOwlDir_Click(object sender, EventArgs e)
        {
            txtBaseOwlPath.Text = getDefaultOwlDir();
        }

        #endregion

        //========================================================================================

        #region private methods

        private void addItemToStatusList(string item)
        {
            lstStatus.Items.Insert(lstStatus.Items.Count, item);
            lstStatus.SetSelected(lstStatus.Items.Count - 1, true);
        }

        private string getDefaultOwlDir()
        {
            string projectFileDir = Path.GetDirectoryName(Properties.Settings.Default.partFilePath);
            string projectDir = projectFileDir.Substring(0, projectFileDir.LastIndexOf('\\'));

            return Path.Combine(projectDir, s_Config.DefaultOwlDirectory);
        }

        private string getDefaultOutputFilePath()
        {
            string projectFileName = Path.GetFileNameWithoutExtension(Properties.Settings.Default.partFilePath);

            return Path.Combine(getDefaultOwlDir(), Path.Combine(s_Config.DefaultExportDirectory, projectFileName + ".owl"));
        }

        private void browseOutputFile()
        {
            try
            {
                openFileDialog2.InitialDirectory = txtOutputFile.Text;
            }
            catch (Exception)
            {
                openFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            switch (openFileDialog2.ShowDialog(this))
            {
                case DialogResult.OK:
                    txtOutputFile.Text = openFileDialog2.FileName;
                    Properties.Settings.Default.outputFilePath = txtOutputFile.Text;
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void browseOwlFolder()
        {
            try
            {
                folderBrowserDialog1.SelectedPath = txtBaseOwlPath.Text;
            }
            catch (Exception)
            {
                folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            switch (folderBrowserDialog1.ShowDialog(this))
            {
                case DialogResult.OK:
                    txtBaseOwlPath.Text = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.baseOwlDir = txtBaseOwlPath.Text;
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void startExtraction()
        {
            TimeSpan timeUsed_Total, timeUsed_Single;
            DateTime startTime = DateTime.Now;
            DateTime singleStartTime;

            string outputFile = txtOutputFile.Text;
            string baseOwlDir = txtBaseOwlPath.Text;

            panPathSettings.Enabled = false;
            panBottom.Enabled = false;
            progressBar1.Value = 0;

            try
            {
                // Path validation. If not valid an exception will be thrown.
                Path.GetDirectoryName(outputFile);
                Path.GetFileName(outputFile);
                Path.GetDirectoryName(txtBaseOwlPath.Text);

                lstStatus.Items.Clear();

                addItemToStatusList("Initializing the Sesame Framework...");
                addItemToStatusList("");

                if (string.IsNullOrEmpty(baseOwlDir))
                    m_InformationModelAccess.Connect(getDefaultOwlDir());
                else m_InformationModelAccess.Connect(baseOwlDir);

                double count = m_MethodList.Count;

                for (int i = 0; i < count; i++)
                {
                    singleStartTime = DateTime.Now;

                    addItemToStatusList(m_MethodList[i]);

                    m_InformationModelAccess.BeginExtraction(m_MethodList[i]);

                    progressBar1.Value = (int)((double)(i + 1) / count * 100.0);

                    timeUsed_Single = DateTime.Now - singleStartTime;

                    addItemToStatusList("(" + m_InformationModelAccess.Count + " in " + timeUsed_Single.TotalSeconds + " s)");
                    addItemToStatusList("");
                }

                addItemToStatusList("Writing file: " + outputFile);

                m_InformationModelAccess.Export(outputFile);

                timeUsed_Total = DateTime.Now - startTime;

                addItemToStatusList("");
                addItemToStatusList("Finished!");
                addItemToStatusList("Time used: " + timeUsed_Total.TotalSeconds + " seconds");

                cmdOpen.Visible = true;
                panPathSettings.Enabled = true;
                panBottom.Enabled = true;

                FileInfo owlFile = new FileInfo(txtOutputFile.Text);

                double fileSizeKB = Math.Round(owlFile.Length / 1024.0, 2, MidpointRounding.ToEven);
                double fileSizeMB = Math.Round(fileSizeKB / 1024.0, 2, MidpointRounding.ToEven);

                if (fileSizeKB >= 1024)
                    cmdOpen.Text = "Open " + Path.GetFileName(txtOutputFile.Text) + " [" + fileSizeMB + " MB]";
                else
                    cmdOpen.Text = "Open " + Path.GetFileName(txtOutputFile.Text) + " [" + fileSizeKB + " KB]";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        //========================================================================================
    }
}
