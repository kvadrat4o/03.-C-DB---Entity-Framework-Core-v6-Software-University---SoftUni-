using System;
using System.Collections.Generic;
using System.Text;

namespace Mapping.App.Models
{
    public class ManagerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();

        public int EmployeesCount { get; set; }
    }
}
