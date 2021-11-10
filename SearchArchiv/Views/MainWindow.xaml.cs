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
        }

        private void Search_ID_Click(object sender, RoutedEventArgs e)
        { 
            // Start searching ID number.
            SearchingClass searchingClass = new SearchingClass();
          var ResultsDates = searchingClass.StartSearch(ID_Input.Text.Trim().ToString()); 
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

    }
}
