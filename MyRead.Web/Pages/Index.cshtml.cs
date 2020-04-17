using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyRead.Core;
using MyRead.Data;

namespace MyRead.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookData bookData;

        public IndexModel(ILogger<IndexModel> logger, IBookData bookData)
        {
            _logger = logger;
            this.bookData = bookData;
        }

        public IEnumerable<Book> Books { get; set; }

        public void OnGet()
        {
            Books = bookData.GetActiveBooks();
        }
    }
}
