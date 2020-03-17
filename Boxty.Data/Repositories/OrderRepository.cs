using AutoMapper;
using Boxty.Data;
using Boxty.Models;
using Boxty.Models.Repositories;
using Boxty.ViewModels.OutputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boxty.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BoxtyDbContext context;
        private readonly ShoppingCart shoppingCart;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderRepository(BoxtyDbContext context, ShoppingCart shoppingCart, IMapper mapper, IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
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
            
            foreach (var order in context.Orders)
            {
                foreach (var detail in context.OrderDetails.Where(x => x.OrderId == order.Id))
                {
                    var model = new OrderOutputModel
                    {
                        Date = detail.Date,
                        Comment = detail.Comment,
                        Product = productRepository.GetProductById(detail.ProductId)
                    };

                    orders.Add(model);
                }
            }
            
            return orders;
        }

        public void CreateOrder(Order order)
        {
            order.Date = DateTime.Now;
            order.SenderId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            context.Orders.Add(order);
            context.SaveChanges();
            CreateOrderDetail(order);
        }

        public void AddOrderDetail(Order order)
        {

        }

        private void CreateOrderDetail(Order order)
        {
            var shoppingCartItems = shoppingCart.Items;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductId = shoppingCartItem.Product.Id,
                    Comment = shoppingCartItem.Comment,
                    Date = DateTime.Now
                };

                context.OrderDetails.Add(orderDetail);
            }
            context.SaveChanges();
        }
    }

}