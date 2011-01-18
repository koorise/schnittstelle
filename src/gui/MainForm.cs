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

namespace Schnittstelle_NX_GUI
{
    public partial class MainForm : Form
    {
        //========================================================================================

        #region Fields

        private static Config s_Config = new Config();

        private InformationModelAccess m_InformationModelAccess;
        private ObjectModelAccess m_ObjectModelAccess;

        #endregion

        //========================================================================================

        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        //========================================================================================

        #region MainForm

        private void MainForm_Load(object sender, EventArgs e)
        {
            BringToFront();
            txtPartFile.Text = Properties.Settings.Default.partFilePath;
            txtSimFile.Text = Properties.Settings.Default.motionSimFile;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPartFile.Text))
                Properties.Settings.Default.partFilePath = txtPartFile.Text;

            if (!string.IsNullOrEmpty(txtSimFile.Text))
                Properties.Settings.Default.motionSimFile = txtSimFile.Text;

            Properties.Settings.Default.Save();
        }

        #endregion

        //========================================================================================

        #region Buttons

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            lstMethods.Items.Clear();
            lstMethods.Items.Add("Initializing NX...");
            load();
        }

        private void cmdExtract_Click(object sender, EventArgs e)
        {
            extract();
        }

        private void cmdBrowsePartFile_Click(object sender, EventArgs e)
        {
            browsePartFile();
        }

        private void cmdBrowseSimFile_Click(object sender, EventArgs e)
        {
            browseSimFile();
        }

        #endregion

        //========================================================================================

        #region Control events

        private void lblSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < lstMethods.Items.Count; i++)
                lstMethods.SetItemChecked(i, true);
        }

        private void lblDeselectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < lstMethods.Items.Count; i++)
                lstMethods.SetItemChecked(i, false);
        }

        private void txtPartFile_TextChanged(object sender, EventArgs e)
        {
            reloadAction();
        }

        private void txtSimFile_TextChanged(object sender, EventArgs e)
        {
            reloadAction();
        }


        #endregion

        //========================================================================================

        #region private methods

        private void addItemsToMethodList(List<string> methodList)
        {
            foreach (string methodName in methodList)
                lstMethods.Items.Add(methodName, true);
        }

        List<object> paramList;

        private void load()
        {
            string partFile = txtPartFile.Text;
            string simFile = txtSimFile.Text;

            bool fLoadSimulation = !string.IsNullOrEmpty(simFile);
            string simName = Path.GetFileNameWithoutExtension(simFile);

            paramList = new List<object>();
            paramList.Add(partFile);
            paramList.Add(simName);
            paramList.Add(fLoadSimulation);

            cmdLoad.Text = "Please wait...";
            cmdExtract.Enabled = false;
            grpPathSettings.Enabled = false;
            cmdLoad.Enabled = false;
            browseToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem.Enabled = false;

            try
            {
                if (m_ObjectModelAccess != null)
                    m_ObjectModelAccess.CloseAll();
                                
                initializeNx(paramList);
                                                                
                if (m_InformationModelAccess != null)
                    m_InformationModelAccess.ObjectModelController = m_ObjectModelAccess;
                else m_InformationModelAccess = new InformationModelAccess(m_ObjectModelAccess, false);

                lstMethods.Items.Clear();

                addItemsToMethodList(m_InformationModelAccess.GetAllMethods());
                
                cmdLoad.Text = "Loaded!";
                cmdExtract.Enabled = true;
                panSelection.Enabled = true;
                extractSelectedToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                cmdLoad.Enabled = true;
                cmdLoad.Text = "Load";
            }

            browseToolStripMenuItem.Enabled = true;
            grpPathSettings.Enabled = true;

            BringToFront();
        }

        private void initializeNx(object param)
        {
            List<object> paramList = (List<object>) param;

            if ((bool) paramList[2])
                m_ObjectModelAccess = new ObjectModelAccess((string)paramList[0], (string)paramList[1]);
            else
                m_ObjectModelAccess = new ObjectModelAccess((string)paramList[0]);
        }

        private void browsePartFile()
        {
            try
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(txtPartFile.Text);
            }
            catch (Exception)
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            switch (openFileDialog1.ShowDialog(this))
            {
                case DialogResult.OK:
                    txtPartFile.Text = openFileDialog1.FileName;
                    Properties.Settings.Default.partFilePath = txtPartFile.Text;
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void browseSimFile()
        {
            try
            {
                openFileDialog2.InitialDirectory = Path.GetDirectoryName(txtSimFile.Text);
            }
            catch (Exception)
            {
                openFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            switch (openFileDialog2.ShowDialog(this))
            {
                case DialogResult.OK:
                    txtSimFile.Text = openFileDialog2.FileName;
                    Properties.Settings.Default.motionSimFile = txtSimFile.Text;
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void extract()
        {
            List<string> methodList = new List<string>();

            for (int i = 0; i < lstMethods.Items.Count; i++)
                if (lstMethods.GetItemChecked(i))
                    methodList.Add(lstMethods.GetItemText(lstMethods.Items[i]));

            if (methodList.Count > 0)
            {
                ProgressForm pf = new ProgressForm(m_InformationModelAccess, methodList);
                Hide();

                switch (pf.ShowDialog(this))
                {
                    default:
                        Show();
                        BringToFront();
                        break;
                }
            }
            else MessageBox.Show(this, "Select at least one method!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void reloadAction()
        {
            lstMethods.Items.Clear();
            cmdLoad.Text = "Load (*)";
            cmdLoad.Enabled = true;
            cmdExtract.Enabled = false;
            loadToolStripMenuItem.Enabled = true;
            panSelection.Enabled = false;
        }

        #endregion

        //========================================================================================

        #region menu items

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load();
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void extractSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extract();
        }

        #endregion

        //========================================================================================
    }
}
