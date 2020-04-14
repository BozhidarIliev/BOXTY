namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.Models;

    public interface IShoppingCartService
    {
        Task<List<Product>> GetItemsFromCart();

        void AddToCart(int productId);

        void RemoveFromCart(int productId);

        void ClearCart();

        void CreateOrder();
    }
}
