using System;
using System.Collections.Generic;
using System.Text;

namespace MyRead.Core
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
