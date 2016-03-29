//-------------------------------------------------------------------------------------------------
// <copyright company="Microsoft">
//    Author: Matt Scott (mrscott). Copyright (c) Microsoft Corporation.  All rights reserved.
//
//    The use and distribution terms for this software are covered by the
//    Microsoft Limited Permissive License: 
//    http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
//    which can be found in the file license_mslpl.txt at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//
//    You must not remove this notice, or any other, from this software.
//
//    Project homepage can be found here: http://www.codeplex.com/dbe  
// </copyright>
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.IO;
using dbe.Properties;

namespace dbe
{
    public partial class UiFmMain : Form
    {
        public UiFmMain()
        {
            Application.Idle += new EventHandler(Application_Idle);
            InitializeComponent();            
        }

        void Application_Idle(object sender, EventArgs e)
        {         
            // Return the status bar to "Ready" every 20 seconds
            if (DateTime.Now.Second % 20 == 0)
                SetStatusBarMsg(Resources.StatusBarReady);

            // Update the UI based on dirty status
            if (Program.Dbe.FDirtied)
                EnablePostDirtyUI();

            // Update the title bar to stay in sync with doc status
            SetTitleBarCaption(Program.Dbe.FLoaded ? Path.GetFileName(Program.Dbe.PkgFullName) : null, Program.Dbe.FDirtied);
         }

        private void SetTitleBarCaption(string sOpenedFileName, bool fDirty)
        {
            if (!string.IsNullOrEmpty(sOpenedFileName))
            {
                if (fDirty)
                    Text = sOpenedFileName + "* - " + Program.Dbe.AppName;
                else
                    Text = sOpenedFileName + " - " + Program.Dbe.AppName;
            }
            else
            {
                Text = Program.Dbe.AppName;
            }
        }
               
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Dbe.FDirtied)
            {
                DialogResult dr = MessageBox.Show
                                    ("Do you want to save the changes to " + Path.GetFileName(Program.Dbe.PkgFullName),
                                    Program.Dbe.AppName, MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        if (!FSave())
                            return;
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        Debug.Fail("Unexpected dialog result");
                        break;
                }
            }

            if (DialogResult.OK != openFileDialog1.ShowDialog())
                return;

            string sFileName = Path.GetFileName(openFileDialog1.FileName);

            SetStatusBarMsg("Loading \"" + sFileName + "\"...");
            Application.DoEvents();

            try
            {
                Program.Dbe.Close();
                Program.Dbe.Load(openFileDialog1.FileName, DbeCore.DataSource.OpenXmlPackage);
                SetTitleBarCaption(sFileName, false);
                EnablePreDirtyUI();
                SetStatusBarMsg("\"" + sFileName + "\": " + Program.Dbe.Dal.ContentControls.Count + 
                                   " Content Controls, " + Program.Dbe.Dal.XmlParts.Count + " Custom XML parts");
            }
            catch (Exception ex)
            {
                DisableAllUI();
                Mbox.ShowSimpleMsgBoxWarning("Failed to open. " + ex.Message);
                SetStatusBarMsg("\"" + sFileName + "\": failed to open. " + ex.Message);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSave();
        }

        public bool FSave()
        {
            try
            {
                uiCustomXmlParts.XmlTextEditor.CommitChanges();
            }
            catch (Exception ex)
            {
                Mbox.ShowSimpleMsgBoxWarning("Please fix XML syntax error before saving: " +
                                               Environment.NewLine + Environment.NewLine + "\t" + ex.Message.ToString());
                return false;
            }

            try
            {
                Program.Dbe.Save();
                SetStatusBarMsg("Document Saved");
                return true;
            }
            catch (Exception ex)
            {
                Mbox.ShowSimpleMsgBoxWarning("Failed to save. " + ex.Message);
            }

            return false;
        }

        public void DisableAllUI()
        {
            scSurface.Enabled =
            scSurface.Visible =
            saveToolStripMenuItem2.Enabled =
            toggleContentControlsToolStripMenuItem.Enabled =
            toggleCustomXMLPartsToolStripMenuItem.Enabled =
            toolStripMenuItem2.Enabled =
            undoToolStripMenuItem.Enabled =
            cutToolStripMenuItem.Enabled =
            copyToolStripMenuItem.Enabled =
            pasteToolStripMenuItem.Enabled =
            selectAllToolStripMenuItem.Enabled =
            cutToolStripButton.Enabled =
            copyToolStripButton.Enabled =
            pasteToolStripButton.Enabled =
            saveToolStripButton.Enabled =
            docGenToolStripButton.Enabled =
            documentGeneratorToolStripMenuItem.Enabled =
            contentControlLocatorToolStripMenuItem.Enabled =
                false;
        }

