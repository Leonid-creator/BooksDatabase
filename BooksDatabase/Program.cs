
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Books
{
    public class Program
    {
        static void Main(string[] args)
        {
            string booksBriefCsv = "E:\\booksBrief.csv";
            string booksCsv = "E:\\books.csv";
            //string booksXlsx = "E:\\books4.xlsx";

            //Console.WriteLine($"1 - AddBooksFromXLSX {booksXlsx}");
            Console.WriteLine("2 - RemoveFromBooks");
            Console.WriteLine("3 - Remove 5 Test Books");
            Console.WriteLine($"4 - BooksFromCSV {booksBriefCsv} (8 entries)");
            Console.WriteLine($"5 - BooksFromCSV {booksCsv} (49 entries)");

            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                //DBInteraction.AddBooksFromXLMS(booksXlsx);
            }
            else if (choice == 2)
            {
                while (true)
                {
                    Console.WriteLine("Enter book name:");
                    string name = Console.ReadLine();
                    if (name == "exit")
                    {
                        break;
                    }
                    else
                    {
                        DBInteraction.RemoveFromBooks(name);
                    }
                }
            }
            else if (choice == 3)
            {
                DBInteraction.RemoveFromBooks("To Kill a Mockingbird");
                DBInteraction.RemoveFromBooks("1984");
                DBInteraction.RemoveFromBooks("The Great Gatsby");
                DBInteraction.RemoveFromBooks("Jane Eyre");
                DBInteraction.RemoveFromBooks("The Picture of Dorian Gray");
            }
            else if (choice == 4)
            {
                DBInteraction.AddBooksFromCSV(booksBriefCsv);
            }
            else if (choice == 5)
            {
                DBInteraction.AddBooksFromCSV(booksCsv);
            }
            else
            {
                Console.WriteLine("error");
            }
        }
    }
}
