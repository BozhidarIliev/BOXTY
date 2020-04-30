namespace Boxty.Controllers
{
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
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(productService.GetProducts<ProductViewModel>());
        }

        public IActionResult Details(int id)
        {
            var item = productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateInputModel item)
        {
            if (ModelState.IsValid)
            {
                await productService.AddProduct(item);
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductCreateInputModel item = new ProductCreateInputModel();
            item.Categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCreateInputModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    item.Categories = categoryService.GetAllCategories<CategoryDropDownViewModel>();
                    await productService.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return productService.GetProducts<ProductViewModel>().Any(x => x.Id == id);
        }
    }
}
