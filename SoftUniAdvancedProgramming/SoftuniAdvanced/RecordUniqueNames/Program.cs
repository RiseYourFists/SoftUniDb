using System;
using System.Collections.Generic;

namespace RecordUniqueNames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());

            var uniqueNames = new HashSet<string>();

            for (int i = 0; i < inputs; i++)
            {
                uniqueNames.Add(Console.ReadLine());
            }

            Console.WriteLine(string.Join(Environment.NewLine, uniqueNames));
        }
    }
}
