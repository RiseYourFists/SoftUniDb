using System;
using System.Linq;

namespace AddVAT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var VAT = 0.2;
            Func<double, double> addVAT = x => x + x * VAT;
            var input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => addVAT(double.Parse(x)))
                .ToList();

            input.ForEach(x => Console.WriteLine($"{x:f2}"));
        }
    }
}
