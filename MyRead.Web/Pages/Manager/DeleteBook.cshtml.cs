using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core.Entities;
using MyRead.Data;
using Serilog;

namespace MyRead.Web.Pages.Manager
{
    public class DeleteBookModel : PageModel
    {
        private readonly IData<Book> bookData;

        [TempData]
        public string DeleteNotification { get; set; }

        public DeleteBookModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }
        public async Task<IActionResult> OnGet(int bookId)
        {
            var bookEntity = await bookData.GetByIdAsync(bookId);
            if (bookEntity == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int bookId)
        {
            try
            {
                var bookEntity = await bookData.GetByIdAsync(bookId);
                if (bookEntity != null)
                {
                    await DeleteBookAsync(bookEntity);
                    DeleteNotification = $"{bookEntity.Title} deleted from database";
                }
                else
                {
                    DeleteNotification = $"Couldn't delete {bookEntity.Title} from database";
                }
            }
            catch(Exception ex)
            {
                Log.Logger.Error("Exception in DeleteBook OnPostAsync: " + ex);
                DeleteNotification = $"Book couldn't be deleted, check logs.";
            }

            return RedirectToPage("./ListBook");
        }

        public async Task DeleteBookAsync(Book bookEntity)
        {
            bookData.Remove(bookEntity);
            await bookData.CommitAsync();
        }
    }
}