using AutoMapper;
using Boxty.Extensions;
using Boxty.Models;
using Boxty.Services;
using Boxty.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IMapper mapper;
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IMapper mapper, IShoppingCartService shoppingCartService)
        {
            this.mapper = mapper;
            this.shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetItemsFromCart()
        {
            var items = await shoppingCartService.GetItemsFromCart();

            return items;
        }


        [HttpPost]
        public void AddToCart(int productId)
        {
            shoppingCartService.AddToCart(productId);
        }

    }
}
