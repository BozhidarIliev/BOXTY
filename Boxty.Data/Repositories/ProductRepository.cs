using Boxty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxty.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BoxtyDbContext context;

        public ProductRepository(BoxtyDbContext context)
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
