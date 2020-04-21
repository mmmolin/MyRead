using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRead.Data
{
    public class BookData : ICrudData<Book>
    {
        private readonly BookContext bookContext;

        public BookData(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public IEnumerable<Book> GetAll()
        {
            return bookContext.Books.ToList();
        }

        //public Book AddBook(Book book)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Commit()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteBook(int bookId)
        //{
        //    throw new NotImplementedException();
        //}



        //public Book GetBookById(int bookId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
