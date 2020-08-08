﻿using System.Collections.Generic;

namespace EFCore.Controllers
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        void AddProduct(Product product);
    }
}
