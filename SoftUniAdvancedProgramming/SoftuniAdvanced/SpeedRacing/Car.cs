using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class Car
    {
        public Car(string model, double fuelAmount, double fuelConsumptionPerKm)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelConsumptionPerKm = fuelConsumptionPerKm;
            TravelledDistance = 0;
        }

        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionPerKm { get; set; }
        public double TravelledDistance { get; set; }

        public void Drive(double distance)
        {
            var burnedFuel = distance * this.FuelConsumptionPerKm;
            if(this.FuelAmount - burnedFuel < 0 )
            {
                Console.WriteLine("Insufficient fuel for the drive");
                return;
            }

            this.FuelAmount -= burnedFuel;
            this.TravelledDistance += distance;
        }

        public string GetStats()
        {
            var sb = new StringBuilder();

            sb.Append($"{this.Model} {this.FuelAmount:F2} {this.TravelledDistance}");
            return sb.ToString();
        }
    }

}