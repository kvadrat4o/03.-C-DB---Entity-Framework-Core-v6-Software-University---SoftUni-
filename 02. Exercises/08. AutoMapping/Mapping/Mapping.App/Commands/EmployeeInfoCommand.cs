using AutoMapper;
using Mapping.App.Models;
using Mapping.Data;
using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Text;

namespace Mapping.App
{
    //EmployeeInfo <employeeId> 
    public class EmployeeInfoCommand
    {
        public static string Execute(string[] parameters)
        {
            var output = new StringBuilder();
            if (!int.TryParse(parameters[1], out int id))
            {
                throw new ArgumentException("Invalid Id");
            }
            using (var db = new EmployeeContext())
            {
                var employee = db.Employees
                    .Find(id);
                var dto = Mapper.Map<EmployeeDTO>(employee);
                output.Append($"ID: {id} - {dto.FirstName} {dto.LastName} - ${dto.Salary:f2}");
            }
            return output.ToString();
        }
    }
}