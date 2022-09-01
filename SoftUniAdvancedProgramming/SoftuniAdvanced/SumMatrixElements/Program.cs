using System;
using System.Linq;

namespace SumMatrixElements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var matrix = new int[size[0], size[1]];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var input = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            var result = 0;

            foreach (var item in matrix)
            {
                result += item;
            }

            Console.WriteLine($"{size[0]}\n{size[1]}\n{result}");
        }
    }
}
