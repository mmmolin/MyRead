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
            var bookEntity = new Book()
            {
                BookID = bookId
            };

            bookData.Remove(bookEntity);
            await bookData.CommitAsync();

            return RedirectToPage("./ListBook");
        }
    }
}