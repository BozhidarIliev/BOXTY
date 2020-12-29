using Boxty.Common;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Data.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Sandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var test = new OrderItemServiceTests();
            test.CreateOrderItem();
        }
    }

    public class OrderItemServiceTests : BaseServiceTests
    {
        private IOrderItemService orderItemService => this.ServiceProvider.GetRequiredService<IOrderItemService>();
        private IOrderService orderService => this.ServiceProvider.GetRequiredService<IOrderService>();
        private IDriverService driverService => this.ServiceProvider.GetRequiredService<IDriverService>();

        public void CreateOrderItem()
        {

            var orderItem = new OrderItem
            {
                ProductId = 1,
                Comment = string.Empty,
            };

            var orderItem2 = new OrderItem
            {
                ProductId = 1,
                Comment = string.Empty,
            };

            var list = new List<OrderItem>();
            list.Add(orderItem);
            list.Add(orderItem2);

            Order order = new Order
            {
                Id = 1,
                Delivery = true,
                Items = list,
            };

            orderService.CreateOrder(order);

            var result = driverService.GetCurrentOrderItems();


        }
    }
}
