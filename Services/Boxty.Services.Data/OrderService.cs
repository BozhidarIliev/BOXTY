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
        private readonly IDeletableEntityRepository<Models.OrderItem> orderItemRepository;
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IOrderItemService orderItemService, IDeletableEntityRepository<Order> orderRepository, IDeletableEntityRepository<Models.OrderItem> orderItemRepository, IUserService userService, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.orderItemService = orderItemService;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.userService = userService;
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return orderRepository.All();
        }


        public IEnumerable<T> GetOrders<T>()
        {
            return orderRepository.All().To<T>();
        }

        [Authorize(Roles = "waiter, admin, manager")]
        public async Task CreateOrder(Order order)
        {
            order.Status = GlobalConstants.Sent;
            orderRepository.AddAsync(order).Wait();
            await orderRepository.SaveChangesAsync();

            await orderItemService.CreateOrderItem(order);
        }

        public async Task MarkAsDone(int orderId)
        {
            var order = GetOrderById(orderId).Result;
            order.Status = GlobalConstants.OrderCompleted;
            await orderItemService.DeleteOrderItemsByOrderId(orderId);
            await orderRepository.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await orderRepository.GetByIdWithDeletedAsync(orderId);
        }

        public async Task DeleteOrderItem(int orderId)
        {
            var order = await this.GetOrderById(orderId);
            await orderItemService.DeleteOrderItemsByOrderId(orderId);
            orderRepository.Delete(order);
            await orderItemRepository.SaveChangesAsync();
        }

        public Order GetOrderByDestination(string destination)
        {
            var list = orderRepository.All();
            return list.FirstOrDefault(x => x.Destination == destination);
        }

        public async Task UpdateOrder(int tableId, IEnumerable<OrderItem> items)
        {
            var order = GetOrderByDestination(tableId.ToString());
            await orderItemService.UpdateOrderItem(order.Id, tableId, items);
        }
    }
}
