using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SongsQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var songList = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            var songsQueue = new Queue<string>(songList);

            while(songsQueue.Count > 0)
            {
                var input = Console.ReadLine();
                var tokens = input.Split();
                var action = tokens[0];

                switch (action)
                {
                    case "Play":
                        PlaySong(songsQueue);
                        break;
                    case "Add":

                        var pattern = @"Add ([A-z ]+)";
                        var songMatch = Regex.Match(input, pattern);
                        var song = songMatch.Groups[1].Value;

                        AddSong(songsQueue, song);
                        break;
                    case "Show":
                        ShowSongs(songsQueue);
                        break;
                }
            }
            Console.WriteLine("No more songs!");
        }

        private static void ShowSongs(Queue<string> songsQueue)
        {
            
            Console.WriteLine(string.Join(", ", songsQueue));
        }

        private static void AddSong(Queue<string> songsQueue, string song)
        {
            if(songsQueue.Contains(song))
            {
                Console.WriteLine($"{song} is already contained!");
                return;
            }
            songsQueue.Enqueue(song);
        }

        private static void PlaySong(Queue<string> songsQueue)
        {
            songsQueue.Dequeue();
        }
    }
}
