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
using System.IO;
using System.IO.Packaging;

namespace dbe
{
    /// <summary>
    /// Package Writer -- Write data to Open XML Package from a DAL
    /// </summary>
    class PkgWriter : IdbContentWriter
    {
        private string m_sPkgFullName;
        private Dal m_dal;

        /// <summary>
        /// Writes data to the Open XML package given a dal. Returns null if success, an error message otherwise.
        /// </summary>
        /// <param name="dal"></param>
        void IdbContentWriter.Write(Dal dal, string sPkgFullName)
        {
            m_dal = dal;
            m_sPkgFullName = sPkgFullName;

            PkgNavigator pn = new PkgNavigator(m_sPkgFullName);
            pn.Load();
            try
            {
                foreach (Dal.XP xp in dal.XmlParts)
                {                  
                    if (xp.FCreate)
                        CreateNewCustomXmlPart(xp.Id, pn);                      

                    if (xp.FDelete)
                        DeleteCustomXmlPart(xp.Id, pn);
                }              
                
                SaveStoreItemData(dal, pn);
                SaveContentControlData(dal, pn);
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
        
        public void CreateNewCustomXmlPart(string sItemId, PkgNavigator pn)
        {
            const string sCustomXml = "/customXml";
            const string sPartName = "item";

            // Create a unique name for part
            int cPr = 1;
            Uri uriCx = new Uri(sCustomXml + "/" + sPartName + cPr + ".xml", UriKind.Relative);
            while (pn.Package.PartExists(uriCx))
                uriCx = new Uri(sCustomXml + "/" + sPartName + (++cPr) + ".xml", UriKind.Relative);

            // Create the name for the prop file
            Uri uriCxProps = new Uri(sCustomXml + "/" + sPartName + "Props" + cPr + ".xml", UriKind.Relative);

            // Try to create the xml parts
            PackagePart ppCx;
            PackagePart ppCxProps;
            ppCx = pn.Package.CreatePart(uriCx, "application/xml");
            ppCxProps = pn.Package.CreatePart(uriCxProps, "application/vnd.openxmlformats-officedocument.customXmlProperties+xml");

            // Create the relationship between the property xml and the item xml
            ppCx.CreateRelationship(uriCxProps, 
                    TargetMode.Internal, 
                    PkgNavigator.OXMLRelTypes.CustomXMLPropsType,
                    "R" + Guid.NewGuid().ToString().Replace("-", string.Empty) /* Using a guid to fix http://www.codeplex.com/dbe/WorkItem/View.aspx?WorkItemId=10200) */
                ); 

            // Write the property xml
            XmlDocument xdProp = new XmlDocument();
            string sPropXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\" ?><ds:datastoreItem ds:itemID=\"" + sItemId + "\" xmlns:ds=\"http://schemas.openxmlformats.org/officeDocument/2006/customXml\"/>";
            xdProp.LoadXml(sPropXml);
            xdProp.Save(ppCxProps.GetStream(FileMode.Create, System.IO.FileAccess.Write));

            // Create a relationship between the document and the new xml part
            pn.GetMainPackagePart().CreateRelationship(new Uri(".." + sCustomXml + "/" + sPartName + cPr + ".xml", UriKind.Relative), TargetMode.Internal, PkgNavigator.OXMLRelTypes.CustomXMLRelType);
        } 
        
        public void DeleteCustomXmlPart(string sItemId, PkgNavigator pn)
        {
            PkgNavigator.CustomXmlItem cxi = pn.GetCustomXmlItemById(sItemId);
            if (cxi == null)
                throw new FileNotFoundException("Could not find the custom xml part to delete", "Item ID: " + sItemId);

            // Delete all relationships to the Xml part and the Xml props part
            List<PackageRelationship> rgpr = new List<PackageRelationship>();
            rgpr.AddRange(cxi.ppXmlItem.GetRelationships());                
            foreach (PackageRelationship pr in rgpr)
                cxi.ppXmlItem.DeleteRelationship(pr.Id);

            // Delete all relationships to the Xml props part
            rgpr.Clear();
            rgpr.AddRange(cxi.ppXmlItemProp.GetRelationships());
            foreach (PackageRelationship pr in cxi.ppXmlItemProp.GetRelationships())
                cxi.ppXmlItemProp.DeleteRelationship(pr.Id);

            // Delete the relationship between the main document and this xml part                
            pn.GetMainPackagePart().DeleteRelationship(cxi.sMainRelId);

            // Delete the physical parts from the package
            pn.Package.DeletePart(cxi.ppXmlItem.Uri);
            pn.Package.DeletePart(cxi.ppXmlItemProp.Uri);
        }

        private void SaveStoreItemData(Dal dal, PkgNavigator pn)
        {
            List<PkgNavigator.CustomXmlItem> rgcxi = pn.GetCustomXmlItems();

            foreach (PkgNavigator.CustomXmlItem cxi in rgcxi)
            {               
                // If we have a good store id, find the one in the dal and use it to override the one in the package
                if (!string.IsNullOrEmpty(cxi.sItemId))
                {
                    Dal.XP xp = dal.FindXmlPart(cxi.sItemId);

                    // If we found it...
                    if (xp != null) 
                    {
                        using (Stream stmWritePart = cxi.ppXmlItem.GetStream(System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            xp.XmlDom.Save(stmWritePart);                            
                        }
                    }
                }
            }
        }

        private XmlNode CreateAndAppendNode(XmlNode xnParent, string sNs, string sName)
        {
            return CreateAndAppendNode(xnParent, sNs, sName, null, null, null);
        }

        private XmlNode CreateAndAppendNode(XmlNode xnParent, string sNs, string sName, string sAttribName1)
        {
            return CreateAndAppendNode(xnParent, sNs, sName, sAttribName1, null, null);
        }

        private XmlNode CreateAndAppendNode(XmlNode xnParent, string sNs, string sName, string sAttribName1, string sAttribName2)
        {
            return CreateAndAppendNode(xnParent, sNs, sName, sAttribName1, sAttribName2, null);
        }

        private XmlNode CreateAndAppendNode(XmlNode xnParent, string sNs, string sName, string sAttribName1, string sAttribName2, string sAttribName3)
        {
            XmlNode xnNew = null;
            XmlAttribute xa1 = null, xa2 = null, xa3 = null;
            XmlDocument xdOwner = xnParent.OwnerDocument;

            xnNew = xdOwner.CreateElement(sName, sNs);

            if (sAttribName1 != null)
            {
                xa1 = xdOwner.CreateAttribute(sAttribName1, sNs);
                xnNew.Attributes.Append(xa1);
            }
            if (sAttribName2 != null)
            {
                xa2 = xdOwner.CreateAttribute(sAttribName2, sNs);
                xnNew.Attributes.Append(xa2);
            }
            if (sAttribName3 != null)
            {
                xa3 = xdOwner.CreateAttribute(sAttribName3, sNs);
                xnNew.Attributes.Append(xa3);
            }

            xnParent.PrependChild(xnNew);
            return xnNew;
        }

        private void SaveContentControlData(Dal dal, PkgNavigator pn)
        {
            SaveCCDataToPackagePart(dal, pn.GetMainPackagePart());
            SaveCCDataToPackagePart(dal, pn.GetCommentsPackagePart());
            SaveCCDataToPackagePart(dal, pn.GetEndNotesPackagePart());
            SaveCCDataToPackagePart(dal, pn.GetFootNotesPackagePart());
            SaveCCDataToPackagePartList(dal, pn.GetHeaderPackageParts());
            SaveCCDataToPackagePartList(dal, pn.GetFooterPackageParts());
        }

        private void SaveCCDataToPackagePart(Dal dal, PackagePart pp)
        {
            if (pp == null)
                return;

            try
            {
                XmlDocument xd = PkgNavigator.GetXdFromPp(pp);
                if (xd == null)
                    return;

                WriteCCDataFromDalToXd(xd, dal);
                SaveXdToPackagePart(xd, pp);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
            }        
        }

        private void SaveCCDataToPackagePartList(Dal dal, List<PackagePart> rgpp)
        {
            foreach (PackagePart pp in rgpp)
                SaveCCDataToPackagePart(dal, pp);
        }

        private void WriteCCDataFromDalToXd(XmlDocument xd, Dal dal)
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

            // From the XSD:
            /*
             * Note that XPATH and STOREITEMID are required
            <xsd:complexType name="CT_DataBinding" odoc:ID="c1314638-ae46-4bb0-9fb3-ddc3a4c31946">
		        <xsd:attribute name="prefixMappings" wbld:cname="prefixMappings" type="ST_String" wbld:comment="Defines the prefix-&gt;namespace mappings for the xpath" odoc:ID="7c85af68-916d-4ffc-9586-f74c269554e7" />
		        <xsd:attribute name="xpath" type="ST_String" use="required" wbld:cname="xpath" wbld:comment="The xpath to the data source for this databinding" odoc:ID="d2af1566-869b-4b9d-a9ff-1fd2454f260f" />
		        <xsd:attribute name="storeItemID" type="ST_String" use="required" wbld:cname="storeItemID" wbld:comment="This is the itemID of the store item that we are linked to" odoc:ID="d06b6407-1c7b-46a7-a5f4-898afefb3ec7" />
	        </xsd:complexType>
            */

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
            string sId = null;
            XmlNode xnDataBinding = null;
            foreach (XmlNode xnCC in xnlCC)
            {
                // Get references to tags
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

                // Get the id
                if (xnId.Attributes.Count == 0)
                {
                    Debug.Fail("No id on this node:", xnCC.OuterXml);
                    continue;
                }
                sId = xnId.Attributes[0].Value;


                // Find the cc in the DAL and write to the xml of the package dom
                Dal.CC cc = dal.FindContentControl(sId);
                if (cc != null) // If we found it...
                {
                    // -------- <w:tag> ----------- //
                    if (!string.IsNullOrEmpty(cc.Tag))
                    {
                        if (xnTag == null) // If node does not exist, create it
                            xnTag = CreateAndAppendNode(xnCC, sWdNs, "w:tag", "w:val");

                        // The element should exist by now so set values
                        xnTag.Attributes["w:val"].Value = cc.Tag;
                    }
                    // -------- </w:tag> ----------- //

                    // -------- <w:alias> ----------- //
                    if (!string.IsNullOrEmpty(cc.Title))
                    {
                        if (xnAlias == null) // If node does not exist, create it
                            xnAlias = CreateAndAppendNode(xnCC, sWdNs, "w:alias", "w:val");

                        // The element should exist by now so set values
                        xnAlias.Attributes["w:val"].Value = cc.Title;
                    }
                    // -------- </w:alias> ----------- //

                    // -------- <w:dataBinding> ----------- //

                    // Clear any existing dataBinding node to initialize state
                    // Note that this fixes bug http://www.codeplex.com/dbe/WorkItem/View.aspx?WorkItemId=11562
                    if (xnDataBinding != null)
                    {
                        xnCC.RemoveChild(xnDataBinding);
                    }

                    // The XSD says that the xpath and storeitemid are required attributes. 
                    // So if either one of those values are empty do not touch the databinding element
                    if (!string.IsNullOrEmpty(cc.XPath) && !string.IsNullOrEmpty(cc.XmlPartId))
                    {
                        // If we have no info for prefix mappings, dont add the attribute
                        if (string.IsNullOrEmpty(cc.PrefixMappings))
                        {
                            // Start from scratch
                            xnDataBinding = CreateAndAppendNode(xnCC, sWdNs, "w:dataBinding", "w:xpath", "w:storeItemID");

                            // The element should exist by now so set values
                            xnDataBinding.Attributes["w:xpath"].Value = cc.XPath;
                            xnDataBinding.Attributes["w:storeItemID"].Value = cc.XmlPartId;
                        }
                        else
                        {
                            // Start from scratch
                            xnDataBinding = CreateAndAppendNode(xnCC, sWdNs, "w:dataBinding", "w:prefixMappings", "w:xpath", "w:storeItemID");

                            // The element should exist by now so set values
                            xnDataBinding.Attributes["w:prefixMappings"].Value = cc.PrefixMappings;
                            xnDataBinding.Attributes["w:xpath"].Value = cc.XPath;
                            xnDataBinding.Attributes["w:storeItemID"].Value = cc.XmlPartId;
                        }
                    }                    
                    // -------- </w:dataBinding> ----------- //                  
                }
                else
                {
                    Debug.Fail("Could not find cc in dal with id " + sId);
                }
            } // End for each
        }

        private void SaveXdToPackagePart(XmlDocument xd, PackagePart pp)
        {
            // Try to save the part
            try
            {
                Stream stmWritable = pp.GetStream(System.IO.FileMode.Create, System.IO.FileAccess.Write);
                xd.Save(stmWritable);
                stmWritable.Close();
            }
            catch (Exception ex)
            {
                Debug.Fail("Failed to save xml.", ex.ToString());
            }
        }

        Dal.CCK CckFromXn(XmlNode xn)
        {
            switch (xn.LocalName)
            {
                case "comboBox": return Dal.CCK.ComboBox;
                case "date": return Dal.CCK.DatePicker;
                case "docPartList": return Dal.CCK.BuildingBlocksGallery;
                case "dropDownList": return Dal.CCK.DropDownList;
                case "picture": return Dal.CCK.Picture;
                case "richText": return Dal.CCK.RichText; // Broken right now because of O12/663921
                case "text": return Dal.CCK.PlainText;
                case "group": return Dal.CCK.Group;
            }

            //   Debug.Fail("Cannot find cck from xn where it's name is: " + xn.LocalName);
            return Dal.CCK.RichText; // If we cannot find the cc assume it is rich text (hopefully O12/663921 will be fixed so this is more accurate)
        }
    }
}
