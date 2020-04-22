using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }        

        public IEnumerable<Author> GetAll()
        {
            return bookContext.Authors.ToList();
        }

        public Author GetById(int entityId)
        {
            return bookContext.Authors.Where(x => x.AuthorID == entityId).FirstOrDefault();
        }

        public async Task<int> CommitAsync()
        {
            return await bookContext.SaveChangesAsync();
        }
    }
}
