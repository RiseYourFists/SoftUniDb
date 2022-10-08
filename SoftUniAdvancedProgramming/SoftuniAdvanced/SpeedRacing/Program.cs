using System;
using System.Collections.Generic;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var cars = new Dictionary<string, Car>();

            var inputs = int.Parse(Console.ReadLine());
            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split();
                var model = input[0];
                var fuelAmount = double.Parse(input[1]);
                var fuelConsumption = double.Parse(input[2]);

                if (!cars.ContainsKey(model))
                {
                    cars.Add(model, new Car(model, fuelAmount, fuelConsumption));
                }
            }

            string driveInfo;
            while ((driveInfo = Console.ReadLine()) != "End")
            {
                var tokens = driveInfo.Split();
                var model = tokens[1];
                var distance = double.Parse(tokens[2]);

                if (cars.ContainsKey(model))
                {
                    cars[model].Drive(distance);
                }
            }

            foreach (var item in cars)
            {
                Console.WriteLine(item.Value.GetStats());
            }
        }
    }
}