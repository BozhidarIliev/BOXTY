namespace Boxty.Areas.Waiter.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "waiter, admin")]
    [Area("Waiter")]
    public class WaiterController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Reservations()
        {
            return this.View();
        }
    }
}
