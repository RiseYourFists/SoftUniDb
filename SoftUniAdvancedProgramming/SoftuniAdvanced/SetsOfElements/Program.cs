using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SetsOfElements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var setSizes = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var setOne = new HashSet<string>();
            var setTwo = new HashSet<string>();

            for (int i = 0; i < setSizes[0]; i++)
            {
                setOne.Add(Console.ReadLine());
            }

            for (int i = 0; i < setSizes[1]; i++)
            {
                setTwo.Add(Console.ReadLine());
            }

            string[] result;

            if (setSizes[0] >= setSizes[1])
            {
                result = setOne.Where(x => setTwo.Contains(x)).ToArray();
            }
            else
            {
                result = setTwo.Where(x => setOne.Contains(x)).ToArray();
            }

            Console.WriteLine(string.Join(' ', result));
        }
    }
}
