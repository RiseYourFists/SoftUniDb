using System;
using System.Linq;

namespace PredicateForNames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            Predicate<string> predicate = (x => x.Length <= n);
            Func<string, bool> validate = (x => predicate(x));
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var result = input.Where(validate).ToList();
            result.ForEach(x => Console.WriteLine(x));
        }
    }
}
