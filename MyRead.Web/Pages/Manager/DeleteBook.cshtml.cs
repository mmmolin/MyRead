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
        public void OnGet()
        {
            // get book show info
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