using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace dbe
{
    public partial class UiFmDocGen : Form
    {
        private Thread m_docGenThread = null;
        
        public UiFmDocGen()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Program.Dbe.PkgFullName) ||
                !File.Exists(Program.Dbe.PkgFullName))
            {
                Mbox.ShowSimpleMsgBoxError("Cannot start generation because template file is missing or invalid");
                return;
            }

            if (string.IsNullOrEmpty(tbInputFolder.Text) ||
                !Directory.Exists(tbInputFolder.Text))
            {
                Mbox.ShowSimpleMsgBoxError("Cannot start generation because XML data directory is missing or invalid");
                return;
            }

            if (string.IsNullOrEmpty(tbOutputFolder.Text) ||
                !Directory.Exists(tbOutputFolder.Text))
            {
                Mbox.ShowSimpleMsgBoxError("Cannot start generation because output directory is missing or invalid");
                return;
            }

            if (Directory.GetFiles(tbInputFolder.Text, "*.xml").Length == 0)
            {
                Mbox.ShowSimpleMsgBoxWarning("Cannot start generation because input folder does not contain any XML files");
                return;
            }

            btnGenerate.Enabled = false;
            toolStripStatusLabelStatus.Text = "Generating...";
            toolStripProgressBar1.Visible = true;

            // Clear the log
            string logFileFullName = DocGen.GetLogFileName(tbOutputFolder.Text);
            try
            {
                File.Delete(logFileFullName);
            }
            catch (Exception ex)
            {
                Mbox.ShowSimpleMsgBoxError("Could not clear log. Document generation will not continue. " + ex.Message);
                return;
            }

            // Start generation on another thread
            m_docGenThread = new Thread(GenerateWorker); 
            m_docGenThread.Start();

            // Keep pumping messages while generating
            while (m_docGenThread.IsAlive)
            {
                Thread.Sleep(150);
                Application.DoEvents();
            }
            
            btnGenerate.Enabled = true;
            toolStripStatusLabelStatus.Text = "Ready.";
            toolStripProgressBar1.Visible = false;
        }
     
        private void GenerateWorker()
        {
            try
            {
                new DocGen().Generate(Program.Dbe.PkgFullName, tbInputFolder.Text, tbOutputFolder.Text);
                Process.Start(tbOutputFolder.Text);
            }
            catch (Exception ex)
            {
                Mbox.ShowSimpleMsgBoxWarning("Error encountered while generating. " + ex.Message);
            }
        }

        private void btnInputFolderOpenDlg_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select input folder containing XML data files";
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                tbInputFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOuputFolderOpenDlg_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select output folder to contain generated Word documents. Note a log will also be saved here.";
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                tbOutputFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void UiFmDocGen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_docGenThread != null && m_docGenThread.IsAlive)
            {
                if (DialogResult.Yes == MessageBox.Show("Generation is not yet completed. Are you sure you want to abort generation?", "Document Generator", MessageBoxButtons.YesNo, MessageBoxIcon.Hand))
                {
                    try
                    {
                        m_docGenThread.Abort();
                    }
                    catch { }; // Best effort
                }
                else
                {
                    e.Cancel = false;
                }                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://dbe.codeplex.com/Wikipage?title=documentgenerator");
        }
    }
}
