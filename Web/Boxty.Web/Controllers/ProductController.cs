namespace Boxty.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting.Internal;

    [Authorize(Roles = "manager,admin")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment environment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment hostEnvironment)
        {
            this.categoryService = categoryService;
            this.environment = hostEnvironment;
            this.productService = productService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(productService.GetProducts<ProductViewModel>());
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var item = productService.GetProductById<ProductViewModel>(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Create()
        {
            var categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();
            var viewModel = new ProductCreateInputModel
            {
                Categories = categories,
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();
                return View(model);
            }

            try
            {
                await productService.AddProduct(model, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = "Product added successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.Admin)]
        public IActionResult Edit(int id)
        {
            var product = productService.GetProductById<ProductEditInputModel>(id);
            product.Categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();
                return this.View(input);
            }

            input.Id = id;
            await this.productService.UpdateAsync(input);
            return this.RedirectToAction(nameof(this.Details), new { input.Id });
        }


        [Authorize(Roles = GlobalConstants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.productService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = GlobalConstants.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
