namespace Boxty.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route(GlobalConstants.DefaultApiRoute)]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;
        private readonly ITableService tableService;

        public OrderController(IOrderItemService orderItemService, IOrderService orderService, ITableService tableService)
        {
            this.orderItemService = orderItemService;
            this.orderService = orderService;
            this.tableService = tableService;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            var orders = orderService.GetAllOrders();
            foreach (var order in orders)
            {
                order.Items = orderItemService.GetAllCurrentOrderItemsByOrderId(order.Id);
            }

            return orders;
        }

        [HttpPost]
        [Route("EndOrder")]
        public async Task EndOrder(int tableId)
        {
            var order = orderService.GetOrderByDestination(tableId.ToString());
            await orderService.MarkAsDone(order.Id);
            await tableService.ChangeTableStatus(tableId);
        }
    }
}
