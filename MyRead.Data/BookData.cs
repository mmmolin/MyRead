using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRead.Data
{
    public class BookData : IBookData
    {
        public Book AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetActiveBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
