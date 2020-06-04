using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using MyRead.Data;
using Serilog;

namespace MyRead.Web.Pages.Manager
{
    public class ListBookModel : PageModel
    {
        private readonly IData<Book> bookData;

        public ListBookModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        [TempData]
        public string AddNotification { get; set; }

        [TempData]
        public string DeleteNotification { get; set; }

        [TempData]
        public string EditNotification { get; set; }

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
            try
            {
                var bookEntity = await bookData.GetByIdAsync(bookId);
                if (bookEntity != null)
                {
                    bookEntity.IsArchived = true;
                    bookEntity.CurrentPage = bookEntity.Pages;
                    bookEntity.EndDate = DateTime.Today;
                    await SaveChangesToDatabaseAsync();
                }
            }
            catch
            {
                // TODO: Log exception
                // TODO: Notification?
            }

            await this.OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSetCurrentPageAsync(int bookId, string currentPage)
        {
            try
            {
                var bookEntity = await bookData.GetByIdAsync(bookId);
                if (bookEntity != null && int.Parse(currentPage) <= bookEntity.Pages)
                {
                    bookEntity.CurrentPage = int.Parse(currentPage);
                    await SaveChangesToDatabaseAsync();
                }
            }
            catch
            {
                // TODO: Log exception
                // TODO: Notification?
            }

            await this.OnGetAsync();
            return Page();
        }

        private async Task SaveChangesToDatabaseAsync()
        {
            await bookData.CommitAsync();
        }
    }
}