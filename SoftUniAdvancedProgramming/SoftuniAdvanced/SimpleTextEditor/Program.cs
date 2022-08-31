using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleTextEditor
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
             *· 1 someString - appends someString to the end of the text
             *· 2 count - erases the last count elements from the text
             *· 3 index - returns the element at position index from the text
             *· 4 - undoes the last not undone command of type 1 / 2 and returns the text to the state before that operation
             */

            var outputLog = new Stack<string>();
            var currString = new StringBuilder();
            var operations = int.Parse(Console.ReadLine());

            for (int i = 0; i < operations; i++)
            {
                var tokens = GetData(Console.ReadLine());

                var action = tokens[0];

                switch (action)
                {
                    case "1":
                        outputLog.Push(currString.ToString());
                        currString.Append(tokens[1]);
                        break;
                    case "2":
                        outputLog.Push(currString.ToString());
                        var count = int.Parse(tokens[1]);
                        currString.Remove(currString.Length - count, count);
                        break;
                    case "3":
                        var index = int.Parse(tokens[1]);
                        Console.WriteLine(currString[index - 1]);
                        break;
                    case "4":
                        currString.Clear();
                        currString.Append(outputLog.Pop());
                        break;
                }
            }
        }

        private static List<string> GetData(string input)
        {
            var result = new List<string>();

            var pattern = @"1 ([A-z ]+)";
            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                result.Add("1");
                result.Add(match.Groups[1].Value);
                return result;
            }

            result = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            return result;
        }
    }
}
