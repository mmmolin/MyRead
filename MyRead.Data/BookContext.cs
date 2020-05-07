using Microsoft.EntityFrameworkCore;
using MyRead.Core.Entities;

namespace MyRead.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> bookContext) : base(bookContext)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Book>().ToTable("Book");
        }
    }
}
