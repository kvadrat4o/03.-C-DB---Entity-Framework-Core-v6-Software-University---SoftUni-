using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FastFood.Models
{
    [XmlType("Catrgory")]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3)]
        [XmlElement("Name")]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}