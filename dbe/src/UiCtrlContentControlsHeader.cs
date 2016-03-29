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

namespace dbe
{
    public partial class UiCtrlContentControlsHeader : UserControl
    {
        public UiCtrlContentControlsHeader()
        {
            InitializeComponent();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            lClose.ForeColor = Color.White;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            lClose.ForeColor = Color.LightGray;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ((UiFmMain)ParentForm).HidePanel1();
        }       

        private void UCContentControlsHeader_Leave(object sender, EventArgs e)
        {
            BackColor = SystemColors.InactiveCaption;
        }

        private void UCContentControlsHeader_Enter(object sender, EventArgs e)
        {
            BackColor = SystemColors.ActiveCaption;
        }

        private void UCContentControlsHeader_DoubleClick(object sender, EventArgs e)
        {
            ((UiFmMain)ParentForm).TogglePanel2();
        }       
    }
}
