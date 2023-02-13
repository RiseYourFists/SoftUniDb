using System;

namespace TestApp
{
    using MiniORM;
    using System.Collections.Generic;
    using TestApp.Data.Models;

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Pesho"
                },
                new Employee()
                {
                    Id=2,
                    Name = "Gosho"
                }
            };

        }
    }
}
