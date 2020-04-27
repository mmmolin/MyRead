﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRead.Core
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        [Required]
        public int Pages { get; set; }

        public Author Author { get; set; }
    }
}
