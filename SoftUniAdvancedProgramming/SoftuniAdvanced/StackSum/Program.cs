using System;
using System.Collections.Generic;
using System.Linq;

namespace StackSum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var initialInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var stack = new Stack<int>();

            foreach (var item in initialInput)
            {
                stack.Push(item);
            }

            while (true)
            {
                var input = Console.ReadLine().ToLower();

                if(input == "end")
                {
                    break;
                }

                var token = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var action = token[0];
                if(action == "add")
                {
                    var values = GetValues(token);
                    StackPushAll(values, stack);
                    continue;
                }

                if(action == "remove")
                {
                    var value = int.Parse(token[1]);
                    StackRemove(stack, value);
                }

            }

            StackSum(stack);
        }

        private static void StackPushAll(List<int> values, Stack<int> stack)
        {
            foreach (var item in values)
            {
                stack.Push(item);
            }
        }

        private static List<int> GetValues(string[] token)
        {
            var result = new List<int>();

            for (int i = 1; i < token.Length; i++)
            {
                result.Add(int.Parse(token[i]));
            }

            return result;
        }

        private static void StackSum(Stack<int> stack)
        {
            var totalSum = 0;

            foreach(var item in stack)
            {
                totalSum += item;
            }
            Console.WriteLine($"Sum: {totalSum}");
        }

        private static void StackRemove(Stack<int> stack, int elements)
        {
            if(stack.Count < elements)
            {
                return;
            }

            for (int i = 0; i < elements; i++)
            {
                stack.Pop();
            }
        }
    }
}
