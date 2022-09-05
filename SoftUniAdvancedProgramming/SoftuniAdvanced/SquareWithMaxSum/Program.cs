using System;
using System.Linq;

namespace SquareWithMaxSum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var rows = size[0];
            var cols = size[1];

            var squareHeight = 2;
            var squareWidth = 2;
            var matrix = new int[rows, cols];

            WriteValues(rows, cols, matrix);

            var biggestPosX = 0;
            var biggestPosY = 0;
            var biggestSum = int.MinValue;

            for (int i = 0; i < rows; i++)
            {
                if(RowsOutOfRange(matrix, squareHeight, i))
                {
                    break;
                }

                for (int j = 0; j < cols; j++)
                {
                    if(ColsOutOfRange(matrix, squareWidth, j))
                    {
                        break;
                    }

                    var sum = GetSum(i, j, matrix, squareHeight, squareWidth);

                    if(sum > biggestSum)
                    {
                        biggestSum = sum;
                        biggestPosX = i;
                        biggestPosY = j;
                    }
                }
            }

            PrintSquare(matrix, biggestPosX, biggestPosY, squareHeight, squareWidth);
            Console.WriteLine(biggestSum);
        }

        private static void PrintSquare(int[,] matrix, int biggestPosX, int biggestPosY, int squareHeight, int squareWidth)
        {
            if(matrix.GetLength(0) <= 0 || matrix.GetLength(1) <= 0)
            {
                return;
            }

            for (int i = biggestPosX; i < squareHeight + biggestPosX; i++)
            {
                for (int j = biggestPosY; j < squareWidth + biggestPosY; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static int GetSum(int height, int width, int[,] matrix, int squareHeight, int squareWidth)
        {
            var sum = 0;

            if (matrix.GetLength(0) <= 0 || matrix.GetLength(1) <= 0)
            {
                return 0;
            }

            for (int i = height; i < squareHeight + height; i++)
            {
                for (int j = width; j < squareWidth + width; j++)
                {
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        private static bool ColsOutOfRange(int[,] matrix, int squareWidth, int pos)
        {
            if(pos + squareWidth - 1 >= matrix.GetLength(1))
            {
                return true;
            }

            return false;
        }

        private static bool RowsOutOfRange(int[,] matrix, int squareHeight, int pos)
        {
            if(pos + squareHeight - 1 >= matrix.GetLength(0))
            {
                return true;
            }

            return false;
        }

        private static void WriteValues(int rows, int cols, int[,] matrix)
        {
            for (int i = 0; i < rows; i++)
            {
                var input = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = input[j];
                }
            }
        }
    }
}
