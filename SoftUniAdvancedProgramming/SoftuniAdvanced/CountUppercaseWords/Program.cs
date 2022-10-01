using System;
using System.Linq;

namespace CountUppercaseWords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<string, bool> isFirstUpper = x => char.IsUpper(x[0]);
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = input.Where(isFirstUpper).ToList();
            result.ForEach(x => Console.WriteLine(x));
        }

    }
}
