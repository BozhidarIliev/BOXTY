namespace Boxty.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route(GlobalConstants.DefaultApiRoute)]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderItemService orderItemService;
        private readonly IOrderService orderService;
        private readonly ITableService tableService;
        private readonly ITableItemService tableItemService;

        public OrderController(IOrderItemService orderItemService, IOrderService orderService, ITableService tableService, ITableItemService tableItemService)
        {
            this.orderItemService = orderItemService;
            this.orderService = orderService;
            this.tableService = tableService;
            this.tableItemService = tableItemService;
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
        [Route("CompleteOrder")]
        public async Task CompleteOrder(int tableId)
        {
            var order = orderService.GetOrderByDestination(tableId.ToString());
            await orderService.MarkAsCompleted(order.Id);
            await tableService.CloseTable(tableId);
        }
    }
}
