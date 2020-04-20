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
    using Boxty.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpContext httpContext;
        private readonly IUserService userService;
        private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly UserManager<BoxtyUser> userManager;

        public ShoppingCartService(IUserService userService, IOrderItemService orderItemService, IOrderService orderService, IHttpContextAccessor httpContextAccessor, IProductService productService, UserManager<BoxtyUser> userManager)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.userService = userService;
            this.orderItemService = orderItemService;
            this.orderService = orderService;
            this.productService = productService;
            this.userManager = userManager;
        }

        public async Task<IQueryable<OrderItem>> GetItemsFromShoppingCart()
        {
            var cart = await this.GetShoppingCart();
            return cart.Items.AsQueryable();
        }

        public async Task<ShoppingCart> GetShoppingCart() // TODO httpContext should be passed as a parameter
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
            var shoppingCartItem = cart.Items.FirstOrDefault(x => (x.Product.Id == product.Id) && (x.Comment == null));

            if (shoppingCartItem != null)
            {
                shoppingCartItem.Amount++;
            }
            else 
            {
                cart.Items = cart.Items.Concat(new List<OrderItem> { 
                    new OrderItem { Product = product, Amount = 1 } });
            }

            await SessionHelper.SetObjectAsJsonAsync(httpContext.Session, GlobalConstants.ShoppingCart, cart);
        }

        public async Task RemoveFromCart(int productId)
        {
            var cart = await this.GetShoppingCart();
            var shoppingCartItems = cart.Items.ToList();
            var shoppingCartItem = shoppingCartItems.FirstOrDefault(x => x.Product.Id == productId);

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
        }

        public void ClearCart()
        {
            httpContext.Session.Remove(GlobalConstants.ShoppingCart);
        }

        public async Task CreateOrder(string address)
        {
            var cart = await this.GetShoppingCart();
            var shoppingCartItems = cart.Items.AsQueryable().To<OrderItem>().ToList();

            var user = userService.GetCurrentUser();
            Order order = new Order
            {
                Status = GlobalConstants.SentOnlineStatus,
                Destination = address,
            };
            await orderService.CreateOrder(order, shoppingCartItems);
        }

        public async Task AddComment(int productId, string comment)
        {
            var cart = await this.GetShoppingCart();
            var shoppingCartItems = cart.Items.ToList();
            var product = shoppingCartItems.FirstOrDefault(x => x.Product.Id == productId);

            if (product != null)
            {
                await this.RemoveFromCart(productId);

                product.Comment = comment;

                shoppingCartItems.Add(product);
                await SessionHelper.SetObjectAsJsonAsync(httpContext.Session, GlobalConstants.ShoppingCart, cart);
            }
        }

    }
}
