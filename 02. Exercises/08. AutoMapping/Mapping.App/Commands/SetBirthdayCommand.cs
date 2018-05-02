using AutoMapper;
using Mapping.App.Models;
using Mapping.Data;
using System;
using System.Globalization;

namespace Mapping.App
{
    //SetBirthday <employeeId> <date: "dd-MM-yyyy"> 
    public class SetBirthdayCommand
    {
        public static string Execute(string[] parameters)
        {
            string output = string.Empty;
            using (EmployeeContext db = new EmployeeContext())
            {
                var id = int.Parse(parameters[1]);
                var employee = db.Employees.Find(id);
                //var employeeDto = Mapper.Map<EmployeeDTO>(employee);
                var bday = employee.Birthday;
                var bdayInput = DateTime.ParseExact(parameters[3], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                bday = bdayInput;
                db.SaveChanges();
                return output = $"Birthday added successfully to user {id}";
            }
        }
    }
}