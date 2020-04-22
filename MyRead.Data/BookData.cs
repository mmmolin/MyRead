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

        public IEnumerable<Book> GetAll()
        {
            return bookContext.Books.ToList();
        }

        public Book GetById(int entityId)
        {
            return bookContext.Books.Where(x => x.BookID == entityId).FirstOrDefault();
        }

        

        public async Task<int> CommitAsync()
        {
            return await bookContext.SaveChangesAsync();
        }
    }
}
