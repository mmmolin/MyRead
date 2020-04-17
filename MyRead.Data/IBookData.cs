using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRead.Data
{
    public interface IBookData
    {
        IEnumerable<Book> GetActiveBooks();
        Book GetBookById(int bookId);
        Book AddBook(Book book);
        void DeleteBook(int bookId);
        int Commit();

    }
}
