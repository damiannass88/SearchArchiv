using SearchArchiv.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            LoadSettingsContent();
        }
        private void LoadImages()
        {
            // Methode to Get base64 image string, and set on Image Source.
            SearchArchiv_Logo.Source = ImagesClass.MemoringStreamBitmap(Convert.FromBase64String(ImagesClass.SearchArchivLogoBase64));
        }
        private void LoadSettingsContent()
        {
            // Put xml structur in to Settings Window content template.
            itemsControl.ItemsSource = Settings_AppConfig_Class.GetPathsFromXML();
        }

        private void AddPath_butt_Click(object sender, RoutedEventArgs e)
        {
            // Make Visible section to add neu path settings record.
            AddPath_StackPanel.Visibility = Visibility.Visible;
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            // Question? To be sure to save the changes.
            if (MessageBox.Show(string.Format("Bitte beachten Sie, dass alle bestehenden Pfade in diesem Fenster überschrieben werden. \n " +
                "!!! Please note that all existing paths will be overwritten again this from window. !!! \n\n " +
                " Sind Sie sicher, dass Sie die Änderungen speichern möchte ? ", System.Windows.Forms.MessageBoxIcon.Warning),
                   "Durchsuchende Pfade Settings", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // if true.
                SavePathSettings();
            }
            else
            {
                // Close window without save.
                this.DialogResult = false;
            }
        }

        private void SavePathSettings()
        {
            // Methode to send inputs dates in class Settings_AppConfig, if everythings ist right.
  
            List<PathsCollection> NewListPaths = new List<PathsCollection>();

            foreach (PathsCollection item in itemsControl.Items)
            {
                NewListPaths.Add(new PathsCollection()
                {
                    Name = item.Name,
                    Path = item.Path,
                    Structured = item.Structured,
                    PriorityColor = item.PriorityColor
                });
            }

            // if every 3 inputs felds are not empty.
            if(NamePath_TextBox_Add.Text != "" & Path_TextBox_Add.Text != "" & PathStructured_TextBox_Add.Text != "")
            {
                NewListPaths.Add(new PathsCollection()
                {
                    Name = NamePath_TextBox_Add.Text,
                    Path = Path_TextBox_Add.Text,
                    Structured = Convert.ToBoolean(PathStructured_TextBox_Add.Text),
                    PriorityColor = PathPrioColor_TextBox_Add.Text,
                });
            }
            if (NewListPaths.Any())
            {
                // Sent dates to class Methods and close window.
                Settings_AppConfig_Class config_Class = new Settings_AppConfig_Class();
                config_Class.CreateNewXML(NewListPaths);
                this.DialogResult = true;
            };
        }
    }
}
