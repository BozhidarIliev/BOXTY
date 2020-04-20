namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Web.ViewModels;

    public interface IOrderService
    {
        IEnumerable<OrderOutputModel> CurrentOrders();

        Task CreateOrder(Order order, IEnumerable<OrderItem> items);

        Task MarkAsDone(int orderId);

        Task<Order> GetOrderById(int orderId);
    }
}
