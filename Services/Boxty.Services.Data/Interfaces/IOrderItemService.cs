using Boxty.Data.Models;
using Boxty.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boxty.Services.Data.Interfaces
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetAllOrderItems();

        IEnumerable<T> GetOrderItems<T>();

        IEnumerable<T> GetKitchenOrderItems<T>();

        Task MarkAsReady(int orderItemId);

        Task MarkAsDone(int orderItemId);

        Task CreateOrderItem(Order order);

        Task DeleteOrderItem(int orderItemId);

        OrderItem GetOrderItemById(int orderItemId);
        IEnumerable<OrderItem> GetAllCurrentOrderItemsByOrderId(int orderId);

        IEnumerable<T> GetCurrentOrderItemsByOrderId<T>(int orderId);

        Task DeleteOrderItemsByOrderId(int orderId);

        Task UpdateOrderItem(int orderId,int tableId, IEnumerable<OrderItem> items);
    }
}
