using Boxty.Models;
using Boxty.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services
{
    public interface IShoppingCartService
    {
        Task<List<Product>> GetItemsFromCart();
        void AddToCart(int productId);
        void RemoveFromCart(int productId);
        void ClearCart();
        void CreateOrder();
    }
}
