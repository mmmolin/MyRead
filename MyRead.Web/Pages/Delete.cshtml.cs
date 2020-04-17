using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IBookData bookData;
        public DeleteModel(IBookData bookData)
        {
            this.bookData = bookData;
        }

        public Book Book { get; set; }

        public void OnGet(int bookId)
        {
            Book = bookData.GetBookById(bookId);
        }

        public IActionResult OnPost(int bookId)
        {
            bookData.DeleteBook(bookId);
            bookData.Commit();

            return RedirectToPage("./List");
        }
    }
}