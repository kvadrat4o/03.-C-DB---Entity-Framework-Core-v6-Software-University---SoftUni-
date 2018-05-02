﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Models
{
    public class User
    {
        public User()
        {

        }
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> ProductsBought { get; set; } = new List<Product>();
        public ICollection<Product> ProductsSold { get; set; } = new List<Product>();
    }
}
