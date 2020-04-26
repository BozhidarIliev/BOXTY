namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Web.ViewModels;

    public interface IOrderService
    {
        IEnumerable<T> CurrentOrders<T>();

        void CreateOrder(Order order);

        Task MarkAsDone(int orderId);

        Task<Order> GetOrderById(int orderId);

        Order GetOrderByDestination(string destination);

        void UpdateOrder(int orderId, IEnumerable<OrderItem> items);
    }
}
