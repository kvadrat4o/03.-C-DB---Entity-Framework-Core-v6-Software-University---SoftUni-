﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Models
{
    public class BookCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
