using Boxty.Models;
using Boxty.Services.Interfaces;
using Boxty.Services.Mapping;
using Boxty.Web.ViewModels;
using Boxty.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services.Data
{
    public class TableItemService : ITableItemService
    {
        private readonly IHttpContextAccessor context;
        private readonly IProductService productService;

        public TableItemService(IHttpContextAccessor context, IProductService productService)
        {
            this.context = context;
            this.productService = productService;
        }

        public async Task<IEnumerable<T>> GetPendingItems<T>(int tableId)
        {
            var items = await SessionHelper.GetObjectFromJsonAsync<IEnumerable<TableItemViewModel>>(context.HttpContext.Session, tableId.ToString());
            if (items == null)
            {
                items = new List<TableItemViewModel>();
                await SessionHelper.SetObjectAsJsonAsync(context.HttpContext.Session, tableId.ToString(), items);
            }

            return items.AsQueryable().To<T>();
        }

        public async Task AddPendingItem(int tableId, int productId)
        {
            var table = await GetPendingItems<TableItemViewModel>(tableId);
            var items = table.ToList();
            var item = items.FirstOrDefault(x => (x.ProductId == productId) && (x.Comment == string.Empty));

            if (item == null)
            {
                var product = productService.GetProductById(productId);
                items.Add(new TableItemViewModel { ProductId = productId, ProductName = product.Name, ProductPrice = product.Price, Amount = 1 });
            }
            else
            {
                item.Amount++;
            }
            table = items;
            await SessionHelper.SetObjectAsJsonAsync(context.HttpContext.Session, tableId.ToString(), table);

        }

        public async Task RemovePendingItem(int tableId, int itemIndex)
        {
            var table = await GetPendingItems<TableItemViewModel>(tableId);
            var items = table.ToList();
            var item = items[itemIndex];

            if (item.Amount > 1)
            {
                item.Amount--;
            }
            else
            {
                items.Remove(item);
            }
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

                item.Amount = 1;
                item.Comment = model.Comment;
                items.Add(item);
                SessionHelper.SetObjectAsJson(context.HttpContext.Session, model.TableId.ToString(), items);
            }
            else if (commentedItem != null)
            {
                commentedItem.Amount++;
                SessionHelper.SetObjectAsJson(context.HttpContext.Session, model.TableId.ToString(), items);
                await this.RemovePendingItem(model.TableId, model.ItemIndex);
            }
        }
    }
}
