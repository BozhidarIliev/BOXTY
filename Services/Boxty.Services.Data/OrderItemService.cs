using Boxty.Common;
using Boxty.Data.Common.Repositories;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Interfaces;
using Boxty.Services.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Services.Data
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IDeletableEntityRepository<OrderItem> orderItemRepository;

        public OrderItemService(IDeletableEntityRepository<OrderItem> orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return orderItemRepository.All();
        }

        public IEnumerable<T> GetKitchenOrderItems<T>()
        {
            return orderItemRepository.All().Where(x => x.Status == GlobalConstants.Sent).To<T>();
        }

        public IEnumerable<T> GetOrderItems<T>()
        {
            return orderItemRepository.All().To<T>();
        }

        public IEnumerable<OrderItem> GetAllCurrentOrderItemsByOrderId(int orderId)
        {
            var items = orderItemRepository.All().Where(x => x.Id == orderId);

            return items.To<OrderItem>();
        }

        public IEnumerable<T> GetCurrentOrderItemsByOrderId<T>(int orderId)
        {
            var items = orderItemRepository.All().Where(x => x.OrderId == orderId);
            
            return items.To<T>();
        }

        public async Task CreateOrderItem(Order order)
        {
            var id = order.Id;
            var items = order.Items.ToList();
            foreach (var item in items)
            {
                item.OrderId = id;
                item.Status = GlobalConstants.Sent;
                item.Product = null;
            }

            await orderItemRepository.AddRangeAsync(items);
            await orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsReady(int orderItemId)
        {
            var orderItem = this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.Ready;
            orderItemRepository.Update(orderItem);
            await orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsDone(int orderItemId)
        {

            var orderItem = this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.Completed;
            orderItemRepository.Update(orderItem);
            await orderItemRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            var orderItem = this.GetOrderItemById(orderItemId);
            this.orderItemRepository.Delete(orderItem);
            await orderItemRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderItemsByOrderId(int orderId)
        {
            var items = orderItemRepository.All().Where(x => (x.Id == orderId) && (x.IsDeleted == false));
            foreach (var item in items)
            {
                orderItemRepository.Delete(item);
            }
            await orderItemRepository.SaveChangesAsync();
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return this.orderItemRepository.All().FirstOrDefault(x => x.Id == orderItemId);
        }

        public async Task UpdateOrderItem(int orderId, int tableId, IEnumerable<OrderItem> items)
        {
            var orderItems = orderItemRepository.All().Where(x => x.Id == orderId);
            if (items.Count() > 0)
            {
                var order = new Order
                {
                    Id = orderId,
                    Destination = tableId.ToString(),
                    Items = items,
                };
                await CreateOrderItem(order);
            }
        }
    }
}
