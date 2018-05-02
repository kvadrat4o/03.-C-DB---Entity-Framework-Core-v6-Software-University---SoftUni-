namespace Mapping.App
{
    using AutoMapper;
    using Mapping.App.Models;
    using Mapping.Data;
    using System;
    using System.Text;

    //EmployeePersonalInfo <employeeId> 
    public class EmployeePersonalInfoCommand
    {
        public static string Execute(string[] parameters)
        {
            StringBuilder output = new StringBuilder();
            if (!int.TryParse(parameters[1], out int id))
            {
                throw new ArgumentException("Invalid Id");
            }
            using (var db = new EmployeeContext())
            {
                var employee = db.Employees.Find(id);
                var dto = Mapper.Map<EmployeeDTO>(employee);

                if (dto == null)
                    throw new ArgumentException("No Employee with that id !");
                var date = string.Empty;
                var address = employee.Address;
                if (employee.Birthday != null)
                    date = employee.Birthday.ToString();
                if (employee.Address == null)
                    address = "No Address";

                output.Append($"ID: {id} - {dto.FirstName} {dto.LastName} - ${dto.Salary:f2}"+ Environment.NewLine + $"Birthday: {date}"
                              + Environment.NewLine + $"Address: {address}");
            }
            return output.ToString();
        }
    }
}