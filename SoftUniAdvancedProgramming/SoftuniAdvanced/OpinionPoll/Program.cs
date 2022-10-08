using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();
            var inputs = int.Parse(Console.ReadLine());
            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(' ');
                var name = input[0];
                var age = int.Parse(input[1]);

                people.Add(new Person(name, age));
            }

            var sorted = SortPeople(people, (x => x.Age > 30));
            foreach (var item in sorted)
            {
                Console.WriteLine($"{item.Name} - {item.Age}");
            }
        }
        
        public static List<Person> SortPeople(List<Person> collection, Predicate<Person> predicate)
        {
            var newCollection = collection.Where(x => predicate(x));
            return newCollection.OrderBy(x => x.Name).ToList();
        }
    }
}
