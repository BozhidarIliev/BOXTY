namespace Boxty.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
    }
}
