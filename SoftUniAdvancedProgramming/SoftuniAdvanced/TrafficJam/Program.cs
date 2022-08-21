using System;
using System.Collections.Generic;

namespace TrafficJam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var passageCount = int.Parse(Console.ReadLine());
            var carsPassed = 0;
            var carsQueue = new Queue<string>();

            while (true)
            {
                var input = Console.ReadLine();

                if(input == "end")
                {
                    Console.WriteLine($"{carsPassed} cars passed the crossroads.");
                    break;
                }

                if(input == "green")
                {
                    var len = passageCount;

                    if(carsQueue.Count < passageCount)
                    {
                        len = carsQueue.Count;
                    }

                    for (int i = 0; i < len; i++)
                    {
                        Console.WriteLine($"{carsQueue.Dequeue()} passed!");
                    }

                    carsPassed += len;
                    continue;
                }

                carsQueue.Enqueue(input);
            }
        }
    }
}
