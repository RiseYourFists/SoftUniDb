using System;
using System.Linq;

namespace RecursiveArraySum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var result = ArraySum(array, 0);

            Console.WriteLine(result);
        }

        public static int ArraySum(int[] array, int index)
        {
            if (index >= array.Length)
            {
                return 0;
            }

            return array[index] + ArraySum(array, index + 1);
        }
    }

}
