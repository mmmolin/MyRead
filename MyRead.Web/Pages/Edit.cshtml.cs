using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly IBookData bookData;
        public EditModel(IBookData bookData)
        {
            this.bookData = bookData;
        }

        [BindProperty]
        public Book Book { get; set; }
        public void OnGet(int bookId)
        {
            Book = bookData.GetBookById(bookId);
        }

        public IActionResult OnPost()
        {
            bookData.AddBook(Book);
            return RedirectToPage("./List");
        }
    }
}