namespace Boxty.Services
{
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.All();
        }

        public IEnumerable<T> GetAllCategories<T>()
        {
            
            return categoryRepository.All().To<T>();
        }

        public Category GetCategoryById(int categoryId)
        {
            return categoryRepository.All().FirstOrDefault(x => x.Id == categoryId);
        }

        public async Task CreateCategory(string name)
        {
            var category = new Category
            {
                Name = name,
            };
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            if (categoryRepository.All().Any(x => x.Name == category.Name))
            {
                throw new DbUpdateConcurrencyException();
            }

            categoryRepository.Update(category);
            await categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = new Category
            {
                Id = id,
            };
            categoryRepository.Delete(category);
            await categoryRepository.SaveChangesAsync();
        }

    }
}
