using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());
            var family = new Family();

            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split();
                var name = input[0];
                var age = int.Parse(input[1]);

                family.AddMember(name, age);
            }

            var oldest = family.GetOldest();

            if (oldest.Name == null || oldest.Age == -1) return;

            Console.WriteLine($"{oldest.Name} {oldest.Age}");
        }
    }
}
