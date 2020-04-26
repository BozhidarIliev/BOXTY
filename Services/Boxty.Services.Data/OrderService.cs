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
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IOrderItemService orderItemService, IDeletableEntityRepository<Order> orderRepository, IDeletableEntityRepository<OrderItem> orderItemRepository, IUserService userService, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.orderItemService = orderItemService;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.userService = userService;
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<T> CurrentOrders<T>()
        {
            return orderRepository.All().To<T>();
        }

        public void CreateOrder(Order order)
        {
            orderRepository.AddAsync(order);
            orderRepository.SaveChangesAsync().Wait();

            orderItemService.CreateOrderItem(order).Wait();
        }

        [Authorize(Roles = "waiter, admin, manager")]
        public async Task MarkAsDone(int orderId)
        {
            var order = await this.GetOrderById(orderId);
            order.Status = GlobalConstants.OrderCompleted;
            orderRepository.HardDelete(order);
            await orderRepository.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await this.orderRepository.GetByIdWithDeletedAsync(orderId);
        }

        public async Task DeleteOrderItem(int orderId)
        {
            var order = await this.GetOrderById(orderId);
            this.orderRepository.HardDelete(order);
            await this.orderItemRepository.SaveChangesAsync();
        }

        public Order GetOrderByDestination(string destination)
        {
            var list = this.orderRepository.All();
            return list.FirstOrDefault(x => x.Destination == destination);
        }

        public void UpdateOrder(int tableId, IEnumerable<OrderItem> items)
        {
            var order = GetOrderByDestination(tableId.ToString());
            orderItemService.UpdateOrderItem(order.Id, tableId, items);
        }
    }
}
