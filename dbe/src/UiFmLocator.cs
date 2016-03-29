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
using Word = Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.IO;

namespace dbe
{
    public partial class UiFmLocator : Form
    {
        private Word.Document m_wdoc;
        private string m_sWdDocFullName;
        private bool m_fWordQuit;
        private bool m_fCCEnterInWord;
        private bool m_fChangesMade;
        private string m_sCCIdEnteredInWord;
        private Dal m_dal;
        
        public UiFmLocator()
        {
            InitializeComponent();
        }

        public void Init(Dal dal, string sWdDocFullName)
        {
            m_dal = dal;
            m_sWdDocFullName = sWdDocFullName;
            RefreshFromDal();
            m_fChangesMade = false;
            Word.Application wapp = BootWord();
            if (wapp == null)
                return;
                        
            object oMiss = Type.Missing;
            wapp.Documents.AddOld(ref oMiss, ref oMiss);
            wapp.ActiveWindow.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMaximize;

            m_wdoc = OpenDocument(wapp, sWdDocFullName);
            if (m_wdoc == null)
                return;

            object oIndex = 2;
            ((Word._Document)wapp.Documents.get_Item(ref oIndex)).Close(ref oMiss, ref oMiss, ref oMiss);

            ((Word.DocumentClass)m_wdoc).DocumentEvents_Event_Close += new Microsoft.Office.Interop.Word.DocumentEvents_CloseEventHandler(CCFinderUI_DocumentEvents_Event_Close);
            m_wdoc.ContentControlOnEnter += new Word.DocumentEvents2_ContentControlOnEnterEventHandler(m_wdoc_ContentControlOnEnter);
            SetUpWordWindowAfterDocOpen(wapp);
            m_wdoc.Activate();

            const int nApproxWordWidth = 846;

            this.Width = Screen.PrimaryScreen.WorkingArea.Width - nApproxWordWidth;
            this.Left = nApproxWordWidth;
            this.Top = 0;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
        
        void CCFinderUI_DocumentEvents_Event_Close()
        {
            m_fWordQuit = true;
        }

        void m_wdoc_ContentControlOnEnter(Microsoft.Office.Interop.Word.ContentControl ContentControl)
        {
            try
            {   
                m_sCCIdEnteredInWord = ContentControl.ID;
                m_fCCEnterInWord = true;
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
            }            
        }
      
        private Word.Document OpenDocument(Word.Application wapp, string sWdDocFullName)
        {
            Word.Document wdoc = null;
            try
            {
                object oFileName = sWdDocFullName;
                object oFalse = false;
                object oTrue = true;
                object oMiss = Type.Missing;
                wdoc = wapp.Documents.Open(ref oFileName, ref oFalse, ref oTrue, ref oFalse, ref oMiss, ref oMiss, ref oMiss, ref oMiss, ref oMiss,
                                            ref oMiss, ref oMiss, ref oMiss, ref oMiss, ref oMiss, ref oTrue, ref oMiss);

            }
            catch (Exception ex)
            {
                Debug.Fail("Failed to open document.", ex.Message);            
            }

            return wdoc;
        }

        private Word.Application BootWord()
        {
            Word.Application wapp = null;
            try
            {
                wapp = new Word.Application();
                wapp.Visible = true;               
            }
            catch (Exception ex)
            {
                Debug.Fail("Failed to instantiate Word.", ex.Message);
            }          

            return wapp;
        }
      
        private void SetUpWordWindowAfterDocOpen(Word.Application wapp)
        {
            if (wapp != null)
            {
                try
                {
                    wapp.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateNormal;
                    wapp.ActiveWindow.Left = 0;
                    wapp.ActiveWindow.Top = 0;
                    wapp.ActiveWindow.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    wapp.ActiveWindow.Width = 634;
                    
                    wapp.ActiveWindow.ToggleRibbon();
                    wapp.ActiveWindow.DisplayRulers = false;  
                    wapp.ActiveWindow.View.Type = Word.WdViewType.wdPrintView;
                    wapp.ActiveWindow.View.Zoom.Percentage = 100;
                    wapp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                    //wapp.ActiveDocument.Protect wdAllowOnlyReading
                }
                catch (Exception ex)
                {
                    Debug.Fail("Failed to set up Word window, ect.", ex.Message);
                }
            }
        }

        public void RefreshFromDal()
        {
            this.lvcc.Items.Clear();

            // Now display in the UI            
            foreach (Dal.CC cc in m_dal.ContentControls)
            {
                ListViewItem lvi = new ListViewItem(new string[] { cc.Id, cc.Tag, cc.Type.ToString(), cc.XPath });
                lvi.Tag = cc;
                this.lvcc.Items.Add(lvi);
            }
        }

        private void lvcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvcc.SelectedItems.Count == 0)
                return;

            string sCCId = lvcc.SelectedItems[0].Text;
            object oIndex = sCCId;           

            Word.ContentControl cc = null;
            try
            {
                cc = m_wdoc.ContentControls.get_Item(ref oIndex);
            }
            catch (Exception ex)
            {
                Debug.Fail("Failed to get content control " + sCCId + " in the Word document.", ex.Message);
                return;
            }

            if (cc == null)
            {
                Debug.Fail("Failed to find content control " + sCCId + " in the Word document.");
                return;
            }

            try
            {
                cc.Range.Select();
                m_wdoc.Activate();
                object oWinIndex = 1;
                string sCaption = m_wdoc.Windows.get_Item(ref oWinIndex).Caption;
                sCaption += " - " + m_wdoc.Application.Name;
                IntPtr wordHandle = ShellApi.FindWindow("OpusApp", sCaption);
                ShellApi.SetForegroundWindow(wordHandle);
            }
            catch (Exception ex)
            {
                Debug.Fail("Failed to select cc", ex.Message);
            }
        }

        private void tsbCloseWord_Click(object sender, EventArgs e)
        {
            QuitWord();

            if (m_fChangesMade)
                this.DialogResult = DialogResult.OK; // Tell the caller changes have been made (OK means changes)
        }

        private void CCFinderUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuitWord();

            if (m_fChangesMade)
                this.DialogResult = DialogResult.OK; // Tell the caller changes have been made (OK means changes)
        }

        private void QuitWord()
        {
            if (m_fWordQuit)
                return;
            
            if (m_wdoc == null || m_wdoc.Application == null)
                return;

            object oMiss = Type.Missing;

            try
            {
                ((Word._Application)m_wdoc.Application).Quit(ref oMiss, ref oMiss, ref oMiss);
            }
            catch (Exception ex)
            {
                Debug.Fail("Failed to quit Word.", ex.Message);
            }
        }
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_fWordQuit)
                this.Close();


            if (m_fCCEnterInWord)
            {
                if (!string.IsNullOrEmpty(m_sCCIdEnteredInWord))
                {
                    ListViewItem lvi = lvcc.FindItemWithText(m_sCCIdEnteredInWord);
                    if (lvi != null)
                    {
                        this.Select();
                        lvi.Selected = true;                     
                        lvcc.Select();
                        lvi.EnsureVisible();
                    }
                }

                m_fCCEnterInWord = false;
            }
        }
    }
}