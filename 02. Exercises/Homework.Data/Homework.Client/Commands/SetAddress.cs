using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Homework.Data;

namespace Homework.Client.Commands
{
    public class SetAddress
    {
        public static string Execute(string[] command)
        {
            var id = int.Parse(command[1]);
            var address = string.Join(" ", command.Skip(2));

            using (var db = new HomeworkDbContext())
            {
                var employee = db.Employees.Find(id);
                employee.Address = address;

                db.SaveChanges();
            }

            return "Address is set";
        }
    }
}
