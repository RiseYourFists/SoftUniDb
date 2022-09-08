using System;
using System.Text;

namespace PascalTriangleDemo
{
    class Program
    {
        public static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            var jaggedArray = new long[rows][];
            long val = 1;

            for (int i = 0; i < rows; i++)
            {
                var tempArray = new long[i + 1];

                for (int j = 0; j <= i; j++)
                {
                    if (j == 0 || i == 0)
                    {
                        val = 1;
                    }
                    else
                    {
                        val = val * (i - j + 1) / j;
                    }

                    tempArray[j] = val;
                }

                jaggedArray[i] = tempArray;
            }

            var result = new StringBuilder();

            for (int i = 0; i < jaggedArray.GetLength(0); i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    result.Append(jaggedArray[i][j] + " ");
                }
                result.Append(Environment.NewLine);
            }

            Console.WriteLine(result);
        }
    }
}