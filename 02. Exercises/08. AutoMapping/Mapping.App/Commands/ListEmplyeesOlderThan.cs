using AutoMapper.QueryableExtensions;
using Mapping.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace Mapping.App
{
    public class ListEmplyeesOlderThan
    {
        public static string Execute(string[] command)
        {
            int years;

            if (!int.TryParse(command[1], out years) || years < 0)
                throw new ArgumentException("Invalid Id");

            var result = new StringBuilder();

            using (var db = new EmployeeContext())
            {
                var employees = db.Employees
                    .Include(e => e.Manager)
                    .ThenInclude(x => x.FirstName)
                    .ProjectTo<ListEmployeesOlderThanDTO>()
                    .ToList();

                if (!employees.Any())
                    throw new ArgumentException($"No employees older than {years}");

                foreach (var e in employees)
                {

                    var manager = e.Manager == null ? "[no manager]" : e.Manager.LastName;

                    result.AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:f2} - Manager: {manager}");
                }

            }

            return result.ToString();
        }
    }
}