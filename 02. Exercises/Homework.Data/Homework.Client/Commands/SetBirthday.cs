using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Homework.Client.ModelsDBO;
using Homework.Data;

namespace Homework.Client.Commands
{
    public class SetBirthday
    {
        public static string Execute(string[] command)
        {
            var id = int.Parse(command[1]);
            var date = DateTime.ParseExact(command[3], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            using (var db = new HomeworkDbContext())
            {
                var employee = db.Employees.Find(id);

                employee.BirthDay = date;

                db.SaveChanges();
            }

            return $"Birthday is set";
        }
    }
}
