using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var trainers = new Dictionary<string, Trainer>();
            GetTrainers(trainers);

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                foreach (var item in trainers)
                {
                    item.Value.CheckElements(input);
                }
            }

            var result = trainers.OrderByDescending(x => x.Value.Badges).ToList();
            result.ForEach(x => Console.WriteLine($"{x.Key} {x.Value.Badges} {x.Value.Pokemons.Count}"));

        }

        private static void GetTrainers(Dictionary<string, Trainer> trainers)
        {
            string input;
            while ((input = Console.ReadLine()) != "Tournament")
            {
                var tokens = input.Split();
                var trainer = tokens[0];
                var pokemon = tokens[1];
                var element = tokens[2];
                var health = int.Parse(tokens[3]);

                if (!trainers.ContainsKey(trainer))
                {
                    trainers.Add(trainer, new Trainer(trainer));
                }

                trainers[trainer].AddPokemon(new Pokemon(pokemon, element, health));
            }
        }
    }
}
