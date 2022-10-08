using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());
            var cars = new List<Car>();
            GetCars(inputs, cars);

            var type = Console.ReadLine();
            var result = new List<Car>();
            if (type == "flammable")
            {
                Predicate<Car> flammable = x => x.Engine.Power > 250 && x.Cargo.Type == "flammable";
                result = cars.Where(x => flammable(x)).ToList();
            }
            else
            {
                Predicate<Car> fragile = x => x.Tires.Any(p => p.Pressure < 1) && x.Cargo.Type == "fragile";
                result = cars.Where(x => fragile(x)).ToList();
            }

            foreach (var item in result)
            {
                Console.WriteLine(item.Model);
            }
        }

        private static void GetCars(int inputs, List<Car> cars)
        {
            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split();
                var model = input[0];

                var engineSpeed = int.Parse(input[1]);
                var enginePower = int.Parse(input[2]);
                var engine = new Engine(engineSpeed, enginePower);

                var cargoWeight = int.Parse(input[3]);
                var cargoType = input[4];
                var cargo = new Cargo(cargoWeight, cargoType);

                var tirePack = new Tire[4];
                var count = 0;

                for (int j = 5; j < input.Length; j += 2)
                {
                    var pressure = double.Parse(input[j]);
                    var year = int.Parse(input[j + 1]);
                    tirePack[count] = new Tire(pressure, year);
                    count++;
                }
                cars.Add(new Car(model, engine, cargo, tirePack));
            }
        }
    }
}
