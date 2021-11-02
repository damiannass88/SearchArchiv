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
    /// Interaction logic for Settings_Window.xaml
    /// </summary>
    public partial class Settings_Window : Window
    {
        public Settings_Window()
        {
            InitializeComponent();
            LoadImages();
        }
        private void LoadImages()
        {
            // Methode to Get base64 image string, and set on Image Source.
            SearchArchiv_Logo.Source = ImagesClass.MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.SearchArchivLogoBase64));
        }
    }
}
