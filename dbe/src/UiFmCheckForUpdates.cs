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
using System.Net;
using System.Diagnostics;

namespace dbe
{
    public partial class UiFmCheckForUpdates : Form
    {
        public UiFmCheckForUpdates()
        {
            InitializeComponent();
        }

        private void UiFmCheckForUpdates_Load(object sender, EventArgs e)
        {
            // Show a progress message
            lMsg.Text = Resources.CheckUpdatesProgress;

            // Make a call to the codeplex page and asynchronously download the page so we can parse out the current version
            string sDbeUrl = Resources.CodePlexDbeUrl;
            WebClient webClient = new WebClient();
            webClient.UseDefaultCredentials = true;
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(sDbeUrl));
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Now that the page has been downloaded, lets parse.
            double nCurrVer = GetVerFromDbeHtmlStr(e.Result);
            if (nCurrVer > 0) /* On error, the version would be zero */
            {
                // Update UI to show we are done downloading
                progressBar1.Visible = false;

                // Releases to the web contain version numbers with only two fields of significance, eg. 2.4
                // Therefore, to compare versions, we strip the remaining version info from the local version
                string sLocalVer = Program.Dbe.Version;
                string[] rgsLocalVer = sLocalVer.Split('.');
                sLocalVer = rgsLocalVer[0] + "." + rgsLocalVer[1];
                double nLocalVer = Double.Parse(sLocalVer);

                if (nCurrVer > nLocalVer)
                {
                    lMsg.Text = Resources.CheckUpdatesNewAvail;
                    llDownload.Visible = true;
                }
                else
                {
                    lMsg.Text = Resources.CheckUpdatesUpToDate;
                    llDownload.Visible = false;
                }
            }
        }

        /// <summary>
        /// Parses html from codeplex.com/dbe to grab the current version number on the page. Returns 0.0 on failure.
        /// </summary>
        /// <returns>The version on success, or 0.0 on failure</returns>
        double GetVerFromDbeHtmlStr(string sHtml)
        {
            // The version is locked in this string CCT_?.?_, where ? is a number
            // Algorithm: Search for the prefix key "CCT_" then find the next _ and grab the number in between
            // This technique supports versions including 1.0 to 99.9

            // Details:
            //  Grab the string starting at i+4 whose length is 5, where i = the indexof(prefix key)
            //  Example: 
            //      CCT_12.6_
            //         ^     
            //         i+4

            const string sPrefixKey = "CCT_";
            int iFind = sHtml.IndexOf(sPrefixKey, 0);
            if (iFind != -1)
            {
                sHtml = sHtml.Substring(iFind + 4, 5);
                iFind = sHtml.IndexOf("_", 0);
                if (iFind != -1)
                {
                    sHtml = sHtml.Substring(0, iFind);
                    double nVer;
                    Double.TryParse(sHtml, out nVer);
                    return nVer;
                }
            }

            return 0;
        }

        private void llDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Resources.CodePlexReleaseUrl);
        }
    }
}