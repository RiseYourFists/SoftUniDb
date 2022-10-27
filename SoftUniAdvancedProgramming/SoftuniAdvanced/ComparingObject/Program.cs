using System;
using System.Collections.Generic;

namespace ComparingObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<Person> people = new List<Person>();

            while ((input = Console.ReadLine()) != "END")
            {
                var personToken = input.Split();

                people.Add(new Person(personToken[0], int.Parse(personToken[1]), personToken[2]));
            }

            var pos = int.Parse(Console.ReadLine()) - 1;
            var matches = 1;
            var notEqualPeople = 0;

            for (int i = 0; i < people.Count; i++)
            {
                if (i == pos) continue;

                if (people[pos].CompareTo(people[i]) == 0)
                {
                    matches++;
                    continue;
                }

                notEqualPeople++;
            }
            if(matches > 1)
            Console.WriteLine($"{matches} {notEqualPeople} {people.Count}");
            else
                Console.WriteLine("No matches");
        }
    }
}
