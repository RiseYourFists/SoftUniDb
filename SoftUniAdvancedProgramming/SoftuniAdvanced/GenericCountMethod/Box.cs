using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericBoxOfString
{
    public class Box<T>
        where T : IComparable
    {
        public Box()
        {
            Items = new List<T>();
        }

        private List<T> Items { get; set; }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public void Swap(int item1, int item2)
        {
            var holder = Items[item1];
            Items[item1] = Items[item2];
            Items[item2] = holder;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var value in this.Items)
            {
                sb.AppendLine($"{value.GetType()}: {value}");
            }

            return sb.ToString();
        }

        public int CompareCount(T itemComparrison)
            => Items.Count(x => x.CompareTo(itemComparrison) > 0);
    }
}
