using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintEvenNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var intArray = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var numberQueue = new Queue<int>();

            foreach (var item in intArray)
            {
                numberQueue.Enqueue(item);
            }

            var result = new List<int>();

            while (numberQueue.Count > 0)
            {
                var currNum = numberQueue.Dequeue();

                if (currNum % 2 == 0)
                {
                    result.Add(currNum);
                }
            }

            Console.WriteLine(string.Join(", ", result));
        }
    }
}
