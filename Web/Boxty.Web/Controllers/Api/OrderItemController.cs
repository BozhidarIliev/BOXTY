namespace Boxty.Controllers.Api
{
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

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
        public IEnumerable<OrderOutputModel> GetOrders()
        {
            var orderItems = orderItemService.GetOrderItems<OrderOutputModel>().Where(x => (x.IsDeleted == false));
            var output = new List<OrderOutputModel>();
            foreach (var orderItem in orderItems)
            {
                for (int i = 0; i < orderItem.Amount; i++)
                {
                    output.Add(orderItem);
                }
            }

            return output;
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderItem[] orders)
        {
            // orderService.CreateOrder(order,orders);
            return null;
        }
    }
}
