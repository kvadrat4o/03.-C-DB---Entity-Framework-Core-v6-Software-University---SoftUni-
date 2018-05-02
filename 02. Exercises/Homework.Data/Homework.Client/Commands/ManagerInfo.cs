using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Homework.Client.ModelsDBO;
using Homework.Data;
using Microsoft.EntityFrameworkCore;

namespace Homework.Client.Commands
{
    public class ManagerInfo
    {
        public static string Execute(string[] command)
        {
            int id;
            try
            {
                id = int.Parse(command[1]);
            }
            catch (Exception)
            {
                throw new ArgumentException("invalid id");
            }

            var result = new StringBuilder();

            using (var db = new HomeworkDbContext())
            {
                var manager = db.Employees
                    .Include(x => x.Employees)
                    .SingleOrDefault(x => x.Id == id);

                if (manager == null)
                {
                    throw new ArgumentException("There is No Person With That Id !");
                }

                var dto = Mapper.Map<ManagerDto>(manager);

                if (dto.EmployeesCount == 0)
                {
                    throw new ArgumentException($"{dto.FirstName} is not a manager !");
                }

                result.AppendLine($"{dto.FirstName} {dto.LastName} | Employees: {dto.EmployeesCount}");

                foreach (var e in dto.Employees)
                {
                    result.AppendLine($"- {e.FirstName} {e.LastName} - ${e.Salary:f2}");
                }
            }

            return result.ToString();
        }
    }
}
