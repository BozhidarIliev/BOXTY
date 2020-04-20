﻿namespace Boxty.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services;
    using Boxty.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : Controller
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderService orderService;
        private readonly SignInManager<BoxtyUser> signInManager;

        public ShoppingCartController(IProductService productService, IUserService userService, IShoppingCartService shoppingCartService, IOrderService orderRepository, SignInManager<BoxtyUser> signInManager)
        {
            this.productService = productService;
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
            this.orderService = orderRepository;
            this.signInManager = signInManager;
        }

        private bool IsAuthenticated => User.Identity.IsAuthenticated;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            shoppingCartService.AddToCart(id);
            return Redirect("/product");
        }

        public IActionResult Remove(int productId)
        {
            shoppingCartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Checkout()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Login", "Identity");
            }

            if (userService.CheckCurrentUserBeforePurchase())
            {
                return RedirectToAction("UpdateShippingInfo", "Users");
            }

            var cart = await shoppingCartService.GetShoppingCart();
            var items = cart.Items;
            if (items.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "Your card is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                var user = userService.GetCurrentUser();
                Order order = new Order
                {
                    Status = GlobalConstants.SentOnlineStatus,
                    Destination = user.Address,
                };
                await orderService.CreateOrder(order, items);
                shoppingCartService.ClearCart();
            }

            return RedirectToAction("CheckoutComplete");
        }
    }
}
