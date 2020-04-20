namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using Boxty.Data.Models;

    public interface IProductService
    {
        IEnumerable<Product> GetProducts();

        Product GetProductById(int id);
    }
}
