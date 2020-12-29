using AutoMapper.QueryableExtensions;
using Boxty.Common;
using Boxty.Data.Common.Repositories;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace Boxty.Services.Data.Tests
{
    public class OrderItemServiceTests : BaseServiceTests
    {
        private IOrderItemService orderItemService => this.ServiceProvider.GetRequiredService<IOrderItemService>();

        [Fact]
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
                Status = GlobalConstants.Sent,
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

            var actual = orderItemService.GetOrderItemById(1);


            Assert.Equal(orderItem.Id, actual.Id);
            Assert.Equal(orderItem.ProductId, actual.ProductId);
            Assert.Equal(orderItem.Comment, actual.Comment);
            Assert.Equal(orderItem.CreatedBy, actual.CreatedBy);
            Assert.False(actual.IsDeleted);
            Assert.Equal(orderItem.OrderId, actual.OrderId);
            Assert.Equal(orderItem.Status, actual.Status);
        }

        [Fact]
        public void MarkAsSentByOrderId()
        {
            var orderItem = new OrderItem
            {
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
            orderItemService.MarkAsSentByOrderId(order.Id);

            var actual = orderItemService.GetOrderItemById(orderItem.Id);

            Assert.Equal(GlobalConstants.Sent, actual.Status);
        }

        [Fact]
        public void MarkAsCompleted()
        {
            var orderItem = new OrderItem
            {
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
            orderItemService.MarkAsCompleted(orderItem.Id);

            var actual = orderItemService.GetOrderItemById(orderItem.Id);

            Assert.Equal(GlobalConstants.Completed, actual.Status);
        }

        [Fact]
        public void MarkAsCompletedByOrderId()
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

            Assert.Equal(GlobalConstants.Completed, actual.Status);
        }

        [Fact]
        public void DeleteOrderItemsById()
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
            orderItemService.DeleteOrderItemsByOrderId(order.Id);

            var actual = orderItemService.GetOrderItemById(orderItem.Id);

            Assert.True(actual.IsDeleted);
        }

        [Fact]
        public void GetOrderItemById()
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

            var actual = orderItemService.GetOrderItemById(orderItem.Id);

            Assert.Equal(orderItem.Id, actual.Id);
            Assert.Equal(orderItem.IsDeleted, actual.IsDeleted);
        }

        [Fact]
        public void UpdateOrderItem()
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

            var orderItem2 = new OrderItem
            {
                Id = 2,
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
            var orderItems = new List<OrderItem>();
            orderItems.Add(orderItem2);

            orderItemService.CreateOrderItem(order);


            orderItemService.UpdateOrderItem(order.Id, 1, orderItems);

            var actual = orderItemService.GetOrderItemById(orderItem2.Id);

            Assert.Equal(orderItem2.Id, actual.Id);
            Assert.Equal(orderItem2.IsDeleted, actual.IsDeleted);
        }

        [Fact]
        public void DeleteOrderItem()
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

            var actual = orderItemService.GetOrderItemById(orderItem.Id);

            Assert.Equal(orderItem.Id, actual.Id);
            Assert.False(actual.IsDeleted);
        }
    }
}
