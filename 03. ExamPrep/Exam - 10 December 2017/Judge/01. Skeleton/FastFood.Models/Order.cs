using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FastFood.Models
{
    [XmlType("Order")]
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [XmlElement("Customer")]
        public string Customer { get; set; }
        [Required]
        [XmlElement("DateTime")]
        public DateTime DateTime { get; set; }
        [Required]
        [XmlElement("Type")]
        public OrderType Type { get; set; }
        [Required]
        public decimal TotalPrice { get; set; } 

        public int EmployeeId { get; set; }
        [Required]
        [XmlElement("Employee")]
        public Employee Employee { get; set; }
        [XmlElement("Items")]
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}