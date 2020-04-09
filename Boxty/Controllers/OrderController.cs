using Boxty.Models;
using Boxty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boxty.Controllers
{

    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public OrderController(IOrderService orderService,IShoppingCartService shoppingCartService, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.orderService = orderService;
            this.shoppingCartService = shoppingCartService;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService; 
        }

        public IActionResult Index()
        {
            return View(this.orderService.AllOrders());
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your order! :) ";
            return View();
        }
    
        [Authorize(Roles = GlobalConstants.Admin)]
        [HttpPost]
        public IActionResult MarkAsDone(int productId, int orderId)
        {
            orderService.MarkAsDone(productId, orderId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromOrders()
        {
            return null;
        }
    }
}