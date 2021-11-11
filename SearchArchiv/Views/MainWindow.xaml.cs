using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using SearchArchiv.Classes;

namespace SearchArchiv.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            LoadImages();
        }

        private bool IsWindowFocused = true; 

        private void LoadImages()
        {
            // Methode to Get base64 image string, and set on Image Source.
            Logo_Firm_image.Source = ImagesClass.MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.SaxasImageBase64));
            Settings_BtnIcon.Source = ImagesClass.MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.SettingsImageBase64));
        } 

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // DragMove Window Methode
            MainWindow_Name.Cursor = System.Windows.Input.Cursors.Hand;

            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

            }

            if (e.ButtonState == MouseButtonState.Released)
            {
                MainWindow_Name.Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void Adwers_footer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Go to website adwers.com
            System.Diagnostics.Process.Start("https://www.adwers.com/de/");
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            // Close Aplication.
            System.Windows.Application.Current.Shutdown();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Minimize Aplication.
            this.WindowState = WindowState.Minimized;
        }

        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            // Clear Input textbox.
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
            var SKey = ID_Input.Text.Trim().ToString();

            if (SKey == "")
            {
                MainItemsControl.ItemsSource = null;
                ItemsControl_OldVersionIDs.ItemsSource = null;
                return;
            }

            SearchingClass searchingClass = new SearchingClass();
            var ResultsDates = searchingClass.StartSearch(SKey);
            MainItemsControl.ItemsSource = ResultsDates;
        }
 
        private void Settings_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Open Settings Window.
            Settings_Window settings_Window = new Settings_Window();

            if (settings_Window.ShowDialog() == true)
            {
                return;
            }
        }

        private void Info_btn_Click(object sender, RoutedEventArgs e)
        {
            // Create Object Info_Window and Open that window with about infos.
            Info_Window info_Window = new Info_Window();

            if (info_Window.ShowDialog() == true)
            {
                return;
            }
        }

        private void GetOldIDFiles(string NameIDsOld, string NameSettPathOld, string PathToFolderOld)
        {
            SearchingClass searchingClass = new SearchingClass();

            // Get files for concrete old ID version Item.
            var ResultsFiles = searchingClass.GetOldIdItemsFromStructedLevel(NameIDsOld, NameSettPathOld, PathToFolderOld);

            if (ItemsControl_OldVersionIDs.ItemsSource != null)
                ItemsControl_OldVersionIDs.ItemsSource = null;

            ItemsControl_OldVersionIDs.ItemsSource = ResultsFiles;
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
        
        private void Dropdown_OldVerions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SenderComboBoxItem = sender as System.Windows.Controls.ComboBox;
                if (SenderComboBoxItem.Items.Count == 0)
                    return;
                var NameIDsOld = ((SearchArchiv.Classes.SearchingClass.OldVerDates)SenderComboBoxItem.SelectedValue).NameIDsOld;
                var NameSettPathOld = ((SearchArchiv.Classes.SearchingClass.OldVerDates)SenderComboBoxItem.SelectedValue).NameSettPathOld;
                var PathToFolderOld = ((SearchArchiv.Classes.SearchingClass.OldVerDates)SenderComboBoxItem.SelectedValue).PathToFolderOld;

                GetOldIDFiles(NameIDsOld, NameSettPathOld, PathToFolderOld);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n" +
                    "  -Bitte verwenden Sie eine andere ID Numer. \n\nSearchArchiv Fehler-Hinweis: (F4) \n\nError Message: \n"
                    + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_Loupe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IconBtn_3d_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IconBtn_SaveDXF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_DXF_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToDXF;
                if (SenderBtn_DXF_Content == null)
                    return;
                string SaveName = SenderBtn_DXF_Content.Remove(0, SenderBtn_DXF_Content.LastIndexOf("\\") + 1);

                string SavePath_PDF_Name = string.Empty;

                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Title = "Save DXF";
                saveFileDialog1.DefaultExt = "dxf";
                saveFileDialog1.FileName = SaveName;
                saveFileDialog1.Filter = "DXF files (*.dxf)|*.dxf|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string newDirectory = saveFileDialog1.FileName;
                    System.IO.File.Copy(SenderBtn_DXF_Content, newDirectory, true);
                };
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen." +
                    " \n  -Bitte verwenden Sie eine andere Sammlung. \n\nSearchArchiv Fehler-Hinweis: (F5) " +
                    "\n\nError Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_SavePDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_PDF_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToPDF;
                if (SenderBtn_PDF_Content == null)
                    return;
                string SaveName = SenderBtn_PDF_Content.Remove(0, SenderBtn_PDF_Content.LastIndexOf("\\") + 1);

                string SavePath_PDF_Name = string.Empty;

                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Title = "Save PDF";
                saveFileDialog1.DefaultExt = "pdf";
                saveFileDialog1.FileName = SaveName;
                saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string newDirectory = saveFileDialog1.FileName;
                    System.IO.File.Copy(SenderBtn_PDF_Content, newDirectory, true);
                };
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n " +
                    " -Bitte verwenden Sie eine andere Sammlung. \n\nSearchArchiv Fehler-Hinweis: (F6)" +
                    " \n\nError Message: \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_OpenDOC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_DOC_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToDOC;
                if (SenderBtn_DOC_Content == null)
                    return;

                System.Diagnostics.Process.Start(SenderBtn_DOC_Content);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie OFFICE und die Dateiendung, der DOC/DOCX-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F7) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_PathToFolder_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToFolder;
                if (SenderBtn_PathToFolder_Content == null)
                    return;

                System.Diagnostics.Process.Start(SenderBtn_PathToFolder_Content);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung." +
                    " \n\nSearchArchiv Fehler-Hinweis: (F10) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void IconBtn_PrintPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_PDF_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToPDF;
                if (SenderBtn_PDF_Content == null)
                    return;

                SendtoPrint((string)SenderBtn_PDF_Content);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung." +
                    " \n\nSearchArchiv Fehler-Hinweis: (F11) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            } 
        }
        private static bool ProcessIsRunning()
        {
            return (System.Diagnostics.Process.GetProcessesByName("AcroRd32").Length == 0);
        }
        private static void SendtoPrint(string Printfile)
        {
            try
            {
                if (ProcessIsRunning())
                {
                    System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                    myProcess.StartInfo.FileName = "acroRd32.exe";
                    myProcess.StartInfo.Arguments = "/h";
                    myProcess.Start();
                    int milliseconds = 1000;
                    System.Threading.Thread.Sleep(milliseconds);
                }

                System.Diagnostics.Process p = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        Verb = "print",
                        FileName = Printfile
                    }
                };
                p.Start();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung. \n\n" +
                    "Bitte überprüfen Sie Adobe und die Dateiendung, der PDF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F12) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        private void IconBtn_OpenDXF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_DXF_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToDXF;
                if (SenderBtn_DXF_Content == null)
                    return;

                System.Diagnostics.Process.Start(SenderBtn_DXF_Content);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie OPENING PROGRAMM und die Dateiendung, der DXF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F8) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void OpenBtn_OpenPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SenderBtn = sender as System.Windows.Controls.Button;
                var SenderBtn_PDF_Content = ((SearchArchiv.Classes.SearchingClass.IDsDates)((System.Windows.FrameworkElement)SenderBtn.Content).DataContext).PathToPDF;
                if (SenderBtn_PDF_Content == null)
                    return;

                System.Diagnostics.Process.Start(SenderBtn_PDF_Content);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Bei der Aufgabe ist was schief gelaufen. \n  -Bitte verwenden Sie eine andere Sammlung.\n\n" +
                    "Bitte überprüfen Sie Adobe und die Dateiendung, der PDF-Datei. \n\n" +
                    " \n\nSearchArchiv Fehler-Hinweis: (F9) \n\nError Message:" +
                    " \n" + ex.Message, "INFORMATION", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void ID_Input_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                StartSearching();
                return;
            }
        }
    }
}
