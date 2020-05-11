using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyRead.Core.Entities;
using MyRead.Data;

namespace MyRead.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IData<Book> bookData;

        public IndexModel(ILogger<IndexModel> logger, IData<Book> bookData)
        {
            _logger = logger;
            this.bookData = bookData;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await bookData.GetAll()
                .Where(x => !x.IsArchived)
                .Include(x => x.Author)
                .ToListAsync();
        }
    }
}
