namespace Boxty.Services
{
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return categoryRepository.All();
        }

        public Category GetCategoryById(int categoryId)
        {
            var items = categoryRepository.All();
            return items.FirstOrDefault(x => x.Id == categoryId);
        }

        public async Task CreateCategory(string name)
        {
            var category = new Category
            {
                Name = name,
            };
            await categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategory(Category category)
        {
            var items = await categoryRepository.AllAsync();
            if (items.Any(x => x.Name == category.Name))
            {
                throw new DbUpdateConcurrencyException();
            }

            categoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            var category = new Category
            {
                Id = id,
            };
            categoryRepository.Delete(category);
        }

    }
}
