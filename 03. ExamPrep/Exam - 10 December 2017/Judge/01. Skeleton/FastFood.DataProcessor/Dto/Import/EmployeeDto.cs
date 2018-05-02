using FastFood.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Import
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public int PositionId { get; set; }
        [Required]
        public string Position { get; set; }

        [Required]
        [Range(15, 80)]
        public int Age { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
