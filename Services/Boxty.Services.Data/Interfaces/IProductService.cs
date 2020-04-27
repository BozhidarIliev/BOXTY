namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public interface IProductService
    {
        IEnumerable<T> GetProducts<T>();

        Product GetProductById(int id);

        IEnumerable<Product> GetProductsByCategory(string categoryName);
    }
}
