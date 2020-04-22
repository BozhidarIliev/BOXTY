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
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IDeletableEntityRepository<OrderItem> orderItemRepository;

        public OrderItemService(IUserService userService, IProductService productService, IDeletableEntityRepository<OrderItem> orderItemRepository)
        {
            this.userService = userService;
            this.productService = productService;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<T>> GetCurrentOrderItems<T>()
        {
            var items = await orderItemRepository.AllAsync();
            return items.To<T>();
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return null;
        }

        public async Task CreateOrderItem(Order order, string status, IEnumerable<OrderItem> items)
        {
            var userId = userService.GetCurrentUser().Id;
            if (status == GlobalConstants.SentOnlineStatus)
            {
                foreach (var item in items)
                {
                    item.Status = status;
                    item.OrderId = order.Id;
                    item.Product = productService.GetProductById(item.Product.Id);
                }
            }

            await this.orderItemRepository.AddRangeAsync(items);
            await this.orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsDone(int orderItemId)
        {
            var orderItem = await this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.ReadyForServing;
            await this.orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsServed(int orderItemId)
        {
            var orderItem = await this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.Served;
            await this.orderItemRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            var orderItem = await this.GetOrderItemById(orderItemId);
            this.orderItemRepository.HardDelete(orderItem);
            await this.orderItemRepository.SaveChangesAsync();
        }

        public async Task<OrderItem> GetOrderItemById(int orderItemId)
        {
            return await this.orderItemRepository.GetByIdWithDeletedAsync(orderItemId);
        }

        //public  Remove(int productId, IOrderable orderable)
        //{
        //    var items = orderable.Items.ToList();
        //    var item = items.FirstOrDefault(x => x.Product.Id == productId);

        //    if (item != null && item.Amount > 1)
        //    {
        //        item.Amount--;
        //    }
        //    else
        //    {
        //        items.Remove(item);
        //    }

        //    return orderable;
        //}
    }
}
