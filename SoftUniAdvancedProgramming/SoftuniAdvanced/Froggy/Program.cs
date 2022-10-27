using System;
using System.Linq;
using System.Text;

namespace Froggy
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rocks = Console.ReadLine().Split(", ").Select(int.Parse).ToList();
            var frog = new Frog<int>(rocks);

            var sb = new StringBuilder();

            foreach (var rock in frog)
            {
                sb.Append($"{rock}, ");
            }

            sb.Remove(sb.Length - 2, 2);
            Console.WriteLine(sb);
        }
    }
}
