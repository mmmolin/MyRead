using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;
using System.Collections.Generic;
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
            this.authorData = authorData;
        }

        [BindProperty]
        public Book Book { get; set; }
        
        [BindProperty]
        public Author Author { get; set; }
        
        public void OnGet(int bookId)
        {
            Book = new Book();
            Author = new Author();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var newBook = new Book();
            var bookUpdated = await TryUpdateModelAsync<Book> // TryUpdateModelAsync to avoid overposting vulnerability
                (newBook, nameof(Book), b => b.Title, b => b.CurrentPage, b => b.Pages);
            
            var newAuthor = new Author();
            var authorUpdated = await TryUpdateModelAsync<Author>
                (newAuthor, nameof(Author), a => a.FirstName, a => a.LastName);

            if(bookUpdated && authorUpdated)
            {
                newBook.Author = newAuthor;
                bookData.Add(newBook);
                await bookData.CommitAsync();

                return RedirectToPage("./Index");
            }
            
            return Page();
        }
    }
}