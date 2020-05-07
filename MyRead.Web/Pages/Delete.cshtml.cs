using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core.Entities;
using MyRead.Data;

namespace MyRead.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IData<Book> bookData;
        public DeleteModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        public Book Book { get; set; }

        public void OnGet(int bookId)
        {
            Book = new Book();
        }

        public IActionResult OnPost(int bookId)
        {
            //bookData.DeleteBook(bookId);
            //bookData.Commit();

            return RedirectToPage("./List");
        }
    }
}