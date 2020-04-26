namespace Boxty.Controllers
{
    using Boxty.Services;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UsersController : Controller
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        // [HttpGet]
        // [AllowAnonymous]
        // public IActionResult Login()
        // {
        //    if (this.User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        // return this.View();
        // }

        // [HttpPost]
        // [AllowAnonymous]
        // public IActionResult Login(LoginInputModel loginModel)
        // {
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(loginModel);
        //    }

        // var result = this.UserService.LogUser(loginModel);

        // if (result != SignInResult.Success)
        //    {
        //        this.ViewData[GlobalConstants.ModelError] = GlobalConstants.LoginError;
        //        return this.View(loginModel);
        //    }

        // return RedirectToAction("Index", "Home");
        // }

        // [HttpGet]
        // [AllowAnonymous]
        // public IActionResult Register()
        // {
        //    if (this.User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        // return this.View();
        // }

        // [HttpPost]
        // [AllowAnonymous]
        // public IActionResult Register(RegisterInputModel registerModel)
        // {
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(registerModel);
        //    }

        // var result = this.UserService.RegisterUser(registerModel).Result;
        //    if (result != SignInResult.Success)
        //    {
        //        this.ViewData[GlobalConstants.ModelError] = string.Format(GlobalConstants.UserNameNotUnique, registerModel.UserName);
        //        return this.View(registerModel);
        //    }

        // return RedirectToAction("Index", "Home");
        // }

        // [HttpGet]
        // public IActionResult Logout()
        // {
        //    this.UserService.Logout();

        // return RedirectToAction("Index", "Home");
        // }

        [HttpGet]
        public IActionResult UpdateShippingInfo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateShippingInfo(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Users/UpdateShippingInfo");
            }

            if (!this.UserService.UpdateShippingInfo(model).IsCompletedSuccessfully)
            {
                return RedirectToAction("UpdateShippingInfo", "Users");
            }

            return RedirectToAction("Checkout", "ShoppingCart"); // CheckoutComplete
        }
    }
}
