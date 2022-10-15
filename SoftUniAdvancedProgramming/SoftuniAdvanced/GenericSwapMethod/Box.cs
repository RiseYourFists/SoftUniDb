using System;
using System.Collections.Generic;
using System.Text;

namespace GenericBoxOfString
{
    public class Box<T>
    {
        public Box()
        {
            list = new List<T>();
        }

        private List<T> list;

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Swap(int item1, int item2)
        {
            var holder = list[item1];
            list[item1] = list[item2];
            list[item2] = holder;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var value in this.list)
            {
                sb.AppendLine($"{value.GetType()}: {value}");
            }

            return sb.ToString();
        }
    }
}
