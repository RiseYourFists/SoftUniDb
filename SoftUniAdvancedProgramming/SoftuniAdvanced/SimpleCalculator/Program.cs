using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = 0;
            var expression = Console.ReadLine().Split().Reverse();
            var stack = new Stack<string>();

            foreach (var item in expression)
            {
                stack.Push(item);
            }

            result = int.Parse(stack.Pop());

            while (stack.Count > 0)
            {
                var symbol = stack.Pop();
                var num = int.Parse(stack.Pop().ToString());

                if(symbol == "+")
                {
                    result += num;
                    continue;
                }

                result -= num;
            }

            Console.WriteLine(result);
        }
    }
}
