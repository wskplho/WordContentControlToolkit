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
using System.Xml;
using System.IO;
using System.Diagnostics;
using dbe.Properties;

namespace dbe
{
    public partial class UiCtrlXmlTextEditor : UserControl
    {
        private Dal.XP m_xp;
        private string m_sOrigXmlCache;

        public UiCtrlXmlTextEditor()
        {
            InitializeComponent();
        }

        public void Init(Dal.XP xp)
        {
            m_xp = xp;

            string sXml = m_xp.XmlDom.OuterXml.ToString();

            if (FXmlWellFormed(sXml, false))
                sXml = XmlUtil.PrettyPrint(sXml);

            // Cache the xml so we can revert changes
            m_sOrigXmlCache = sXml;            
            rtbXml.Font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Regular, GraphicsUnit.Point);
            rtbXml.ForeColor = Color.Black;            
            rtbXml.Text = sXml;          
            rtbXml.Focus();
            rtbXml.SelectionLength = 0;            
        }

        public bool FXmlWellFormed(string sXml, bool fDisplayUI)
        {
            if (string.IsNullOrEmpty(sXml))
            {
                if (fDisplayUI)
                    Mbox.ShowSimpleMsgBoxWarning(Resources.SyntaxCheckFailureEmptyXml);
                return false;
            }

            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(sXml);
                
                if (fDisplayUI)
                    Mbox.ShowSimpleMsgBoxInfo(Resources.SyntaxCheckSuccess);                    
            }
            catch (XmlException ex)
            {
                if (fDisplayUI)
                    Mbox.ShowSimpleMsgBoxWarning(Resources.SyntaxCheckErrorPrefix + ex.Message);

                return false;
            }

            return true;
        }        

        public void CommitChanges()
        {
            string sXml = rtbXml.Text;
            if (string.IsNullOrEmpty(sXml))
                return; 

            m_xp.XmlDom.LoadXml(sXml);
            m_xp.Ns = m_xp.XmlDom.DocumentElement.NamespaceURI;
        }

        private void tsbCheckSyntax_Click(object sender, EventArgs e)
        {
            string sXml = rtbXml.Text;
            FXmlWellFormed(sXml, true);           
        }

        private void tsbReformatXml_Click(object sender, EventArgs e)
        {
            string sXml = rtbXml.Text;
            if (FXmlWellFormed(sXml, false))
                sXml = XmlUtil.PrettyPrint(sXml);

            rtbXml.Text = sXml;
            rtbXml.Focus();
            rtbXml.SelectionLength = 0;
        }

        private void tsbRevert_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show
                               (Resources.ConfirmRevertXml, Program.Dbe.AppName, MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                rtbXml.Text = m_sOrigXmlCache;
            }
        }

        public void Clear()
        {
            rtbXml.Text = string.Empty;                       
        }

        public RichTextBox MainTextBox
        {
            get { return rtbXml; }
        }

        /// <summary>
        /// Gets the current namespace of the xml in the text editor. This can be called even while the user is editing the document
        /// as long as the document element is not malformed. If the document element is malformed, this function returns null, otherwise the current namespace.
        /// </summary>    
        public string GetCurrNamespace()
        {
            return XmlUtil.GetNsFromStr(rtbXml.Text);
        }

        private void tsbWordWrap_Click(object sender, EventArgs e)
        {
            rtbXml.WordWrap = !rtbXml.WordWrap;
            tsbWordWrap.Checked = rtbXml.WordWrap;
        }

        private void rtbXml_ModifiedChanged(object sender, EventArgs e)
        {
            if (!Program.Dbe.FDirtied && /* If we are already dirtied, short circuit out of further checks */
                 rtbXml.Modified /* Only set dirty status to tru if rtbXml was modified */
                )
                Program.Dbe.FDirtied = true;
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                rtbXml.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                File.WriteAllText(saveFileDialog1.FileName, rtbXml.Text);
            }
        }
    }
}
