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

        public void CreateOrderItem()
        {
            var orderItem = new OrderItem
            {
                Id = 1,
                ProductId = 5,
                Comment = "1",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "2",
                OrderId = 1,
            };

            var order = new Order
            {
                Id = 1,
                Items = new List<OrderItem>
                {
                    orderItem,
                },
            };


            orderItemService.CreateOrderItem(order);
            orderItemService.MarkAsCompletedByOrderId(order.Id);

            var actual = orderItemService.GetOrderItemById(orderItem.Id);

        }
    }
}
