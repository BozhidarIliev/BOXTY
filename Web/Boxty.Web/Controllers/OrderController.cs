﻿namespace Boxty.Controllers
{
    using Boxty.Common;
    using Boxty.Services;
    using Boxty.Services.Interfaces;
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
            return this.View(this.orderService.CurrentOrders());
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        [HttpPost]
        public IActionResult MarkAsDone(int productId, int orderId)
        {
            this.orderService.MarkAsDone(productId, orderId);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromOrders()
        {
            return null;
        }
    }
}