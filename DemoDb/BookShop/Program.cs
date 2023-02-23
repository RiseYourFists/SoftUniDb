using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models;
using BookShop.Models.Enums;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();
            //var command = Console.ReadLine();
            var books = GetBooksByPrice(context);
            Console.WriteLine(books);

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
    }
}
