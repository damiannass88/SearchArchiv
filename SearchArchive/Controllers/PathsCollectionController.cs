// /******************************************************
//  * MIT License
//  * Copyright © 2025 Damian Nass
// 
//  * Permission is hereby granted, free of charge, to any person obtaining a copy
//  * of this software to use, copy, modify, merge, publish, and distribute it.
// 
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND.
//  * For inquiries, contact: damiannass@nas4.tech
//  ******************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SearchArchive.Models;

namespace SearchArchive.Controllers
{
    /// <summary>
    ///     Class to management Config XML file, with paths to be searched
    /// </summary>
    public class PathsCollectionController
    {
        private static readonly string PathToXmlSettingsPaths = Directory.GetCurrentDirectory() + "/SettingsPaths.xml";

        private static readonly string ErrorMessageFileCannotBeOpen =
            "SettingsPaths.xml kann nicht offnen werden. \n Bitte Laufwerke Pruefen.\n Error Message: \n";

        private static readonly string ErrorMessageFileCannotBeCreate =
            "SettingsPaths.xml kann nicht erstellt werden. \n Bitte Laufwerke Pruefen.\n Error Message: \n";

        private static readonly string SuccessMessageFileCreated =
            "Neue Datei SettingsPaths.xml wurde erstellt! \n\n Bitte App neu Starten.";

        public static List<PathsCollection> GetPathsCollectionFromDefinedPathways()
        {
            try
            {
                var listSettingsPaths = new List<PathsCollection>();

                // get paths dates from SettingsPaths.xml file.
                var document = XDocument.Load(PathToXmlSettingsPaths);
                var listPathsXml = document.Descendants("Path").ToList();

                foreach (var xElement in listPathsXml.Distinct())
                {
                    var nameAttribute = xElement.Attribute("Name");
                    var pathAttribute = xElement.Attribute("Path");
                    var structuredAttribute = xElement.Attribute("Structured");
                    var priorityColorAttribute = xElement.Attribute("PriorityColor");

                    if (nameAttribute != null && pathAttribute != null && structuredAttribute != null &&
                        priorityColorAttribute != null)
                        listSettingsPaths.Add(new PathsCollection
                        {
                            Name = nameAttribute.Value,
                            Path = pathAttribute.Value,
                            Structured = Convert.ToBoolean(structuredAttribute.Value),
                            PriorityColor = priorityColorAttribute.Value
                        });
                }

                if (listSettingsPaths.Any()) return listSettingsPaths;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ErrorMessageFileCannotBeOpen + ex.Message, TitleCaption.INFORMATION.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return null;
        }

        // Method to re-creates paths structure in to SettingsPaths.xml file. 
        public void CreateNewXML(List<PathsCollection> NewListPaths)
        {
            try
            {
                var xml = XDocument.Load(PathToXmlSettingsPaths);
                xml.RemoveNodes();
                xml.Add(new XElement("Paths"));

                foreach (var newItem in NewListPaths)
                {
                    var NewPath = new XElement("Path");
                    NewPath.Add(new XAttribute("Name", newItem.Name));
                    NewPath.Add(new XAttribute("Path", newItem.Path));
                    NewPath.Add(new XAttribute("Structured", newItem.Structured.ToString().ToLower()));
                    NewPath.Add(new XAttribute("PriorityColor", newItem.PriorityColor));

                    xml.Element("Paths")?.Add(NewPath);
                }

                xml.Save(PathToXmlSettingsPaths);

                // Checking if file exists to prove success.
                if (File.Exists(PathToXmlSettingsPaths))
                    MessageBox.Show(SuccessMessageFileCreated, TitleCaption.SUCCESS.ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ErrorMessageFileCannotBeCreate + ex.Message, TitleCaption.INFORMATION.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}