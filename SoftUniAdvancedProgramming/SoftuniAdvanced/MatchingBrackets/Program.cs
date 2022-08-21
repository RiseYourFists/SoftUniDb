using System;
using System.Collections.Generic;

namespace MatchingBrackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var expression = Console.ReadLine();
            var startingBracket = new Stack<int>();

            for (int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == '(')
                {
                    startingBracket.Push(i);
                }

                if(expression[i] == ')')
                {
                    var startPos = startingBracket.Pop();
                    var len =  i + 1 - startPos;
                    Console.WriteLine(expression.Substring(startPos, len));
                }
            }
        }
    }
}
