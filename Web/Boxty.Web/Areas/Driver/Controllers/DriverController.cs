namespace Boxty.Areas.Driver.Controllers
{
    using Boxty.Common;
    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.DriverArea)]
    public class DriverController : Controller
    {
        public DriverController()
        {

        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
