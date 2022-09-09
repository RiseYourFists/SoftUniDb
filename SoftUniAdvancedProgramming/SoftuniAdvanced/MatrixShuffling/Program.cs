using System;
using System.Linq;

namespace MatrixShuffling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var height = size[0];
            var width = size[1];

            var matrix = WriteArray(height, width);

            while (true)
            {
                var input = Console.ReadLine().Split();

                if (input[0] == "END")
                {
                    break;
                }

                if(input[0] == "swap" && input.Length == 5)
                {
                    var targetRow = int.Parse(input[1]);
                    var targetCol = int.Parse(input[2]);
                    var moveTargetRow = int.Parse(input[3]);
                    var moveTargetCol = int.Parse(input[4]);

                    if(ParamOutOfRange(matrix, targetRow, targetCol, moveTargetRow, moveTargetCol))
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }

                    var tempCell = matrix[moveTargetRow, moveTargetCol];
                    matrix[moveTargetRow, moveTargetCol] = matrix[targetRow, targetCol];
                    matrix[targetRow, targetCol] = tempCell;
                    PrintSquare(matrix);
                    continue;
                }
                Console.WriteLine("Invalid input!");
            }

        }

        private static void PrintSquare(string[,] matrix)
        {
            if (matrix.GetLength(0) <= 0 || matrix.GetLength(1) <= 0)
            {
                return;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        private static bool ParamOutOfRange(string[,] matrix, int targetRow, int targetCol, int moveTargetRow, int moveTargetCol)
        {
            var lenRow = matrix.GetLength(0);
            var lenCol = matrix.GetLength(1);

            if(targetRow >= lenRow || moveTargetRow >= lenRow || targetCol >= lenCol || moveTargetCol >= lenCol)
            {
                return true;
            }
            return false;
        }

        private static string[,] WriteArray(int height, int width)
        {
            var matrix = new string[height, width];

            for (int i = 0; i < height; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            return matrix;
        }
    }
}
