namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Http;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IDeletableEntityRepository<BaseOrder> baseOrderRepository;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IDeletableEntityRepository<Order> orderRepository, IDeletableEntityRepository<BaseOrder> baseOrderRepository, IUserService userService, IMapper mapper, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.orderRepository = orderRepository;
            this.baseOrderRepository = baseOrderRepository;
            this.userService = userService;
            this.mapper = mapper;
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<OrderOutputModel> CurrentOrders()
        {
            return baseOrderRepository.All().To<OrderOutputModel>().OrderByDescending(x => x.CreatedOn);
        }

        public IEnumerable<OrderOutputModel> CurrentOrders()
        {
            return baseOrderRepository.All().To<OrderOutputModel>().OrderByDescending(x => x.CreatedOn);
        }

        public void CreateOrder(Order order, BaseOrder[] items)
        {
            context.Orders.Add(order);
            context.SaveChanges();

            context.OrderDetails.AddRange(CreateOrderDetail(order, order.Status, items));
            context.SaveChanges();
        }

        private BaseOrder[] CreateOrderDetail(Order order, string status, BaseOrder[] items)
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

            return items;
        }

        public void MarkAsDone(int productId, int orderId)
        {
            // var product = context.OrderDetails.First(
            //    s => s.ProductId == productId && s.OrderId == orderId);

            // product.Status = GlobalConstants.DeliveringStatus;
            // context.SaveChanges();
        }

        public void RemoveFromOrders(int productId, int orderId)
        {
            // var product = context.OrderDetails.First(
            //    s => s.ProductId == productId && s.OrderId == orderId);

            // product.Status = GlobalConstants.RemovedStatus;
            // context.SaveChanges();
        }
    }
}
