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

        public IEnumerable<T> GetKitchenOrderItems<T>()
        {
            return orderItemRepository.All().Where(x => x.Status == GlobalConstants.Sent).To<T>();
        }

        public IEnumerable<OrderItem> GetCurrentOrderItemsByOrderId(int orderId)
        {
            var items = orderItemRepository.All().Where(x => x.OrderId == orderId);

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

        public async Task MarkAsSentByOrderId(int orderId)
        {
            foreach (var orderItem in this.GetCurrentOrderItemsByOrderId(orderId))
            {
                orderItem.Status = GlobalConstants.Sent;
            }

            await orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsCompleted(int orderItemId)
        {
            var orderItem = this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.Completed;
            orderItemRepository.Update(orderItem);
            await orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsCompletedByOrderId(int orderId)
        {
            var orderItems = this.GetOrderItemsByOrderId(orderId);
            foreach (var item in orderItems)
            {
                var orderItem = this.GetOrderItemById(item.Id);
                orderItem.Status = GlobalConstants.Completed;
                orderItemRepository.Update(orderItem);
            }

            await DeleteOrderItemsByOrderId(orderId);
        }

        public async Task DeleteOrderItemsByOrderId(int orderId)
        {
            var items = this.GetOrderItemsByOrderId(orderId);
            foreach (var item in items)
            {
                var orderItem = GetOrderItemById(item.Id);
                orderItemRepository.Delete(orderItem);
            }
            await orderItemRepository.SaveChangesAsync();
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return this.orderItemRepository.AllWithDeleted().FirstOrDefault(x => x.Id == orderItemId);
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return orderItemRepository.All().Where(x => (x.OrderId == orderId) && (x.IsDeleted == false));
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

        public async Task DeleteOrderItem(int orderItemId)
        {
            var orderItem = GetOrderItemById(orderItemId);
            orderItemRepository.Delete(orderItem);
            await orderItemRepository.SaveChangesAsync();
        }
    }
}
