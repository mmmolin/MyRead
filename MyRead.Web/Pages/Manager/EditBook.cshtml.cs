using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Core.Models;
using MyRead.Data;
using System.Threading.Tasks;

namespace MyRead.Web.Pages.Manager
{
    public class EditBookModel : PageModel
    {
        private readonly IData<Book> bookData;
        public EditBookModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        [BindProperty]
        public BookModel BookModel { get; set; }
        public async Task OnGetAsync(int BookID)
        {
            var bookEntity = await bookData.GetByIdAsync(BookID);
            var bookModel = new BookModel
            {
                BookID = bookEntity.BookID,
                Title = bookEntity.Title,
                Pages = bookEntity.Pages
            };
            BookModel = bookModel;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bookEntity = await bookData.GetByIdAsync(BookModel.BookID);
            bool bookIsUpdated = await TryUpdateModelAsync<Book>(bookEntity, nameof(BookModel), 
                x => x.Title, x => x.Pages);

            if(bookIsUpdated)
            {
                await bookData.CommitAsync();
            }

            return RedirectToPage("./ListBook");
        }

    }
}