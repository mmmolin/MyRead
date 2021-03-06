﻿using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Book> GetAll()
        {
            return bookContext.Books;
        }

        public async Task<Book> GetByIdAsync(int entityId)
        {
            return await bookContext.Books.Where(x => x.BookID == entityId)
                .Include(x => x.Author)
                .FirstOrDefaultAsync();
        }

        public void Remove(Book entity)
        {
            bookContext.Books.Remove(entity);
        }

        public async Task<int> CommitAsync()
        {
            return await bookContext.SaveChangesAsync();
        }
    }
}
