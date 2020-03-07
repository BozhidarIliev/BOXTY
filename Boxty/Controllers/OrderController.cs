using Boxty.Models;
using Boxty.Models.Repositories;
using Boxty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCartService shoppingCartService; 
        private readonly ShoppingCart shoppingCart;

        public OrderController(IOrderRepository orderRepository,IShoppingCartService shoppingCartService, ShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartService = shoppingCartService;
            this.shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            return View(this.orderRepository.AllOrders());
        }

        
        //[Authorize]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCartService.GetCartItems(shoppingCart.Id);
            shoppingCart.Items = items;
            if (shoppingCart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Your card is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(order);
                shoppingCartService.ClearCart(shoppingCart.Id);
            }
            return Redirect("/Home/Index");

        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            return View();
        }
    }
}