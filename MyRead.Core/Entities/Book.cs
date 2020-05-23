using System;

namespace MyRead.Core.Entities
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public bool IsArchived { get; set; }
        public string CoverFilePath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Author Author { get; set; }
    }
}
