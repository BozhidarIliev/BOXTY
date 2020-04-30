namespace Boxty.Controllers
{
    using Boxty.Services;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class UsersController : Controller
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        [HttpGet]
        public IActionResult UpdateShippingInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShippingInfo(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Users/UpdateShippingInfo");
            }

            await this.UserService.UpdateShippingInfo(model);

            return RedirectToAction("Checkout", "ShoppingCart"); // CheckoutComplete
        }
    }
}
