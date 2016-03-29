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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using dbe.Properties;
using System.Text.RegularExpressions;

namespace dbe
{
    public partial class UiCtrlCustomXmlParts : UserControl
    {
        public UiCtrlCustomXmlParts()
        {
            InitializeComponent();            
        }

        private List<Dal.XP> m_rgxp;        
        public void Populate(List<Dal.XP> rgxp)
        {
            m_rgxp = rgxp;
            RefreshState();
        }

        public void RefreshState()
        {
            RefreshStateHelper(false, 0);
        }

        public void RefreshStateAndSelectLast()
        {
            RefreshStateHelper(true, 0);
        }

        public void RefreshStateAndSelectIndex(int nIndex)
        {
            RefreshStateHelper(false, nIndex);
        }

        public void RefreshStateAndStayOnCurrentIndex()
        {
            int i = cbParts.SelectedIndex;
            RefreshStateAndSelectIndex(i);
        }     

        private void RefreshStateHelper(bool fSelectLast, int nSelectIndex)
        {
            // Clear views
            storeTree.Clear();

            // Populate the combo box            
            cbParts.Items.Clear();
            cbParts.DisplayMember = "Display";
            cbParts.ValueMember = "Value";
            string sPartName = string.Empty;
            foreach (Dal.XP xp in m_rgxp)
            {
                // Do not show xp's that were flagged as deleted
                if (xp.FDelete)
                    continue;

                if (!string.IsNullOrEmpty(xp.Ns))
                    sPartName = xp.Ns;
                else
                    sPartName = Resources.NoNamespaceDisplayName + "(" + GetUniqueEmptyNsDisplayNum() + ")";

                cbParts.Items.Add(new Cbi(sPartName, xp));
            }

            EnableOrDisableUI(cbParts.Items.Count != 0 /*fEnable*/);

            if (fSelectLast)
                cbParts.SelectedIndex = cbParts.Items.Count - 1;
            else
            {
                try
                {
                    if (cbParts.Items.Count > 0)
                        cbParts.SelectedIndex = nSelectIndex;
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.ToString());
                }
            }

            // Force a selection change
            cbParts_SelectionChangeCommitted(this, null);
        }

        private void EnableOrDisableUI(bool fEnable)
        {
            cbParts.Enabled =
                tcXmlViews.Enabled =
                lDeleteXmlPart.Enabled =
                    fEnable;

            pnInfoBarNoXml.Visible = !fEnable;
        }

        private string GetCurrCbPartName()
        {
            if (cbParts.Items.Count == 0 || cbParts.SelectedItem == null)
                return null;

            return ((Cbi)cbParts.SelectedItem).Display;
        }

        private Dal.XP GetCurrCbXmlPart()
        {
            if (cbParts.Items.Count == 0 || cbParts.SelectedItem == null)
                return null;

            return (Dal.XP)((Cbi)cbParts.SelectedItem).Value;
        }

        private void SetCurrCbPartName(string sName)
        {
            if (cbParts.Items.Count == 0 || cbParts.SelectedItem == null)
                return;

            object oValueCache = ((Cbi)cbParts.SelectedItem).Value;
            Cbi cbiNew = new Cbi(sName, oValueCache);
            int iPart = cbParts.SelectedIndex;
            cbParts.Items[iPart] = cbiNew;                            
        }

        private void InitBindViewTab(Dal.XP xp)
        {
            storeTree.LoadTreeViewFromXmlDOM(xp.XmlDom, xp.Id);
        }

        private void InitEditViewTab(Dal.XP xp)
        {
            xmlTextEditor1.Init(xp);
        }      

        public void Clear()
        {
            cbParts.Items.Clear();
            xmlTextEditor1.Clear();
            storeTree.Clear();
        }

        public UiCtrlXmlTextEditor XmlTextEditor
        {
            get { return this.xmlTextEditor1; }
        }

        private void lCreateNewXmlPart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // First try to commit xml changes (if they exist), on error cancel the operation
            try
            {
                xmlTextEditor1.CommitChanges();
            }
            catch (Exception ex)
            {
                Mbox.ShowSimpleMsgBoxWarning(Resources.FixXmlErrorRequestBeforeNewPart + Environment.NewLine + 
                                             Environment.NewLine + "\t" + ex.Message.ToString());
                return;

            }
            
