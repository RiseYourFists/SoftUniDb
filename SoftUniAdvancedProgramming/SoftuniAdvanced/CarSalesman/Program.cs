using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var engineInline = new List<Engine>();
            var carList = new List<Car>();

            int inputs = int.Parse(Console.ReadLine());
            GetEngines(engineInline, inputs);

            inputs = int.Parse(Console.ReadLine());
            GetCars(engineInline, carList, inputs);

            Console.WriteLine(PrintCars(carList));
        }

        private static void GetCars(List<Engine> engineInline, List<Car> carList, int inputs)
        {
            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var len = input.Length;
                var model = input[0];
                var enModel = input[1];
                var engine = engineInline.FirstOrDefault(x => x.Model == enModel);
                var car = new Car()
                {
                    Model = model,
                    Engine = engine
                };

                if(len == 4)
                {
                    car.Weight = int.Parse(input[2]);
                    car.Color = input[3];
                }

                if(len == 3)
                {
                    if (int.TryParse(input[2], out var weight))
                    {
                        car.Weight = weight;
                    }
                    else
                    {
                        car.Color = input[2];
                    }
                }
                carList.Add(car);
            }
        }

        private static void GetEngines(List<Engine> engineInline, int inputs)
        {
            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var len = input.Length;
                var model = input[0];
                var power = int.Parse(input[1]);
                var engine = new Engine()
                {
                    Model = model,
                    Power = power
                };

                if (len == 4)
                {
                    engine.Displacement = int.Parse(input[2]);
                    engine.Efficiency = input[3];
                }

                if (len == 3)
                {
                    if (int.TryParse(input[2], out var displacement))
                    {
                        engine.Displacement = displacement;
                    }
                    else
                    {
                        engine.Efficiency = input[2];
                    }
                }
                engineInline.Add(engine);
            }
        }

        private static string IsNull(string input)
        {
            string notAvaliable = "n/a";
            return input == null ? notAvaliable : input;
        }
        
        private static string IsNull(int? input)
        {
            string notAvaliable = "n/a";
            return input == null ? notAvaliable : $"{input}";
        }

        private static string PrintCars(List<Car> carList)
        {
            var sb = new StringBuilder();

            foreach (var car in carList)
            {
                sb.AppendLine($"{car.Model}:");
                sb.AppendLine($" {car.Engine.Model}:");
                sb.AppendLine($"   Power: {car.Engine.Power}");
                sb.AppendLine($"   Displacement: {IsNull(car.Engine.Displacement)}");
                sb.AppendLine($"   Efficiency: {IsNull(car.Engine.Efficiency)}");
                sb.AppendLine($" Weight: {IsNull(car.Weight)}");
                sb = sb.AppendLine($" Color: {IsNull(car.Color)}");
            }
            return sb.ToString();
        }
    }
}
