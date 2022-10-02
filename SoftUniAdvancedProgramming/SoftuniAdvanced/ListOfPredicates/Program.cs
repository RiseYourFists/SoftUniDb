using System;
using System.Collections.Generic;
using System.Linq;

namespace ListOfPredicates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var range = int.Parse(Console.ReadLine());
            var compare = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var predicates = new List<Predicate<int>>();

            foreach (var item in compare)
            {
                predicates.Add((x => x % item == 0));
            }

            var result = new List<int>();

            for (int i = 1; i <= range; i++)
            {
                if(Validate(i, predicates))
                {
                    result.Add(i);
                }
            }

            Console.WriteLine(string.Join(' ', result));
        }

        public static bool Validate(int num , List<Predicate<int>> predicates)
        {
            foreach (var item in predicates)
            {
                if(item(num))
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}
