using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IteratorsAndComparators
{
    public class Program
    {
        static void Main(string[] args)
        {
            Book bookOne = new Book("Animal Farm", 2003, "George Orwell");
            Book bookTwo = new Book("The Documents in the Case", 2002, "Dorothy Sayers", "Robert Eustace");
            Book bookThree = new Book("The Documents in the Case", 1930);

            var books = new Book[]
            {
                bookOne,
                bookTwo,
                bookThree
            };

            var library = new Library(books);
        }
    }
}

public class Library
{
    public Library(params Book[] books)
    {
        BookShelf = new List<Book>(books);
    }

    private List<Book> BookShelf { get; set; }
}

public class Book
{
    public Book(string title, int year, params string[] authors)
    {
        Title = title;
        Year = year;
        Authors = new List<string>(authors);
    }

    public string Title { get; set; }

    public int Year { get; set; }

    public List<string> Authors { get; set; }
}