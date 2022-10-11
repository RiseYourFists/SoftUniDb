using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class Trainer
    {
        public Trainer(string name)
        {
            Name = name;
            Badges = 0;
            Pokemons = new List<Pokemon>();
        }

        public string Name { get; set; }
        public int Badges { get; set; }
        public List<Pokemon> Pokemons { get; set; }

        public void AddPokemon(Pokemon pokemon)
        {
            this.Pokemons.Add(pokemon);
        }
        public void CheckElements(string element)
        {
            if (this.Pokemons.Any(x => x.Element == element))
            {
                Badges++;
                return;
            }

            this.Pokemons.ForEach(x => x.Health -= 10);
            CheckHealth();
        }

        private void CheckHealth()
        {
            var newPokemonList = new List<Pokemon>(this.Pokemons);
            foreach (var item in this.Pokemons)
            {
                if (item.Health <= 0)
                {
                    newPokemonList.Remove(item);
                }
            }
            this.Pokemons = new List<Pokemon>(newPokemonList);
        }
    }
}

