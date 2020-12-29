namespace Boxty.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Data;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = "manager,admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(this.categoryService.GetAllCategories<CategoryViewModel>());
        }

        public IActionResult Details(int id)
        {
            var category = categoryService.GetCategoryById(id);
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return this.View(category);
            }

            try
            {
                await categoryService.CreateCategory(category.Name);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(category);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
