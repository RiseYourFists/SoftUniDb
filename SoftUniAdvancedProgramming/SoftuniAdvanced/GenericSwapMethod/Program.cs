using GenericBoxOfString;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericSwapMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var count = int.Parse(Console.ReadLine());
            var newBox = new Box<int>();

            for (int i = 0; i < count; i++)
            {
                newBox.Add(int.Parse(Console.ReadLine()));
            }

            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();

            newBox.Swap(tokens[0], tokens[1]);

            Console.WriteLine(newBox.ToString());
        }
    }
}