        public void EnablePreDirtyUI()
        {
            scSurface.Enabled =
            scSurface.Visible =
            toggleContentControlsToolStripMenuItem.Enabled =
            toggleCustomXMLPartsToolStripMenuItem.Enabled =
            toolStripMenuItem2.Enabled =
            undoToolStripMenuItem.Enabled =
            cutToolStripMenuItem.Enabled =
            copyToolStripMenuItem.Enabled =
            pasteToolStripMenuItem.Enabled =
            selectAllToolStripMenuItem.Enabled =
            cutToolStripButton.Enabled =
            copyToolStripButton.Enabled =
            pasteToolStripButton.Enabled =
            docGenToolStripButton.Enabled =
            documentGeneratorToolStripMenuItem.Enabled =
            contentControlLocatorToolStripMenuItem.Enabled =
                true;
        }

        public void EnablePostDirtyUI()
        {
            saveToolStripMenuItem2.Enabled =
            saveToolStripButton.Enabled =
                true;
        }

        public void SetStatusBarMsg(string sMsg)
        {
            toolStripStatusLabel1.Text = sMsg;
        }       

        public SplitContainer Surface
        {
            get { return scSurface; }
        }

        public UiCtrlCustomXmlParts UiCustomXmlParts
        {
            get { return uiCustomXmlParts; }
        }

        public UiCtrlContentControls UiContentControls
        {
            get { return uiContentControls; }
        }

        private void collapseContentControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePanel1();
        }

        private void toggleXMLDataStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePanel2();
        }

        public void TogglePanel1()
        {
            scSurface.Panel1Collapsed = !(scSurface.Panel1Collapsed);
        }

        public void TogglePanel2()
        {
            scSurface.Panel2Collapsed = !(scSurface.Panel2Collapsed);
        }

        public void HidePanel1()
        {
            scSurface.Panel1Collapsed = true;
        }

        public void HidePanel2()
        {
            scSurface.Panel2Collapsed = true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTitleBarCaption(null, false);
            Program.Dbe.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Dbe.FDirtied)
            {
                DialogResult dr = MessageBox.Show
                                    ("Do you want to save the changes to " + Path.GetFileName(Program.Dbe.PkgFullName),
                                    Program.Dbe.AppName, MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        if (!FSave())
                            return;
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        Debug.Fail("Unexpected dialog result");
                        break;
                }
            }

            Program.Dbe.Close();
            Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Undo();
        }    

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.SelectAll();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            this.uiCustomXmlParts.XmlTextEditor.MainTextBox.Paste();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiFmAbout fmAbout = new UiFmAbout();
            fmAbout.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mbox.ShowSimpleMsgBoxInfo("This feature has not yet been implemented.");
        }

        private void UiFmMain_Load(object sender, EventArgs e)
        {
            SetTitleBarCaption(null, false);
        }

        private void UiFmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.Dbe.FDirtied)
            {
                DialogResult dr = MessageBox.Show
                                    ("Do you want to save the changes to " + Path.GetFileName(Program.Dbe.PkgFullName),
                                    Program.Dbe.AppName, MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.Cancel :
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes :
                        if (!FSave())
                            e.Cancel = true;
                        break;
                    case DialogResult.No :
                        break;
                    default :
                        Debug.Fail("Unexpected dialog result");
                        break;
                }                    
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.CodePlexDbeUrl);
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.CodePlexDbeUrl);
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiFmCheckForUpdates fmCfu = new UiFmCheckForUpdates();
            fmCfu.ShowDialog();
        }

        private void documentGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiFmDocGen fmDocGen = new UiFmDocGen();
            fmDocGen.ShowDialog();
        }

        private void contentControlLocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Dbe.FDirtied)
            {
                DialogResult dr = MessageBox.Show
                                    ("Do you want to save the changes to " + Path.GetFileName(Program.Dbe.PkgFullName) + " so that the document is in sync with the viewer?",
                                    Program.Dbe.AppName, MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (dr == DialogResult.Yes)
                    if (!Program.Dbe.Ui.FSave())
                        return;
            }

            UiFmLocator finder = new UiFmLocator();
            finder.Init(Program.Dbe.Dal, Program.Dbe.PkgFullName);
            finder.ShowDialog();
        }
    }
}