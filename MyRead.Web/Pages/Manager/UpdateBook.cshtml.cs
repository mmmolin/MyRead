using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager.Books
{
    public class UpdateModel : PageModel
    {
        private IData<Book> bookData { get; set; }
        private IData<Author> authorData { get; set; }
        public UpdateModel(IData<Book> bookData, IData<Author> authorData)
        {
            this.bookData = bookData;
            this.authorData = authorData;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGetAsync(int BookID)
        {
            Book = await bookData.GetByIdAsync(BookID);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            return Page();
        }
    }
}