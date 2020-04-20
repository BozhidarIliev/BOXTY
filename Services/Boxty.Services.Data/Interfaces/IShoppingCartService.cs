namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Models;

    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCart();

        Task AddToCart(int productId);

        Task RemoveFromCart(int productId);

        void ClearCart();

        Task CreateOrder(string address);

        Task AddComment(int productId, string comment);
    }
}
