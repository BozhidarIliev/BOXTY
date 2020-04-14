namespace Boxty.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Boxty.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class TableItemService : ITableItemService
    {
        private readonly BoxtyDbContext context;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IOrderService orderService;

        public TableItemService(BoxtyDbContext context, IMapper mapper, IProductService productService, IUserService userService, IOrderService orderService)
        {
            this.context = context;
            this.mapper = mapper;
            this.productService = productService;
            this.userService = userService;
            this.orderService = orderService;
        }

        public async Task<TableItemViewModel[]> GetTableItems(int tableId)
        {
            var tableItems = await context.TableItems.Where(x => x.TableId == tableId).Include(x => x.Product).ToArrayAsync();
            var result = mapper.Map<TableItemViewModel[]>(tableItems);

            return result;
        }

        public async Task<string> SelectTableItem(TableItemInputModel model)
        {
            var tableId = model.TableId;
            var productId = model.ProductId;

            var list = context.TableItems.Where(x => x.TableId == tableId).Where(x => x.Product.Id == productId);

            if (!list.Any() || model.Comment != string.Empty)
            {
                var tableItem = new TableItem
                {
                    Product = productService.GetProductById(productId),
                    Amount = 1,
                    Comment = model.Comment,
                    TableId = model.TableId,
                    WaiterId = userService.GetCurrentUser().Id,
                    Status = "selectedProduct",
                };
                context.TableItems.Add(tableItem);
            }
            else
            {
                list.First(x => x.Comment == string.Empty).Amount++;
            }

            await context.SaveChangesAsync();
            return "True";
        }

        public async Task<string> DeleteSelectedItem(int tableItemId)
        {
            var tableItem = context.TableItems.First(x => x.Id == tableItemId);
            if (tableItem.Amount > 1)
            {
                tableItem.Amount--;
            }
            else
            {
                context.TableItems.Remove(tableItem);
            }

            await context.SaveChangesAsync();
            return "True";
        }

        public async void OrderSelectedItems(int tableId)
        {
            var tableItems = await this.GetTableItems(tableId);

            var orderItems = mapper.Map<BaseOrder[]>(tableItems);
            var order = new Order
            {
                Destination = tableId.ToString(),
                SenderId = userService.GetCurrentUser().Id,
                SentOn = DateTime.Now,
                Status = GlobalConstants.SentByWaiter,
            };

            orderService.CreateOrder(order, orderItems);
        }
    }
}
