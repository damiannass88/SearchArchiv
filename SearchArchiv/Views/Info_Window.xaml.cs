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
using System.Windows;
using System.Windows.Input;
using SearchArchive.Models;

namespace SearchArchive.Views
{
    /// <summary>
    ///     Interaction logic for Info_Window.xaml
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
            // Method to Get base64 image string, and set on Image Source. 
            Adwers_Logo.Source =
                ImagesBase64.MemorizingStreamBitmap(Convert.FromBase64String(ImagesBase64.AdwersLogoBase64));
        }

        private void Info_Go_Adwers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Go to website adwers.com
            Process.Start("https://www.adwers.com/de/");
        }
    }
}