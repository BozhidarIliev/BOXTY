namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<Product> GetProducts()
        {
            return productRepository.All();
        }

        public Product GetProductById(int id)
        {
            return productRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
