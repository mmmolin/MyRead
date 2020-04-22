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
        private readonly IData<Book> bookData;

        public UpdateModel(IData<Book> bookData)
        {
            this.bookData = bookData;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet(int bookId)
        {
            Book = bookData.GetById(bookId); //Null check
        }

        public IActionResult OnPost()
        {
           if(!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}