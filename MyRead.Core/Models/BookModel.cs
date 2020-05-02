using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRead.Core.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
