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
using System.Linq;
using System.Windows;
using SearchArchive.Controllers;
using SearchArchive.Models;
using MessageBox = System.Windows.MessageBox;

namespace SearchArchive.Views
{
    /// <summary>
    ///     Interaction logic for Settings_Window.xaml
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
            // Method to Get base64 image string, and set on Image Source.
            SearchArchiv_Logo.Source =
                ImagesBase64.MemorizingStreamBitmap(Convert.FromBase64String(ImagesBase64.SearchArchiveLogoBase64));
        }

        private void LoadSettingsContent()
        {
            // Put xml structure in to Settings Window content template.
            itemsControl.ItemsSource = PathsCollectionController.GetPathsCollectionFromDefinedPathways();
        }

        private void AddPath_button_Click(object sender, RoutedEventArgs e)
        {
            // Make Visible section to add neu path settings record.
            AddPath_StackPanel.Visibility = Visibility.Visible;
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            // Question => "To be sure to save the changes".
            if (MessageBox.Show(string.Format(
                        "Bitte beachten Sie, dass alle bestehenden Pfade in diesem Fenster überschrieben werden. \n " +
                        "!!! Please note that all existing paths will be overwritten again this from window. !!! \n\n " +
                        " Sind Sie sicher, dass Sie die Änderungen speichern möchte ? "),
                    "Durchsuchende Pfade Settings", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                SavePathSettings();
            else
                // Close window without save.
                DialogResult = false;
        }

        // Method to send inputs data to save in CreateNewXML, if everything ist right.
        private void SavePathSettings()
        {
            var NewListPaths = new List<PathsCollection>();

            foreach (PathsCollection item in itemsControl.Items)
                NewListPaths.Add(new PathsCollection
                {
                    Name = item.Name,
                    Path = item.Path,
                    Structured = item.Structured,
                    PriorityColor = item.PriorityColor
                });

            // if every 3 inputs are not empty.
            if (NamePath_TextBox_Add.Text != "" &&
                Path_TextBox_Add.Text != "" &&
                PathStructured_TextBox_Add.Text != "")
                NewListPaths.Add(new PathsCollection
                {
                    Name = NamePath_TextBox_Add.Text,
                    Path = Path_TextBox_Add.Text,
                    Structured = Convert.ToBoolean(PathStructured_TextBox_Add.Text),
                    PriorityColor = PathPrioColor_TextBox_Add.Text
                });
            if (NewListPaths.Any())
            {
                // Sent data to class Methods and close window.
                var config_Class = new PathsCollectionController();
                config_Class.CreateNewXML(NewListPaths);
                DialogResult = true;
            }
        }
    }
}