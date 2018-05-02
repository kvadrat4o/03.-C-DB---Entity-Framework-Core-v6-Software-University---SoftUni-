using System;
using P02_DatabaseFirst;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Collections.Generic;

namespace P02_DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            //3.	Employees Full Information
            var employees = db.Employees.Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.MiddleName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                }).ToList().OrderBy(e => e.EmployeeId);
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            //4.	Employees with Salary Over 50 000
            var employeesSalaries = db.Employees.Select( e => new
                {
                    e.FirstName,
                    e.Salary
                }).ToList().Where(e => e.Salary > 50000).OrderBy(e => e.FirstName);
            foreach (var empl in employeesSalaries)
            {
                Console.WriteLine($"{ empl.FirstName}");
            }

            //5.	Employees from Research and Development
            var employeesRnD = db.Employees.Where(e => e.Department.Name == "Research and Development ")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new { e.FirstName, e.LastName, e.Salary, e.Department })
                .ToList();
            foreach (var empl in employeesRnD)
            {
                Console.WriteLine($"{empl.FirstName} {empl.LastName} from {empl.Department.Name} - ${empl.Salary:f2}");
            }

            //6.	Adding a New Address and Updating Employee
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var nakov = db.Employees
                    .Include(x=>x.Address)
                    .SingleOrDefault(x => x.LastName == "Nakov");
            nakov.Address = address;

            foreach (var item in db.Employees
                    .Include(x => x.Address)
                    .OrderByDescending(x => x.AddressId)
                    .Take(10)
                    .ToList())
            {
                Console.WriteLine(item.Address.AddressText);
            }

            //7.	Employees and Projects
            using (db = new SoftUniContext())
            {
                var employees1 = db.Employees
                    .Include(x => x.EmployeesProjects)
                    .ThenInclude(x => x.Project)
                    .Where(x => x.EmployeesProjects
                    .Any(s => s.Project.StartDate.Year >= 2001 &&
                    s.Project.StartDate.Year <= 2003))
                    .Take(30)
                    .ToList();
                var format = "M/d/yyyy h:mm:ss tt";

                foreach (var e in employees1)
                {
                    var managerId = e.ManagerId;
                    var manager = db.Employees.Find(managerId);

                    Console.WriteLine($"{e.FirstName} {e.LastName} - " +
                        $"Manager: {manager.FirstName} {manager.LastName}");

                    foreach (var p in e.EmployeesProjects)
                    {
                        Console.Write($"--{p.Project.Name} - " +
                            $"{p.Project.StartDate.ToString(format, CultureInfo.InvariantCulture)} - ");


                        if (p.Project.EndDate == null)
                            Console.WriteLine("not finished");
                        else
                        {
                            Console.WriteLine(p.Project.EndDate.Value.ToString(format, CultureInfo.InvariantCulture));
                        }

                    }
                }
            }

            //8.	Addresses by Town
            using (db = new SoftUniContext())
            {
                var addresses = db.Addresses
                    .Include(a => a.Town)
                    .Select(a => new
                {
                    a.AddressText,
                    a.Town.Name,
                    a.Employees,
                    a.TownId,
                    a.AddressId
                }).ToList();

                foreach (var addr in addresses.OrderByDescending(a => a.Employees.Count).ThenBy(a => a.Name).ThenBy(a => a.AddressText).Take(10))
                {
                    Console.WriteLine($"{addr.AddressText}, {addr.Name} - {addr.Employees.Count} employees");
                }
            }

            //9.	Employee 147
            using (db = new SoftUniContext())
            {
                var employee = db.Employees
                    .Include(e => e.EmployeesProjects)
                    .ThenInclude(ep => ep.Project)
                    .Where(e => e.EmployeeId == 147)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    });

                var projects = db.Projects
                    .Include(p => p.EmployeesProjects)
                    .ThenInclude(ep => ep.Employee)
                    .Where(x => x.EmployeesProjects.Any(e => e.EmployeeId == 147))
                    .ToList();

                foreach (var em in employee)
                {
                    Console.WriteLine($"{em.FirstName} {em.LastName} - {em.JobTitle}");
                }
                foreach (var proj in projects.OrderBy(p => p.Name))
                {
                    Console.WriteLine($"{proj.Name}");
                }
            }

            //10.	Departments with More Than 5 Employees
            using (db = new SoftUniContext())
            {
                var departments = db.Departments
                    .Include(d => d.Employees)
                    .Select(d => new
                    {
                        d.Name,
                        d.Manager.FirstName,
                        d.Manager.LastName,
                        d.Employees
                    })
                    .Where(d => d.Employees.Count > 5)
                    .ToList();
                foreach (var dept in departments.OrderBy(d => d.Employees.Count).ThenBy(x => x.Name))
                {
                    Console.WriteLine($"{dept.Name} – {dept.FirstName} {dept.LastName}");
                    foreach (var empl in dept.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                    {
                        Console.WriteLine($"{empl.FirstName} {empl.LastName} - {empl.JobTitle}");
                    }
                    Console.WriteLine(new string('-',10));
                }
            }

            //11.	Find Latest 10 Projects
            using (db = new SoftUniContext())
            {
                var projects = db.Projects
                    .Select(p => new
                    {
                        p.Name,
                        p.Description,
                        p.StartDate
                    })
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .ToList();

                foreach (var proj in projects.OrderBy(a => a.Name))
                {
                    Console.WriteLine($"{proj.Name}\n{proj.Description}\n{proj.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)}");
                }
            }

            //12.	Increase Salaries
            using (db = new SoftUniContext())
            {
                var employeesToIncrease = db.Employees
                    .Include(e => e.Department)
                    .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing " || e.Department.Name == "Information Services")
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();
                var increased = new List<Employee>();
                for (int i = 0; i < employeesToIncrease.Count(); i++)
                {
                    
                    employeesToIncrease[i].Salary += employeesToIncrease[i].Salary * (decimal)0.12;
                }
                
                foreach (var empl  in employeesToIncrease)
                {
                    Console.WriteLine($"{empl.FirstName} {empl.LastName} (${empl.Salary:f2})");
                }
            }

            //13.	Find Employees by First Name Starting With "Sa"
            using (db = new SoftUniContext())
            {
                var employeesNames = db.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Salary,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var empl in employeesNames)
                {
                    Console.WriteLine($"{empl.FirstName} {empl.LastName} - {empl.JobTitle} - (${empl.Salary:f2})");
                }
            }

            //14.	Delete Project by Id
            using (db = new SoftUniContext())
            {
                var emplProjToDelete = db.EmployeesProjects
                    .Where(ep => ep.ProjectId == 2)
                    .ToList();
                db.EmployeesProjects.RemoveRange(emplProjToDelete);
                var projectsToDelete = db.Projects
                    .Find(2);
                db.Projects.Remove(projectsToDelete);
                db.SaveChanges();

                var projects1 = db.Projects
                    .Take(10)
                    .ToList();

                foreach (var proj in projects1)
                {
                    Console.WriteLine($"{proj.Name}");
                }
            }

            // 15.Remove Towns
            using (db = new SoftUniContext())
            {
                var input = Console.ReadLine();
                var town = db.Towns
                    .Where(x => x.Name == input)
                    .FirstOrDefault();

                if (town == null)
                {
                    Console.WriteLine("No Such town");
                    return;
                }

                var adresses = db.Addresses
                    .Where(x => x.Town.Name == input)
                    .ToList();

                foreach (var e in db.Employees)
                {
                    foreach (var adress in adresses)
                    {
                        if (adress.AddressId == e.AddressId)
                            e.AddressId = null;
                    }
                }

                db.Addresses.RemoveRange(adresses);
                db.Towns.Remove(town);

                Console.WriteLine($"{adresses.Count()} addresses in {town.Name} were deleted");

                db.SaveChanges();
            }
        }
    }
}
