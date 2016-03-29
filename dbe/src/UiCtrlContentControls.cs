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
using System.IO;

namespace dbe
{
    public partial class UiCtrlContentControls : UserControl
    {
        private Dal m_dal;

        public UiCtrlContentControls()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.lvcc.Items.Clear();
        }

        public void Populate(Dal dal)
        {
            m_dal = dal;
            PopulateGroupDropDown();
            RefreshFromDal(GroupK.None);
        }

        public enum GroupK
        {
            Type,
            Location,
            XPath,
            Namespace,
            Bound,
            None
        }

        public void PopulateGroupDropDown()
        {
            // Populate the group selector
            tsbGroupBy.Items.Clear();
            tsbGroupBy.Items.Add(GroupK.None.ToString());
            tsbGroupBy.Items.Add(GroupK.Type.ToString());
            tsbGroupBy.Items.Add(GroupK.Location.ToString());
            tsbGroupBy.Items.Add(GroupK.Bound.ToString());
            tsbGroupBy.Items.Add(GroupK.Namespace.ToString());
            tsbGroupBy.Items.Add(GroupK.XPath.ToString());
            tsbGroupBy.SelectedIndex = 0;
        }

        public void PopulateGroups(GroupK groupK)
        {
            lvcc.Groups.Clear();
            
            switch (groupK)
            {
                case GroupK.Type:
                    {
                        foreach (Dal.CCK cck in m_dal.GetUniqueCCTypes())
                        {
                            ListViewGroup lvg = new ListViewGroup();
                            lvg.Header = cck.ToString();
                            lvg.HeaderAlignment = HorizontalAlignment.Left;
                            lvg.Name = cck.ToString();
                            lvcc.Groups.Add(lvg);
                        }
                        break;
                    }
                case GroupK.Location:
                    {
                        foreach (Dal.SubDocK subDocK in m_dal.GetUniqueCCSubDocs())
                        {
                            ListViewGroup lvg = new ListViewGroup();
                            lvg.Header = subDocK.ToString();
                            lvg.HeaderAlignment = HorizontalAlignment.Left;
                            lvg.Name = subDocK.ToString();
                            lvcc.Groups.Add(lvg);
                        }
                        break;
                    }
                case GroupK.XPath:
                    {
                        foreach (string sXpath in m_dal.GetUniqueCCXPaths())
                        {
                            ListViewGroup lvg = new ListViewGroup();
                            
                            if (string.IsNullOrEmpty(sXpath))
                                lvg.Header = "(No XPath)";
                            else
                                lvg.Header = sXpath;

                            lvg.HeaderAlignment = HorizontalAlignment.Left;
                            lvg.Name = sXpath;
                            lvcc.Groups.Add(lvg);
                        }
                        break;
                    }
                case GroupK.Bound:
                    {
                        ListViewGroup lvg = new ListViewGroup();
                        lvg.Header = "Bound";
                        lvg.HeaderAlignment = HorizontalAlignment.Left;
                        lvg.Name = "Bound";
                        lvcc.Groups.Add(lvg);

                        lvg = new ListViewGroup();
                        lvg.Header = "Unbound";
                        lvg.HeaderAlignment = HorizontalAlignment.Left;
                        lvg.Name = "Unbound";
                        lvcc.Groups.Add(lvg);
                        break;
                    }
                case GroupK.Namespace:
                    {
                        foreach (string sNs in m_dal.GetUniqueCCBoundNamespaces())
                        {
                            ListViewGroup lvg = new ListViewGroup();
                            lvg.Header = sNs;
                            lvg.HeaderAlignment = HorizontalAlignment.Left;
                            lvg.Name = sNs;
                            lvcc.Groups.Add(lvg);
                        }
                        break;
                    }
            }            
        }

        public ListViewGroup GetLvgFromCc(GroupK groupK, Dal.CC cc)
        {
            ListViewGroup lvg = null;
            switch (groupK)
            {
                case GroupK.Type:
                    lvg = lvcc.Groups[cc.Type.ToString()];
                    break;
                case GroupK.Location:
                    lvg = lvcc.Groups[cc.Subdoc.ToString()];
                    break;
                case GroupK.XPath:
                    lvg = lvcc.Groups[cc.XPath];
                    break;
                case GroupK.Namespace:
                    Dal.XP xp = m_dal.FindXmlPart(cc.XmlPartId);
                    if (xp != null)
                        lvg = lvcc.Groups[xp.Ns];
                    break;
                case GroupK.Bound:
                    if (string.IsNullOrEmpty(cc.XPath))
                        lvg = lvcc.Groups["Unbound"];
                    else
                        lvg = lvcc.Groups["Bound"];
                    break;
                default:
                    lvg = null;
                    break;
            }

            return lvg;
        }

