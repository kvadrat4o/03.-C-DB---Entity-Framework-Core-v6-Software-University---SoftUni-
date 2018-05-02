using AutoMapper;
using Mapping.App.Models;
using Mapping.Data;
using Mapping.Models;
using System;
using System.Linq;

namespace Mapping.App
{
    //AddEmployee <firstName> <lastName> <salary> 
    public class AddEmployeeCommand
    {
        public static string Execute(string [] parameters)
        {
            string result = String.Empty;
            using (EmployeeContext db = new EmployeeContext())
            {
                EmployeeDTO employeeDto = new EmployeeDTO()
                {
                    FirstName = parameters[1],
                    LastName = parameters[2],
                    Salary = decimal.Parse(parameters[3])
                };
                db.Employees.Add(Mapper.Map<Employee>(employeeDto));
                db.SaveChanges();
                return result = $"User with name {parameters[1]} {parameters[2]} and salary {parameters[3]} wa ssuccessfully added!";
            }
        }
    }
}