using System;
using System.Collections.Generic;
using System.Linq;

namespace SameValuesInArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

            var occurenceDictionary = new Dictionary<double, int>();

            foreach (var item in input)
            {
                if(occurenceDictionary.ContainsKey(item))
                {
                    occurenceDictionary[item]++;
                    continue;
                }

                occurenceDictionary.Add(item, 1);
            }

            foreach (var item in occurenceDictionary)
            {
                Console.WriteLine($"{item.Key} - {item.Value} times");
            }
        }
    }
}
