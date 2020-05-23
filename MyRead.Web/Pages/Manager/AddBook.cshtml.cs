using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using MyRead.Core.Models;
using MyRead.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyRead.Web.Pages.Manager
{
    public class AddBookModel : PageModel
    {
        private readonly IData<Book> bookData;
        private readonly IData<Author> authorData;

        private readonly IWebHostEnvironment environment;

        public AddBookModel(IData<Book> bookData, IData<Author> authorData, IWebHostEnvironment environment)
        {
            this.bookData = bookData;
            this.authorData = authorData;

            this.environment = environment;
        }

        [TempData]
        public string AddNotification { get; set; }

        [Required]
        [BindProperty]
        public IFormFile UploadedPicture { get; set; }

        [BindProperty]
        public BookModel BookModel { get; set; }

        [Required]
        [BindProperty]
        public int AuthorId { get; set; }

        public List<SelectListItem> AuthorSelect { get; set; }

        public async Task OnGetAsync()
        {
            BookModel = new BookModel();
            await PopulateAuthorSelectAsync();
        }

        private async Task PopulateAuthorSelectAsync()
        {
            var authorEntities = await authorData.GetAll()
                .OrderBy(x => x.LastName)
                .ToListAsync(); // Read more about this!
            AuthorSelect = authorEntities.Select(x => new SelectListItem
            {
                Value = x.AuthorID.ToString(),
                Text = $"{x.LastName}, {x.FirstName}"
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateAuthorSelectAsync();
                return Page();
            }

            // Create Method
            var filePath = Path.Combine("images/covers", UploadedPicture.FileName); 
            var file = Path.Combine(environment.WebRootPath, filePath);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadedPicture.CopyToAsync(fileStream);
            }

            var authorEntity = await authorData.GetByIdAsync(AuthorId);
            var bookEntity = new Book
            {
                Title = BookModel.Title,
                Pages = BookModel.Pages,
                CurrentPage = BookModel.CurrentPage,
                StartDate = DateTime.Today,
                CoverFilePath = filePath
            };

            bookEntity.Author = authorEntity;
            bookData.Add(bookEntity);
            await bookData.CommitAsync();

            AddNotification = $"{bookEntity.Title} added to database";

            return RedirectToPage("./ListBook");
        }
    }
}