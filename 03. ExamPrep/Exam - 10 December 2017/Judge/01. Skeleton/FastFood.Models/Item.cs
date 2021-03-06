﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FastFood.Models
{
    [XmlType("Item")]
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3)]
        [XmlElement("Name")]
        public string Name { get; set; }

        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        [Range(typeof(decimal),"0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}