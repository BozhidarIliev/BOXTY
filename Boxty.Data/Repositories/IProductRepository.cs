using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int id);
    }
}
