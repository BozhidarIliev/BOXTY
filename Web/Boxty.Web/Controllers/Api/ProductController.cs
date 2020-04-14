namespace Boxty.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Data;
    using Boxty.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly BoxtyDbContext context;

        public ProductController(BoxtyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}
