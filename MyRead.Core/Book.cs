using System;
using System.Collections.Generic;
using System.Text;

namespace MyRead.Core
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }

        public Author Author { get; set; }
    }
}
