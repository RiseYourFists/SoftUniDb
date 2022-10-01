using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace FilterByAge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var times = int.Parse(Console.ReadLine());
            var users = FillList(times);
            var state = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());

            Func<string, int, Dictionary<string, int>, Dictionary<string, int>> narrowDown = FilterList;

            var result = narrowDown(state, age, users);
            var pattern = Console.ReadLine();
            Action<Dictionary<string, int>> printPattern = GetPatternType(pattern);

            printPattern(result);
        }

        private static Action<Dictionary<string, int>> GetPatternType(string pattern)
        {
            switch(pattern)
            {
                case "name":
                    return PrintName;
                case "age":
                    return PrintAge;
                default:
                    return PrintNameAndAge;
            }
        }

        private static void PrintNameAndAge(Dictionary<string, int> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
        }

        private static void PrintAge(Dictionary<string, int> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item.Value);
            }
        }

        private static void PrintName(Dictionary<string, int> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item.Key);
            }
        }

        private static Dictionary<string, int> FilterList(string arg1, int arg2, Dictionary<string, int> collection)
        {
            var result = new Dictionary<string, int>();
            foreach (var item in collection)
            {
                if (arg1 == "younger")
                {
                    if(item.Value <= arg2)
                    {
                        result.Add(item.Key, item.Value);
                    }
                }

                if (arg1 == "older")
                {
                    if(item.Value >= arg2)
                    {
                        result.Add(item.Key, item.Value);
                    }
                }
            }
            return result;
        }

        private static Dictionary<string, int> FillList(int users)
        {
            var userList = new Dictionary<string, int>();

            for (int i = 0; i < users; i++)
            {
                var input = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                userList.Add(input[0], int.Parse(input[1]));
            }

            return userList;
        }
    }
}
