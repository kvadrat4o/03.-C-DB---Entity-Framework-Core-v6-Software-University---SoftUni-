using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Homework.Client.ModelsDBO;
using Homework.Data;
using Microsoft.EntityFrameworkCore;

namespace Homework.Client.Commands
{
    public class ListEmployeesOlderThan
    {
        public static string Execute(string[] command)
        {
            int years;

            if (!int.TryParse(command[1], out years) || years < 0)
                throw new ArgumentException("Invalid Id");

            var result = new StringBuilder();

            using (var db = new HomeworkDbContext())
            {
                var employees = db.Employees
                    .Include(x => x.Manager)
                    .ThenInclude(x=>x.FirstName)
                    .ProjectTo<ListEmployeesOlderThanDto>()
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
