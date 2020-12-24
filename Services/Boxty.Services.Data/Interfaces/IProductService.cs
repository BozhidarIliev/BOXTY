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

        public T GetProductById<T>(int id);

        IEnumerable<Product> GetProductsByCategory(string categoryName);

        Task AddProduct(ProductCreateInputModel model, string imagePath);

        Task UpdateAsync(ProductEditInputModel model);

        Task DeleteAsync(int id);

        bool ProductExists(int id);
    }
}
