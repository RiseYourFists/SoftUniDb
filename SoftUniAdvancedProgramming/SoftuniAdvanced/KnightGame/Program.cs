using System;

namespace KnightGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());

            var chessBoard = WriteArray(size, size);
            var removed = 0;

            while (true)
            {
                var attacks = -1;
                var rowPos = 0;
                var colsPos = 0;
                var currAttacks = 0;

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if(chessBoard[i, j] == '0')
                        {
                            continue;
                        }

                        currAttacks = GetAttacks(chessBoard, i, j);

                        if(currAttacks > attacks)
                        {
                            attacks = currAttacks;
                            colsPos = j;
                            rowPos = i;
                        }
                    }
                }

                if(attacks == 0)
                {
                    break;
                }

                chessBoard[rowPos, colsPos] = '0';
                removed++;
            }


            Console.WriteLine(removed);
        }
        
        private static int GetAttacks(char[,] chessBoard, int row, int col)
        {
            var attacks = 0;

            if (InRange(chessBoard, "UL", row, col) && chessBoard[row - 2, col - 1] == 'K')
            {
                attacks++;
            }

            if(InRange(chessBoard, "UR", row, col) && chessBoard[row - 2, col + 1] == 'K')
            {
                attacks++;
            }

            if(InRange(chessBoard, "TL", row, col) && chessBoard[row - 1, col - 2] == 'K')
            {
                attacks++;
            }

            if(InRange(chessBoard, "BL", row, col) && chessBoard[row + 1, col - 2] == 'K')
            {
                attacks++;
            }

            if(InRange(chessBoard, "TR", row, col) && chessBoard[row - 1, col + 2] == 'K')
            {
                attacks++;
            }

            if(InRange(chessBoard, "BR", row, col) && chessBoard[row + 1, col + 2] == 'K')
            {
                attacks++;
            }

            if(InRange( chessBoard, "DL", row, col) && chessBoard[row + 2, col - 1] == 'K')
            {
                attacks++;
            }

            if(InRange(chessBoard, "DR", row, col) && chessBoard[row + 2, col + 1] == 'K')
            {
                attacks++;
            }

            return attacks;
        }

        private static bool InRange(char[,] chessBoard, string posCheck,int row,int col)
        {

            var rowLen = chessBoard.GetLength(0) - 1;
            var colLen = chessBoard.GetLength(1) - 1;

            switch (posCheck)
            {
                case "UL":
                    if(row - 2 < 0 || col - 1 < 0)
                    {
                        return false;
                    }
                    break;
                case "UR":
                    if(row - 2 < 0 || col + 1 > colLen)
                    {
                        return false;
                    }
                    break;
                case "TL":
                    if(row - 1 < 0 || col - 2 < 0)
                    {
                        return false;
                    }
                    break;
                case "BL":
                    if(row + 1 > rowLen || col - 2 < 0)
                    {
                        return false;
                    }
                    break;
                case "TR": 
                    if(row - 1 < 0 || col + 2 > colLen)
                    {
                        return false;
                    }
                    break;
                case "BR":
                    if(row + 1 > rowLen || col + 2 > colLen)
                    {
                        return false;
                    }
                    break;
                case "DL":
                    if(row + 2 > rowLen || col - 1 < 0)
                    {
                        return false;
                    }
                    break;
                case "DR":
                    if(row + 2 > rowLen || col + 1 > colLen)
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }

        private static char[,] WriteArray(int height, int width)
        {
            var matrix = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                var input = Console.ReadLine();

                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            return matrix;
        }
    }
}
