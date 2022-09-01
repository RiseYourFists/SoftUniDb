using System;
using System.Linq;

namespace SymbolInMatrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());

            var matrix = new char[size, size];

            for (int i = 0; i < size; i++)
            {
                var input = Console.ReadLine();

                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            var symbol = Console.ReadLine();

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var ch = symbol.ToCharArray();
                    if (matrix[row,col] == ch[0])
                    {
                        Console.WriteLine($"({row}, {col})");
                        return;
                    }
                }
            }
            
            Console.WriteLine($"{symbol} does not occur in the matrix");
        }
    }
}
