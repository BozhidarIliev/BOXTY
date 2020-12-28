namespace Boxty.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Web.ViewModels;

    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();

        IEnumerable<Order> GetAllOrdersWithDeleted();

        Task CreateOrder(Order order);

        Task MarkAsCompleted(int orderId);

        Task<Order> GetOrderByIdAsync(int orderId);

        Order GetOrderByDestination(string destination);

        Task UpdateOrder(int orderId, IEnumerable<Models.OrderItem> items);

        Task DeleteOrder(int orderId);
    }
}
