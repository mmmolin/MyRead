using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core;
using MyRead.Core.Models;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager
{
    public class ListAuthorModel : PageModel
    {
        private IData<Author> authorData;
        public ListAuthorModel(IData<Author> authorData)
        {
            this.authorData = authorData;
        }
        public List<AuthorModel> AuthorModels { get; set; }
        public Book BookEntity { get; set; }
        public async Task OnGet(Book bookEntity)
        {
            var authorEntities = await authorData.GetAllAsync();
            AuthorModels = authorEntities.Select(x => 
            new AuthorModel 
            {
                AuthorID = x.AuthorID, 
                FirstName = x.FirstName, 
                LastName = x.LastName 
            }).ToList();
            
            BookEntity = bookEntity;
        }

        public async Task OnPostAsync(int id)
        {
            var test = id;
        }
    }
}