namespace Boxty.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    public class OrderService : IOrderService
    {
        private readonly IOrderItemService orderItemService;
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IDeletableEntityRepository<OrderItem> orderItemRepository;

        public OrderService(IOrderItemService orderItemService, IDeletableEntityRepository<Order> orderRepository, IDeletableEntityRepository<OrderItem> orderItemRepository)
        {
            this.orderItemService = orderItemService;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return orderRepository.All();
        }

        public IEnumerable<Order> GetAllOrdersWithDeleted()
        {
            return orderRepository.AllWithDeleted();
        }

        public Order GetOrderByDestination(string destination)
        {
            var list = orderRepository.All();
            return list.Where(x => x.Destination == destination).FirstOrDefault(x => x.Status != GlobalConstants.Completed);
        }

        public async Task CreateOrder(Order order)
        {
            if (order.Delivery == true)
            {
                order.Status = GlobalConstants.Sent;
            }
            else
            {
                order.Status = GlobalConstants.Open;
            }
            orderRepository.AddAsync(order).Wait();

            await orderRepository.SaveChangesAsync();

            await orderItemService.CreateOrderItem(order);
        }

        public async Task MarkAsCompleted(int orderId)
        {
            var order = GetOrderByIdAsync(orderId).Result;
            if (order.Delivery == true)
            {
                order.Status = GlobalConstants.Delivered;
            }
            else
            {
                order.Status = GlobalConstants.Completed;
            }


            await orderItemService.MarkAsCompletedByOrderId(orderId);
            await this.DeleteOrder(orderId);

            await orderRepository.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await orderRepository.GetByIdWithDeletedAsync(orderId);
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await this.GetOrderByIdAsync(orderId);

            await orderItemService.DeleteOrderItemsByOrderId(orderId);
            orderRepository.Delete(order);

            await orderItemRepository.SaveChangesAsync();
        }

        public async Task UpdateOrder(int tableId, IEnumerable<OrderItem> items)
        {
            var order = GetOrderByDestination(tableId.ToString());
            await orderItemService.UpdateOrderItem(order.Id, tableId, items);
        }
    }
}
