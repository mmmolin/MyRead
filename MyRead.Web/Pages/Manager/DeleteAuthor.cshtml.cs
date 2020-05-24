using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core.Entities;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager
{
    public class DeleteAuthorModel : PageModel
    {
        private readonly IData<Author> authorData;
        public DeleteAuthorModel(IData<Author> authorData)
        {
            this.authorData = authorData;
        }

        [TempData]
        public string DeleteNotification { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(int authorId)
        {
            var authorEntity = await authorData.GetByIdAsync(authorId);
            
            if(authorEntity != null)
            {
                DeleteNotification = $"Deleted {authorEntity.FirstName} {authorEntity.LastName} from database";

                authorData.Remove(authorEntity);
                await authorData.CommitAsync();
            }            

            return RedirectToPage("./ListAuthor");
        }
    }
}