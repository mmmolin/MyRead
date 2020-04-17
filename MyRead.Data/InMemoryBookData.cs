using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRead.Data
{
    public class InMemoryBookData : IBookData
    {
        private List<Book> books { get; set; }
        public InMemoryBookData()
        {
            this.books = new List<Book> { new Book { BookID = 1, Title = "The light fantastic",  Author = new Author {FirstName = "Terry Pratchett"}},
                                        new Book { BookID = 2, Title = "Jingo", Author = new Author {FirstName = "Terry Pratchett"}},
                                        new Book { BookID = 3, Title = "Going postal", Author = new Author {FirstName = "Terry Pratchett"}},
            };
        }

        public IEnumerable<Book> GetActiveBooks()
        {
            return books;
        }

        public Book GetBookById(int bookId)
        {
            return books.Where(b => b.BookID == bookId).FirstOrDefault(); ;
        }

        public Book AddBook(Book book)
        {
            book.BookID = books.Max(b => b.BookID) + 1;
            books.Add(book);
            return book;
        }

        public void DeleteBook(int bookId)
        {
            var book = GetBookById(bookId);
            if(book != null)
            {
                books.Remove(book);
            }            
        }

        public int Commit()
        {
            return 1;
        }
    }
}
