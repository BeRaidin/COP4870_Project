﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class FileItem : ContentItem
    {
        public string FilePath { get; set; }

        public FileItem()
        {
            FilePath = string.Empty;
        }
    }
}
