using AutoMapper;
using Boxty.Data;
using Boxty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxty.Services
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private BoxtyDbContext context;
        private ShoppingCart cart = new ShoppingCart();
        public ShoppingCartService(UserManager<BoxtyUser> userManager, BoxtyDbContext context, IMapper mapper) : base(userManager, context, mapper)
        {
            this.context = context;
        }

        public ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart() { Id = cartId };
        }

        public void AddToCart(string shoppingCartId, Product product, int amount)
        {
            var shoppingCartItem = context.ShoppingCartItems.SingleOrDefault(
                s => s.Product.Id == product.Id && s.ShoppingCartId == shoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCartId,
                    Product = product,
                    Amount = amount
                };

                context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            context.SaveChanges();
        }

        public int RemoveFromCart(string shoppingCartId, Product product, int amount)
        {
            var shoppingCartItem = context.ShoppingCartItems.First(
                s => s.Product.Id == product.Id && s.ShoppingCartId == shoppingCartId);

            var returnAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                { 
                    shoppingCartItem.Amount--;
                    amount = shoppingCartItem.Amount;
                }
                else
                {
                    context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            context.SaveChanges();

            return returnAmount;
        }

        public ICollection<ShoppingCartItem> GetCartItems(string shoppingCartId)
        {
            return context.ShoppingCartItems.Where(x => x.ShoppingCartId == shoppingCartId)
                .Include(s => s.Product)
                .ToList();
        }

        public void ClearCart(string shoppingCartId)
        {
            var cartItems = context
                .ShoppingCartItems
                .Where(x => x.ShoppingCartId == shoppingCartId);

            context.ShoppingCartItems.RemoveRange(cartItems);

            context.SaveChanges();
        }

        public decimal GetCartTotal(string shoppingCartId)
        {
            return context.ShoppingCartItems.Where(x => x.ShoppingCartId == shoppingCartId)
                .Select(x => x.Product.Price * x.Amount).Sum();
        }
    }
}
