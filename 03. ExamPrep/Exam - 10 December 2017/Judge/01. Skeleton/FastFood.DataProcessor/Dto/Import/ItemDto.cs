﻿using FastFood.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Import
{
    public class ItemDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        public string Category { get; set; }
        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
