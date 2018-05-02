using AutoMapper;
using Mapping.App.Models;
using Mapping.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace Mapping.App
{
    //ManagerInfo <employeeId>
    public class ManagerInfoCommand
    {
        public static string Execute(string[] parameters)
        {
            int id;
            try
            {
                id = int.Parse(parameters[1]);
            }
            catch (Exception)
            {
                throw new ArgumentException("invalid id");
            }

            StringBuilder output = new StringBuilder();

            using (var db = new EmployeeContext())
            {
                var manager = db.Employees
                    .Include(e => e.Employees)
                    .SingleOrDefault(empl => empl.EmployeeId == id);

                if (manager == null)
                {
                    throw new ArgumentException("There is No Person With That Id !");
                }
                var dto = Mapper.Map<ManagerDTO>(manager);
                if (dto.EmployeesCount == 0)
                {
                    throw new ArgumentException($"{dto.FirstName} is not a manager !");
                }
                output.AppendLine($"{dto.FirstName} {dto.LastName} | Employees: {dto.EmployeesCount}");
                foreach (var e in dto.Employees)
                {
                    output.AppendLine($"- {e.FirstName} {e.LastName} - ${e.Salary:f2}");
                }
            }
            return output.ToString();
        }
    }
}