using AutoMapper;
using Mapping.App.Models;
using Mapping.Data;
using System;

namespace Mapping.App
{
    //SetAddress <employeeId> <address> 
    public class SetAddressCommand
    {
        public static string Execute(string[] parameters)
        {
            string output = string.Empty;
            using (EmployeeContext db = new EmployeeContext())
            {
                var id = int.Parse(parameters[1]);
                var employee = db.Employees.Find(id);
                //var employeeDto = Mapper.Map<EmployeeDTO>(employee);
                var address = employee.Address;
                var addressInput = parameters[1];
                address = addressInput;
                db.SaveChanges();
                return output = $"Address added successfully to user {id}";
            }
        }
    }
}