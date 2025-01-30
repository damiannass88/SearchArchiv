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
using SearchArchive.Models;

namespace SearchArchive.Controllers
{
    /// <summary>
    ///     Searching Class
    /// </summary>
    public class SearchController
    {
        private readonly string errorMessageF1 =
            "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F1) \n\nError Message: \n";

        private readonly string errorMessageF2 =
            "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F2) \n\nError Message: \n";

        private readonly string errorMessageF3 =
            "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F3) \n\nError Message: \n";

        public List<OutcomeInfosData> StartSearch(string searchIdName)
        {
            var results = SearchFiles(searchIdName);
            return results;
        }
        
        public List<OutcomeInfosData> GetOldIdItemsFromStructuredLevel(string nameIDsOld, string nameSettPathOld,
            string pathToFolderOld)
        {
            var results = new List<OutcomeInfosData>();

            try
            {
                if (Directory.Exists(pathToFolderOld))
                {
                    var itemsInDirector = Directory.EnumerateFiles(pathToFolderOld, "*", SearchOption.TopDirectoryOnly)
                        .ToList();

                    if (itemsInDirector.Any())
                    {
                        var pathToPdf = itemsInDirector
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "pdf");
                        var pathToDxf = itemsInDirector
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "dxf");
                        var pathToTif = itemsInDirector
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "tif");
                        var pathToJt = itemsInDirector
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "jt");
                        var pathToDoc = itemsInDirector
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "doc" ||
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "docx");
                        var nameVer = pathToFolderOld
                            .Remove(0, pathToFolderOld.LastIndexOf(@"\", StringComparison.Ordinal) + 1).ToUpper();

                        results.Add(new OutcomeInfosData
                        {
                            NameVer = nameVer,
                            NameSettPath = nameSettPathOld,
                            PathToPDF = pathToPdf,
                            PathToDXF = pathToDxf,
                            PathToTIF = pathToTif,
                            PathToJT = pathToJt,
                            PathToFolder = pathToFolderOld,
                            PathToDOC = pathToDoc,
                            NameIDsOld = nameIDsOld,
                            PriorityColor = "DimGray"
                        });
                    }
                    else
                    {
                        return new List<OutcomeInfosData>();
                    }
                }
                else
                {
                    return new List<OutcomeInfosData>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorMessageF3 + ex.Message, TitleCaption.INFORMATION.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return new List<OutcomeInfosData>();
            }

