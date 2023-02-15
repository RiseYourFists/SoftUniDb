using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var dbContext = new SoftUniContext();
            var result = AddNewAddressToEmployee(dbContext);
            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            foreach (var employee in context.Employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {Math.Round(employee.Salary, 2):f2}");
            }

            return sb.ToString();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb =new StringBuilder();

            var result = context.Employees.Where(e => e.Salary > 50000).OrderBy(e => e.FirstName).ToList();
            foreach (var employee in result)
            {
                sb.AppendLine($"{employee.FirstName} - {Math.Round(employee.Salary, 2):f2}");
            }

            return sb.ToString();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary,
                    DepartmentName = e.Department.Name
                })
                .ToArray()
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine(
                    $"{employee.FirstName} {employee.LastName} " +
                    $"from {employee.DepartmentName} - ${employee.Salary:F2}");
            }

            return sb.ToString();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employees = context
                .Employees
                .Where(e => e.LastName == "Nakov")
                .ToArray();

            foreach (var employee in employees)
            {
                employee.Address = address;
            }

            context.UpdateRange(employees);
            context.SaveChanges();

            var sb = new StringBuilder();

            var results = context
                .Employees
                .OrderByDescending(e => e.Address.AddressId)
                .Take(10)
                .Select(e => new
                {
                    AddressName = e.Address.AddressText
                })
                .ToArray();

            foreach (var addressResult in results)
            {
                sb.AppendLine($"{addressResult.AddressName}");
            }

            return sb.ToString();
        }
    }
}
