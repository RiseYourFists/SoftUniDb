using System;
using System.Linq;

namespace ActionPoint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action<string> print = Console.WriteLine;
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            input.ToList().ForEach(print);
        }
    }
}
