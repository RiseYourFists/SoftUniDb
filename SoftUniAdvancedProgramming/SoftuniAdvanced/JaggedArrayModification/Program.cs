using System;
using System.Linq;

namespace JaggedArrayModification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var jaggedArray = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();

                jaggedArray[i] = input;
            }

            while (true)
            {
                var tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (tokens[0] == "END")
                {
                    break;
                }

                var action = tokens[0];
                var row = int.Parse(tokens[1]);
                var col = int.Parse(tokens[2]);
                var value = int.Parse(tokens[3]);

                if (row >= rows || row < 0)
                {
                    PrintError();
                    continue;
                }

                if (col >= jaggedArray[row].Length || col < 0)
                {
                    PrintError();
                    continue;
                }

                switch (action)
                {
                    case "Add":
                        jaggedArray[row][col] += value;
                        break;
                    case "Subtract":
                        jaggedArray[row][col] -= value;
                        break;
                }

            }

            for (int i = 0; i < jaggedArray.GetLength(0); i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write($"{jaggedArray[i][j]} ");
                }
                Console.WriteLine();
            }

        }

        private static void PrintError()
        {
            Console.WriteLine("Invalid coordinates");
        }
    }
}
