using System;
using System.Linq;

namespace SumOfMatrixColumns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var matrix = new int[size[0], size[1]];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            for (int i = 0; i < size[1]; i++)
            {
                var result = 0;

                for (int j = 0; j < size[0]; j++)
                {
                    result += matrix[j, i];
                }

                Console.WriteLine(result);
            }
        }
    }
}
