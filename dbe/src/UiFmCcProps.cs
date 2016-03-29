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
using dbe.Properties;

namespace dbe
{
    public partial class UiFmCcProps : Form
    {
        Dal.CC m_cc = null;
        Dal m_dal = null;

        public UiFmCcProps()
        {
            InitializeComponent();
        }
        
        private void tbXmlStoreId_TextChanged(object sender, EventArgs e)
        {
            string sNs = Resources.NoNamespaceDisplayName;
            string sId = tbXmlStoreId.Text;
            if (!string.IsNullOrEmpty(sId))
            {
                Dal.XP xp = Program.Dbe.Dal.FindXmlPart(sId);
                if (xp != null)
                {
                    if (!string.IsNullOrEmpty(xp.Ns))
                    {
                        sNs = xp.Ns;
                    }
                }
            }

            tbPartNs.Text = sNs;
        }

        public void Init(Dal.CC cc, Dal dal)
        {
            m_dal = dal;
            m_cc = cc;
            tbId.Text = m_cc.Id;
            tbTitle.Text = m_cc.Title;
            tbTag.Text = m_cc.Tag;
            tbLocation.Text = m_cc.Subdoc.ToString();
            tbPrefixMappings.Text = m_cc.PrefixMappings;
            tbType.Text = m_cc.Type.ToString();
            tbXmlStoreId.Text = m_cc.XmlPartId;
            tbXpath.Text = m_cc.XPath;      
        }       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FSDiff(m_cc.Title, tbTitle.Text) ||
                FSDiff(m_cc.Tag, tbTag.Text) ||
                FSDiff(m_cc.PrefixMappings, tbPrefixMappings.Text) ||
                FSDiff(m_cc.XmlPartId, tbXmlStoreId.Text) ||
                FSDiff(m_cc.XPath,tbXpath.Text))
           {
               this.DialogResult = DialogResult.Yes;
           }                        
            
            m_cc.Title = tbTitle.Text;
            m_cc.Tag = tbTag.Text;
            m_cc.PrefixMappings = tbPrefixMappings.Text;
            m_cc.XmlPartId = tbXmlStoreId.Text;
            m_cc.XPath = tbXpath.Text;

            this.Close();
        }

        private bool FSDiff(string s1, string s2)
        {
            // Add empty strings to normalize nulls
            s1 += string.Empty;
            s2 += string.Empty;

            return s1 != s2;
        }
    }
}