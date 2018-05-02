using System;
using System.Collections.Generic;
using System.Text;
using Homework.Models;

namespace Homework.Client.ModelsDBO
{
    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        public int EmployeesCount { get; set; }
    }
}
