﻿using System;
using System.Globalization;
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
            var result = GetDepartmentsWithMoreThan5Employees(dbContext);
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

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var output = new StringBuilder();

            var employees = context.Employees
                .Where(e =>
                    e.EmployeesProjects
                        .Any(p => p.Project.StartDate.Year >= 2001
                                            && p.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    })

                })
                .ToArray();

            foreach (var employee in employees)
            {
                output.AppendLine(
                    $"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");
                foreach (var project in employee.Projects)
                {
                    var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    var endDate = (!project.EndDate.HasValue)
                        ? "not finished"
                        : project.EndDate.Value
                            .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    output.AppendLine($"--{project.Name} - {startDate} - {endDate}");
                }
            }
            return output.ToString();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var output = new StringBuilder();

            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeeCount = a.Employees.Count

                })
                .ToArray();

            foreach (var address in addresses)
            {
                output.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
            }

            return output.ToString();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var output = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects.Where(p => p.EmployeeId == 147)
                        .Select(p => new
                        {
                            ProjectName = p.Project.Name,
                        })
                })
                .ToArray();

            output.AppendLine($"{employees[0].FirstName} {employees[0].LastName} - {employees[0].JobTitle}");

            foreach (var project in employees[0].Projects.OrderBy(p => p.ProjectName))
            {
                output.AppendLine(project.ProjectName);
            }
            
            return output.ToString();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var output = new StringBuilder();

            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                })
                .ToArray();

            foreach (var department in departments)
            {
                output.AppendLine($"{department.Name} - {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)
                {
                    output.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return output.ToString();
        }
    }
}