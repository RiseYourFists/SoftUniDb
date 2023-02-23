using System;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Models;
using BookShop.Initializer;
using BookShop.Models.Enums;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            
            var context = new BookShopContext();
            //DbInitializer.ResetDatabase(context);
            //var input = int.Parse(Console.ReadLine());
            var result = RemoveBooks(context);
            Console.WriteLine(result);

        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var sb = new StringBuilder();

            var restrictions = new Dictionary<string, int>();
            var counter = 0;
            foreach (var name in Enum.GetNames(typeof(AgeRestriction)))
            {
                restrictions.Add(name.ToLower(), counter++);
            }

            var book = context.Books
                .Where(b => b.AgeRestriction == (AgeRestriction)restrictions[command.ToLower()])
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            book.ForEach(b => sb.AppendLine(b));

            return sb.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold
                              && b.Copies < 5000)
                .Select(b => b.Title)
                .ToList();

            books.ForEach(b => sb.AppendLine(b));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new { b.Title, b.Price })
                .ToList();

            books.ForEach(b => 
                sb.AppendLine($"{b.Title} - ${b.Price:f2}")
                );

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue
                            && b.ReleaseDate.Value.Year != year)
                .Select(b => b.Title)
                .ToList();

            books.ForEach(b => sb.AppendLine(b));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var categoryNames = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            var books = context.BookCategories
                .Include(bc => bc.Category)
                .Include(bc => bc.Book)
                .Where(bc => categoryNames
                    .Contains(bc.Category.Name.ToLower()))
                .Select(bc => bc.Book.Title)
                .OrderBy(b => b)
                .ToList();

            books.ForEach(b => sb.AppendLine(b));


            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var sb = new StringBuilder();

            var tokens = date.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var day = int.Parse(tokens[0]);
            var month = int.Parse(tokens[1]);
            var year = int.Parse(tokens[2]);

            var filter = new DateTime(year, month, day);

            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue
                && b.ReleaseDate.Value < filter)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                })
                .ToList();

            books.ForEach(b => sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:f2}"));

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .ToList()
                .OrderBy(a => a)
                .ToList();

            authors.ForEach(a => sb.AppendLine(a));

            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            books.ForEach(b => sb.AppendLine(b));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Include(b => b.Author)
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToList();

            books.ForEach(b => sb.AppendLine(b));

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList();
            
            return books.Count;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            var authors = context.Authors
                .Include(a => a.Books)
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}",
                    TotalCopies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.TotalCopies)
                .ToList();

            authors.ForEach(a => sb.AppendLine($"{a.FullName} - {a.TotalCopies}"));
            
            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context.Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(cb => cb.Book)
                .Select(c => new
                {
                    Category = c.Name,
                    TotalProfit = c.CategoryBooks
                        .Select(cb => cb.Book.Price * cb.Book.Copies)
                        .Sum()
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Category)
                .ToList();

            books.ForEach(b => sb.AppendLine($"{b.Category} ${b.TotalProfit:f2}"));

            return sb.ToString().TrimEnd();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categories = context.Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(cb => cb.Book)
                .Select(c => new
                {
                    Catergory = c.Name,
                    MostRecentBooks = c.CategoryBooks
                        .OrderByDescending(cb => cb.Book.ReleaseDate)
                        .Take(3)
                        .Select(b => new
                        {
                            b.Book.Title,
                            b.Book.ReleaseDate
                        })
                })
                .OrderBy(c => c.Catergory)
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Catergory}");
                foreach (var mostRecentBook in category.MostRecentBooks)
                {
                    string year = (mostRecentBook.ReleaseDate.HasValue) 
                        ? mostRecentBook.ReleaseDate.Value.Year.ToString() 
                        : "N/A";

                    sb.AppendLine($"{mostRecentBook.Title} ({year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
                .ToList();

            books.ForEach(b => b.Price += 5m);

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var bookIds = booksToRemove
                .Select(b => b.BookId);

            var count = booksToRemove.Count;

            var mappings = context.BookCategories
                .Where(cb => bookIds
                    .Contains(cb.BookId))
                .ToList();

            mappings.ForEach(m => context.Remove(m));
            booksToRemove.ForEach(b => context.Remove(b));
            context.SaveChanges();
                
            return count;
        }
    }
}
