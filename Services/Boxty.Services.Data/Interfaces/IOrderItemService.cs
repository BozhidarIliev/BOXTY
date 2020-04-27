using Boxty.Data.Models;
using Boxty.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boxty.Services.Data.Interfaces
{
    public interface IOrderItemService
    {
        Task CreateOrderItem(Order order);
        IEnumerable<T> GetOrderItems<T>();

        Task MarkAsDone(int orderItemId);

        Task MarkAsServed(int orderItemId);

        Task DeleteOrderItem(int orderItemId);

        OrderItem GetOrderItemById(int orderItemId);

        IEnumerable<T> GetCurrentOrderItemsByOrderId<T>(int orderId);

        void UpdateOrderItem(int orderId,int tableId, IEnumerable<OrderItem> items);
    }
}
