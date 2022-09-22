using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Wardrobe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());

            var wardrobe = new Dictionary<string, Dictionary<string, int>>();
            FillWardrobe(inputs, wardrobe);

            var searchInfo = Console.ReadLine().Split();
            var clothColor = searchInfo[0];
            var clothType = searchInfo[1];

            PrintWardrobe(wardrobe, clothColor, clothType);
        }

        private static void PrintWardrobe(Dictionary<string, Dictionary<string, int>> wardrobe, string clothColor, string clothType)
        {
            foreach (var color in wardrobe)
            {
                Console.WriteLine($"{color.Key} clothes:");

                foreach (var cloth in color.Value)
                {
                    Console.Write($"* {cloth.Key} - {cloth.Value}");

                    if (clothColor == color.Key && clothType == cloth.Key)
                    {
                        Console.Write(" (found!)");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static void FillWardrobe(int inputs, Dictionary<string, Dictionary<string, int>> wardrobe)
        {

            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(" -> ");

                var color = input[0];
                var clothes = input[1].Split(",");

                if (!wardrobe.ContainsKey(color))
                {
                    wardrobe.Add(color, new Dictionary<string, int>());
                }

                foreach (var item in clothes)
                {
                    if (!wardrobe[color].ContainsKey(item))
                    {
                        wardrobe[color].Add(item, 0);
                    }

                    wardrobe[color][item]++;
                }
                
            }
        }
    }
}
