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
            var engineInline = new Dictionary<string, Engine>();
            var carList = new Dictionary<string, Car>();

            int inputs = int.Parse(Console.ReadLine());
            GetEngines(engineInline, inputs);

            inputs = int.Parse(Console.ReadLine());
            GetCars(engineInline, carList, inputs);

            Console.WriteLine(PrintCars(carList));
        }


        private static void GetCars(Dictionary<string, Engine> engineInline, Dictionary<string, Car> carList, int inputs)
        {
            for (int i = 0; i < inputs; i++)
            {
                var newCar = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var carParams = GetParams(newCar);
                var model = carParams[0];
                var enModel = carParams[1];
                var weight = int.Parse(carParams[2]);
                var color = carParams[3];

                carList.Add(model, new Car(model, engineInline[enModel], weight, color));
            }
        }

        private static void GetEngines(Dictionary<string, Engine> engineInline, int inputs)
        {
            for (int i = 0; i < inputs; i++)
            {
                var newEngine = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var engineParams = GetParams(newEngine);
                var model = engineParams[0];
                var power = int.Parse(engineParams[1]);
                var displacement = int.Parse(engineParams[2]);
                var efficiency = engineParams[3];

                engineInline.Add(model, new Engine(model, power, displacement, efficiency));
            }
        }

        private static List<string> GetParams(string[] data)
        {
            var len = data.Length;
            var result = new List<string>();
            switch (len)
            {
                case 2:
                    result = data.ToList();
                    result.Add("-1");
                    result.Add("-1");
                    break;
                case 3:
                    result.Add(data[0]);
                    result.Add(data[1]);
                    if (int.TryParse(data[2], out int val))
                    {
                        result.Add(data[2]);
                        result.Add("-1");
                        break;
                    }
                    result.Add("-1");
                    result.Add(data[2]);
                    break;
                default:
                    result = data.ToList();
                    break;
            }

            return result;
        }

        public static bool IsNull(string input)
        {
            if (int.TryParse(input, out int output))
            {
                return output == -1;
            }

            return false;
        }
        public static bool IsNull(int input)
        {
            return input == -1;
        }
        private static string PrintCars(Dictionary<string, Car> carList)
        {
            var sb = new StringBuilder();
            var notAvaliable = "n/a";

            foreach (var item in carList.Values)
            {
                sb.AppendLine($"{item.Model}:");
                sb.AppendLine($"{item.Engine.Model}:");
                sb.AppendLine($"  Power: {item.Engine.Power}");

                string displacement = (IsNull(item.Engine.Displacement)) ? notAvaliable : $"{item.Engine.Displacement}";
                sb.AppendLine($"  Displacement: {displacement}");

                string efficiency = (IsNull(item.Engine.Efficiency)) ? notAvaliable : $"{item.Engine.Efficiency}";
                sb.AppendLine($"  Efficiency: {efficiency}");

                string weight = (IsNull(item.Weight)) ? notAvaliable : $"{item.Weight}";
                sb.AppendLine($"Weight: {weight}");

                string color = (IsNull(item.Color)) ? notAvaliable : $"{item.Color}";
                sb.AppendLine($"Color: {color}");

            }
            return sb.ToString();
        }
    }
}
