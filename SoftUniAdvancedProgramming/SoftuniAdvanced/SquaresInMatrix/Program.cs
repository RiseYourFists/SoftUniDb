using System;
using System.Linq;

namespace SquaresInMatrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var rows = size[0];
            var cols = size[1];
            var matrix = new string[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            var squareSize = 2;
            var squaresFound = 0;

            for (int row = 0; row < rows; row++)
            {
                if(RowOutOfBounds(matrix, squareSize, row))
                {
                    break;
                }

                for (int col = 0; col < cols; col++)
                {
                    if(ColOutOfBounds(matrix, squareSize, col))
                    {
                        break;
                    }

                    if(HasSquare(matrix, squareSize, row, col))
                    {
                        squaresFound++;
                    }
                }
                
            }

            Console.WriteLine(squaresFound);
        }

        private static bool HasSquare(string[,] matrix, int squareSize, int row, int col)
        {
            var match = matrix[row, col];
            var rowLen = row + squareSize;

            for (int i = row; i < rowLen; i++)
            {
                var colLen = col + squareSize;

                for (int j = col; j < colLen; j++)
                {
                    if(matrix[i, j] != match)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool ColOutOfBounds(string[,] matrix, int squareSize, int col)
        {
            return col + squareSize - 1 >= matrix.GetLength(1);
        }

        private static bool RowOutOfBounds(string[,] matrix, int squareSize, int row)
        {
            return row + squareSize - 1 >= matrix.GetLength(0);
        }
    }
}
