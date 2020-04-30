namespace Boxty.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Data;
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductController(IProductService productService, IDeletableEntityRepository<Product> productRepository)
        {
            this.productService = productService;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<ProductOutputModel> GetProducts()
        {
            return productService.GetProducts<ProductOutputModel>();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await productRepository.All().FirstAsync(x => x.Id == id);
        }

        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //    await productRepository.SaveChangesAsync();
        //}

        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await productService.DeleteProduct(id);
        }

        private bool ProductExists(int id)
        {
            return productRepository.All().Any(e => e.Id == id);
        }
    }
}
