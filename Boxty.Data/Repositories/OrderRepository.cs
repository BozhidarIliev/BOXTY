using AutoMapper;
using Boxty.Data;
using Boxty.Models;
using Boxty.Models.Repositories;
using Boxty.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BoxtyDbContext context;
        private readonly ShoppingCart shoppingCart;
        private readonly IMapper mapper;

        public OrderRepository(BoxtyDbContext context, ShoppingCart shoppingCart, IMapper mapper)
        {
            this.context = context;
            this.shoppingCart = shoppingCart;
            this.mapper = mapper;
        }

        public IEnumerable<OrderOutputModel> AllOrders()
        {
            List<OrderOutputModel> orders = new List<OrderOutputModel>();
            
            foreach (var order in context.OrderDetails)
            {
                var model = new OrderOutputModel
                {
                    ProductId = order.ProductId,
                    SentTime = order.SentTime
                };
                orders.Add(model);
            }
            
            return orders;
        }

        public void CreateOrder(Order order)
        {
            order.SentTime = DateTime.Now;

            context.Orders.Add(order);
            context.SaveChanges();
            CreateOrderDetail(order);
        }

        private bool CreateOrderDetail(Order order)
        {
            var shoppingCartItems = shoppingCart.Items;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.Id,
                    OrderId = order.Id,
                    Price = shoppingCartItem.Product.Price,
                    SentTime = DateTime.Now
                };

                context.OrderDetails.Add(orderDetail);
            }
            return context.SaveChanges()>0;
        }
    }

}