        public void RefreshFromDal(GroupK groupK)
        {
            lvcc.Items.Clear();
            
            if (groupK == GroupK.None)
            {
                lvcc.ShowGroups = false;
            }
            else
            {
                lvcc.ShowGroups = true;
                PopulateGroups(groupK);
            }

            // Now display in the UI            
            foreach (Dal.CC cc in m_dal.ContentControls)
            {
                // Figure out the group kind if any
                ListViewGroup lvg = GetLvgFromCc(groupK, cc);

                // Create the list item based on if a group was created
                ListViewItem lvi = null;
                if (lvg == null)
                    lvi = new ListViewItem(new string[] { string.Empty, cc.Id, cc.Tag, cc.Type.ToString(), cc.XPath }, cc.Type.ToString());
                else
                    lvi = new ListViewItem(new string[] { string.Empty, cc.Id, cc.Tag, cc.Type.ToString(), cc.XPath }, cc.Type.ToString(), lvg);
                
                lvi.Tag = cc;
                lvcc.Items.Add(lvi);
            }
        }

        private void UpdateLviCc(Dal.CC cc, ListViewItem lvi, bool fPropsChanged)
        {
            string sCurrTag = lvi.SubItems[2].Text;
            string sCurrXpath = lvi.SubItems[4].Text;
            
            lvi.SubItems[2].Text = cc.Tag;
            lvi.SubItems[4].Text = cc.XPath;

            if (fPropsChanged)
                lvi.BackColor = Color.LightBlue;
                   
        }

        private void lvcc_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(UiCtrlStoreTree.BindInfo)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void lvcc_DragDrop(object sender, DragEventArgs e)
        {
            UiCtrlStoreTree.BindInfo bi = (UiCtrlStoreTree.BindInfo)e.Data.GetData(typeof(UiCtrlStoreTree.BindInfo));

            //Returns the location of the mouse pointer in the ListView control.
            Point cp = lvcc.PointToClient(new Point(e.X, e.Y));

            //Obtain the item that is located at the specified location of the mouse pointer.
            ListViewItem lvi = lvcc.GetItemAt(cp.X, cp.Y);
            if (lvi != null)
            {
                lvi.Selected = true;                
                lvcc.Select();
                lvi.EnsureVisible();
            }
            
            // Update DAL
            string sCCId = lvi.SubItems[1].Text;
            Dal.CC cc = m_dal.FindContentControl(sCCId);
            Debug.Assert(cc != null, "Could not find cc in the dal");
            cc.XPath = bi.XPath;
            cc.XmlPartId = bi.StoreID;
            cc.PrefixMappings = bi.PrefixMappings;

            // Update UI
            lvi.SubItems[4].Text = bi.XPath;
            lvi.BackColor = Color.Cyan;

            // Signal the DBE that we dirtied the doc
            Program.Dbe.FDirtied = true;
        }

        private void lvcc_DoubleClick(object sender, EventArgs e)
        {
            if (lvcc.SelectedItems.Count == 0)
                return;
            
            ListViewItem lvi = lvcc.SelectedItems[0];
            string sCCId = lvi.SubItems[1].Text;
            Dal.CC cc = m_dal.FindContentControl(sCCId);
            Debug.Assert(cc != null, "Could not find cc in the dal");
            if (cc == null)
                return;

            UiFmCcProps editUi = new UiFmCcProps();
            editUi.Init(cc, m_dal);
            if (DialogResult.Yes == editUi.ShowDialog())
            {
                Program.Dbe.FDirtied = true;
                UpdateLviCc(cc, lvi, true /* fModified */);
            }
        }

        private void btnLocator_Click(object sender, EventArgs e)
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
            finder.Init(m_dal, Program.Dbe.PkgFullName);
            finder.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lvcc.Refresh();
        }      

        private void lvcc_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // TODO: Implement sorting
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            RefreshFromDal(GroupK.Location);
        }

        private void cbGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFromDal((GroupK)Enum.Parse(typeof(GroupK), tsbGroupBy.SelectedItem.ToString()));
        }
    }
}
