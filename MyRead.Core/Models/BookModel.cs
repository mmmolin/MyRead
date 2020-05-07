using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRead.Core.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        
        [StringLength(70, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [RegularExpression(@"\d+")]
        // Custom range attribute here
        public int CurrentPage { get; set; }

        [RegularExpression(@"\d+$")]
        [Range(1, 3000)]
        [Required]
        public int Pages { get; set; }

        public bool IsArchived { get; set; }
    }
}
