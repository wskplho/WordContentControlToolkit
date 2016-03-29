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
using System.Windows.Forms;
using dbe.Properties;
using System.Reflection;

namespace dbe
{
    /// <summary>
    /// Data Binding Editor Core -- The core controller of this application
    /// </summary>
    public class DbeCore
    {
        public enum DataSource
        {
            OpenXmlPackage,
            WordOM,
            Unknown
        }

        private Dal m_dal;
        private DataSource m_ds;
        private UiFmMain m_ui;       
        private string m_sPkgFullName;
        private bool m_fLoaded;
        private bool m_fDirtied;
       
        public DbeCore(UiFmMain ui)
        {
            m_dal = new Dal();
            m_ui = ui;
            m_fLoaded = false;
            m_sPkgFullName = null;
        }        

        /// <summary>
        /// Attempts to load the package from a given data source. Returns null on success, an error message otherwise
        /// </summary>
        public void Load(string sPkgFullName, DataSource ds)
        {
            m_sPkgFullName = sPkgFullName;
            m_ds = ds;

            // Clear the dal
            m_dal.Clear();

            // Our loader is agnostic
            IdbContentLoader loader = null;

            // Based on the datasource, decide how to load the data
            switch (ds)
            {
                case DataSource.OpenXmlPackage:
                    loader = new PkgLoader();
                    break;
                case DataSource.WordOM :
                    loader = new OmLoader();
                    break;
            }

            // Load it
            loader.Load(m_dal, sPkgFullName);            

            // Display
            m_ui.UiContentControls.Populate(m_dal);
            m_ui.UiCustomXmlParts.Populate(m_dal.XmlParts);            
            m_fLoaded = true;
        }

        public void Save()
        {
            IdbContentWriter saver = null;

            // Based on the datasource, decide how to save the data
            switch (m_ds)
            {
                case DataSource.OpenXmlPackage:
                    {
                        saver = new PkgWriter();
                        break;
                    }
            }

            // Save it
            saver.Write(m_dal, m_sPkgFullName);
           
            // At this point we succesfully saved, so set that we are not dirty
            m_fDirtied = false;
        }

        public void Close()
        {
            Clear();            
            m_ui.DisableAllUI();
        }

        /// <summary>
        /// Clears the memory of the DBE including child objects. Useful for reset.
        /// </summary>
        public void Clear()
        {
            // Clear this memory
            m_sPkgFullName = null;
            m_ds = DataSource.Unknown;
            m_fLoaded = false;
            m_fDirtied = false;
            m_dal.Clear();
            
            // Clear children's memory
            m_ui.UiContentControls.Clear();
            m_ui.UiCustomXmlParts.Clear();

            // Clean up
            GC.Collect();
        }

        public Dal Dal
        {
            get { return m_dal; }
        }

        public bool FLoaded
        {
            get { return m_fLoaded; }
            set { m_fLoaded = value; }
        }

        public bool FDirtied
        {
            get { return m_fDirtied; }
            set { m_fDirtied = value; }
        }

        public string PkgFullName
        {
            get { return m_sPkgFullName; }
            set { m_sPkgFullName = value; }
        }

        public string AppName
        {
            get { return Resources.DbeName; }
        }

        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public UiFmMain Ui
        {
            get { return m_ui; }
        }
    }
}
