using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRead.Data
{
    public class AuthorData : ICrudData<Author>
    {
        private readonly BookContext bookContext;
        public AuthorData(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public IEnumerable<Author> GetAll()
        {
            return bookContext.Authors.ToList();
        }
    }
}
