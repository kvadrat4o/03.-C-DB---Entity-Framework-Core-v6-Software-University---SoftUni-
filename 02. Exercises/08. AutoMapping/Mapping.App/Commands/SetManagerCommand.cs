using Mapping.Data;
using System;

namespace Mapping.App
{
    public class SetManagerCommand
    {
        //SetManager <employeeId> <managerId>
        public static string Execute(string[] parameters)
        {
            using (var db = new EmployeeContext())
            {
                var employee = db.Employees.Find(int.Parse(parameters[1]));
                var manager = db.Employees.Find(int.Parse(parameters[2]));
                manager.Employees.Add(employee);
                db.SaveChanges();
            }
            return $"Manager {int.Parse(parameters[2])} is set for Manager to Employee Id {int.Parse(parameters[1])}";
        }
    }
}