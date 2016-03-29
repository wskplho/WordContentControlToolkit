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
using System.IO.Packaging;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace dbe
{
    /// <summary>
    /// Package Navigator -- Contains helper functions to navigate a Word Open XML package
    /// </summary>
    class PkgNavigator
    {
        #region Office XML Relationship Types
        public static class OXMLRelTypes
        {
            public const string OfficeDocumentRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
            public const string ThemeRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme";
            public const string ImageRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image";
            public const string StylesRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";
            public const string HyperlinkRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";
            public const string SettingsRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings";
            public const string CommentsRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";

            public const string CorePropertiesRelType = @"http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";
            public const string CustomPropertiesRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties";
            public const string AppPropertiesRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties";

            public const string CustomXMLRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml";
            public const string CustomXMLPropsType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps";
            public const string VBAProjectRelType = @"http://schemas.microsoft.com/office/2006/relationships/vbaProject";
            public const string XLMSheetRelType = @"http://schemas.microsoft.com/officeDocument/2006/relationships/XLMSheets";
            public const string ExcelWorkSheetRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet";
            public const string WordFooterRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer";
            public const string WordHeaderRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/header";
            public const string WordFootNotesRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes";
            public const string WordEndNotesRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes";

            public const string HiddenTextElement = @"w:vanish";
            public const string ExcelFooterElement = @"oddFooter";
            public const string ExcelHeaderElement = @"oddHeader";

            public const string WordDocumentContentType = @"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml";
            public const string WordMacroEnabledDocumentContentType = @"application/vnd.ms-word.document.macroEnabled.main+xml";
            public const string ExcelWorkbookContentType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml";
            public const string ExcelMacroEnabledWorkbookContentType = @"application/vnd.ms-excel.sheet.macroEnabled.main+xml";
            public const string PowerPointPresentationContentType = @"application/vnd.openxmlformats-officedocument.presentationml.presentation.main+xml";
            public const string PowerPointMacroEnabledPresentationContentType = @"application/vnd.ms-powerpoint.presentation.macroEnabled.main+xml";
            public const string PowerPointSlideRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide";
            public const string PowerPointNotesMasterRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster";
            public const string PowerPointNotesRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide";
            public const string CommentAuthorsRelType = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors";
        }
        #endregion

        private Package m_pkg;
        private PackagePart m_ppMain;
        private string m_sWdNs;
        private string m_sPackageFullPath;

        public string WordNamespace
        {
            get { return m_sWdNs; }
        }
        
        public PkgNavigator(string sPackageFullPath)
        {
            m_sPackageFullPath = sPackageFullPath;
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(m_sPackageFullPath))
                throw new ArgumentNullException("sPackageFullPath", "We were asked to load a package but the full name was null or empty");

            m_pkg = Package.Open(m_sPackageFullPath);
            m_ppMain = GetMainPart(m_pkg);
            m_ppMain.GetStream();
            m_sWdNs = GetWordNamespace(m_pkg);                
        }

        public void Close()
        {
            m_pkg.Close();
        }

        private PackagePart GetMainPart(Package package)
        {
            PackageRelationshipCollection relcoll = package.GetRelationshipsByType(OXMLRelTypes.OfficeDocumentRelType);
            IEnumerator<PackageRelationship> enumrel = relcoll.GetEnumerator();
            enumrel.MoveNext();
            PackageRelationship mainRel = enumrel.Current;
            System.Uri partURI = mainRel.TargetUri;
            System.Uri uri = PackUriHelper.ResolvePartUri(new Uri(mainRel.SourceUri.ToString(), UriKind.Relative), mainRel.TargetUri);
            return package.GetPart(uri);
        }

        private PackageRelationshipCollection GetRelationshipsByTypeFromMainPart(string sRelType)
        {
            return m_ppMain.GetRelationshipsByType(sRelType);
        }

        private PackageRelationship GetSingleRelationshipByTypeFromMainPart(string sRelType)
        {
            return GetFirstRelByType(m_ppMain, sRelType);
        }        

        private string GetWordNamespace(Package pkg)
        {
            string wordNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/5/mainxhshrfegdhjfdsjhdsajhfds";
            PackagePart mainPart = GetMainPart(pkg);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(mainPart.GetStream());
            wordNamespace = xmldoc.DocumentElement.NamespaceURI;
            return wordNamespace;
        }       

        private PackagePart GetSinglePackagePartFromMain(string sRelType)
        {
            PackageRelationship pr = GetSingleRelationshipByTypeFromMainPart(sRelType);
            if (pr == null)
                return null;

            return GetPartFromRel(pr);
        }

        private List<PackagePart> GetPackagePartsFromMain(string sRelType)
        {
            List<PackagePart> rgpp = new List<PackagePart>();

            // There will be only a single comment part for a Word document
            PackageRelationshipCollection prc = GetRelationshipsByTypeFromMainPart(sRelType);

            PackagePart pp = null;
            foreach (PackageRelationship pr in prc)
            {
                pp = GetPartFromRel(pr);
                rgpp.Add(pp);
            }

            return rgpp;
        }

        public List<PackagePart> GetHeaderPackageParts()
        {
            return GetPackagePartsFromMain(OXMLRelTypes.WordHeaderRelType);
        }

        public List<PackagePart> GetFooterPackageParts()
        {
            return GetPackagePartsFromMain(OXMLRelTypes.WordFooterRelType);
        }

        public PackagePart GetCommentsPackagePart()
        {
            return GetSinglePackagePartFromMain(OXMLRelTypes.CommentsRelType);
        }
     
        public PackagePart GetFootNotesPackagePart()
        {
            return GetSinglePackagePartFromMain(OXMLRelTypes.WordFootNotesRelType);
        }

        public PackagePart GetEndNotesPackagePart()
        {
            return GetSinglePackagePartFromMain(OXMLRelTypes.WordEndNotesRelType);
        }

        public PackagePart GetMainPackagePart()
        {
            return m_ppMain;
        }

        public Package Package
        {
            get { return m_pkg; }
        }

        public class CustomXmlItem
        {
            public Stream stmXmlItem;
            public Stream stmXmlItemProp;
            public PackagePart ppXmlItem;
            public PackagePart ppXmlItemProp;
            public string sItemId;
            public string sMainRelId;
        }

        public List<CustomXmlItem> GetCustomXmlItems()
        {
            List<CustomXmlItem> rgcxi = new List<CustomXmlItem>();

            PackagePart ppXmlStoreItem;
            PackageRelationship prXmlItemProp;
            PackagePart ppItemProp;

            PackageRelationshipCollection prcXmlStoreItems = m_ppMain.GetRelationshipsByType(OXMLRelTypes.CustomXMLRelType);
            foreach (PackageRelationship prXmlStoreItem in prcXmlStoreItems)
            {
                ppXmlStoreItem = GetPartFromRel(prXmlStoreItem);
                
                // If we have a rel but no part, then the rel is dangling, so skip this cxi
                if (ppXmlStoreItem == null)
                   continue;

                // Get the prop xml package part
                prXmlItemProp = GetFirstRelByType(ppXmlStoreItem, OXMLRelTypes.CustomXMLPropsType);
                ppItemProp = GetPartFromRel(prXmlItemProp);
                
                // Create a cxi that represents this custom xml item
                CustomXmlItem cxi = new CustomXmlItem();
                cxi.stmXmlItem = ppXmlStoreItem.GetStream();
                cxi.stmXmlItemProp = ppItemProp.GetStream();
                cxi.ppXmlItem = ppXmlStoreItem;
                cxi.ppXmlItemProp = ppItemProp;
                cxi.sItemId = GetCustomXmlItemIdFromStmProp(cxi.stmXmlItemProp);
                cxi.sMainRelId = prXmlStoreItem.Id;
                rgcxi.Add(cxi);
            }

            return rgcxi;
        }

        public CustomXmlItem GetCustomXmlItemById(string sItemId)
        {
            List<CustomXmlItem> rgcxi = GetCustomXmlItems();
            foreach (CustomXmlItem cxi in rgcxi)
            {
                if (cxi.sItemId == sItemId)
                    return cxi;
            }

            return null;
        }

        public string GetCustomXmlItemIdFromStmProp(Stream stmItemProp)
        {
            Debug.Assert(stmItemProp.Position == 0, "The stream must be rewound to zero for its contents to be fully read");
            XmlDocument xdItemProp = new XmlDocument();
            bool fXdLoaded = false;
            string sItemId = null;

            try
            {
                xdItemProp.Load(stmItemProp);
                fXdLoaded = true;
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());                
            }

            if (fXdLoaded)
            {
                if (xdItemProp.DocumentElement.HasAttributes &&
                    xdItemProp.DocumentElement.Attributes.GetNamedItem("ds:itemID") != null)
                {
                    sItemId = xdItemProp.DocumentElement.Attributes.GetNamedItem("ds:itemID").Value;
                }
                else
                {
                    Debug.Fail("Expecting a custom xml properties file which should have a ds:itemID attribute");
                }
            }

            // Rewind the stream so that others can use the stream
            stmItemProp.Position = 0;

            return sItemId;
        }

        //public CustomXmlItem GetCustomXmlAndPropFromId(string sId 
        public static XmlDocument GetXdFromPp(PackagePart pp)
        {
            XmlDocument xd = new XmlDocument();
            Stream stm = pp.GetStream();
            Debug.Assert(stm.Position == 0, "The stream must be rewound to zero for its contents to be fully read");
            try
            {
                xd.Load(stm);
            }
            catch (Exception ex)
            {
                xd = null;
                Debug.Fail(ex.ToString());                
            }
            finally
            {
                stm.Close();
            }

            return xd;
        }        

        public static PackageRelationship GetFirstRelByType(PackagePart packagePart, string relType)
        {
            PackageRelationshipCollection relcoll = packagePart.GetRelationshipsByType(relType);
            IEnumerator<PackageRelationship> enumrel = relcoll.GetEnumerator();
            enumrel.MoveNext();
            PackageRelationship rel = enumrel.Current;
            return rel;
        }

        public static PackagePart GetPartFromRel(PackageRelationship pr)
        {
            System.Uri uri;

            if (pr.TargetMode == TargetMode.External)
            {
                uri = pr.TargetUri;
                return null;
            }
            else
            {
                uri = PackUriHelper.ResolvePartUri(new Uri(pr.SourceUri.ToString(), UriKind.Relative), pr.TargetUri);
            }

            // If the package does not exist, then this is a dangling relationship, notify the caller by returning null
            if (!pr.Package.PartExists(uri))
                return null;

            return pr.Package.GetPart(uri);
        }
    }
}
