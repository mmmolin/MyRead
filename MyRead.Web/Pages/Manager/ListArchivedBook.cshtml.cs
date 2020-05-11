using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager
{
    public class ListArchivedBookModel : PageModel
    {
        private IData<Book> bookData;
        
        public ListArchivedBookModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }
        
        public List<Book> ArchivedBooks { get; set; } 

        public async Task OnGet()
        {
            ArchivedBooks = await bookData.GetAll()
                .Where(x => x.IsArchived)
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteArchivedBookAsync(int bookId) // return Task or Task<IActionResult>
        {
            var archivedBook = new Book { BookID = bookId };
            bookData.Remove(archivedBook);
            await bookData.CommitAsync();

            await this.OnGet();
            return Page();
        }
    }
}