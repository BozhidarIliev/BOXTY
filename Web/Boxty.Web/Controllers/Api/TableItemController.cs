namespace Boxty.Controllers.Api
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.ViewModels;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TableItemController : Controller
    {
        private readonly BoxtyDbContext context;
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly ITableItemService tableItemService;

        public TableItemController(IOrderService orderService, IOrderItemService orderItemService, IUserService userService, IProductService productService,  ITableItemService tableItemService)
        {
            this.orderService = orderService;
            this.orderItemService = orderItemService;
            this.userService = userService;
            this.productService = productService;
            this.tableItemService = tableItemService;
        }

        [HttpGet("{id}")]
        [Route("GetTableItems")]
        public async Task<IEnumerable<TableItemViewModel>> GetTableItems(int tableId)
        {
            var items = await orderItemService.GetCurrentOrderItems<TableItemViewModel>();
            return items.Where(x => x.Destination == tableId.ToString());
        }

        [HttpGet("{id}")]
        [Route("GetPendingItems")]
        public async Task<IEnumerable<TableItemViewModel>> GetPendingItems(int tableId)
        {
            return await tableItemService.GetPendingItems<TableItemViewModel>(tableId);
        }

        [HttpPost]
        public async Task AddPendingItem(TableItemInputModel model)
        {
            await tableItemService.AddPendingItem(model.TableId, model.ProductId);
        }

        [HttpPost("{id}")]
        public async Task SendOrderItems(int tableId)
        {
            var items = await tableItemService.GetPendingItems<OrderItem>(tableId);
            Order order = new Order
            {
                Status = GlobalConstants.SentByWaiter,
                Destination = tableId.ToString(),
                Delivery = GlobalConstants.No,
            };
            await this.orderService.CreateOrder(order, items);
        }

        [HttpDelete]
        public async Task RemovePendingItem(TableItemInputModel model)
        {
            await tableItemService.RemovePendingItem(model.TableId, model.ProductId);
        }



    }
}
