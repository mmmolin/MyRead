using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRead.Core.Models
{
    public class BookModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
