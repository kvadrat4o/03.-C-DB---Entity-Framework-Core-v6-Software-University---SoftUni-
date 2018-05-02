using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.Company_Roster
{
    class CompanyRoster
    {
        static void Main(string[] args)
        {
            var departments = new Dictionary<string, List<Employee>>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                string name = input[0];
                decimal salary = decimal.Parse(input[1]);
                string position = input[2];
                string department = input[3];
                var emp = new Employee(name, salary, position, department);
                if (input.Length == 5)
                {
                    var isAge = int.TryParse(input[4], out int age);
                    if (isAge)
                    {
                        emp.Age = age;
                    }
                    else
                    {
                        emp.Email = input[4];
                    }
                }
                else if(input.Length == 6)
                {
                    emp.Email = input[4];
                    emp.Age = int.Parse(input[5]);
                }
                if (!departments.ContainsKey(department))
                {
                    departments.Add(department, new List<Employee>());
                }
                departments[department].Add(emp);
            }
            string highestDepartment = "";
            decimal highestSalary = 0;
            foreach (var d in departments)
            {
                decimal depSalary = 0;
                int empCount = 0;
                foreach (var e in d.Value)
                {
                    depSalary += e.Salary;
                }
                decimal avgSalary = depSalary / d.Value.Count();
                if (avgSalary > highestSalary)
                {
                    highestSalary = avgSalary;
                    highestDepartment = d.Key;
                }
            }
            Console.WriteLine($"Highest Average Salary: {highestDepartment}");
            foreach (var e in departments[highestDepartment].OrderByDescending(e => e.Salary))
            {
                Console.WriteLine(e);
            }
        }
    }
}
