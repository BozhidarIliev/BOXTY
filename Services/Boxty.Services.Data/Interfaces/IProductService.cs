namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public interface IProductService
    {
        IEnumerable<T> GetProducts<T>();

        Product GetProductById(int id);

        IEnumerable<Product> GetProductsByCategory(string categoryName);

        Task AddProduct(ProductCreateInputModel model);

        Task Update(ProductCreateInputModel model);

        Task DeleteProduct(int id);
    }
}
