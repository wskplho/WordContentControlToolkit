﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using dbe;
using System.IO;
namespace dbe.test
{
    /// <summary>
    ///This is a test class for dbe.DbeCore and is intended
    ///to contain all dbe.DbeCore Unit Tests
    ///</summary>
    [TestClass()]
    public class DbeCoreTest
    {
        private TestContext testContextInstance;
        private string m_tmpDocFullName;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // Write a temp document to disk for reading 
            m_tmpDocFullName = Path.GetTempFileName();
            FileStream fs = new FileStream(m_tmpDocFullName, FileMode.OpenOrCreate);
            fs.Write(Resource1.test_docx, 0, Resource1.test_docx.Length);
            fs.Close();

            Program.Dbe = null;
        }
        //
        //Use TestCleanup to run code after each test has run
        //
        [TestCleanup()]
        public void MyTestCleanup()
        {
            File.Delete(m_tmpDocFullName);
        }
        //
        #endregion


        /// <summary>
        ///A test for DbeCore (UiFmMain)
        ///</summary>
        [DeploymentItem("dbe.exe")]
        [TestMethod()]
        public void LoadTest()
        {
            // Make sure we are starting fresh
            Assert.IsNull(Program.Dbe);
            
            // Load the test document
            Program.Dbe = new DbeCore(new UiFmMain());
            Program.Dbe.Load(m_tmpDocFullName, DbeCore.DataSource.OpenXmlPackage);
            Assert.AreEqual(Program.Dbe.PkgFullName, m_tmpDocFullName);
            
            // Verify the content controls loaded properly
            Assert.AreEqual<int>(Program.Dbe.Dal.ContentControls.Count, 9);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].Type, Dal.CCK.Picture);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].Subdoc, Dal.SubDocK.MainDocument);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].Id, "238035879");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[1].Type, Dal.CCK.DropDownList);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[1].Subdoc, Dal.SubDocK.MainDocument);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[2].Type, Dal.CCK.DatePicker);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[3].Type, Dal.CCK.BuildingBlocksGallery);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[4].Type, Dal.CCK.PlainText);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[4].Subdoc, Dal.SubDocK.Comments);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[5].Type, Dal.CCK.Group);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[5].Subdoc, Dal.SubDocK.Endnotes);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[6].Type, Dal.CCK.RichText);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[6].Subdoc, Dal.SubDocK.Footnotes);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[7].Type, Dal.CCK.PlainText);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[7].Subdoc, Dal.SubDocK.Header);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[7].XPath, "/abc/b");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[7].XmlPartId, "{ADB7254E-7D01-4ECE-BFCC-6546D7A077F0}");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[8].Type, Dal.CCK.ComboBox);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[8].Subdoc, Dal.SubDocK.Footer);

            // Verify the custom xml loaded properly
            Assert.AreEqual<int>(Program.Dbe.Dal.XmlParts.Count, 2);
            Assert.AreEqual(Program.Dbe.Dal.XmlParts[0].Id, "{68CAD847-3F65-4469-A5D7-F8EEF642F45B}");
            Assert.AreEqual(Program.Dbe.Dal.XmlParts[1].Id, "{ADB7254E-7D01-4ECE-BFCC-6546D7A077F0}");
            Assert.AreEqual(Program.Dbe.Dal.XmlParts[1].XmlDom.OuterXml, "<abc><a /><b>foo</b><c /></abc>");
        }

        [TestMethod()]
        public void EditAndSaveTest()
        {
            // Make sure we are starting fresh
            Assert.IsNull(Program.Dbe);

            // Load the test document
            Program.Dbe = new DbeCore(new UiFmMain());
            Program.Dbe.Load(m_tmpDocFullName, DbeCore.DataSource.OpenXmlPackage);
            Assert.AreEqual(Program.Dbe.PkgFullName, m_tmpDocFullName);

            // Make an edit, then save, reload and check
            Assert.IsNull(Program.Dbe.Dal.ContentControls[0].XPath);
            Program.Dbe.Dal.ContentControls[0].XmlPartId = "{ADB7254E-7D01-4ECE-BFCC-6546D7A077F0}";
            Program.Dbe.Dal.ContentControls[0].XPath = "/abc/a";
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].XPath, "/abc/a");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].XmlPartId, "{ADB7254E-7D01-4ECE-BFCC-6546D7A077F0}");
            Program.Dbe.Save();
            Program.Dbe.Close();
            Program.Dbe.Load(m_tmpDocFullName, DbeCore.DataSource.OpenXmlPackage);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].XPath, "/abc/a");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].XmlPartId, "{ADB7254E-7D01-4ECE-BFCC-6546D7A077F0}");
        }
    }


}