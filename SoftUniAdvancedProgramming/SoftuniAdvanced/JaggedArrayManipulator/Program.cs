using System;
using System.Linq;
using System.Text;

namespace JaggedArrayManipulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var jaggedArray = new double[rows][];

            jaggedArray = WriteArray(rows);

            ScanArray(jaggedArray);

            while (true)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (input[0] == "End")
                {
                    break;
                }

                var action = input[0];
                var row = int.Parse(input[1]);
                var col = int.Parse(input[2]);
                var value = int.Parse(input[3]);

                switch (action)
                {
                    case "Add":
                        if(IndexValid(jaggedArray, row, col))
                        {
                            jaggedArray[row][col] += value;
                        }
                        break;
                    case "Subtract":
                        if(IndexValid(jaggedArray, row, col))
                        {
                            jaggedArray[row][col] -= value;
                        }
                        break;

                }

            }

            PrintJaggedArray(jaggedArray);
        }

        private static void PrintJaggedArray(double[][] jaggedArray)
        {
            var len = jaggedArray.GetLength(0);
            var sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    sb.Append(jaggedArray[i][j] + " ");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb);
        }

        private static bool IndexValid(double[][] jaggedArray, int row, int col)
        {
            var rowLen = jaggedArray.GetLength(0) - 1;
            if(row < 0 || row > rowLen)
            {
                return false;
            }

            var colLen = jaggedArray[row].Length - 1;
            if(col < 0 || col > colLen)
            {
                return false;
            }

            return true;
        }

        private static void ScanArray(double[][] jaggedArray)
        {
            for (int i = 0; i < jaggedArray.GetLength(0) - 1; i++)
            {
                if (jaggedArray[i].Length == jaggedArray[i + 1].Length)
                {
                    MultiplyScannedElements(jaggedArray, i);
                    continue;
                }

                DivideScannedElements(jaggedArray, i);
            }
        }

        private static void DivideScannedElements(double[][] jaggedArray, int pos)
        {
            for (int i = pos; i <= pos + 1; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    jaggedArray[i][j] /= 2;
                }
            }
        }

        private static void MultiplyScannedElements(double[][] jaggedArray, int pos)
        {
            for (int i = pos; i <= pos + 1; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    jaggedArray[i][j] *= 2;
                }
            }
        }

        private static double[][] WriteArray(int rows)
        {
            var tempArray = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
                tempArray[i] = input;
            }

            return tempArray;
        }
    }
}
