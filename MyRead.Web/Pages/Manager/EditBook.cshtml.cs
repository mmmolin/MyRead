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
    public class EditBookModel : PageModel
    {
        private readonly IData<Book> bookData;
        private readonly IData<Author> authorData;

        public EditBookModel(IData<Book> bookData, IData<Author> authorData)
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

            AuthorId = bookEntity.Author.AuthorID;

            var authorEntities = await authorData.GetAllAsync();
            AuthorSelect = authorEntities.Select(x => new SelectListItem 
            { 
            Value = x.AuthorID.ToString(),
            Text = $"{x.FirstName} {x.LastName}"
            }).ToList();
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

            var authorEntity = await authorData.GetByIdAsync(AuthorId);

            if(bookIsUpdated)
            {
                bookEntity.Author = authorEntity;
                await bookData.CommitAsync();
            }

            return RedirectToPage("./ListBook");
        }

    }
}