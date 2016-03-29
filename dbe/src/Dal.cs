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
using System.Text;
using System.Xml;

namespace dbe
{
    /// <summary>
    /// Data Access Layer -- the in-memory representation and storage of content controls and xml parts
    /// </summary>
    public class Dal
    {
        private List<CC> m_rgcc;
        private List<XP> m_rgxp;
        
        /// <summary>
        /// Content Control Type (Kind)
        /// </summary>
        public enum CCK
        {
            PlainText,
            RichText,
            Group,
            Picture,
            DatePicker,
            DropDownList,
            ComboBox,
            BuildingBlocksGallery,
            NULL
        }
                
        public enum SubDocK
        {
            MainDocument,
            Comments,
            Endnotes,
            Footer,
            Footnotes,
            Header,
            NULL
        }

        /// <summary>
        /// Content Control
        /// </summary>
        public class CC
        {
            private string m_sTitle;
            private string m_sTag;
            private CCK m_cctype = CCK.NULL;
            private string m_sXpath;
            private string m_sPrefixMappings;
            private string m_sXmlPartId;
            private string m_sText;
            private string m_id;
            private SubDocK m_subDocType = SubDocK.NULL;

            public SubDocK Subdoc
            {
                get { return m_subDocType; }
                set { m_subDocType = value; }
            }

            public string Id
            {
                get { return m_id; }
                set { m_id = value; }
            }

            public string Title
            {
                get { return m_sTitle; }
                set { m_sTitle = value; }
            }

            public string Tag
            {
                get { return m_sTag; }
                set { m_sTag = value; }
            }

            public CCK Type
            {
                get { return m_cctype; }
                set { m_cctype = value; }
            }

            public string XPath
            {
                get { return m_sXpath; }
                set { m_sXpath = value; }
            }

            public string PrefixMappings
            {
                get { return m_sPrefixMappings; }
                set { m_sPrefixMappings = value; }
            }

            public string XmlPartId
            {
                get { return m_sXmlPartId; }
                set { m_sXmlPartId = value; }
            }

            public string Text
            {
                get { return m_sText; }
                set { m_sText = value; }
            }
        }

        /// <summary>
        /// Xml Part
        /// </summary>
        public class XP
        {
            private string m_sNs;
            private XmlDocument m_xdPart;
            private string m_sId;
            private bool m_fCreate;
            private bool m_fDelete;

            public string Ns
            {
                get { return m_sNs; }
                set { m_sNs = value; }
            }

            public string Id
            {
                get { return m_sId; }
                set { m_sId = value; }
            }

            public XmlDocument XmlDom
            {
                get { return m_xdPart; }
                set { m_xdPart = value; }
            }

            public bool FCreate
            {
                get { return m_fCreate; }
                set { m_fCreate = value; }
            }

            public bool FDelete
            {
                get { return m_fDelete; }
                set { m_fDelete = value; }
            }
        }

        public Dal()
        {
            m_rgcc = new List<CC>();
            m_rgxp = new List<XP>();
        }

        public void Clear()
        {
            m_rgxp.Clear();
            m_rgcc.Clear();
        }

        public void AddContentControl(CC cc)
        {
            m_rgcc.Add(cc);
        }

        public CC FindContentControl(string sId)
        {
            foreach (CC cc in m_rgcc)
            {
                if (cc.Id == sId)
                    return cc;
            }

            return null;
        }

        public XP FindXmlPart(string sId)
        {
            foreach (XP xp in m_rgxp)
            {
                if (xp.Id == sId)
                    return xp;
            }

            return null;
        }

        public void AddXmlPart(XP xp)
        {
            m_rgxp.Add(xp);
        }

        public List<XP> XmlParts
        {
            get
            {
                return m_rgxp;
            }
        }

        public List<string> GetUniqueCCBoundNamespaces()
        {
            List<string> rg = new List<string>();
            string sPartNs = null;
            foreach (CC cc in m_rgcc)
            {
                if (string.IsNullOrEmpty(cc.XmlPartId))
                    continue;

                XP xp = FindXmlPart(cc.XmlPartId);
                if (xp == null)
                    continue;

                sPartNs = xp.Ns;
                if (!rg.Contains(sPartNs))
                    rg.Add(sPartNs);
            }

            return rg;
        }

        public List<string> GetUniqueCCXPaths()
        {
            List<string> rgsXpaths = new List<string>();

            foreach (CC cc in m_rgcc)
            {
                if (!rgsXpaths.Contains(cc.XPath))
                    rgsXpaths.Add(cc.XPath);
            }

            return rgsXpaths;
        }

        public List<SubDocK> GetUniqueCCSubDocs()
        {
            List<SubDocK> rgsdk = new List<SubDocK>();

            foreach (CC cc in m_rgcc)
            {
                if (!rgsdk.Contains(cc.Subdoc))
                    rgsdk.Add(cc.Subdoc);
            }

            return rgsdk;
        }

        public List<CCK> GetUniqueCCTypes()
        {
            List<CCK> rgcck = new List<CCK>();

            foreach (CC cc in m_rgcc)
            {
                if (!rgcck.Contains(cc.Type))
                    rgcck.Add(cc.Type);
            }

            return rgcck;
        }

        public List<CC> ContentControls
        {
            get
            {
                return m_rgcc;
            }
        }
    }
}
