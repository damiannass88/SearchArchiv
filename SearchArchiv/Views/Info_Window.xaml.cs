using SearchArchiv.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SearchArchiv.Views
{
    /// <summary>
    /// Interaction logic for Info_Window.xaml
    /// </summary>
    public partial class Info_Window : Window
    {
        public Info_Window()
        {
            InitializeComponent();
            LoadImages();
        }
        private void LoadImages()
        {
            // Methode to Get base64 image string, and set on Image Source. 
            Adwers_Logo.Source = ImagesClass.MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.AdwersLogoBase64));
        }
        private void Info_Go_Adwers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Go to website adwers.com
            System.Diagnostics.Process.Start("https://www.adwers.com/de/");
        }
    }
}
