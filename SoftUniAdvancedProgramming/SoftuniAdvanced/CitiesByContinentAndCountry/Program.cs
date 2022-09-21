using System;
using System.Collections.Generic;

namespace CitiesByContinentAndCountry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());
            var continents = new Dictionary<string, Dictionary<string, List<string>>>();

            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var continent = input[0];
                var country = input[1];
                var city = input[2];

                if(continents.ContainsKey(continent))
                {
                    if (continents[continent].ContainsKey(country))
                    {
                        continents[continent][country].Add(city);
                        continue;
                    }

                    continents[continent].Add(country, new List<string> { city });
                    continue;
                }

                continents.Add(continent, new Dictionary<string, List<string>> { });
                continents[continent].Add(country, new List<string> { city });
            }

            foreach (var continent in continents)
            {
                Console.WriteLine($"{continent.Key}:");
                foreach (var country in continent.Value)
                {
                    Console.WriteLine($"{country.Key} -> {string.Join(", ", country.Value)}");
                }
            }
        }
    }
}
