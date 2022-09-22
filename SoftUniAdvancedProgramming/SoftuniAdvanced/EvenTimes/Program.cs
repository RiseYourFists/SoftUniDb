using System;
using System.Collections.Generic;
using System.Linq;

namespace EvenTimes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());

            var numbers = new Dictionary<string, int>();

            for (int i = 0; i < inputs; i++)
            {
                var newNum = Console.ReadLine();

                if(numbers.ContainsKey(newNum))
                {
                    numbers[newNum]++;
                    continue;
                }

                numbers.Add(newNum, 1);
            }

            var result = numbers.First(x => x.Value % 2 == 0);
            Console.WriteLine(result.Key);
        }
    }
}
