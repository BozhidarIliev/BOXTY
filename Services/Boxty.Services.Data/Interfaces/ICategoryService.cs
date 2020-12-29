namespace Boxty.Services.Interfaces
{
    using Boxty.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        IEnumerable<T> GetAllCategories<T>();

        Category GetCategoryById(int categoryId);

        Task CreateCategory(string name);

        Task UpdateCategory(Category category);

        Task DeleteCategory(int id);
    }
}
