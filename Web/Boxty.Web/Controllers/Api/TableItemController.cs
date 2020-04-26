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
        private readonly ITableService tableService;
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly ITableItemService tableItemService;

        public TableItemController(ITableService tableService, IOrderService orderService, IOrderItemService orderItemService, IUserService userService, IProductService productService,  ITableItemService tableItemService)
        {
            this.tableService = tableService;
            this.orderService = orderService;
            this.orderItemService = orderItemService;
            this.userService = userService;
            this.productService = productService;
            this.tableItemService = tableItemService;
        }

        [HttpGet("{id}")]
        [Route("GetTableItems")]
        public IEnumerable<TableItemViewModel> GetTableItems(int tableId)
        {
            var order = orderService.GetOrderByDestination(tableId.ToString());
            if (order == null)
            {
                return null;
            }

            var items = orderItemService.GetCurrentOrderItemsByOrderId<TableItemViewModel>(order.Id);
            return items;
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

        [HttpPost]
        [Route("SendOrder")]
        public async Task SendOrderItems(int id)
        {
            var items = await tableItemService.GetPendingItems<OrderItem>(id);
            if (items != null)
            {
                if (tableService.GetTableById(id).Available == true)
                {
                    await tableService.ChangeTableStatus(id);
                    Order order = new Order
                    {
                        Status = GlobalConstants.SentByWaiter,
                        Destination = id.ToString(),
                        Delivery = false,
                        Items = items,
                    };
                    orderService.CreateOrder(order);
                }
                else
                {
                    orderService.UpdateOrder(id, items);
                }

                await tableItemService.ClearPendingItems(id);
            }
        }

        [HttpDelete]
        public async Task RemovePendingItem(TableItemDeleteModel model)
        {
            await tableItemService.RemovePendingItem(model.TableId, model.ItemIndex);
        }

        [HttpPost]
        [Route("Comment")]
        public void AddComment(AddTableItemCommentInputModel model)
        {
            tableItemService.AddComment(model);
        }

    }
}
