using System;
using System.Linq;

namespace CustomComparator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Func<int, int, int> customComparer = (x, y) =>
            {
                return (x % 2 == 0 && y % 2 != 0)
                ? -1 : (x % 2 != 0 && y % 2 == 0)
                ? 1 : x > y
                ? 1 : x < y
                ? -1 : 0; 
            };

            Array.Sort(array, (x,y) => customComparer(x,y));

            Console.WriteLine(string.Join(' ', array));
        }
    }
}
