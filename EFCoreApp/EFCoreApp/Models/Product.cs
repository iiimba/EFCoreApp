﻿using EFCoreApp.Controllers;

namespace EFCoreApp.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal RetailPrice { get; set; }

        public long CategoryId { get; set; }

        public Category Category { get; set; }
    }
}