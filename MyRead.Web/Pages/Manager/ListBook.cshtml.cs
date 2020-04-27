using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages.Books.Manager
{
    public class IndexModel : PageModel
    {
        private readonly IData<Book> bookData;
        
        public IndexModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        public IEnumerable<Book> Books { get; set; }
        
        public async void OnGetAsync()
        {
            Books = await bookData.GetAllAsync();
        }
    }
}