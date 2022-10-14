using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOfT
{
    public class Box<T>
    {
        private List<T> elements = new List<T>();
        public int Count { get { return elements.Count; } }

        public void Add(T element)
        {
            elements.Add(element);
        }

        public T Remove()
        {
            var item = elements[Count - 1];
            elements.RemoveAt(Count - 1);
            return item;
        }
    }
}
