namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Boxty.Models;
    using Boxty.Services.Interfaces;

    public class ProductService : IProductService
    {
        private readonly BoxtyDbContext context;

        public ProductService(BoxtyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> Products => context.Products;

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(x => x.Id == id);
        }
    }
}
