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
            var command = Console.ReadLine();
            var books = GetBooksByAgeRestriction(context, command);
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
    }
}
