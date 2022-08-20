using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var stack = new Stack<char>();
            foreach (var item in input)
            {
                stack.Push(item);
            }

            var result = new StringBuilder();

            while (stack.Count > 0)
            {
                result.Append(stack.Pop());
            }

            Console.WriteLine(result.ToString());
        }
    }
}
