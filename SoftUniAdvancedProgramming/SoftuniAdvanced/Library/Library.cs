using System.Collections;
using System.Collections.Generic;

public class Library : IEnumerable<Book>
{
    public Library(params Book[] books)
    {
        this.books = new List<Book>(books);
    }

    private List<Book> books;

    public IEnumerator<Book> GetEnumerator() => new LibraryIterator(this.books);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    private class LibraryIterator : IEnumerator<Book>
    {
        public LibraryIterator(List<Book> books)
        {
            Reset();
            this.books = books;
        }
        private readonly List<Book> books;

        private int index;

        public Book Current => books[index];
        public bool MoveNext() => ++index < books.Count;

        public void Dispose() { }
        public void Reset() => index = -1;
        object IEnumerator.Current => Current;
    }
}