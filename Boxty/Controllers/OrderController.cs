using Boxty.Models;
using Boxty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boxty.Controllers
{
    [Authorize(Roles = "admin,manager,employee,waiter")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderRepository;
        private readonly IShoppingCartService shoppingCartService; 
        private readonly ShoppingCart shoppingCart;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public OrderController(IOrderService orderRepository,IShoppingCartService shoppingCartService, ShoppingCart shoppingCart, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartService = shoppingCartService;
            this.shoppingCart = shoppingCart;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService; 
        }

        public IActionResult Index()
        {
            return View(this.orderRepository.AllOrders());
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your order! :) ";
            return View();
        }

        public IActionResult MarkAsDone(int productId)
        {
            return null;
        }
    }
}