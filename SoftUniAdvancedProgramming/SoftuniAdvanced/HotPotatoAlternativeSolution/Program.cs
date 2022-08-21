using System;
using System.Collections.Generic;

namespace HotPotatoAlternativeSolution
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var players = new Queue<string>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            var turns = int.Parse(Console.ReadLine());

            while (players.Count > 1)
            {
                for (int i = 0; i < turns - 1; i++)
                {
                    players.Enqueue(players.Dequeue());
                }

                Console.WriteLine($"Removed {players.Dequeue()}");
            }

            Console.WriteLine($"Last is {players.Dequeue()}");
        }
    }
}
