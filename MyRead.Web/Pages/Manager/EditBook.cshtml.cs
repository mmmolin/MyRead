using Microsoft.AspNetCore.Hosting;
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
        public IFormFile UploadedImage { get; set; }

        [BindProperty]
        public BookModel BookModel { get; set; }

        [Required]
        [BindProperty]
        public int AuthorId { get; set; }

        public string ImageFilePath { get; set; }

        public List<SelectListItem> AuthorSelect { get; set; }

        public async Task<IActionResult> OnGetAsync(int BookID)
        {
            var bookEntity = await bookData.GetByIdAsync(BookID);
            if (bookEntity == null)
            {
                return RedirectToPage("./NotFound");
            }

            BookModel = MapEntityToModel(bookEntity);
            ImageFilePath = $"/{bookEntity.CoverFilePath}";
            AuthorId = bookEntity.Author.AuthorID;

            await PopulateAuthorSelectAsync();

            return Page();
        }

        private BookModel MapEntityToModel(Book bookEntity)
        {
            var bookModel = new BookModel
            {
                BookID = bookEntity.BookID,
                Title = bookEntity.Title,
                CurrentPage = bookEntity.CurrentPage,
                Pages = bookEntity.Pages
            };
            BookModel = bookModel;

            return bookModel;
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

            try
            {
                var bookEntity = await bookData.GetByIdAsync(BookModel.BookID);

                if (UploadedImage != null)
                {
                    // Upload new Picture
                    var newFilePath = Path.Combine("images/covers", UploadedImage.FileName);
                    await UploadImageToFileSystem(newFilePath);
                    // Delete old picture
                    DeleteOldImageFromFileSystem(bookEntity.CoverFilePath);
                    // Edit picture path in db
                    bookEntity.CoverFilePath = newFilePath;
                }

                bool bookIsUpdated = await UpdateBookEntity(bookEntity);
                if (bookIsUpdated)
                {
                    await SaveBookEntityToDatabase();
                }

                EditNotification = $"Changes were made to {bookEntity.Title}";
            }
            catch
            {
                // TODO: Log Exception
                EditNotification = $"Book was not updated, check log.";
            }

            return RedirectToPage("./ListBook");
        }

        private async Task UploadImageToFileSystem(string newImagePath)
        {
            var file = Path.Combine(environment.WebRootPath, newImagePath);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadedImage.CopyToAsync(fileStream);
            }
        }

        private void DeleteOldImageFromFileSystem(string oldImagePath)
        {
            var oldFilePath = Path.Combine(environment.WebRootPath, oldImagePath);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
        }

        private async Task<bool> UpdateBookEntity(Book bookEntity)
        {
            bool bookIsUpdated = await TryUpdateModelAsync<Book>(bookEntity, nameof(BookModel),
                x => x.Title, x => x.CurrentPage, x => x.Pages);

            if (bookIsUpdated)
            {
                var authorEntity = await authorData.GetByIdAsync(AuthorId);
                bookEntity.Author = authorEntity;
            }

            return bookIsUpdated;
        }

        private async Task SaveBookEntityToDatabase()
        {
            await bookData.CommitAsync();
        }
    }
}