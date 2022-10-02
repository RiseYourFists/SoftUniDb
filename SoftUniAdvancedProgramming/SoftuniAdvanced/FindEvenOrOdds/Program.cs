using System;
using System.Linq;

namespace FindEvenOrOdds
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var range = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var type = Console.ReadLine();
            Predicate<int> predicate = GetAction(type);

            for (int i = range[0]; i <= range[1]; i++)
            {
                if(predicate(Math.Abs(i)))
                {
                    Console.Write($"{i} ");
                }
            }
        }

        private static Predicate<int> GetAction(string v)
        {
            if (v == "even") return (x => x % 2 == 0);
            return (x => x % 2 == 1);
        }
    }
}
