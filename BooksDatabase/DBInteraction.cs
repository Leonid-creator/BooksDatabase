//using DocumentFormat.OpenXml.Bibliography;
//using DocumentFormat.OpenXml.InkML;
//using DocumentFormat.OpenXml.Office2013.Excel;
//using DocumentFormat.OpenXml.Packaging;
using Microsoft.EntityFrameworkCore;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using BooksDatabase;
using System.ComponentModel;

namespace Books
{
    internal class DBInteraction
    {
        public static void AddBooksFromCSV(string filePath)
        {
            int cellsInFrontOfPublisher = 5;
            var reader = new StreamReader(filePath);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Read();

            while (csv.Read())
            {
                string[] fields = csv.Parser.Record;
                string[] filteredFields = fields.Where(field => !string.IsNullOrWhiteSpace(field)).ToArray();
                string[] briefBookInfo = filteredFields[..(cellsInFrontOfPublisher)];

                for (int i = cellsInFrontOfPublisher; i < filteredFields.Length; i++)
                {
                    string[] bookProperties = new string[cellsInFrontOfPublisher + 1];
                    for (int j = 0; j < briefBookInfo.Length; j++)
                    {
                        bookProperties[j] = briefBookInfo[j];
                    }
                    bookProperties[^1] = filteredFields[i];
                    AddNewBook(bookProperties);
                }
            }
        }

        public static void AddNewGenre(string genreName)
        {
            BooksDbContext dbContext = new BooksDbContext();
            Genre newGenre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = genreName
            };
            dbContext.Genres.Add(newGenre);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                PrintExceptionMessage(ex);
            }
        }

        public static void AddNewAuthor(string authorName)
        {
            BooksDbContext dbContext = new BooksDbContext();
            Author newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = authorName
            };
            dbContext.Authors.Add(newAuthor);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                PrintExceptionMessage(ex);
            }
        }

        public static void AddNewPublisher(string publisherName)
        {
            BooksDbContext dbContext = new BooksDbContext();
            Publisher newPublisher = new Publisher
            {
                Id = Guid.NewGuid(),
                Name = publisherName
            };
            dbContext.Publishers.Add(newPublisher);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                PrintExceptionMessage(ex);
            }
        }

        public static void AddNewBook(string[] bookProperties)
        {
            try
            {
                string title = bookProperties[0];
                int pages = int.Parse(bookProperties[1]);
                string genre = bookProperties[2];
                DateTime releaseDate = Convert.ToDateTime(bookProperties[3]);
                string author = bookProperties[4];
                string publisher = bookProperties[5];

                BooksDbContext dbContext = new BooksDbContext();

                Genre existingGenre = new Genre();
                if (!dbContext.Genres.Any(b => b.Name == genre))
                {
                    AddNewGenre(genre);
                }
                existingGenre = dbContext.Genres.FirstOrDefault(a => a.Name == genre);

                Author existingAuthor = new Author();
                if (!dbContext.Authors.Any(b => b.Name == author))
                {
                    AddNewAuthor(author);
                }
                existingAuthor = dbContext.Authors.FirstOrDefault(a => a.Name == author);

                Publisher existingPublisher = new Publisher();
                if (!dbContext.Publishers.Any(b => b.Name == publisher))
                {
                    AddNewPublisher(publisher);
                }
                existingPublisher = dbContext.Publishers.FirstOrDefault(a => a.Name == publisher);

                Book newBook = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Pages = pages,
                    GenreID = existingGenre.Id,
                    AuthorID = existingAuthor.Id,
                    PublisherID = existingPublisher.Id,
                    ReleaseDate = releaseDate
                };

                var existingBook = dbContext.Books.FirstOrDefault(b => b.Title == newBook.Title
                                                                         && b.Pages == newBook.Pages
                                                                         && b.GenreID == newBook.GenreID
                                                                         && b.PublisherID == newBook.PublisherID
                                                                         && b.AuthorID == newBook.AuthorID
                                                                         && b.ReleaseDate == newBook.ReleaseDate);

                if (existingBook == null)
                {
                    dbContext.Books.Add(newBook);
                    try
                    {
                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        PrintExceptionMessage(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error while adding entry");
                Console.WriteLine(ex.ToString());
            }
            
        }

        public static void RemoveFromAuthors(string authorName)
        {
            BooksDbContext dbContext = new BooksDbContext();
            Author existingAuthor = dbContext.Authors.FirstOrDefault(a => a.Name == authorName);
            dbContext.Authors.Remove(existingAuthor);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                PrintExceptionMessage(ex);
            }
        }

        public static void RemoveFromBooks(string bookName)
        {
            BooksDbContext dbContext = new BooksDbContext();
            Book existingBook = new Book();
            while (dbContext.Books.Any(b => b.Title == bookName))
            {
                existingBook = dbContext.Books.FirstOrDefault(a => a.Title == bookName);
                dbContext.Books.Remove(existingBook);

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    PrintExceptionMessage(ex);
                }
            }
        }

        public static void PrintExceptionMessage(Exception ex)
        {
            Console.WriteLine("Error when saving changes:");
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner exception details:");
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
