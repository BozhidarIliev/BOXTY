namespace Boxty.Services
{
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
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

        public T GetProductById<T>(int id)
        {
            var product = this.productRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            return productRepository.All().Where(x => x.Category.Name == categoryName);
        }

        public async Task AddProduct(ProductCreateInputModel model, string imagePath)
        {
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Description = model.Description,
            };

            Directory.CreateDirectory($"{imagePath}/products/");

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var dbImage = new Image
            {
                Extension = extension,
            };
            product.Image = dbImage;

            var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);
            
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductEditInputModel model)
        {
            var currentProduct = productRepository.All().FirstOrDefault(x => x.Id == model.Id);

            currentProduct.Name = model.Name;
            currentProduct.Price = model.Price;
            currentProduct.CategoryId = model.CategoryId;
            currentProduct.Description = model.Description;

            await productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await productRepository.All().FirstAsync(x => x.Id == id);
            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();
        }

        public bool ProductExists(int id)
        {
            return productRepository.All().Any(x => x.Id == id);
        }
    }
}
