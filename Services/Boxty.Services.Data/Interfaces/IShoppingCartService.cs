namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Web.ViewModels;

    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCart();

        Task AddToCart(int productId);

        Task<ShoppingCart> RemoveFromCart(int index);

        void ClearCart();

        Task CreateOrder(string address);

        Task AddComment(AddCommentViewModel model);
    }
}
