﻿using Microsoft.EntityFrameworkCore;
using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRead.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Book>().ToTable("Book");
        }
    }
}
