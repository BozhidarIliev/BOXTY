namespace Boxty.Services
{
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<T> GetProducts<T>()
        {
            return productRepository.All().To<T>();
        }

        public Product GetProductById(int id)
        {
            return productRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            return productRepository.All().Where(x => x.Category.Name == categoryName);
        }

        public async Task AddProduct(ProductCreateInputModel model)
        {
            var product = new Product
            {
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Name = model.Name,
                Price = model.Price,
            };
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
        }

        public async Task Update(ProductCreateInputModel model)
        {
            var product = new Product
            {
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Name = model.Name,
                Price = model.Price,
            };

            productRepository.Update(product);
            await productRepository.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await productRepository.All().FirstAsync(x => x.Id == id);

            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();
        }
    }
}
