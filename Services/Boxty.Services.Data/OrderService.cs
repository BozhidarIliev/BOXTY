namespace Boxty.Services
{
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

        public IEnumerable<OrderOutputModel> CurrentOrders()
        {
            //return orderRepository.All().To<OrderOutputModel>().OrderByDescending(x => x.CreatedOn);
            return null;
        }

        public async Task CreateOrder(Order order, IEnumerable<OrderItem> items)
        {
            await orderRepository.AddAsync(order);

            await orderItemService.CreateOrderItem(order, order.Status, items);
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

        public async Task<IQueryable> GetOrderByDestination(string destination)
        {
            var list = await this.orderRepository.AllAsync();
            return list.Where(x => x.Destination == destination);
        }
    }
}
