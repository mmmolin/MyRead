using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;
using System.Collections.Generic;

namespace MyRead.Web.Pages.Books.Manager
{
    public class CreateModel : PageModel
    {
        private readonly ICrudData<Book> bookData;
        private readonly ICrudData<Author> authorData;
        public CreateModel(ICrudData<Book> bookData, ICrudData<Author> authorData)
        {
            this.bookData = bookData;
            this.authorData = authorData;
        }

        [BindProperty]
        public Book Book { get; set; }
        
        [BindProperty]
        public Author Author { get; set; }
        
        public void OnGet(int bookId)
        {
            Book = new Book();
            Author = new Author(); //<---- här är jag!

            var book = bookData.GetAll();
            var author = authorData.GetAll();
        }

        public IActionResult OnPost()
        {
            //bookData.AddBook(Book);
            return RedirectToPage("./List");
        }
    }
}