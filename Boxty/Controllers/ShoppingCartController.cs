using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Boxty.Models;
using Boxty.Services;
using Boxty.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boxty.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderService orderService;
        private readonly SignInManager<BoxtyUser> signInManager;
        private bool isAuthenticated => User.Identity.IsAuthenticated;

        public ShoppingCartController(IMapper mapper, IProductService productService, IUserService userService, IShoppingCartService shoppingCartService, IOrderService orderRepository, SignInManager<BoxtyUser> signInManager)
        {
            this.mapper = mapper;
            this.productService = productService;
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
            this.orderService = orderRepository;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int productId)
        {
            shoppingCartService.AddToCart(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            shoppingCartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Checkout()
        {
            if (!isAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }

            if (userService.CheckCurrentUserBeforePurchase())
            {
                return RedirectToAction("UpdateShippingInfo", "Users");
            }

            var items = shoppingCartService.GetItemsFromCart().Result;
            if (items.Count() == 0)
            {
                ModelState.AddModelError("", "Your card is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                var result = mapper.Map<BaseOrder[]>(items);

                var user = userService.GetCurrentUser();
                Order order = new Order
                {
                    Status = GlobalConstants.SentOnlineStatus,
                    SentOn = DateTime.Now,
                    Sender = user.Id,
                    Destination = user.Address
                };
                orderService.CreateOrder(order, result);
                shoppingCartService.ClearCart();
            }
            return RedirectToAction("CheckoutComplete");

        }

    }
}