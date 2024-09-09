using BooksDatabase;
using System;

namespace BooksDatabase
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public Guid GenreID { get; set; }
        public Guid PublisherID { get; set; }
        public Guid AuthorID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Publisher Publisher { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}

