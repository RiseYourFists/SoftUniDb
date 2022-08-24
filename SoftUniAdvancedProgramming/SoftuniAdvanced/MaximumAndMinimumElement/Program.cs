using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumAndMinimumElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             1 x – Push the element x into the stack.
             2 – Delete the element present at the top of the stack.
             3 – Print the maximum element in the stack.
             4 – Print the minimum element in the stack
            */

            var queries = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();

            for (int i = 0; i < queries; i++)
            {
                var input = Console.ReadLine();
                int[] tokens = input.Split(' ').Select(int.Parse).ToArray();

                var action = tokens[0];

                switch (action)
                {
                    case 1:
                        stack.Push(tokens[1]);
                        break;

                    case 2:
                        stack.Pop();
                        break;

                    case 3:

                        if(stack.Count == 0)
                        {
                            continue;
                        }

                        Console.WriteLine(stack.Max()); ;
                        break;
                    case 4:

                        if(stack.Count == 0)
                        {
                            continue;
                        }

                        Console.WriteLine(stack.Min());
                        break;
                }

            }

            Console.WriteLine(string.Join(", ", stack));
        }
    }
}
