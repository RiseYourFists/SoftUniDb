using System;
using System.Collections.Generic;

namespace UniqueUsernames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());
            var uniqueUsers = new HashSet<string>();

            for (int i = 0; i < inputs; i++)
            {
                uniqueUsers.Add(Console.ReadLine());
            }

            Console.WriteLine(string.Join(Environment.NewLine, uniqueUsers));
        }
    }
}
