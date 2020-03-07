using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boxty.Data;
using Boxty.Data.Repositories;
using Boxty.Models;
using Boxty.Services;
using Boxty.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Boxty.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository productRepository;
        private ShoppingCart shoppingCart;
        private IShoppingCartService service;

        // need to get ShoppingCartMethod

        // service methods
        // GetCart
        // AddToCart
        // RemoveFromCart
        // GetCartItems
        // ClearCart
        // GetCartTotal
        public ShoppingCartController(IShoppingCartService service, IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            this.service = service;
            this.productRepository = productRepository;
            this.shoppingCart = shoppingCart;
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

        public RedirectToActionResult RemoveFromCart(int productId)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.Id == productId);

            if (product != null)
            {
                service.RemoveFromCart(shoppingCart.Id, product, 1);
            }
            return RedirectToAction("Index");
        }
    }
}