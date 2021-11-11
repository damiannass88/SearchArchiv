using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchArchiv.Classes
{
    /// <summary>
    /// Searching Class
    /// </summary>
    public class SearchingClass
    {
        public struct IDsDates
        {
            public string NameVER { get; set; }
            public string NameSettPath { get; set; }
            public string PathToPDF { get; set; }
            public string PathToDXF { get; set; }
            public string PathToTIF { get; set; }
            public string PathToJT { get; set; }
            public string PathToFolder { get; set; }
            public string PathToDOC { get; set; }
            public List<OldVerDates> OldVersionNameAndPath { get; set; }
            public string NameIDsOld { get; set; }
            public string PriorityColor { get; set; }
        }
        public struct OldVerDates
        {
            public string NameIDsOld { get; set; }
            public string NameSettPathOld { get; set; }
            public string PathToFolderOld { get; set; }
        }

        public List<IDsDates> StartSearch(string IDnumber)
        {
            var results = SearchFiles(IDnumber);
            return results;
        }

        public List<IDsDates> SearchFiles(string IDnumber)
        {
            // Czy lepiej ją zapisać?
            var Paths = Settings_AppConfig_Class.GetPathsFromXML();

            List<IDsDates> ResultsFromPaths = new List<IDsDates>();

            foreach (var path in Paths)
            {
                if (path.Structured)
                {
                    // Start searching ID number with Structured form method
                    var result = LookForStructedLevels(path.Name, path.Path, IDnumber, path.PriorityColor);
                    if (result != null)
                        ResultsFromPaths.AddRange(result);
                }
                else
                {
                    // Start searching ID number with one level structur folder method
                    var result = LookOneLevel(path.Name, path.Path, IDnumber, path.PriorityColor);

                    if (result != null)
                        ResultsFromPaths.AddRange(result);
                }
            }
            return ResultsFromPaths;
        }

        public List<IDsDates> LookOneLevel(string PathName, string Path, string IDnumber, string PriorityColor)
        {
            var results = new List<IDsDates>();
            try
            {
                if (Directory.Exists(Path))
                {
                    var PathResults = Directory.EnumerateFiles(Path, IDnumber + "*", SearchOption.TopDirectoryOnly);
                    if (PathResults.Any())
                    {
                        var PathtoPdf = PathResults.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "pdf").LastOrDefault();
                        var PathtoDxf = PathResults.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "dxf").LastOrDefault();
                        var PathtoTif = PathResults.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "tif").LastOrDefault();
                        var PathtoJt = PathResults.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "jt").LastOrDefault();
                        var PathtoDoc = PathResults.OrderBy(s => s)
                            .Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "doc" || a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "docx").LastOrDefault();
                        var Pathtofolder = Path;
                        var NameVer = PathtoPdf.Remove(0, PathtoPdf.LastIndexOf(@"\") + 1).ToUpper();

                        results.Add(new IDsDates()
                        {
                            NameVER = NameVer,
                            NameSettPath = PathName,
                            PathToPDF = PathtoPdf,
                            PathToDXF = PathtoDxf,
                            PathToTIF = PathtoTif,
                            PathToJT = PathtoJt,
                            PathToFolder = Pathtofolder,
                            PathToDOC = PathtoDoc,
                            PriorityColor = PriorityColor
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F1) \n\nError Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }
            return results;
        }

        public List<IDsDates> LookForStructedLevels(string PathName, string Path, string IDnumber, string PriorityColor)
        {
            var results = new List<IDsDates>();
            List<OldVerDates> oldversionNameAndPath = new List<OldVerDates>();

            string FirstFolder = IDnumber.Substring(0, 2);
            string SecondFolder = IDnumber.Substring(2, 2);
            string ActualPath = Path + @"\" + FirstFolder + @"\" + SecondFolder;

            try
            {
                if (Directory.Exists(ActualPath))
                {
                    var DirectorColl = Directory.EnumerateDirectories(ActualPath, IDnumber + "*", SearchOption.TopDirectoryOnly);
                    if (DirectorColl.Any())
                    { 
                        DirectorColl = DirectorColl.OrderBy(a => Directory.GetCreationTime(a));
                        if (DirectorColl.Count() > 1)
                        {
                            // It's sorted with creating data.
                            foreach (var director in DirectorColl)
                            {
                                oldversionNameAndPath.Add(new OldVerDates() { 
                                NameIDsOld = director.Remove(0, director.LastIndexOf(@"\") + 1).ToUpper(),
                                NameSettPathOld = PathName,
                                PathToFolderOld = director.ToString(),
                                });
                            }
                        }
                        var LastDirector = DirectorColl.Last();

                        var ItemsInDirector = Directory.EnumerateFiles(LastDirector, "*", SearchOption.TopDirectoryOnly);
                        // var ItemsInDirector = Directory.EnumerateFiles(LastDirector, "*", SearchOption.TopDirectoryOnly).OrderBy(a => File.GetCreationTime(a));

                        if (ItemsInDirector.Any())
                        {
                            var PathtoPdf = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "pdf").LastOrDefault();
                            var PathtoDxf = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "dxf").LastOrDefault();
                            var PathtoTif = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "tif").LastOrDefault();
                            var PathtoJt = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "jt").LastOrDefault();
                            var PathtoDoc = ItemsInDirector.OrderBy(s => s)
                                .Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "doc" || a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "docx").LastOrDefault();
                            var Pathtofolder = LastDirector;
                            var NameVer = LastDirector.Remove(0, LastDirector.LastIndexOf(@"\") + 1).ToUpper();

                            results.Add(new IDsDates()
                            {
                                NameVER = NameVer,
                                NameSettPath = PathName,
                                PathToPDF = PathtoPdf,
                                PathToDXF = PathtoDxf,
                                PathToTIF = PathtoTif,
                                PathToJT = PathtoJt,
                                PathToFolder = Pathtofolder,
                                PathToDOC = PathtoDoc,
                                OldVersionNameAndPath = oldversionNameAndPath,
                                PriorityColor = PriorityColor
                            });
                        } 
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F2) \n\nError Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }

            return results;
        }


        public List<IDsDates> GetOldIdItemsFromStructedLevel(string NameIDsOld, string NameSettPathOld, string PathToFolderOld)
        {
            var results = new List<IDsDates>();

            try
            {
                if (Directory.Exists(PathToFolderOld))
                {

                    var ItemsInDirector = Directory.EnumerateFiles(PathToFolderOld, "*", SearchOption.TopDirectoryOnly);

                    if (ItemsInDirector.Any())
                    {
                        var PathtoPdf = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "pdf").LastOrDefault();
                        var PathtoDxf = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "dxf").LastOrDefault();
                        var PathtoTif = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "tif").LastOrDefault();
                        var PathtoJt = ItemsInDirector.OrderBy(s => s).Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "jt").LastOrDefault();
                        var PathtoDoc = ItemsInDirector.OrderBy(s => s)
                            .Where(a => a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "doc" || a.Remove(0, a.LastIndexOf(".") + 1).ToLower() == "docx").LastOrDefault();
                        var NameVer = PathToFolderOld.Remove(0, PathToFolderOld.LastIndexOf(@"\") + 1).ToUpper();

                        results.Add(new IDsDates()
                        {
                            NameVER = NameVer,
                            NameSettPath = NameSettPathOld,
                            PathToPDF = PathtoPdf,
                            PathToDXF = PathtoDxf,
                            PathToTIF = PathtoTif,
                            PathToJT = PathtoJt,
                            PathToFolder = PathToFolderOld,
                            PathToDOC = PathtoDoc,
                            NameIDsOld = NameIDsOld,
                            PriorityColor = "DimGray"
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F3) \n\nError Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }

            return results;
        }

    }
}