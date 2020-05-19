using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRead.Core.Entities;
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

        [TempData]
        public string DeleteNotification { get; set; }

        public List<Author> Authors { get; set; }
        public void OnGet()
        {
            Authors = authorData.GetAll().ToList();
        }
    }
}