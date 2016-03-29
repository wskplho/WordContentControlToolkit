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
    public partial class UiFmLicenseViewer : Form
    {
        public UiFmLicenseViewer()
        {
            InitializeComponent();

            tb.Text = Resources.license_mslpl;                       
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}