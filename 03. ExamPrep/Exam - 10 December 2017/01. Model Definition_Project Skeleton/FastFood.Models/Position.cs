using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Position
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength =3)]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}