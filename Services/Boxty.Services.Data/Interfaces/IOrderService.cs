namespace Boxty.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Web.ViewModels;

    public interface IOrderService
    {
        IEnumerable<T> GetOrders<T>();

        void CreateOrder(Order order);

        Task MarkAsDone(int orderId);

        Task<Order> GetOrderById(int orderId);

        Order GetOrderByDestination(string destination);

        void UpdateOrder(int orderId, IEnumerable<OrderItem> items);
    }
}
