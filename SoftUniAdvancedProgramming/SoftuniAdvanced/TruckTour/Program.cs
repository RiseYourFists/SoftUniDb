using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckTour
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pumps = int.Parse(Console.ReadLine());
            var pumpsQueue = new Queue<string>();

            for (int i = 0; i < pumps; i++)
            {
                pumpsQueue.Enqueue(Console.ReadLine());
            }

            for (int i = 0; i < pumps; i++)
            {
                if (HasEnoughFuel(pumpsQueue))
                {
                    Console.WriteLine(i);
                    break;
                }

                FlipOrder(pumpsQueue);
            }

        }

        private static void FlipOrder(Queue<string> pumpsQueue)
        {
            pumpsQueue.Enqueue(pumpsQueue.Dequeue());
        }

        private static bool HasEnoughFuel(Queue<string> pumpsQueue)
        {
            var fuelTank = 0;
            foreach (var item in pumpsQueue)
            {
                var tokens = item.Split().Select(int.Parse).ToArray();
                var fuel = tokens[0];
                var distance = tokens[1];

                fuelTank += fuel;

                if (fuelTank - distance <= 0)
                {
                    return false;
                }

                fuelTank -= distance;
            }
            return true;
        }
    }
}
