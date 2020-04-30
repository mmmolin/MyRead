using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager
{
    public class ListBookModel : PageModel
    {
        private readonly IData<Book> bookData;
        
        public ListBookModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        public List<Book> Books { get; set; }
        
        public  async Task OnGetAsync()
        {
            Books = await bookData.GetAllAsync();
        }
    }
}