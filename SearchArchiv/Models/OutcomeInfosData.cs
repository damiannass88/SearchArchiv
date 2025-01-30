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

using System.Collections.Generic;

namespace SearchArchive.Models
{
    public struct OutcomeInfosData
    {
        public string NameVer { get; set; }
        public string NameSettPath { get; set; }
        public string PathToPDF { get; set; }
        public string PathToDXF { get; set; }
        public string PathToTIF { get; set; }
        public string PathToJT { get; set; }
        public string PathToFolder { get; set; }
        public string PathToDOC { get; set; }
        public List<OldVersionData> OldVersionInfo { get; set; }
        public string NameIDsOld { get; set; }
        public string PriorityColor { get; set; }
    }
}