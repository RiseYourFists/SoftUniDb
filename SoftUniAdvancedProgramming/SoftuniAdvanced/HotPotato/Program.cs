using System;
using System.Collections.Generic;
using System.Linq;

namespace HotPotato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var players = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var turns = int.Parse(Console.ReadLine());
            var quitOrder = new Queue<string>();
            var player = 0;
            while (players.Count > 0)
            {
                player = GetQuitPos(player, players, turns);
                quitOrder.Enqueue(players[player]);
                players.RemoveAt(player);
            }

            while (quitOrder.Count > 1)
            {
                Console.WriteLine($"Removed {quitOrder.Dequeue()}");
            }

            Console.WriteLine($"Last is {quitOrder.Dequeue()}");
        }

        private static int GetQuitPos(int player, List<string> players, int turns)
        {
            var counter = player - 1;

            for (int i = 0; i < turns; i++)
            {
                counter++;

                if(counter >= players.Count)
                {
                    counter = 0;
                }
            }

            return counter;
        }
    }
}
