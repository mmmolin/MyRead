﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using MyRead.Core.Models;
using MyRead.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyRead.Web.Pages.Manager
{
    public class EditBookModel : PageModel
    {
        private readonly IData<Book> bookData;
        private readonly IData<Author> authorData;
        private readonly IWebHostEnvironment environment;

        public EditBookModel(IData<Book> bookData, IData<Author> authorData, IWebHostEnvironment environment)
        {
            this.bookData = bookData;
            this.authorData = authorData;
            this.environment = environment;
        }

        [TempData]
        public string EditNotification { get; set; }

        [BindProperty]
        public IFormFile EditUploadedPicture { get; set; }

        [BindProperty]
        public BookModel BookModel { get; set; }

        [Required]
        [BindProperty]
        public int AuthorId { get; set; }

        public string CoverFilePath { get; set; } 

        public List<SelectListItem> AuthorSelect { get; set; }

        public async Task<IActionResult> OnGetAsync(int BookID)
        {
            var bookEntity = await bookData.GetByIdAsync(BookID);
            if (bookEntity == null)
            {
                return RedirectToPage("./NotFound");
            }

            var bookModel = new BookModel
            {
                BookID = bookEntity.BookID,
                Title = bookEntity.Title,
                CurrentPage = bookEntity.CurrentPage,
                Pages = bookEntity.Pages
            };
            BookModel = bookModel;
            CoverFilePath = $"/{bookEntity.CoverFilePath}";

            AuthorId = bookEntity.Author.AuthorID;

            await PopulateAuthorSelectAsync();

            return Page();
        }

        private async Task PopulateAuthorSelectAsync() // DRY, this is in both Add and Edit
        {
            var authorEntities = await authorData.GetAll()
                .OrderBy(x => x.LastName)
                .ToListAsync(); // Read more about this
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

            // Set new picture, extract this to method, tidy up.
            if (EditUploadedPicture != null)
            {

                // Upload new Picture
                var newFilePath = Path.Combine("images/covers", EditUploadedPicture.FileName);
                var file = Path.Combine(environment.WebRootPath, newFilePath);
                using (var filestream = new FileStream(file, FileMode.Create))
                {
                    await EditUploadedPicture.CopyToAsync(filestream);
                }

                // Delete old picture
                var oldFilePath = Path.Combine(environment.WebRootPath, bookEntity.CoverFilePath);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                // Edit picture path in db
                bookEntity.CoverFilePath = newFilePath;
            }

            bool bookIsUpdated = await TryUpdateModelAsync<Book>(bookEntity, nameof(BookModel), 
                x => x.Title, x => x.CurrentPage, x => x.Pages);

            var authorEntity = await authorData.GetByIdAsync(AuthorId);

            if(bookIsUpdated)
            {
                bookEntity.Author = authorEntity;
                await bookData.CommitAsync();
            }

            EditNotification = $"Changes were made to {bookEntity.Title}";

            return RedirectToPage("./ListBook");
        }

    }
}