﻿namespace Boxty.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            this.orderItemService = orderItemService;
        }

        [HttpGet]
        public IEnumerable<OrderItemOutputModel> GetKitchenOrderItems()
        {
            var items = orderItemService.GetKitchenOrderItems<OrderItemOutputModel>();
            return items;
        }

        [HttpPost]
        [Route("MarkAsDone")]
        public async Task MarkAsDone(int orderItemId)
        {
            await orderItemService.MarkAsCompleted(orderItemId);
        }

        [HttpPost]
        [Route("MarkAsServed")]
        public async Task MarkAsServed(int orderItemId)
        {
            await orderItemService.MarkAsServed(orderItemId);
        }
    }
}
