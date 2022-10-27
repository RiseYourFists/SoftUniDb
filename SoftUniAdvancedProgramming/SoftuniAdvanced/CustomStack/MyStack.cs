using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomStack
{
    public class MyStack<T> : IEnumerable<T>
    {
        public MyStack()
        {
            stack = new List<T>();
        }

        private readonly List<T> stack;

        private int Count { get { return stack.Count; } }

        public void Push(T element)
        {
            stack.Add(element);
        }

        public T Pop()
        {
            if (Count > 0)
            {
                T element = stack[^1];
                stack.Remove(element);
                return element;
            }

            throw new InvalidOperationException("No elements");
        }

        public IEnumerator<T> GetEnumerator() => new MyStackEnumerator(stack);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class MyStackEnumerator : IEnumerator<T>
        {

            public MyStackEnumerator(List<T> collection)
            {
                stack = new List<T>(collection);
                Reset();
            }
            private readonly List<T> stack;

            public T Current => stack[index];

            private int index;

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() => --index >= 0;

            public void Reset() => index = stack.Count;
        }

    }
}
