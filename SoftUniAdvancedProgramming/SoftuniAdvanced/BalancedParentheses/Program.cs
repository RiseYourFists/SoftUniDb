using System;
using System.Collections.Generic;

namespace BalancedParentheses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var charStack = new Stack<char>();
            var input = Console.ReadLine();

            for (int i = 0; i < input.Length; i++)
            {
                var ch = input[i];

                if (ch == '{' || ch == '(' || ch == '[')
                {
                    charStack.Push(ch);
                    continue;
                }

                if (charStack.Count == 0)
                {
                    Console.WriteLine("NO");
                    return;
                }
                char opening = ' ';

                switch (ch)
                {
                    case ')':
                        opening = '(';
                        break;
                    case ']':
                        opening = '[';
                        break;
                    case '}':
                        opening = '{';
                        break;
                }

                if (opening == charStack.Pop())
                {
                    continue;
                }

                Console.WriteLine("NO");
                return;
            }

            Console.WriteLine("YES");
        }
    }
}
