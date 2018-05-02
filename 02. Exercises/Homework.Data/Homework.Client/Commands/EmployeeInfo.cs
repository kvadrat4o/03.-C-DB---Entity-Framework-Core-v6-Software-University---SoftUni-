using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using Homework.Client.ModelsDBO;
using Homework.Data;

namespace Homework.Client.Commands
{
    public class EmployeeInfo
    {
        public static string Execute(string[] command)
        {
            int id;

            if (!int.TryParse(command[1], out id))
            {
                throw new ArgumentException("Invalid Id");
            }
            var result = new StringBuilder();

            using (var db = new HomeworkDbContext())
            {
                var employee = db.Employees
                    .Find(id);

                var dto = Mapper.Map<EmployeeDto>(employee);

                if (dto==null)
                    throw new ArgumentException("No Employee with that id !");
                
                result.Append($"ID: {id} - {dto.FirstName} {dto.LastName} - ${dto.Salary:f2}");
            }
            return result.ToString();
        }
    }
}
