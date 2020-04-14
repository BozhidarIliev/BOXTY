namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;

    using Boxty.Models;

    public interface IProductService
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int id);
    }
}
