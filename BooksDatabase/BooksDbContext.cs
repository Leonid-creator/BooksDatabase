using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BooksDatabase
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;" +
                "AttachDbFileName=C:\\Users\\lenin\\source\\repos\\Leonid-creator\\BooksDatabase\\BooksDatabase\\BooksDb\\BooksDb.mdf;" +
                "Database=booksDataBase;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Book>().HasIndex(b => b.Title).IsUnique();
            modelBuilder.Entity<Book>().HasKey(k => k.Id);
            modelBuilder.Entity<Genre>().HasKey(k => k.Id);
            modelBuilder.Entity<Publisher>().HasKey(k => k.Id);
            modelBuilder.Entity<Author>().HasKey(k => k.Id);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherID);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.AuthorID);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.GenreID);
        }
    }
}

