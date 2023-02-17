using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var result = RemoveTown(dbContext);
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

        public static string GetLatestProjects(SoftUniContext context)
        {
            var output = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .ToArray()
                .OrderBy(p => p.Name);

            foreach (var project in projects)
            {
                output.AppendLine(project.Name);
                output.AppendLine(project.Description);
                output.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }

            return output.ToString();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var engineers = context.Employees
                .Where(employee =>
                    employee.Department.Name == "Engineering"
                    || employee.Department.Name == "Tool Design"
                    || employee.Department.Name == "Marketing"
                    || employee.Department.Name == "Information Services")
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            foreach (var e in engineers)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary * 1.12m:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var output = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.FirstName.ToLower().StartsWith("sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .ToArray();

            foreach (var employee in employees)
            {
                output.AppendLine(
                    $"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }

            return output.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            var output = new StringBuilder();

            var projectTodelete = context.Projects.Find(2);
            var refferedProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == projectTodelete.ProjectId)
                .ToArray();

            context.EmployeesProjects.RemoveRange(refferedProjects);
            context.Remove(projectTodelete);
            context.SaveChanges();

            var projects = context.Projects.Take(10).Select(p => p.Name).ToList();

            projects.ForEach(p => output.AppendLine(p));
            

            return output.ToString().TrimEnd();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var output = new StringBuilder();

            var townToDelete = context.Towns.First(t => t.Name == "Seattle");

            var referredAddresses = context.Addresses
                .Where(a => a.Town.TownId == townToDelete.TownId)
                .ToArray();

            var addressIds = new List<int?>();
            var addressesCount = referredAddresses.Length;

            foreach (var address in referredAddresses)
            {
                addressIds.Add(address.AddressId);
            }

            var employees = context.Employees
                .Where(e => addressIds.Contains(e.AddressId))
                .ToArray();

            foreach (var employee in employees)
            {
                employee.AddressId = null;
            }

            context.Addresses.RemoveRange(referredAddresses);
            context.Towns.Remove(townToDelete);
            context.SaveChanges();

            output.AppendLine($"{addressesCount} addresses in Seattle were deleted");

            return output.ToString().TrimEnd();
        }
    }


}
