using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchArchiv.Classes
{
    /// <summary>
    /// Class to menagment Config XML file, with paths to be searched
    /// </summary>
    public struct PathsCollection
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Structured { get; set; } 
    }

    public class Settings_AppConfig_Class {

        // Not admin right on targets ide, so without dynamic file creating.
        private static readonly string PathtoxmlSettingsPaths = Directory.GetCurrentDirectory() + "/SettingsPaths.xml";
         
        public static List<PathsCollection> GetPathsFromXML()
        {
            // Methode to get paths dates from SettingsPaths.xml file. 
            try
            {
                List<XElement> ListPathsXML = new List<XElement>();
                List<PathsCollection> ListSettingsPaths = new List<PathsCollection>();

                XDocument XML = XDocument.Load(PathtoxmlSettingsPaths);
                ListPathsXML = XML.Descendants("Path").ToList();

                foreach (XElement xelement in ListPathsXML.Distinct())
                {
                     
                    ListSettingsPaths.Add(new PathsCollection()
                    {
                        Name = xelement.Attribute("name").Value,
                        Path = xelement.Attribute("path").Value,
                        Structured = Convert.ToBoolean(xelement.Attribute("structured").Value)
                    });
                }
                if (ListSettingsPaths.Any())
                {
                    return ListSettingsPaths;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("SettingsPaths.xml kann nicht offnen werden. \n Bitte Laufwerke Pruefen.\n Error Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            return null;
        }

        public void CreateNewXML(List<PathsCollection> NewListPaths)
        {
            // Methode to recreates paths dates in to SettingsPaths.xml file. 
            try
            {
                XDocument XML = XDocument.Load(PathtoxmlSettingsPaths);
                XML.RemoveNodes();
                XML.Add(new XElement("Paths")); 

                foreach (PathsCollection newItem in NewListPaths)
                {
                    XElement NewPath = new XElement("Path");
                    NewPath.Add(new XAttribute("name", newItem.Name));
                    NewPath.Add(new XAttribute("path", newItem.Path));
                    NewPath.Add(new XAttribute("structured", newItem.Structured.ToString().ToLower()));

                    XML.Element("Paths").Add(NewPath);
                }
                XML.Save(PathtoxmlSettingsPaths);

                // Checking if file still exists to prove success.
                if (File.Exists(PathtoxmlSettingsPaths))
                    System.Windows.Forms.MessageBox.Show("Neue Datei SettingsPaths.xml wurde erstellt! \n\n Bitte App neu Starten.", "SUCCESS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("SettingsPaths.xml kann nicht erstellt werden. \n Bitte Laufwerke Pruefen.\n Error Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            } 
        }  
    }
}
