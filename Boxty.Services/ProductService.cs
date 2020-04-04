using Boxty.Data;
using Boxty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxty.Services
{
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
