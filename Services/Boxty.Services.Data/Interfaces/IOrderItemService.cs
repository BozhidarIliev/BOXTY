using Boxty.Data.Models;
using Boxty.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boxty.Services.Data.Interfaces
{
    public interface IOrderItemService
    {
        Task CreateOrderItem(Order order, string status, IEnumerable<OrderItem> items);

        // IOrderable Remove(int itemId, IOrderable orderable);
        Task<IEnumerable<T>> GetCurrentOrderItems<T>();

    }
}
