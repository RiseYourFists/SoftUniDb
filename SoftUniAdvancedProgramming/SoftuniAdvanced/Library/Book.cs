using System;
using System.Collections.Generic;

public class Book : IComparable<Book>
{
    public Book(string title, int year, params string[] authors)
    {
        Title = title;
        Year = year;
        Authors = new List<string>(authors);
    }

    public string Title { get; private set; }

    public int Year { get; private set; }

    public List<string> Authors { get; private set; }

    public int CompareTo(Book other)
    {
        var result = Year.CompareTo(other.Year);

        if (result == 0)
        {
            result = Title.CompareTo(other.Title);
        }

        return result;
    }

    public override string ToString() => $"{Title} - {Year}";
}

