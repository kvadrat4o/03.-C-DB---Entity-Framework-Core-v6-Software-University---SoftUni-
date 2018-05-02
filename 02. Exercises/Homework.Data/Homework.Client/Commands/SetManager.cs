using System;
using System.Collections.Generic;
using System.Text;
using Homework.Data;

namespace Homework.Client.Commands
{
    public class SetManager
    {
        public static string Execute(string[] command)
        {

            var empId = int.Parse(command[1]);
            var manId = int.Parse(command[2]);

            using (var db = new HomeworkDbContext())
            {
                var employee = db.Employees.Find(empId);
                var manager = db.Employees.Find(manId);

                manager.Employees.Add(employee);

                db.SaveChanges();
            }

            return $"Manager with Id {manId} is now a Manager to Employee with Id {empId}";
        }
    }
}
