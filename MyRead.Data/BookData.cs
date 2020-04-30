using Microsoft.EntityFrameworkCore;
using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRead.Data
{
    public class BookData : IData<Book>
    {
        private readonly BookContext bookContext;

        public BookData(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public void Add(Book entity)
        {
            bookContext.Add(entity);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await bookContext.Books
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int entityId)
        {
            return await bookContext.Books.Where(x => x.BookID == entityId)
                .Include(x => x.Author)
                .FirstOrDefaultAsync();
        }

        public void Remove(Book book)
        {
            bookContext.Books.Remove(book);
        }

        public async Task<int> CommitAsync()
        {
            return await bookContext.SaveChangesAsync();
        }
    }
}
