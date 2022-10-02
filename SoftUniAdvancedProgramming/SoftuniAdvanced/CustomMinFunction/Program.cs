using System;
using System.Linq;

namespace CustomMinFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<int[], int> minFunc = GetMin;
            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Console.WriteLine(minFunc(input));
        }

        private static int GetMin(int[] collection)
        {
            var min = int.MaxValue;

            foreach (var number in collection)
            {
                if(number < min)
                {
                    min = number;
                }
            }

            return min;
        }
    }
}
