using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRead.Core.Models
{
    public class AuthorModel
    {
        public int AuthorID { get; set; }
        [StringLength(25, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(25, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }
    }
}
