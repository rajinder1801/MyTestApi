﻿using System.Collections.Generic;

namespace MyApiTest.Models
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
