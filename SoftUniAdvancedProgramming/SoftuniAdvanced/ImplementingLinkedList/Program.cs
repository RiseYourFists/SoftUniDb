using System;

namespace ImplementingLinkedList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new DoblyLinkedList();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);
            list.AddFirst(4);

            list.ForEach(x => Console.WriteLine(x.Value));

            list.RemoveLast();
            list.RemoveFirst();

            list.AddLast(5);

            var result = list.ToArray();

            Console.WriteLine("Last print:");

            foreach (var item in result)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
