namespace Boxty.Controllers
{
    using System.Diagnostics;

    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult HttpError(int statusCode)
        {
            return this.View(statusCode);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ActionCompleted()
        {
            return this.View();
        }
    }
}
