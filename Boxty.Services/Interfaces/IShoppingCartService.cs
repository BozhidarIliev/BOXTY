using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Services
{
    public interface IShoppingCartService
    {
        ShoppingCart GetCart(IServiceProvider service);
        void AddToCart(string shoppingCartId, Product product, int amount);
        int RemoveFromCart(string shoppingCartId, Product product, int amount);
        ICollection<ShoppingCartItem> GetCartItems(string shoppingCartId);
        void ClearCart(string shoppingCartId);
        decimal GetCartTotal(string shoppingCartId);
    }
}
