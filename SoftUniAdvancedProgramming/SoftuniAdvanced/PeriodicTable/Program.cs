using System;
using System.Collections.Generic;

namespace PeriodicTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());
            var periodicTable = new SortedSet<string>();

            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < input.Length; j++)
                {
                    periodicTable.Add(input[j]);
                }
            }

            Console.WriteLine(string.Join(' ', periodicTable));
        }
    }
}
