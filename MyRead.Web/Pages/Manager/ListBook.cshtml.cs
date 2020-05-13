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
    public class ListBookModel : PageModel
    {
        private readonly IData<Book> bookData;

        public ListBookModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        public List<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await bookData.GetAll()
                .Where(x => !x.IsArchived)
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostArchiveAsync(int bookId)
        {
            var bookEntity = await bookData.GetByIdAsync(bookId);
            if(bookEntity != null)
            {
                bookEntity.IsArchived = true;
                bookEntity.CurrentPage = bookEntity.Pages;
                await bookData.CommitAsync();
            }

            await this.OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSetCurrentPageAsync(int bookId, string currentPage)
        {
            // ModelState.IsValid?
            // Todo fix this method, it's working but got issues
            var bookEntity = await bookData.GetByIdAsync(bookId);
            if(bookEntity != null && int.Parse(currentPage) <= bookEntity.Pages)
            {
                bookEntity.CurrentPage = int.Parse(currentPage);
                await bookData.CommitAsync();
            }

            await this.OnGetAsync();
            return Page();
        }
    }
}