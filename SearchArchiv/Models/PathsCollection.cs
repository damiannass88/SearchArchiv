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

using System.ComponentModel;

namespace SearchArchive.Models
{
    public class PathsCollection : INotifyPropertyChanged
    {
        private string name;

        private string path;

        private string priorityColor;

        private bool structured;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

        public string Path
        {
            get => path;
            set
            {
                if (path != value)
                {
                    path = value;
                    NotifyPropertyChanged(nameof(Path));
                }
            }
        }

        public bool Structured
        {
            get => structured;
            set
            {
                if (structured != value)
                {
                    structured = value;
                    NotifyPropertyChanged(nameof(Structured));
                }
            }
        }

        public string PriorityColor
        {
            get => priorityColor;
            set
            {
                if (priorityColor != value)
                {
                    priorityColor = value;
                    NotifyPropertyChanged(nameof(PriorityColor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}