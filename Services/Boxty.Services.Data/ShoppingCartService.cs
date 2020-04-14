namespace Boxty.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Data;
    using Boxty.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpContext httpContext;
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly UserManager<BoxtyUser> userManager;
        private readonly IMapper mapper;

        public ShoppingCartService(IUserService userService, IOrderService orderService, IHttpContextAccessor httpContextAccessor, IProductService productService, UserManager<BoxtyUser> userManager, IMapper mapper)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.userService = userService;
            this.orderService = orderService;
            this.productService = productService;
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Product>> GetItemsFromCart()
        {
            var cart = SessionHelper.GetObjectFromJson<List<BaseOrder>>(httpContext.Session, "cart");
            var result = mapper.Map<List<Product>>(cart);
            return result;
        }

        public void AddToCart(int productId)
        {
            List<BaseOrder> cart = SessionHelper.GetObjectFromJson<List<BaseOrder>>(httpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<BaseOrder>();
            }

            cart.Add(new BaseOrder { Product = productService.GetProductById(productId) });
            SessionHelper.SetObjectAsJson(httpContext.Session, "cart", cart);
        }

        public void RemoveFromCart(int productId)
        {
            List<BaseOrder> cart = SessionHelper.GetObjectFromJson<List<BaseOrder>>(httpContext.Session, "cart");
            var product = cart.FirstOrDefault(x => x.Product.Id == productId);
            cart.Remove(product);
            SessionHelper.SetObjectAsJson(httpContext.Session, "cart", cart);
        }

        public void ClearCart()
        {
            httpContext.Session.Remove("cart");
        }

        public void CreateOrder()
        {
            BaseOrder[] cart = SessionHelper.GetObjectFromJson<BaseOrder[]>(httpContext.Session, "cart");
            var user = userService.GetCurrentUser();
            Order order = new Order
            {
                Status = GlobalConstants.SentOnlineStatus,
                SenderId = user.Id,
                Destination = user.Address,
            };
            orderService.CreateOrder(order, cart);
        }
    }
}
