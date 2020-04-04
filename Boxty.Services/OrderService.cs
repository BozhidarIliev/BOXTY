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
        private readonly BoxtyDbContext context;
        private readonly ShoppingCart shoppingCart;
        private readonly IMapper mapper;
        private readonly IProductService productRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(BoxtyDbContext context, ShoppingCart shoppingCart, IMapper mapper, IProductService productRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.shoppingCart = shoppingCart;
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<OrderOutputModel> AllOrders()
        {
            List<OrderOutputModel> orders = new List<OrderOutputModel>();
            
            foreach (var order in context.Orders.Where(x => x.Status == GlobalConstants.SentStatus))
            {
                foreach (var detail in context.OrderDetails.Where(x => x.OrderId == order.Id).Where(x => x.Status != GlobalConstants.DeliveringStatus))
                {
                    var model = new OrderOutputModel
                    {
                        OrderId = order.Id,
                        Date = detail.Date,
                        Comment = detail.Comment,
                        Product = productRepository.GetProductById(detail.ProductId)
                    };

                    orders.Add(model);
                }
            }
            
            return orders.OrderByDescending(x=>x.Date);
        }

        public void CreateOrder(Order order)
        {
            order.Date = DateTime.Now;
            order.SenderId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            order.Delivery = "false";
            order.Status = GlobalConstants.SentStatus;

            context.Orders.Add(order);
            context.SaveChanges();
            CreateOrderDetail(order);
        }

        private void CreateOrderDetail(Order order)
        {
            var shoppingCartItems = shoppingCart.Items;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                for (int i = 0; i < shoppingCartItem.Amount;i++)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = shoppingCartItem.Product.Id,
                        Comment = shoppingCartItem.Comment,
                        Date = DateTime.Now,
                        Status = GlobalConstants.SentStatus
                    };
                    orderDetails.Add(orderDetail);
                }
                context.OrderDetails.AddRange(orderDetails);
            }
            context.SaveChanges();
        }

        public void MarkAsDone(int productId, int orderId)
        {
            var product = context.OrderDetails.First(
                s => s.ProductId == productId && s.OrderId == orderId);

            product.Status = GlobalConstants.DeliveringStatus;
            context.SaveChanges();
        }

        public void RemoveFromOrders(int productId, int orderId)
        {
            var product = context.OrderDetails.First(
                s => s.ProductId == productId && s.OrderId == orderId);

            product.Status = GlobalConstants.RemovedStatus;
            context.SaveChanges();
        }
    }

}