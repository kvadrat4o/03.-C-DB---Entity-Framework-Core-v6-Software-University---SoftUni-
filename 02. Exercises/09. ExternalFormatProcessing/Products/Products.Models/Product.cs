﻿using System;
using System.Collections.Generic;

namespace Products.Models
{
    public class Product
    {
        public Product()
        {

        }

        public int Id { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public ICollection<ProductCategory> Categories { get; set; } = new HashSet<ProductCategory>();
    }
}
