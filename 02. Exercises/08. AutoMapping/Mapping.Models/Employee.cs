using System;
using System.Collections.Generic;

namespace Mapping.Models
{
    public class Employee
    {
        public Employee()
        {

        }
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime? Birthday { get; set; }

        public decimal Salary { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
