using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRead.Core;
using MyRead.Core.Models;
using MyRead.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyRead.Web.Pages.Manager
{
    public class AddBookModel : PageModel
    {
        private readonly IData<Book> bookData;
        private readonly IData<Author> authorData;
        public AddBookModel(IData<Book> bookData, IData<Author> authorData)
        {
            this.bookData = bookData;
            this.authorData = authorData;
        }

        [BindProperty]
        public BookModel BookModel { get; set; }

        [Required]
        [BindProperty]
        public int AuthorId { get; set; }

        public List<SelectListItem> AuthorSelect { get; set; }

        public async Task OnGetAsync(int bookId)
        {
            BookModel = new BookModel();

            var authors = await authorData.GetAllAsync();
            AuthorSelect = authors.Select(x => new SelectListItem
            {
                Value = x.AuthorID.ToString(),
                Text = $"{x.LastName}, {x.FirstName}"
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if required fields are filled in.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // temp, use automapper
            var authorEntity = await authorData.GetByIdAsync(AuthorId);
            var bookEntity = new Book
            {
                Title = BookModel.Title,
                Pages = BookModel.Pages,
                CurrentPage = 0
            };

            bookEntity.Author = authorEntity;
            bookData.Add(bookEntity);
            await bookData.CommitAsync();

            return RedirectToPage("./ListBook");
        }
    }
}