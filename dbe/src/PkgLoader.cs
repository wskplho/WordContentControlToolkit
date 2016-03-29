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
using System.Diagnostics;
using System.IO.Packaging;
using System.IO;

namespace dbe
{
    /// <summary>
    /// Package Loader -- Load data from an Open XML Package into the DAL
    /// </summary>
    class PkgLoader : IdbContentLoader 
    {
        public PkgLoader()
        {
        }

        void IdbContentLoader.Load(Dal dal, string sPkgFullName)
        {
            PkgNavigator pn = new PkgNavigator(sPkgFullName);
            pn.Load();

            try
            {
                LoadCCData(dal, pn);
                LoadStoreItemData(dal, pn);
            }
            catch 
            {
                throw;
            }
            finally
            {
                pn.Close();
            }
        }

        void LoadStoreItemData(Dal dal, PkgNavigator pn)
        {   
            List<PkgNavigator.CustomXmlItem> rgcxi = pn.GetCustomXmlItems();
            XmlDocument xdItem;
            XmlDocument xdItemProp;

            foreach (PkgNavigator.CustomXmlItem cxi in rgcxi)
            {
                try
                {
                    // Load the Xml for the item and its prop
                    xdItem = new XmlDocument();
                    Debug.Assert(cxi.stmXmlItem.Position == 0, "The stream must be rewound to zero for its contents to be fully read");
                    xdItem.Load(cxi.stmXmlItem);
                    cxi.stmXmlItem.Close();
                    xdItemProp = new XmlDocument();
                    Debug.Assert(cxi.stmXmlItemProp.Position == 0, "The stream must be rewound to zero for its contents to be fully read");
                    xdItemProp.Load(cxi.stmXmlItemProp);
                    cxi.stmXmlItemProp.Close();
                }
                catch (Exception ex)
                {
                    // TODO: record these failures and present to user after load
                    Debug.Fail("Failed to load store item or its prop into a dom.", ex.Message);
                   
                    // Continue to next item to try and load that one
                    continue;
                }

                // Populate data structure
                Dal.XP xp = new Dal.XP();
                try
                {
                    xp.XmlDom = xdItem;
                    xp.Ns = xdItem.DocumentElement.NamespaceURI;
                    xp.Id = xdItemProp.DocumentElement.Attributes.GetNamedItem("ds:itemID").Value;

                    // Add it to list
                    dal.AddXmlPart(xp);
                }
                catch (Exception ex)
                {
                    Debug.Fail("Failed to populate xp.", ex.Message);
                }
            }
        }

        void LoadCCData(Dal dal, PkgNavigator pn)
        {
            LoadCCDataFromPackagePart(dal, pn.GetMainPackagePart());
            LoadCCDataFromPackagePart(dal, pn.GetCommentsPackagePart());
            LoadCCDataFromPackagePart(dal, pn.GetEndNotesPackagePart());
            LoadCCDataFromPackagePart(dal, pn.GetFootNotesPackagePart());
            LoadCCDataFromPackagePartList(dal, pn.GetHeaderPackageParts());
            LoadCCDataFromPackagePartList(dal, pn.GetFooterPackageParts());
        }

        void LoadCCDataFromPackagePart(Dal dal, PackagePart pp)
        {
            if (pp == null)
                return;

            XmlDocument xd = new XmlDocument();
            Stream stm = pp.GetStream();
            Debug.Assert(stm.Position == 0, "The stream must be rewound to zero for its contents to be fully read");
            xd.Load(stm);
            stm.Close();
            LoadCCDataFromPartXdIntoDal(xd, dal);
        }

        void LoadCCDataFromPackagePartList(Dal dal, List<PackagePart> rgpp)
        {
            foreach (PackagePart pp in rgpp)
                LoadCCDataFromPackagePart(dal, pp);
        }

        private Dal.SubDocK SubDocKindFromPartXd(XmlDocument xd)
        {
            switch (xd.DocumentElement.LocalName.ToLower())
            {
                case "document" : return Dal.SubDocK.MainDocument;
                case "comments" : return Dal.SubDocK.Comments;
                case "endnotes" : return Dal.SubDocK.Endnotes;
                case "ftr" : return Dal.SubDocK.Footer;
                case "footnotes" : return Dal.SubDocK.Footnotes;
                case "hdr" : return Dal.SubDocK.Header;
            }

            Debug.Fail("Unknown sub doc kind: " + xd.DocumentElement.LocalName);
            return Dal.SubDocK.NULL;
        }
       
