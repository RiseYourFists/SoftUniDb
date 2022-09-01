using System;
using System.Linq;

namespace SumOfPrimaryDiagonal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());

            var matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            var result = 0;

            for (int i = 0; i < size; i++)
            {
                result += matrix[i, i];
            }

            Console.WriteLine(result);
        }
    }
}
