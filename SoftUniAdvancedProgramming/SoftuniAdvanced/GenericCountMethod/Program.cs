using GenericBoxOfString;
using System;

namespace GenericCountMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var newBox = new Box<string>();

            var inputs = int.Parse(Console.ReadLine());
            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine();
                newBox.Add(input);
            }

            Console.WriteLine(newBox.CompareCount(Console.ReadLine()));
        }
    }
}
