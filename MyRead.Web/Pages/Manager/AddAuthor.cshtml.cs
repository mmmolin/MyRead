using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core.Entities;
using MyRead.Core.Models;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager
{
    public class AddAuthorModel : PageModel
    {
        private readonly IData<Author> authorData;
        public AddAuthorModel(IData<Author> authorData)
        {
            this.authorData = authorData;
        }

        [BindProperty]
        public AuthorModel AuthorModel { get; set; } 

        public void OnGet()
        {
            AuthorModel = new AuthorModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            // Method or automapper ?
            var authorEntity = new Author
            {
                FirstName = AuthorModel.FirstName,
                LastName = AuthorModel.LastName
            };

            authorData.Add(authorEntity);
            await authorData.CommitAsync();

            return RedirectToPage("./AddBook");
        }
    }
}