using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRead.Core;
using MyRead.Core.Models;
using MyRead.Data;
using MyRead.Web.Pages.Manager.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRead.Web.Pages.Books.Manager
{
    public class CreateModel : PageModel
    {
        private readonly IData<Book> bookData;
        private readonly IData<Author> authorData;
        public CreateModel(IData<Book> bookData, IData<Author> authorData)
        {
            this.bookData = bookData;
            //this.authorData = authorData;
        }

        [BindProperty]
        public BookModel BookModel { get; set; }

        //public List<SelectListItem> AuthorSelect { get; set; }

        public void OnGet(int bookId)
        {
            BookModel = new BookModel();

            //var authors = await authorData.GetAllAsync();
            //AuthorSelect = authors.Select(x => new SelectListItem 
            //{ 
            //    Value = x.AuthorID.ToString(),
            //    Text = $"{x.LastName}, {x.FirstName}"
            //}).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if required fields are filled in.
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Shitty way to map, use automapper.
            var bookEntity = new Book
            {
                Title = BookModel.Title,
                Pages = BookModel.Pages,
                CurrentPage = 0
            };

            return RedirectToPage("./ListAuthor", bookEntity);
        }
    }
}