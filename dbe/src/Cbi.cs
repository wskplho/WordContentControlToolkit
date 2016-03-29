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

namespace dbe
{
    /// <summary>
    /// Combo Box Item -- useful for populating drop down lists with display/value pairs
    /// </summary>
    public class Cbi
    {
        private string m_sDisplay;
        private object m_oValue;
        public Cbi(string sDisplay, object oValue)
        {
            m_sDisplay = sDisplay;
            m_oValue = oValue;
        }
        public string Display
        {
            get { return m_sDisplay; }
        }
        public object Value
        {
            get { return m_oValue; }
        }
    }
}
