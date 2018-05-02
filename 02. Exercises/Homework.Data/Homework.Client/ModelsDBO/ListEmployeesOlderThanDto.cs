using System;
using System.Collections.Generic;
using System.Text;
using Homework.Models;

namespace Homework.Client.ModelsDBO
{
    public class ListEmployeesOlderThanDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public int ManagerId { get; set; }

        public Employee Manager { get; set; }

        public int Years { get; set; }
    }
}
