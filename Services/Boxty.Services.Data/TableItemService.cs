using AutoMapper;
using Boxty.Common;
using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Interfaces;
using Boxty.Services.Mapping;
using Boxty.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Services.Data
{
    public class TableItemService : ITableItemService
    {
        private readonly IOrderItemService orderItemService;
        private readonly ITableService tableService;
        private readonly IOrderService orderService;
        private readonly IHttpContextAccessor context;
        private readonly IProductService productService;


        public TableItemService(
            IOrderItemService orderItemService,
            ITableService tableService,
            IOrderService orderService,
            IHttpContextAccessor context,
            IProductService productService)
        {
            this.orderItemService = orderItemService;
            this.tableService = tableService;
            this.orderService = orderService;
            this.context = context;
            this.productService = productService;
        }

        public IEnumerable<TableItemViewModel> GetTableItems(int tableId)
        {
            var order = orderService.GetOrderByDestination(tableId.ToString());
            if (order == null)
            {
                return null;
            }

            var items = orderItemService.GetCurrentOrderItemsByOrderId<TableItemViewModel>(order.Id).OrderBy(x => x.ModifiedOn);

            return items;
        }

        public async Task<IEnumerable<T>> GetPendingItems<T>(int tableId)
        {
            var items = await SessionHelper.GetObjectFromJsonAsync<IEnumerable<TableItemViewModel>>(context.HttpContext.Session, tableId.ToString());
            if (items == null)
            {
                items = new List<TableItemViewModel>();
                await SessionHelper.SetObjectAsJsonAsync(context.HttpContext.Session, tableId.ToString(), items);
            }

            return items.OrderBy(x => x.ModifiedOn).AsQueryable().To<T>();
        }

        public async Task AddPendingItem(int tableId, int productId)
        {
            var table = await GetPendingItems<TableItemViewModel>(tableId);
            var items = table.ToList();

            var product = productService.GetProductById(productId);
            items.Add(new TableItemViewModel { ProductId = productId, ProductName = product.Name, ProductPrice = product.Price });

            table = items.OrderBy(x => x.ModifiedOn);
            await SessionHelper.SetObjectAsJsonAsync(context.HttpContext.Session, tableId.ToString(), table);
        }

        public async Task RemovePendingItem(int tableId, int itemIndex)
        {
            var table = await GetPendingItems<TableItemViewModel>(tableId);
            var items = table.ToList();
            var item = items[itemIndex];

            items.Remove(item);
            table = items;
            await SessionHelper.SetObjectAsJsonAsync(context.HttpContext.Session, tableId.ToString(), table);

        }

        public async Task ClearPendingItems(int tableId)
        {
            await SessionHelper.SetObjectAsJsonAsync(context.HttpContext.Session, tableId.ToString(), new List<TableItemViewModel>());
        }

        public async Task AddComment(AddTableItemCommentInputModel model)
        {
            var table = await GetPendingItems<TableItemViewModel>(model.TableId);
            var items = table.ToList();
            var item = items[model.ItemIndex];
            var commentedItem = items.FirstOrDefault(x => (x.Comment == model.Comment) && (x.ProductId == item.ProductId));

            if (item != null && commentedItem == null)
            {
                await this.RemovePendingItem(model.TableId, model.ItemIndex);

                table = await GetPendingItems<TableItemViewModel>(model.TableId);
                items = table.ToList();

                item.Comment = model.Comment;
                items.Add(item);
                SessionHelper.SetObjectAsJson(context.HttpContext.Session, model.TableId.ToString(), items.OrderBy(x => x.ModifiedOn));
            }
            else if (commentedItem != null)
            {
                SessionHelper.SetObjectAsJson(context.HttpContext.Session, model.TableId.ToString(), items.OrderBy(x => x.ModifiedOn));
                await this.RemovePendingItem(model.TableId, model.ItemIndex);
            }
        }

        public async Task SendOrderItems(int id)
        {
            var items = await GetPendingItems<OrderItem>(id);;

            if (items != null)
            {
                if (tableService.GetTableById(id).Available == true)
                {
                    await tableService.MakeUnavailable(id);
                    Order order = new Order
                    {
                        Status = GlobalConstants.Sent,
                        Destination = id.ToString(),
                        Delivery = false,
                        Items = items,
                    };
                    await orderService.CreateOrder(order);
                }
                else
                {
                    await orderService.UpdateOrder(id, items);
                }

                await ClearPendingItems(id);
            }
        }
    }
}
