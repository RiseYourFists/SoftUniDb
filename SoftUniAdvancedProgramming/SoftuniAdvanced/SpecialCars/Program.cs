using System;
using System.Collections.Generic;
using System.Linq;

namespace CarManufacturer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var tireList = new List<Tire[]>(GetTires());

            var engineList = new List<Engine>();
            GetEngines(engineList);

            var carList = new List<Car>();
            GetCars(tireList, engineList, carList);

            if(!carList.Any())
            {
                return;
            }

            var specialCars = carList.Where(x => x.Year >= 2017 
                                              && x.Engine.HorsePower > 330 
                                              && x.GetPressure() >= 9 
                                              && x.GetPressure() <= 10)
                                              .ToList();

            foreach (var car in specialCars)
            {
                car.Drive(20);
            }

            foreach (var car in specialCars)
            {
                Console.WriteLine($"Make: {car.Make}" +
                                  $"\nModel: {car.Model}" +
                                  $"\nYear: {car.Year}" +
                                  $"\nHorsePowers: {car.Engine.HorsePower}" +
                                  $"\nFuelQuantity: {car.FuelQuantity}");
            }
        }

        public static List<Tire[]> GetTires()
        {
            string input;
            var tires = new List<Tire[]>();

            while ((input = Console.ReadLine()) != "No more tires")
            {
                var count = 0;
                var tirePack = input.Split();
                var currTire = new Tire[4];

                for (int i = 0; i < tirePack.Length; i += 2)
                {
                    var year = int.Parse(tirePack[i]);
                    var pressure = double.Parse(tirePack[i + 1]);
                    var tire = new Tire(year, pressure);
                    currTire[count] = tire;
                    count++;
                }

                tires.Add(currTire);
            }

            return tires;
        }

        public static void GetCars(List<Tire[]> tireList, List<Engine> engineList, List<Car> carList)
        {
            string input;
            while ((input = Console.ReadLine()) != "Show special")
            {
                var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var make = tokens[0];
                var model = tokens[1];
                var year = int.Parse(tokens[2]);
                var fuelQuantity = double.Parse(tokens[3]);
                var fuelConsumption = double.Parse(tokens[4]);
                var engineIndex = int.Parse(tokens[5]);
                var tiresIndex = int.Parse(tokens[6]);
                var engine = engineList[engineIndex];
                var tirePack = tireList[tiresIndex];

                carList.Add(new Car(make, model, year, fuelQuantity, fuelConsumption, engine, tirePack));
            }
        }

        public static void GetEngines(List<Engine> engineList)
        {
            string input;
            while ((input = Console.ReadLine()) != "Engines done")
            {
                var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var horsePower = int.Parse(tokens[0]);
                var cubicCapacity = double.Parse(tokens[1]);
                engineList.Add(new Engine(horsePower, cubicCapacity));
            }
        }
    }
}
