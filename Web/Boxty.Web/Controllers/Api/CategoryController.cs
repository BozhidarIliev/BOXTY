namespace Boxty.Web.Controllers.Api
{
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryController(ICategoryService categoryService, IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryService = categoryService;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IEnumerable<CategoryDropDownViewModel> GetAllCategories()
        {
            return categoryService.GetAllCategories<CategoryDropDownViewModel>();
        }
    }
}
