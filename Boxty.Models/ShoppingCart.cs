using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Models
{
    public class ShoppingCart : IShoppingCart
    {
        public ShoppingCart()
        {

        }
        public string Id { get; set; }
        public ICollection<ShoppingCartItem> Items { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart() { Id = cartId };
        }
    }
}
