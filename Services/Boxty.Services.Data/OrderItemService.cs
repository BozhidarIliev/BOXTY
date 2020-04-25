﻿using Boxty.Common;
using Boxty.Data.Common.Repositories;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Interfaces;
using Boxty.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Services.Data
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IDeletableEntityRepository<OrderItem> orderItemRepository;

        public OrderItemService(IUserService userService, IProductService productService, IDeletableEntityRepository<OrderItem> orderItemRepository)
        {
            this.userService = userService;
            this.productService = productService;
            this.orderItemRepository = orderItemRepository;
        }

        public IEnumerable<T> GetCurrentOrderItems<T>()
        {
            var items = orderItemRepository.All();
            return items.To<T>();
        }

        public IEnumerable<T> GetCurrentOrderItemsByOrderId<T>(int orderId)
        {
            var items = orderItemRepository.All().Where(x => x.OrderId == orderId);
            return items.To<T>();
        }

        public async Task CreateOrderItem(Order order)
        {
            var id = order.Id;
            var items = order.Items.ToList();
            foreach (var item in items)
            {
                item.OrderId = id;
            }

            await orderItemRepository.AddRangeAsync(items);
            await orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsDone(int orderItemId)
        {
            var orderItem = this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.ReadyForServing;
            await this.orderItemRepository.SaveChangesAsync();
        }

        public async Task MarkAsServed(int orderItemId)
        {
            var orderItem = this.GetOrderItemById(orderItemId);
            orderItem.Status = GlobalConstants.Served;
            await this.orderItemRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            var orderItem = this.GetOrderItemById(orderItemId);
            this.orderItemRepository.HardDelete(orderItem);
            await this.orderItemRepository.SaveChangesAsync();
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return this.orderItemRepository.All().FirstOrDefault(x => x.Id == orderItemId);
        }
    }
}
