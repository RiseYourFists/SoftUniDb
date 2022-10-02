using System;
using System.Linq;

namespace ReverseAndExclude
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            input.Reverse();
            var n = int.Parse(Console.ReadLine());
            Func<int, bool> validate = (x => x % n != 0);
            var result = input.Where(validate).ToList();
            result.ForEach(x => Console.Write($"{x} "));

        }
    }
}
