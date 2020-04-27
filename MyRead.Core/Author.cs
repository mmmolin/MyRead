﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRead.Core
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