        private void LoadCCDataFromPartXdIntoDal(XmlDocument xd, Dal dal)
        {
            // Example content control XML definition:
            //<w:sdtPr>
            //  <w:rPr>
            //    <w:rStyle w:val="Entry" /> 
            //    <w:szCs w:val="18" /> 
            //   </w:rPr>
            //   <w:alias w:val="SSN" /> 
            //   <w:tag w:val="SSN Tag" /> 
            //   <w:id w:val="4065844" /> 
            //   <w:placeholder>
            //        <w:docPart w:val="9870526EE9CB4DF49255A067C4B66787" /> 
            //    </w:placeholder>
            //   <w:dataBinding w:prefixMappings="xmlns:ns0='http://schemas.microsoft.com/office/2006/metadata/properties' xmlns:ns1='http://www.w3.org/2001/XMLSchema-instance' xmlns:ns2='5c6f7550-9fbd-464c-87de-72bbae7c0540'" w:xpath="/ns0:properties[1]/documentManagement[1]/ns2:SSN[1]" w:storeItemID="{3E0BD050-4BDB-4E91-9197-8CA144C78648}" /> 
            //   <w:text /> 
            //</w:sdtPr>

            Dal.SubDocK subDocKind = SubDocKindFromPartXd(xd);

            // Find all content controls
            NameTable nt = new NameTable();
            XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
            string sWdNs = xd.DocumentElement.NamespaceURI;
            nsManager.AddNamespace("w", sWdNs);
            XmlNodeList xnlCC = xd.SelectNodes("//w:sdtPr", nsManager);

            XmlNode xnAlias = null;
            XmlNode xnTag = null;
            XmlNode xnId = null;
            XmlNode xnPrefixMappings = null;
            XmlNode xnStoreId = null;
            XmlNode xnXpath = null;
            XmlNode xnDataBinding = null;
            Dal.CCK cck;
            foreach (XmlNode xnCC in xnlCC)
            {
                // Reset all references
                xnAlias = null;
                xnTag = null;
                xnId = null;
                xnPrefixMappings = null;
                xnStoreId = null;
                xnXpath = null;
                xnDataBinding = null;

                // Search for known tags
                xnAlias = xnCC.SelectSingleNode("w:alias", nsManager);
                xnTag = xnCC.SelectSingleNode("w:tag", nsManager);
                xnId = xnCC.SelectSingleNode("w:id", nsManager);
                xnDataBinding = xnCC.SelectSingleNode("w:dataBinding", nsManager);
                if (xnDataBinding != null)
                {
                    xnPrefixMappings = xnDataBinding.Attributes.GetNamedItem("w:prefixMappings");
                    xnXpath = xnDataBinding.Attributes.GetNamedItem("w:xpath");
                    xnStoreId = xnDataBinding.Attributes.GetNamedItem("w:storeItemID");
                }

                cck = FindCckFromSdtPrNode(xnCC);

                // Save in data structure
                Dal.CC cc = new Dal.CC();
                if (xnAlias != null && xnAlias.Attributes.Count > 0)
                    cc.Title = xnAlias.Attributes[0].Value;

                if (xnTag != null && xnTag.Attributes.Count > 0)
                    cc.Tag = xnTag.Attributes[0].Value;

                if (xnId != null && xnId.Attributes.Count > 0)
                    cc.Id = xnId.Attributes[0].Value;

                if (xnPrefixMappings != null)
                    cc.PrefixMappings = xnPrefixMappings.Value;

                if (xnXpath != null)
                    cc.XPath = xnXpath.Value;

                if (xnStoreId != null)
                    cc.XmlPartId = xnStoreId.InnerText;

                if (cck != Dal.CCK.NULL)
                    cc.Type = cck;

                cc.Subdoc = subDocKind;

                // Add to list
                dal.AddContentControl(cc);
            }
        }

        Dal.CCK FindCckFromSdtPrNode(XmlNode xnSdtPr)
        {
            foreach (XmlNode xnChild in xnSdtPr.ChildNodes)
            {
                switch (xnChild.LocalName)
                {
                    case "comboBox": return Dal.CCK.ComboBox;
                    case "date": return Dal.CCK.DatePicker;
                    case "docPartList": return Dal.CCK.BuildingBlocksGallery;
                    case "dropDownList": return Dal.CCK.DropDownList;
                    case "picture": return Dal.CCK.Picture;
                    case "richText": return Dal.CCK.RichText; // Will not bind by-design of Word
                    case "text": return Dal.CCK.PlainText;
                    case "group": return Dal.CCK.Group;
                }
            }

            return Dal.CCK.RichText; 
        }     
    }
}
