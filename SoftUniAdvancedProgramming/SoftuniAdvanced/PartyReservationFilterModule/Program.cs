using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PartyReservationFilterModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reservationNames = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var filters = new List<KeyValuePair<Func<string, string, bool>, string>>();

            string[] tokens = GetTokens();
            while (tokens[0] != "Print")
            {
                Func<string, string, bool> filter = GetType(tokens[1]);
                var obj = new KeyValuePair<Func<string, string, bool>, string>(filter, tokens[2]);

                switch (tokens[0])
                {
                    case "Add filter":
                        filters.Add(obj);
                        break;
                    case "Remove filter":
                        filters.Remove(obj);
                        break;
                }

                tokens = GetTokens();
            }
            var list = FilterList(reservationNames, filters);
            Console.WriteLine(string.Join(' ', list));
        }

        private static List<string> FilterList(List<string> reservationNames, List<KeyValuePair<Func<string, string, bool>, string>> filters)
        {
            var list = new List<string>(reservationNames);

            foreach (var filter in filters)
            {
                foreach (var name in reservationNames)
                {
                    if(!filter.Key(name, filter.Value))
                    {
                        continue;
                    }
                    list.Remove(name);
                }
            }
            return list;
        }

        private static Func<string, string, bool> GetType(string type)
        {
            switch (type)
            {
                case "Starts with":
                    return ((x, y) => (Regex.IsMatch(x, $"\\b{y}")));
                case "Ends with":
                    return ((x, y) => (Regex.IsMatch(x, $"{y}\\b")));
                case "Contains":
                    return ((x, y) => (Regex.IsMatch(x, $"{y}")));
                default:
                    return ((x, y) => (x.Length == int.Parse(y)));
            }
        }

        private static string[] GetTokens()
        {
            return Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

