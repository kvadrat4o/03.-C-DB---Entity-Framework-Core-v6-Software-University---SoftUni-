using AutoMapper;
using Homework.Client.ModelsDBO;
using Homework.Data;
using Homework.Models;

namespace Homework.Client.Commands
{
    public class AddEmployee
    {
        public static string Execute(string[] command)
        {
            var firstName = command[1];
            var lastName = command[2];
            var salary = decimal.Parse(command[3]);

            using (var db = new HomeworkDbContext())
            {
                var employee = new EmployeeDto()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Salary = salary
                };

                db.Employees.Add(Mapper.Map<Employee>(employee));

                db.SaveChanges();
            }

            return $"Employee {firstName} {lastName} with Salary {salary} was added";
        }
    }
}