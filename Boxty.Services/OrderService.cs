using AutoMapper;
using Boxty.Data;
using Boxty.Models;
using Boxty.ViewModels.OutputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boxty.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUserService userService;
        private readonly BoxtyDbContext context;
        private readonly IMapper mapper;
        private readonly IProductService producService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(IUserService userService, BoxtyDbContext context, IMapper mapper, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.context = context;
            this.mapper = mapper;
            this.producService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<OrderOutputModel> AllOrders()
        {
            List<OrderOutputModel> orders = new List<OrderOutputModel>();
            
            foreach (var order in context.Orders.Where(x => x.Status == GlobalConstants.SentStatus))
            {
                foreach (var detail in context.OrderDetails.Where(x => x.Id == order.Id).Where(x => x.Status != GlobalConstants.DeliveringStatus))
                {
                    var model = mapper.Map<OrderOutputModel>(detail);
                    model.Product = producService.GetProductById(detail.Id);
                    
                    orders.Add(model);
                }
            }
            
            return orders.OrderByDescending(x=>x.Date);
        }

        public void CreateOrder(Order order, BaseOrder[] items)
        {
            context.Orders.Add(order);
            context.SaveChanges();

            context.OrderDetails.AddRange(CreateOrderDetail(order, order.Status, items));
            context.SaveChanges();
        }

        private BaseOrder[] CreateOrderDetail(Order order ,string status, BaseOrder[] items)
        {
            var userId = userService.GetCurrentUser().Id;
            if (status == GlobalConstants.SentOnlineStatus)
            {
                foreach (var item in items)
                {
                    item.Status = status;
                    item.SentOn = DateTime.Now;
                    item.OrderId = order.Id;

                }
            }
            return items;
        }

        public void MarkAsDone(int productId, int orderId)
        {
            //var product = context.OrderDetails.First(
            //    s => s.ProductId == productId && s.OrderId == orderId);

            //product.Status = GlobalConstants.DeliveringStatus;
            //context.SaveChanges();
        }

        public void RemoveFromOrders(int productId, int orderId)
        {
            //var product = context.OrderDetails.First(
            //    s => s.ProductId == productId && s.OrderId == orderId);

            //product.Status = GlobalConstants.RemovedStatus;
            //context.SaveChanges();
        }
    }

}