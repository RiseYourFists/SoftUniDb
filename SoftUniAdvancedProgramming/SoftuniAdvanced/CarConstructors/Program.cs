using System;

namespace CarManufacturer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var make = Console.ReadLine();
            var model = Console.ReadLine();
            var year = int.Parse(Console.ReadLine());
            var fuelQuantity = double.Parse(Console.ReadLine());
            var fuelConsumption = double.Parse(Console.ReadLine());

            var car = new Car();
            var car1 = new Car(make, model, year);
            var car2 = new Car(make, model, year, fuelQuantity, fuelConsumption);


        }
    }
}
