using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRead.Data
{
    public class AuthorData : IData<Author>
    {
        private readonly BookContext bookContext;
        public AuthorData(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public void Add(Author entity)
        {
            bookContext.Authors.Add(entity);
        }

        public IQueryable<Author> GetAll()
        {
            return bookContext.Authors;
        }

        public async Task<Author> GetByIdAsync(int entityId)
        {
            return await bookContext.Authors.Where(x => x.AuthorID == entityId).FirstOrDefaultAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await bookContext.SaveChangesAsync();
        }

        public void Remove(Author entity)
        {
            bookContext.Remove(entity);
        }
    }
}
