using System;
using System.Collections.Generic;
using System.Linq;

namespace AppliedAritmetics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var initialNumbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                if(input != "print")
                {
                    Func<int, int> quickAction = GetAction(input);
                    initialNumbers = initialNumbers.Select(x => quickAction(x)).ToList();
                    continue;
                }

                Console.WriteLine(string.Join(' ', initialNumbers));
            }
        }

        private static Func<int, int> GetAction(string input)
        {
            switch (input)
            {
                case "multiply":
                    return (x => x * 2);
                case "subtract":
                    return (x => x - 1);
                default:
                    return (x => x + 1);
            }
        }

        
    }
}
