﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using MyRead.Data;

namespace MyRead.Web.Pages.Manager
{
    public class DeleteAuthorModel : PageModel
    {
        private IData<Author> authorData;
        public DeleteAuthorModel(IData<Author> authorData)
        {
            this.authorData = authorData;
        }

        [TempData]
        public string DeleteNotification { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(int authorId) // Cascade did not work, books not deleted.
        {
            var authorEntity = await authorData.GetByIdAsync(authorId);
            //var authorEntity = authorData.GetAll()
            //    .Where(x => x.AuthorID == authorId)
            //    .Include(x => x.Books)
            //    .FirstOrDefault();

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