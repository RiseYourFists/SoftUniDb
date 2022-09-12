using System;
using System.Linq;
using System.Text;

namespace SnakeMoves
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var rows = size[0];
            var cols = size[1];

            var snakeBody = Console.ReadLine();

            var matrix = new char[rows, cols];
            var snakePos = 0;
            var backWards = false;

            for (int i = 0; i < rows; i++)
            {

                if (!backWards)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = snakeBody[snakePos];

                        snakePos++;
                        if (NewSnake(snakeBody, snakePos))
                        {
                            snakePos = 0;
                        }
                    }
                    backWards = true;
                    continue;
                }

                for (int j = cols - 1; j >= 0; j--)
                {
                    matrix[i, j] = snakeBody[snakePos];

                    snakePos++;
                    if (NewSnake(snakeBody, snakePos))
                    {
                        snakePos = 0;
                    }
                }
                backWards = false;
            }

            PrintSnake(matrix);
        }

        private static void PrintSnake(char[,] matrix)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(matrix[i, j]);
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb);
        }

        private static bool NewSnake(string snakeBody, int snakePos)
        {
            return snakeBody.Length == snakePos;
        }
    }
}
