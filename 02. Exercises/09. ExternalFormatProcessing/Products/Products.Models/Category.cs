using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Category
    {
        public Category()
        {

        }
        public int Id { get; set; }

        [Range(3, 15)]

        public string Name { get; set; }

        public ICollection<ProductCategory> Products { get; set; } = new HashSet<ProductCategory>();
    }
}
