using Mapping.Models;

namespace Mapping.App
{
    public class ListEmployeesOlderThanDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public int ManagerId { get; set; }

        public Employee Manager { get; set; }

        public int Years { get; set; }
    }
}