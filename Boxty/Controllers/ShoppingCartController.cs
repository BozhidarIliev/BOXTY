using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boxty.Data;
using Boxty.Data.Repositories;
using Boxty.Models;
using Boxty.Models.Repositories;
using Boxty.Services;
using Boxty.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Boxty.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ShoppingCart shoppingCart;
        private readonly IShoppingCartService service;
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderRepository orderRepository;

        // need to get ShoppingCartMethod

        // service methods
        // GetCart
        // AddToCart
        // RemoveFromCart
        // GetCartItems
        // ClearCart
        // GetCartTotal
        public ShoppingCartController(IShoppingCartService service, IProductRepository productRepository, ShoppingCart shoppingCart, IUserService userService, IShoppingCartService shoppingCartService, IOrderRepository orderRepository)
        {
            this.service = service;
            this.productRepository = productRepository;
            this.shoppingCart = shoppingCart;
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var items = service.GetCartItems(shoppingCart.Id);
            shoppingCart.Items = items;
            
            var model = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = service.GetCartTotal(shoppingCart.Id)
            };
            return View(model);
        }

        public IActionResult AddToCart(int productId)
        {
            Product product = productRepository.GetProductById(productId);

            if (product != null)
            {
                service.AddToCart(shoppingCart.Id, product, 1);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.Id == productId);

            if (product != null)
            {
                service.RemoveFromCart(shoppingCart.Id, product, 1);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout(Order order) 
        {
            if (userService.CheckCurrentUserBeforePurchase())
            {
                return RedirectToAction("UpdateShippingInfo", "Users");
            }

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
            return RedirectToAction("CheckoutComplete");

        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            return View();
        }
    }
}