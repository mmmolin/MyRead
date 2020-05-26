using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core.Entities;
using MyRead.Data;

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
            if(bookEntity == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int bookId)
        {
            var bookEntity = await bookData.GetByIdAsync(bookId);

            DeleteNotification = $"{bookEntity.Title} deleted from database";

            bookData.Remove(bookEntity);
            await bookData.CommitAsync();

            return RedirectToPage("./ListBook");
        }
    }
}