namespace Boxty.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Data;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpContext httpContext;
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly UserManager<BoxtyUser> userManager;

        public ShoppingCartService(IUserService userService, IOrderService orderService, IHttpContextAccessor httpContextAccessor, IProductService productService, UserManager<BoxtyUser> userManager)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.userService = userService;
            this.orderService = orderService;
            this.productService = productService;
            this.userManager = userManager;
        }

        public async Task<ShoppingCart> GetShoppingCart()
        {
            var cart = await SessionHelper.GetObjectFromJsonAsync<ShoppingCart>(httpContext.Session, GlobalConstants.ShoppingCart);
            if (cart == null)
            {
                cart = new ShoppingCart();
                await SessionHelper.SetObjectAsJsonAsync(httpContext.Session, GlobalConstants.ShoppingCart, cart);
            }

            return cart;
        }

        public async Task AddToCart(int productId)
        {
            var cart = await this.GetShoppingCart();
            var product = productService.GetProductById(productId);
            var shoppingCartItem = cart.Items.FirstOrDefault(x => (x.Product.Id == product.Id) && (x.Comment == string.Empty));

            if (shoppingCartItem != null)
            {
                shoppingCartItem.Amount++;
            }
            else 
            {
                cart.Items = cart.Items.Concat(new List<OrderItemOutputModel> { 
                    new OrderItemOutputModel { Product = product, Amount = 1 } });
            }

            await SessionHelper.SetObjectAsJsonAsync(httpContext.Session, GlobalConstants.ShoppingCart, cart);
        }


        public async Task<ShoppingCart> RemoveFromCart(int index)
        {
            var cart = await this.GetShoppingCart();
            var shoppingCartItems = cart.Items.ToList();
            var shoppingCartItem = shoppingCartItems[index];

            if (shoppingCartItem != null && shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                shoppingCartItems.Remove(shoppingCartItem);
            }

            cart.Items = shoppingCartItems;
            await SessionHelper.SetObjectAsJsonAsync(httpContext.Session, GlobalConstants.ShoppingCart, cart);

            return cart;
        }


        public async Task AddComment(AddCommentViewModel model)
        {
            var cart = await this.GetShoppingCart();
            var shoppingCartItems = cart.Items.ToList();
            var item = shoppingCartItems[model.ItemIndex];

            if (item != null)
            {
                cart = await this.RemoveFromCart(model.ItemIndex);
                shoppingCartItems = cart.Items.ToList();

                shoppingCartItems.Add(new OrderItemOutputModel { Product = item.Product, Amount = 1, Comment = model.Comment,});

                cart.Items = shoppingCartItems;
                await SessionHelper.SetObjectAsJsonAsync(httpContext.Session, GlobalConstants.ShoppingCart, cart);
            }
        }

        public async Task CreateOrder()
        {
            var cart = await this.GetShoppingCart();
            var shoppingCartItems = cart.Items.AsQueryable().To<OrderItem>();

            Order order = new Order
            {
                Status = GlobalConstants.Sent,
                Destination = userService.GetCurrentUser().Address,
                Delivery = true,
                Items = shoppingCartItems
            };
            await orderService.CreateOrder(order);
        }

        public void ClearCart()
        {
            httpContext.Session.Remove(GlobalConstants.ShoppingCart);
        }
    }
}
