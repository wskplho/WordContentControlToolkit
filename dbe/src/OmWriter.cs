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
    /// Object Model Writer -- Write data to Word object model (when connected as an addin) from a DAL
    /// </summary>
    class OmWriter : IdbContentWriter
    {
        // Intentionally left not implemented. 
        //
        // Leaving room for the possibility of this tool becoming an addin some day and as such
        // it will need to write data to the Word object model.

        public void Write(Dal dal, string sFileFullName)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