            return results;
        }

        private List<OutcomeInfosData> SearchFiles(string searchIdName)
        {
            // Always current dates.
            var paths = PathsCollectionController.GetPathsCollectionFromDefinedPathways();

            var resultsFromPaths = new List<OutcomeInfosData>();

            foreach (var path in paths)
                if (path.Structured)
                {
                    // Start searching ID number with Structured form method
                    var result = LookForStructuredLevels(path.Name, path.Path, searchIdName, path.PriorityColor);
                    if (result != null)
                        resultsFromPaths.AddRange(result);
                }
                else
                {
                    // Start searching ID number with one level structure folder method
                    var result = LookOneLevel(path.Name, path.Path, searchIdName, path.PriorityColor);

                    if (result != null)
                        resultsFromPaths.AddRange(result);
                }

            return resultsFromPaths;
        }

        private List<OutcomeInfosData> LookForStructuredLevels(string pathName, string path, string searchIdName,
            string priorityColor)
        {
            var results = new List<OutcomeInfosData>();
            var oldVersionInfo = new List<OldVersionData>();

            var firstFolder = searchIdName.Substring(0, 2);
            var secondFolder = searchIdName.Substring(2, 2);
            var actualPath = path + @"\" + firstFolder + @"\" + secondFolder;

            try
            {
                if (!Directory.Exists(actualPath)) return new List<OutcomeInfosData>();

                var enumerateDirectories =
                    Directory.EnumerateDirectories(actualPath, searchIdName + "*", SearchOption.TopDirectoryOnly)
                        .OrderBy(Directory.GetCreationTime).ToList();
                if (!enumerateDirectories.Any()) return new List<OutcomeInfosData>();

                if (enumerateDirectories.Count > 1)
                    // It's sorted Desc with creating data.
                    foreach (var director in enumerateDirectories.OrderByDescending(a => a))
                        oldVersionInfo.Add(new OldVersionData
                        {
                            NameIDsOld = director.Remove(0, director.LastIndexOf(@"\", StringComparison.Ordinal) + 1)
                                .ToUpper(),
                            NameSettPathOld = pathName,
                            PathToFolderOld = director
                        });
                var lastDirector = enumerateDirectories.Last();

                var itemsInDirector =
                    Directory.EnumerateFiles(lastDirector, "*", SearchOption.TopDirectoryOnly).ToList();

                if (itemsInDirector.Any())
                {
                    var pathToPdf = itemsInDirector
                        .OrderBy(s => s).LastOrDefault(a =>
                            a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "pdf");
                    var pathToDxf = itemsInDirector
                        .OrderBy(s => s).LastOrDefault(a =>
                            a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "dxf");
                    var pathToTif = itemsInDirector
                        .OrderBy(s => s).LastOrDefault(a =>
                            a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "tif");
                    var pathToJt = itemsInDirector
                        .OrderBy(s => s).LastOrDefault(a =>
                            a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "jt");
                    var pathToDoc = itemsInDirector
                        .OrderBy(s => s).LastOrDefault(a =>
                            a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "doc" ||
                            a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "docx");
                    var pathToFolder = lastDirector;
                    var nameVer = lastDirector.Remove(0, lastDirector.LastIndexOf(@"\", StringComparison.Ordinal) + 1)
                        .ToUpper();

                    results.Add(new OutcomeInfosData
                    {
                        NameVer = nameVer,
                        NameSettPath = pathName,
                        PathToPDF = pathToPdf,
                        PathToDXF = pathToDxf,
                        PathToTIF = pathToTif,
                        PathToJT = pathToJt,
                        PathToFolder = pathToFolder,
                        PathToDOC = pathToDoc,
                        OldVersionInfo = oldVersionInfo,
                        PriorityColor = priorityColor
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorMessageF2 + ex.Message, TitleCaption.INFORMATION.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }

            return results;
        }

        private List<OutcomeInfosData> LookOneLevel(string pathName, string path, string searchIdName,
            string priorityColor)
        {
            var results = new List<OutcomeInfosData>();
            try
            {
                if (Directory.Exists(path))
                {
                    var pathResults = Directory.EnumerateFiles(path, searchIdName + "*", SearchOption.TopDirectoryOnly)
                        .ToList();
                    if (pathResults.Any())
                    {
                        var pathToPdf = pathResults
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "pdf");
                        var pathToDxf = pathResults
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "dxf");
                        var pathToTif = pathResults
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "tif");
                        var pathToJt = pathResults
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "jt");
                        var pathToDoc = pathResults
                            .OrderBy(s => s).LastOrDefault(a =>
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "doc" ||
                                a.Remove(0, a.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower() == "docx");
                        var pathToFolder = path;
                        if (pathToPdf == null) return new List<OutcomeInfosData>();

                        var nameVer = pathToPdf.Remove(0, pathToPdf.LastIndexOf(@"\", StringComparison.Ordinal) + 1)
                            .ToUpper();

                        results.Add(new OutcomeInfosData
                        {
                            NameVer = nameVer,
                            NameSettPath = pathName,
                            PathToPDF = pathToPdf,
                            PathToDXF = pathToDxf,
                            PathToTIF = pathToTif,
                            PathToJT = pathToJt,
                            PathToFolder = pathToFolder,
                            PathToDOC = pathToDoc,
                            PriorityColor = priorityColor
                        });
                    }
                    else
                    {
                        return new List<OutcomeInfosData>();
                    }
                }
                else
                {
                    return new List<OutcomeInfosData>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorMessageF1 + ex.Message, TitleCaption.INFORMATION.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return new List<OutcomeInfosData>();
            }

            return results;
        }

    }
}