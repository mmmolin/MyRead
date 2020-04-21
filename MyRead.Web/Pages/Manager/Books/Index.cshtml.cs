using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages.Books.Manager
{
    public class IndexModel : PageModel
    {
        private readonly ICrudData<Book> bookData;
        
        public IndexModel(ICrudData<Book> bookData)
        {
            this.bookData = bookData;
        }

        public IEnumerable<Book> Books { get; set; }
        
        public void OnGet()
        {
            Books = bookData.GetAll();
        }
    }
}