            Dal.XP xp = new Dal.XP();
            xp.Id = "{" + Guid.NewGuid().ToString() + "}"; /* Using brackets are required due to SharePoint bug. See http://www.codeplex.com/dbe/WorkItem/View.aspx?WorkItemId=10199 */
            xp.FCreate = true;
            xp.XmlDom = new XmlDocument();
            xp.XmlDom.LoadXml("<root>" + Environment.NewLine + "</root>");
            Program.Dbe.Dal.AddXmlPart(xp);
            Program.Dbe.FDirtied = true;
            RefreshStateAndSelectLast();
        }

        private void lDeleteXmlPart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dal.XP xp = GetCurrCbXmlPart();
            if (xp == null)
                return;

            // Prompt the user to make sure they want to delete
            string sWarningMsg = string.Empty;
            if (string.IsNullOrEmpty(xp.Ns))
                sWarningMsg = Resources.ConfirmDeletePart;
            else
                sWarningMsg = Resources.ConfirmDeletePart + " (" + xp.Ns + ")?";

            if (DialogResult.Yes == MessageBox.Show
                                (sWarningMsg, Program.Dbe.AppName, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {

                // All we need to do is flag this XP that it will be deleted. 
                // The actual deletion from the package will happen on save.
                // The UI on refresh will not show this xp so to the user it looks already gone.
                xp.FDelete = true;

                // Update UI
                RefreshState();
                Program.Dbe.FDirtied = true;
            }
        }

        private void cbParts_Click(object sender, EventArgs e)
        {
            try
            {
                xmlTextEditor1.CommitChanges();
            }
            catch (Exception ex)
            {
                cbParts.DroppedDown = false;
                Mbox.ShowSimpleMsgBoxWarning(Resources.FixXmlErrorRequestBeforeLeave +
                                            Environment.NewLine + Environment.NewLine + "\t" + ex.Message.ToString());
                xmlTextEditor1.Focus();
                return;
            }
        }        

        private void tcXmlViews_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // First try to commit the changes, on error cancel the view change
            try
            {
                xmlTextEditor1.CommitChanges();
            }
            catch (Exception ex)
            {
                Mbox.ShowSimpleMsgBoxWarning(Resources.FixXmlErrorRequestBeforeSwitchViews +
                                             Environment.NewLine + Environment.NewLine + "\t" + ex.Message.ToString());
                e.Cancel = true;
                return;
            }

            Dal.XP xp = GetCurrCbXmlPart();
            if (xp == null)
                return;
            
            if (tcXmlViews.SelectedTab == tpBindView)
                InitBindViewTab(xp);
            else
                InitEditViewTab(xp);
        }

        private void cbParts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Precondition: commiting xml changes happens on click of the combobox (in cbParts_Click)
            Dal.XP xp = GetCurrCbXmlPart();
            if (xp == null)
                return;

            if (tcXmlViews.SelectedTab == tpBindView)
                InitBindViewTab(xp);
            else
                InitEditViewTab(xp);
        }

        private int GetUniqueEmptyNsDisplayNum()
        {           
            Match match = null;
            string sRegExPattern = "[1-9][0-9]{0,4}";
            int nMaxNoNsNum = 0;
            foreach (object oCbItem in cbParts.Items)
            {
                string sCbPartName = ((Cbi)oCbItem).Display;
                if (sCbPartName.StartsWith(Resources.NoNamespaceDisplayName))
                {
                    match = Regex.Match(sCbPartName, sRegExPattern);
                    if (match.Success)
                    {
                        int nNsNum;
                        bool fParsed = Int32.TryParse(match.Value, out nNsNum);
                        if (fParsed)
                            if (nNsNum > nMaxNoNsNum)
                                nMaxNoNsNum = nNsNum;
                    }
                }
            }

            return nMaxNoNsNum + 1;
        }

        /// <summary>
        /// Timer callback. 
        /// Purpose: tries to keep the current namespace in cbParts in sync with the 
        /// xml document in the text editor during user edits. Updates every 1.5 seconds.
        /// </summary>
        private void tmrNsPinger_Tick(object sender, EventArgs e)
        {            
            if (tcXmlViews.SelectedTab == tpEditView)
            {
                if (xmlTextEditor1.MainTextBox.Focused)
                {
                    string sNs = xmlTextEditor1.GetCurrNamespace();
                    string sCurrCbPartName = GetCurrCbPartName();
                    Debug.Assert(sCurrCbPartName != null, "The UI should not be enabled when their are no XML parts in the document.");
                    if (sNs != null && sCurrCbPartName != null && sCurrCbPartName != sNs)
                    {
                        if (sNs == string.Empty)
                        {
                            if (!sCurrCbPartName.StartsWith(Resources.NoNamespaceDisplayName))
                                sNs = Resources.NoNamespaceDisplayName + "(" + GetUniqueEmptyNsDisplayNum() + ")";
                            else
                                sNs = sCurrCbPartName;
                        }

                        SetCurrCbPartName(sNs);
                    }
                }
            }
        }
    }
}
