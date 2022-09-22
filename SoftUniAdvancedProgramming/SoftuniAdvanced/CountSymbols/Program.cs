using System;
using System.Collections.Generic;

namespace CountSymbols
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var symbols = Console.ReadLine().ToCharArray();

            var symbolOccurances = new SortedDictionary<char, int>();

            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbolOccurances.ContainsKey(symbols[i]))
                {
                    symbolOccurances[symbols[i]]++;
                    continue;
                }

                symbolOccurances.Add(symbols[i], 1);
            }

            foreach (var item in symbolOccurances)
            {
                Console.WriteLine($"{item.Key}: {item.Value} time/s");
            }
        }
    }
}
