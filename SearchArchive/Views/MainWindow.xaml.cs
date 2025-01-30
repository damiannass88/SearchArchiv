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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using SearchArchive.Controllers;
using SearchArchive.Models;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using ComboBox = System.Windows.Controls.ComboBox;
using Cursors = System.Windows.Input.Cursors;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SearchArchive.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsWindowFocused = true;

        public MainWindow()
        {
            InitializeComponent();
            LoadImages();
        }

        private void LoadImages()
        {
            // Method to Get base64 image string, and set on Image Source.
            Logo_Firm_image.Source =
                ImagesBase64.MemorizingStreamBitmap(Convert.FromBase64String(ImagesBase64.AdwersStructureLogoImageBase64));
            Settings_BtnIcon.Source =
                ImagesBase64.MemorizingStreamBitmap(Convert.FromBase64String(ImagesBase64.SettingsImageBase64));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // DragMove Window Method
            MainWindow_Name.Cursor = Cursors.Hand;

            if (e.ChangedButton == MouseButton.Left) DragMove();

            if (e.ButtonState == MouseButtonState.Released) MainWindow_Name.Cursor = Cursors.Arrow;
        }

        // Go to website adwers.com lang=de
        private void Adwers_footer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.adwers.com/damiannass/");
        }

        // Close Application.
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Minimize Application.
        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Clear Input textbox.
        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            ID_Input.Text = "";
        }

        private void Search_ID_Click(object sender, RoutedEventArgs e)
        {
            // Start Method StartSearching().
            StartSearching();
        }

        private void StartSearching()
        {
            // Start searching ID number.
            var searchIdName = ID_Input.Text.Trim();

            //Clear Views if nothing to search.
            if (searchIdName == "")
            {
                MainItemsControl.ItemsSource = null;
                ItemsControl_OldVersionIDs.ItemsSource = null;
                return;
            }

            //Clear View OldVersionIDs, for new searching.
            if (ItemsControl_OldVersionIDs.ItemsSource != null)
                ItemsControl_OldVersionIDs.ItemsSource = null;

            var searchingClass = new SearchController();
            var resultsDates = searchingClass.StartSearch(searchIdName);

            MainItemsControl.ItemsSource = resultsDates;
        }

        private void Settings_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Open Settings Window.
            var settingsWindow = new Settings_Window();

            if (settingsWindow.ShowDialog() == true) return;
        }

        private void Info_btn_Click(object sender, RoutedEventArgs e)
        {
            // Create Object Info_Window and Open that window with about infos.
            var infoWindow = new Info_Window();

            if (infoWindow.ShowDialog() == true) return;
        }

        private void GetOldIdFiles(string nameIDsOld, string nameSettPathOld, string pathToFolderOld)
        {
            var searchingClass = new SearchController();

            // Get files for concrete old ID version Item.
            var resultsFiles =
                searchingClass.GetOldIdItemsFromStructuredLevel(nameIDsOld, nameSettPathOld, pathToFolderOld);

            if (ItemsControl_OldVersionIDs.ItemsSource != null)
                ItemsControl_OldVersionIDs.ItemsSource = null;

            ItemsControl_OldVersionIDs.ItemsSource = resultsFiles;
        }

        private void MainWindow_Name_Activated(object sender, EventArgs e)
        {
            if (IsWindowFocused)
            {
                ID_Input.SelectionStart = 0;
                ID_Input.SelectionLength = ID_Input.Text.Length;
                ID_Input.Focus();
                IsWindowFocused = false;
            }
        }

        private void MainWindow_Name_Deactivated(object sender, EventArgs e)
        {
            IsWindowFocused = true;
        }

        private void Dropdown_OldVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var senderComboBoxItem = sender as ComboBox;
                if (senderComboBoxItem.Items.Count == 0)
                    return;
                var nameIDsOld = ((OldVersionData)senderComboBoxItem.SelectedValue).NameIDsOld;
                var nameSettPathOld = ((OldVersionData)senderComboBoxItem.SelectedValue).NameSettPathOld;
                var pathToFolderOld = ((OldVersionData)senderComboBoxItem.SelectedValue).PathToFolderOld;

                GetOldIdFiles(nameIDsOld, nameSettPathOld, pathToFolderOld);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n" +
                                "  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F4) \n\nError Message: \n"
                                + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_3d_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnJtContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToJT;
                if (senderBtnJtContent == null)
                    return;

                Process.Start(senderBtnJtContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie Adobe und die Dateiendung, der JT-Datei. \n\n" +
                    "Jupiter Tessellation(JT) ist ein leichtes 3D-Visualisierungsdateiformat für PLM. \n " +
                    "Es können geöffnet werden in JT2Go Desktop App." +
                    " \n\nSearchArchiv Fehler-Hinweis: (F9) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_SaveDXF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnDxfContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToDXF;
                if (senderBtnDxfContent == null)
                    return;
                var saveName = senderBtnDxfContent.Remove(0, senderBtnDxfContent.LastIndexOf("\\") + 1);

                var savePathPdfName = string.Empty;

                var saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save DXF";
                saveFileDialog1.DefaultExt = "dxf";
                saveFileDialog1.FileName = saveName;
                saveFileDialog1.Filter = "DXF files (*.dxf)|*.dxf|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var newDirectory = saveFileDialog1.FileName;
                    File.Copy(senderBtnDxfContent, newDirectory, true);
                }

                ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bei der Aufgabe ist was schief gelaufen." +
                                " \n  -Bitte verwenden Sie eine andere Sammlung. \n\nSearchArchiv Fehler-Hinweis: (F5) " +
                                "\n\nError Message: \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_SavePDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnPdfContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToPDF;
                if (senderBtnPdfContent == null)
                    return;
                var saveName = senderBtnPdfContent.Remove(0, senderBtnPdfContent.LastIndexOf("\\") + 1);

                var savePathPdfName = string.Empty;
                var saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save PDF";
                saveFileDialog1.DefaultExt = "pdf";
                saveFileDialog1.FileName = saveName;
                saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var newDirectory = saveFileDialog1.FileName;
                    File.Copy(senderBtnPdfContent, newDirectory, true);
                }

                ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n " +
                                " -Bitte verwenden Sie eine andere Sammlung. \n\nSearchArchiv Fehler-Hinweis: (F6)" +
                                " \n\nError Message: \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_OpenDOC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnDocContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToDOC;
                if (senderBtnDocContent == null)
                    return;

                Process.Start(senderBtnDocContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie OFFICE und die Dateiendung, der DOC/DOCX-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F7) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnPathToFolderContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToFolder;
                if (senderBtnPathToFolderContent == null)
                    return;

                Process.Start(senderBtnPathToFolderContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung." +
                    " \n\nSearchArchiv Fehler-Hinweis: (F10) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_PrintPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnPdfContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToPDF;
                if (senderBtnPdfContent == null)
                    return;

                SendtoPrint(senderBtnPdfContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung." +
                    " \n\nSearchArchiv Fehler-Hinweis: (F11) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool ProcessIsRunning_AcroRd32()
        {
            return Process.GetProcessesByName("AcroRd32").Length == 0;
        }

        private static bool ProcessIsRunning_Acrobat()
        {
            return Process.GetProcessesByName("Acrobat").Length == 0;
        }

        private static void SendtoPrint(string printfile)
        {
            try
            {
                // Check if PDF Viewer run already. If not, first start then.
                if (ProcessIsRunning_AcroRd32() & ProcessIsRunning_Acrobat())
                {
                    var myProcess = new Process();
                    myProcess.StartInfo.FileName = "acroRd32.exe";
                    myProcess.StartInfo.Arguments = "/h";

                    //Adobe PDF Reader has 2 name for the Process "acroRd32.exe" and "Acrobat.exe", so for versions names is used block Try/catch/finally
                    try
                    {
                        // Try start process name acroRd32.exe
                        myProcess.Start();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        myProcess.StartInfo.FileName = "Acrobat.exe";
                        // Try start process name Acrobat.exe, if Exception, than main method catch Exception with Message.
                        myProcess.Start();
                    }

                    var milliseconds = 1000;
                    Thread.Sleep(milliseconds);
                }


                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = true,
                        Verb = "print",
                        FileName = printfile
                    }
                };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung. \n\n" +
                    "Bitte überprüfen Sie Adobe und die Dateiendung, der PDF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F12) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_OpenDXF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnDxfContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToDXF;
                if (senderBtnDxfContent == null)
                    return;

                Process.Start(senderBtnDxfContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie OPENING PROGRAMM und die Dateiendung, der DXF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F8) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OpenBtn_OpenPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnPdfContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToPDF;
                if (senderBtnPdfContent == null)
                    return;

                Process.Start(senderBtnPdfContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie Adobe und die Dateiendung, der PDF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F9) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ID_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StartSearching();
        }

        private void IconBtn_Loupe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                var senderBtnTifContent =
                    ((OutcomeInfosData)((FrameworkElement)senderBtn.Content).DataContext).PathToTIF;
                if (senderBtnTifContent == null)
                    return;

                Process.Start(senderBtnTifContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie Foto-Software und die Dateiendung, der TIF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F7) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}