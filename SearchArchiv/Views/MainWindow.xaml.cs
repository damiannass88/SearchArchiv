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
            searchingClass.StartSearch(ID_Input.Text.Trim().ToString());
           
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
    }
}
