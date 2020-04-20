﻿namespace Boxty.Controllers.Api
{
    using Boxty.Services;
    using Boxty.ViewModels;
    using Boxty.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<ShoppingCartViewModel> GetItemsFromCart()
        {
            var cart = await shoppingCartService.GetShoppingCart();

            return new ShoppingCartViewModel { Items = cart.Items, Total = cart.Total };
        }

        [HttpDelete]
        [AllowAnonymous]
        public void RemoveItem(int id)
        {
            shoppingCartService.RemoveFromCart(id);
        }

        [HttpPost]
        public void AddComment(AddCommentInputModel model)
        {
            shoppingCartService.AddComment(model.ProductId, model.Comment);
        }

    }

}
