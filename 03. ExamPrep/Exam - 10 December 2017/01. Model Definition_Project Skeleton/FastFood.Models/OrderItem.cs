using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        [Required]
        public Order Order { get; set; }

        public int ItemId { get; set; }
        [Required]
        public Item Item { get; set; }
        [Range(typeof(decimal),"1", "79228162514264337593543950335")]
        public decimal Quantity { get; set; }
    }
}