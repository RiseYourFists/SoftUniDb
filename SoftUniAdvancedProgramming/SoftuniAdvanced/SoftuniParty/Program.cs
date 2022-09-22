using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SoftuniParty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reservationList = new HashSet<string>();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "PARTY")
                {
                    break;
                }

                reservationList.Add(input);
            }

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                if (reservationList.Contains(input))
                {
                    reservationList.Remove(input);
                }
            }

            var vip = reservationList.Where(x => char.IsDigit(x[0]) == true).ToArray();
            var nonVip = reservationList.Where(x => char.IsDigit(x[0]) != true).ToArray();

            Console.WriteLine(reservationList.Count);
            if (vip.Length > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, vip));
            }

            if (nonVip.Length > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, nonVip));
            }
        }
    }
}
