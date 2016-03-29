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
using System.IO;
using System.Diagnostics;

namespace dbe
{
    class XmlUtil
    {
        /// <summary>
        /// Determines if a given xml document is a schema.
        /// </summary>
        internal static bool FSchema(string sXml)
        {
            return GetNsFromStr(sXml).ToLower() == "http://www.w3.org/2001/xmlschema";
        }

        internal static string PrettyPrint(string sXml)
        {
            string sResult = string.Empty;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Unicode);
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.LoadXml(sXml);
                xtw.Formatting = Formatting.Indented;
                xd.WriteContentTo(xtw);
                xtw.Flush();
                ms.Flush();
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                String FormattedXML = sr.ReadToEnd();
                sResult = FormattedXML;
            }
            catch (XmlException ex)
            {
                Debug.Fail(ex.ToString());
            }

            ms.Close();
            xtw.Close();

            return sResult;
        }

        /// <summary>
        /// Attempts to get the namespace of an xml document given as plain text. 
        /// If the document element is malformed, this function returns null, otherwise the current namespace.
        /// </summary>    
        internal static string GetNsFromStr(string sXml)
        {
            StringReader sr = new StringReader(sXml);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.None;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(sr, settings);
            string sNs = null;
            try
            {
                reader.MoveToContent();
                sNs = reader.NamespaceURI;
            }
            catch (XmlException ex)
            {
                Debug.Fail(ex.ToString());
            }

            return sNs;
        }
    }
}
