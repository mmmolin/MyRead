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
            this.books = new List<Book> { new Book { Id = 1, Title = "The light fantastic", Author = "Terry Pratchett"},
                                        new Book { Id = 2, Title = "Jingo", Author = "Terry Pratchett"},
                                        new Book { Id = 3, Title = "Going postal", Author = "Terry Pratchett"},
            };
        }

        public IEnumerable<Book> GetActiveBooks()
        {
            return books;
        }

        public Book GetBookById(int bookId)
        {
            return books.Where(b => b.Id == bookId).FirstOrDefault(); ;
        }

        public Book AddBook(Book book)
        {
            book.Id = books.Max(b => b.Id) + 1;
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
