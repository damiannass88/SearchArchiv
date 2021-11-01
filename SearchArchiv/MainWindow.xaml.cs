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

namespace SearchArchiv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            StartImages();
        }
        private void StartImages()
        {
            // Methode to Get base64 image string, and set on Image Source.
            Logo_Firm_image.Source = MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.SaxasImageBase64));
            Settings_BtnIcon.Source = MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.SettingsImageBase64));
        }
        public static BitmapImage MemoringStreamBitmap(byte[] base64String)
        {
            // Return BitmapImage, after Memoring StreamSourcer
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(base64String))
            { 
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit(); 
            };
            return bitmapImage;
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
            // go adwers.com
        }
    }
}
