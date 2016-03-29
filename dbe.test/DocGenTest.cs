using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dbe;
using System.IO;

namespace dbe.test
{
    /// <summary>
    /// Summary description for DocGenTest
    /// </summary>
    [TestClass]
    public class DocGenTest
    {
        private TestContext testContextInstance;
        private string m_testDocxLetterFullName;
        private string m_testXmlDataFolder;
        private string m_testGeneratedDocumentsOutputFolder;

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //  Write temporary template document
            m_testDocxLetterFullName = Path.GetTempFileName() + ".docx";
            using (FileStream fs = new FileStream(m_testDocxLetterFullName, FileMode.OpenOrCreate))
            {
                fs.Write(Resource1.docgentest_letter, 0, Resource1.docgentest_letter.Length);
            }

            // Create temporary folder to hold xml data files
            m_testXmlDataFolder = Path.Combine(Path.GetTempPath(), "cctdocgen_xmldata_" + Path.GetRandomFileName());
            Directory.CreateDirectory(m_testXmlDataFolder);
            File.WriteAllText(Path.Combine(m_testXmlDataFolder, "data1.xml"), Resource1.docgentest_letterdata1);
            File.WriteAllText(Path.Combine(m_testXmlDataFolder, "data2.xml"), Resource1.docgentest_letterdata2);

            // Create temporary folder to hold output files
            m_testGeneratedDocumentsOutputFolder = Path.Combine(Path.GetTempPath(), "cctdocgen_outgen_" + Path.GetRandomFileName());
            Directory.CreateDirectory(m_testGeneratedDocumentsOutputFolder);
        }

        //
        //Use TestCleanup to run code after each test has run
        //
        [TestCleanup()]
        public void MyTestCleanup()
        {
            File.Delete(m_testDocxLetterFullName);
            Directory.Delete(m_testXmlDataFolder, true /*recursive*/);
            Directory.Delete(m_testGeneratedDocumentsOutputFolder, true /*recursive*/);
        }

        /// <summary>
        ///A test for Generate
        ///</summary>
        [TestMethod()]
        public void GenerateTest()
        {
            // Generate the documents
            DocGen docGen = new DocGen();
            docGen.Generate(m_testDocxLetterFullName, m_testXmlDataFolder, m_testGeneratedDocumentsOutputFolder);

            // Verify the docs generated (count)
            string[] generatedFiles = Directory.GetFiles(m_testGeneratedDocumentsOutputFolder, "*.docx");
            Assert.AreEqual(generatedFiles.Length, 2);

            // Verify a log was created
            Assert.IsTrue(File.Exists(Path.Combine(m_testGeneratedDocumentsOutputFolder, "_log.txt")));

            // Test the generated documents (just need to sample 1)
            Program.Dbe = new DbeCore(new UiFmMain());
            string generatedDocx1 = Path.Combine(m_testGeneratedDocumentsOutputFolder, "data1.docx");
            Program.Dbe.Load(generatedDocx1, DbeCore.DataSource.OpenXmlPackage);
            Assert.AreEqual(Program.Dbe.PkgFullName, generatedDocx1);

            // Verify the content controls loaded properly
            Assert.AreEqual<int>(Program.Dbe.Dal.ContentControls.Count, 6);
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[0].XPath, "/ns0:root[1]/ns0:to[1]");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[1].XPath, "/ns0:root[1]/ns0:from[1]");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[2].XPath, "/ns0:root[1]/ns0:place[1]/ns0:at[1]");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[3].XPath, "/ns0:root[1]/ns0:place[1]/ns0:date[1]");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[4].XPath, "/ns0:root[1]/ns0:from[1]");
            Assert.AreEqual(Program.Dbe.Dal.ContentControls[5].XPath, "/ns0:root[1]/ns0:from[1]");

            // Verify the custom xml data loaded properly
            Assert.AreEqual<int>(Program.Dbe.Dal.XmlParts.Count, 1);
            Assert.AreEqual(Program.Dbe.Dal.XmlParts[0].XmlDom.OuterXml, "<root xmlns=\"LetterTest\"><to>Jack Smith</to><from>Matt Scott</from><place><at>Yankees Stadium</at><date>2/2/2010</date></place></root>");
        }
    }
}
