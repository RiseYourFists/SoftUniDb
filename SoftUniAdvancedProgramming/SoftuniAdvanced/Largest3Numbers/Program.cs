using System;
using System.Linq;

namespace Largest3Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var result = input.OrderByDescending(x => x).Take(3).ToArray();

            Console.WriteLine(String.Join(' ', result));
        }
    }
}
