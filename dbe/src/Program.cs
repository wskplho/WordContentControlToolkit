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
using System.Windows.Forms;
using dbe.Properties;

namespace dbe
{
    /// <summary>
    /// Program entry point
    /// </summary>
    public static class Program
    {
        private static DbeCore m_dbe;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UiFmMain ui = new UiFmMain();
            m_dbe = new DbeCore(ui);
            Application.Run(ui);
        }

        /// <summary>
        /// Provides global access to the core editor
        /// </summary>
        public static DbeCore Dbe
        {
            get { return m_dbe; }
            set { m_dbe = value; }
        }       
    }
}