using System;
using System.Linq;

namespace DiagonalDifference
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

            var primeDiagonal = SumOfPrimeDiagonal(size, matrix);
            var secondDiagonal = SumOfSecondDiagonal(size, matrix);

            var result = primeDiagonal - secondDiagonal;

            Console.WriteLine(Math.Abs(result));
        }

        private static int SumOfPrimeDiagonal(int size, int[,] matrix)
        {
            var result = 0;

            for (int i = 0; i < size; i++)
            {
                result += matrix[i, i];
            }

            return result;
        }

        private static int SumOfSecondDiagonal(int size, int[,] matrix)
        {
            var result = 0;

            for (int i = size - 1; i >= 0; i--)
            {
                result += matrix[i, size - i - 1];
            }

            return result;
        }
    }
}
