using System;
using System.Collections.Generic;

namespace ParkingLot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parkingLot = new HashSet<string>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var tokens = input.Split(", ");

                if (tokens[0] == "IN")
                {
                    parkingLot.Add(tokens[1]);
                    continue;
                }

                parkingLot.Remove(tokens[1]);
            }

            if(parkingLot.Count == 0)
            {
                Console.WriteLine("Parking Lot is Empty");
                return;
            }

            Console.WriteLine(string.Join(Environment.NewLine, parkingLot));
        }
    }
}
