using System;
using System.Linq;

namespace KnightsOfHonor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action<string> AddSir = (x => Console.WriteLine($"Sir {x}"));
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            input.ForEach(AddSir);
        }
    }
}
