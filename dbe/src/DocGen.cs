using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace dbe
{
    /// <summary>
    /// Generate Word Documents by merging a template document with XML data files
    /// </summary>
    public class DocGen
    {
        private string m_logFileFullName = null;

        public void Generate(string sTemplWordDocumentFullName,
                                string sInputFolderXmlDataFullName,
                                string sOutputFolderForGeneratedDocuments)
        {
            m_logFileFullName = GetLogFileName(sOutputFolderForGeneratedDocuments);
            File.CreateText(m_logFileFullName).Close();
            LogInfo(string.Format("Log created at <{0}>", m_logFileFullName));
            
            LogInfo(string.Format("Generating from template <{0}>, from XML data from <{1}>, to output folder <{2}>",
                    sTemplWordDocumentFullName, sInputFolderXmlDataFullName, sOutputFolderForGeneratedDocuments));

            string[] xmlDataFiles = Directory.GetFiles(sInputFolderXmlDataFullName, "*.xml");
            LogInfo(string.Format("Will attempt to generate {0} instance documents.", xmlDataFiles.Length));

            foreach (string sXmlFileFullName in xmlDataFiles)
            {
                #region Load the XML data
                
                XmlDocument xdData = new XmlDocument();
                try
                {
                    LogInfo(string.Format("Loading XML data file <{0}>", sXmlFileFullName));
                    xdData.Load(sXmlFileFullName);
                }
                catch (Exception ex)
                {
                    LogError(string.Format("Could not load XML file <{0}>. Skipping. {1}", sXmlFileFullName, ex.Message));
                    continue; // <-- If fail continue to try and process more data files
                }

                #endregion 

                #region Create a copy of the template Word document

                string sGeneratedWordDocumentFullName = Path.Combine(sOutputFolderForGeneratedDocuments, 
                                                            Path.GetFileNameWithoutExtension(sXmlFileFullName) + 
                                                                Path.GetExtension(sTemplWordDocumentFullName));

                try
                {
                    LogInfo(string.Format("Initiating generation of instance document <{0}>", sGeneratedWordDocumentFullName));
                    File.Copy(sTemplWordDocumentFullName, sGeneratedWordDocumentFullName, true /*overwrite*/);
                }
                catch (Exception ex)
                {
                    LogError(string.Format("Could not copy template from <{0}> to <{1}>. Skipping. {2}",
                            sTemplWordDocumentFullName, sGeneratedWordDocumentFullName, ex.Message));
                    continue; // <-- If fail continue to try and process more data files
                }

                #endregion

                #region Load the Word document to generate

                Dal documentDal = null;
                try
                {
                    documentDal = new Dal();
                    IdbContentLoader loader = new PkgLoader();
                    loader.Load(documentDal, sGeneratedWordDocumentFullName);
                }
                catch (Exception ex)
                {
                    LogError(string.Format("Could not load Word document to generate <{0}>. {1}",
                            sGeneratedWordDocumentFullName, ex.Message));

                    continue; // <-- If fail continue to try and process more data files
                }

                #endregion

                #region Inject the XML data into the Custom XML part (match by namespace)

                bool fXmlDataInjected = false;
                string dataXmlNs = xdData.DocumentElement.NamespaceURI;
                try
                {
                    LogInfo(string.Format("Attempting to inject XML data (namespace: <{0}>) into instance placeholder", dataXmlNs));
                    foreach (Dal.XP xmlPart in documentDal.XmlParts)
                    {
                        string xmlPartNs = xmlPart.Ns;
                        if (!string.IsNullOrEmpty(xmlPartNs) && !string.IsNullOrEmpty(dataXmlNs)
                            && xmlPartNs == dataXmlNs) // XML Namespaces are treated case sensitive
                        {
                            xmlPart.XmlDom = xdData;
                            fXmlDataInjected = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(string.Format("Could not inject XML data file <{0}> into Word document <{1}>. Skipping. {2}",
                       sXmlFileFullName, sGeneratedWordDocumentFullName, ex.Message));

                    continue; // <-- If fail continue to try and process more data files
                }
                if (!fXmlDataInjected)
                {
                    LogError(string.Format("There was no xml data injected into the Word document <{0}> because it did not contain a placeholder xml document with a matching namespace of {1}",
                       sGeneratedWordDocumentFullName, dataXmlNs));
                }

                #endregion

                #region Write generated document

                // Save
                //
                try
                {
                    IdbContentWriter saver = new PkgWriter();
                    saver.Write(documentDal, sGeneratedWordDocumentFullName);
                    LogInfo(string.Format("Instance document <{0}> generated successfully", sGeneratedWordDocumentFullName));
                }
                catch (Exception ex)
                {
                    LogError(string.Format("Could not write content to new Word document <{0}> . {1}",
                        sGeneratedWordDocumentFullName, ex.Message));

                    continue; // <-- If fail continue to try and process more data files
                }

                #endregion
            }

            LogInfo("Work completed.");
        }

        private void LogError(string msg)
        {
            using (StreamWriter sw = File.AppendText(m_logFileFullName))
            {
                sw.WriteLine(DateTime.Now + ": Error: " + msg);
            }
        }

        private void LogInfo(string msg)
        {
            using (StreamWriter sw = File.AppendText(m_logFileFullName))
            {
                sw.WriteLine(DateTime.Now + ": " + msg);
            }
        }

        public static string GetLogFileName(string outputDirFullName)
        {
            return Path.Combine(outputDirFullName, "_log.txt");
        }
    }
}