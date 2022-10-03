using System;

namespace TriFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var targetScore = int.Parse(Console.ReadLine());
            Func<string, int> getCharSum = GetSum;
            Func<int, int, bool> find1stScore = (x, y) => x >= y;

            var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in input)
            {
                var score = getCharSum(item);

                if (find1stScore(score, targetScore))
                {
                    Console.WriteLine(item);
                    return;
                }
            }
        }

        private static int GetSum(string arg)
        {
            var score = 0;
            foreach (var ch in arg)
            {
                score += ch;
            }

            return score;
        }
    }
}
