namespace Boxty.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Common;
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
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;

        public OrderItemController(IOrderService orderService, IOrderItemService orderItemService)
        {
            this.orderService = orderService;
            this.orderItemService = orderItemService;
        }

        [HttpGet]
        [Authorize(Roles ="kitchenStaff,manager,admin")]
        public IEnumerable<Web.ViewModels.OrderItemOutputModel> GetKitchenOrderItems()
        {
            return orderItemService.GetKitchenOrderItems<Web.ViewModels.OrderItemOutputModel>();
        }

        [HttpPost]
        [Route("MarkAsDone")]
        public async Task MarkAsReady(int orderItemId)
        {
            await orderItemService.MarkAsReady(orderItemId);
        }

        [HttpPost]
        [Route("MarkAsServed")]
        public async Task MarkAsDone(int orderItemId)
        {
            await orderItemService.MarkAsDone(orderItemId);
        }
    }
}
