using Boxty.Models;
using Boxty.Models.Repositories;
using Boxty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boxty.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCartService shoppingCartService; 
        private readonly ShoppingCart shoppingCart;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public OrderController(IOrderRepository orderRepository,IShoppingCartService shoppingCartService, ShoppingCart shoppingCart, IHttpContextAccessor httpContextAccessor, IUserService userService)
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

        
        //[Authorize]
        

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            return View();
        }

        public IActionResult MarkAsDone(int productId)
        {
            return null;
        }
    }
}