using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class Tire
    {
        public Tire(double pressure, int year)
        {
            Pressure = pressure;
            Year = year;
        }

        public double Pressure { get; set; }
        public int Year { get; set; }
    }
}
