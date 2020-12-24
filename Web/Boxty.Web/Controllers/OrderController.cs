namespace Boxty.Controllers
{
    using Boxty.Common;
    using Boxty.Services;
    using Boxty.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public OrderController(IOrderService orderService, IShoppingCartService shoppingCartService, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.orderService = orderService;
            this.shoppingCartService = shoppingCartService;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return this.View(orderService.GetAllOrders());
        }

        public IActionResult MarkAsDone(int id)
        {
            this.orderService.MarkAsDone(id);
            return this.RedirectToAction("Index");
        }
    }
}
