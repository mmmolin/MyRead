using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages.Books
{
    public class ListModel : PageModel
    {
        private readonly IBookData bookData;
        
        public ListModel(IBookData bookData)
        {
            this.bookData = bookData;
        }

        public IEnumerable<Book> Books { get; set; }
        
        public void OnGet()
        {
            Books = bookData.GetActiveBooks();
        }
    }
}