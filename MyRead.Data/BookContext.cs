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

            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(x => x.LastName)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
