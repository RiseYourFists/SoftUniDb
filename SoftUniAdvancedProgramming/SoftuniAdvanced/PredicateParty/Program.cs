using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PredicateParty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var initialNames = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            string input;

            while ((input = Console.ReadLine()) != "Party!")
            {
                var tokens = input.Split();

                Func<string, string, bool> quickChange = GetAction(tokens[1]);


                if (tokens[0] == "Double")
                {
                    initialNames = DoubleNames(initialNames, quickChange, tokens[2]);
                    continue;
                }

                initialNames = RemoveNames(initialNames, quickChange, tokens[2]);
            }

            if (initialNames.Count > 0)
                Console.WriteLine($"{string.Join(", ", initialNames)} are going to the party!");
            else
                Console.WriteLine("Nobody is going to the party!");
        }

        private static List<string> RemoveNames(List<string> initialNames, Func<string, string, bool> quickChange, string param)
        {
            var holder = new List<string>(initialNames);
            foreach (var name in initialNames)
            {
                if (quickChange(name, param))
                {
                    holder.Remove(name);
                }
            }
            return holder;
        }

        private static List<string> DoubleNames(List<string> initialNames, Func<string, string, bool> quickChange, string param)
        {
            var holder = new List<string>(initialNames);
            foreach (var name in initialNames)
            {
                if (quickChange(name, param))
                {
                    var pos = initialNames.IndexOf(name);
                    holder.Insert(pos, name);
                }
            }
            return holder;
        }

        private static Func<string, string, bool> GetAction(string input)
        {
            switch (input)
            {
                case "StartsWith":
                    return CheckStart;
                case "EndsWith":
                    return CheckEnd;
                default:
                    return CheckLenght;
            }
        }

        private static bool CheckLenght(string arg, string param)
        {
            var len = int.Parse(param);
            return arg.Length == len;
        }

        private static bool CheckEnd(string arg, string param)
        {
            return Regex.IsMatch(arg, $"{param}\\b");
        }

        private static bool CheckStart(string arg, string param)
        {
            return Regex.IsMatch(arg, $"\\b{param}");
        }
    }
}
