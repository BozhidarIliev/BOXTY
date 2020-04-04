using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Services
{
    public interface IProductService
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int id);
    }
}
