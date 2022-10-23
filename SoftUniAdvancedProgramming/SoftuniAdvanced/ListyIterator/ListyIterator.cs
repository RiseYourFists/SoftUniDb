using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IteratosAndComparators
{
    public class ListyIterator<T> : IEnumerator<T>
    {
        public ListyIterator(T[] collection)
        {
            Reset();
            Collection = new List<T>(collection);
        }

        private List<T> Collection { get; set; }

        public T Current => Collection[index];

        private int index;

        public bool Move() => MoveNext();

        public bool HasNext() => index + 1 < Collection.Count;

        public void Print() 
        {
            var msg = (Collection.Count > 0 && index < Collection.Count) ? $"{Collection[index]}" : "Invalid Operation!";
            Console.WriteLine(msg);
        }

        public bool MoveNext() => index++ < Collection.Count;

        public void Reset()=> index = 0;

        public void Dispose() { }

        object IEnumerator.Current => Current;
    }
}
