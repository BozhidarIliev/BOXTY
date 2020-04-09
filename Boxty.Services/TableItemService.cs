//using AutoMapper;
//using Boxty.Data;
//using Boxty.Models;
//using Boxty.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Boxty.Services
//{
//    public class TableItemService
//    {
//        private readonly BoxtyDbContext context;
//        private readonly IMapper mapper;
//        private readonly IProductService productService;
//        private readonly IUserService userService;
//        private readonly IOrderService orderService;

//        public TableItemService(BoxtyDbContext context, IMapper mapper, IProductService productService, IUserService userService, IOrderService orderService)
//        {
//            this.context = context;
//            this.mapper = mapper;
//            this.productService = productService;
//            this.userService = userService;
//            this.orderService = orderService;
//        }
//        public async Task<TableItemOutputModel> GetTableItems(int tableId)
//        {
//            var tableItems = await context.TableItems.Where(x => x.TableId == tableId).ToArrayAsync();
//            var result = mapper.Map<TableItemViewModel[]>(tableItems);
//            var model = new TableItemOutputModel
//            {
//                TableId = tableId,
//                TableItems = result
//            };

//            return model;
//        }

//        public async Task<string> SelectTableItem(TableItemInputModel model)
//        {
//            var tableId = model.TableId;
//            var productId = model.ProductId;

//            var list = context.TableItems.Where(x => x.TableId == tableId).Where(x => x.Product.Id == productId);

            
//            if (!list.Any() || model.Comment != "")
//            {
//                var tableItem = new TableDetail
//                {
//                    Product = productService.GetProductById(productId),
//                    Amount = 1,
//                    Comment = model.Comment,
//                    TableId = model.TableId,
//                    WaiterId = userService.GetCurrentUser().Id,
//                    Status = "selectedProduct"
//                };
//                context.TableItems.Add(tableItem);
//            }
//            else 
//            {
//                list.First(x => x.Comment == "").Amount++;
//            }

//            await context.SaveChangesAsync();
//            return "True";
//        }

//        public async Task<string> DeleteSelectedItem(int tableItemId)
//        {
//            var tableItem = context.TableItems.First(x => x.Id == tableItemId);
//            if (tableItem.Amount > 1)
//            {
//                tableItem.Amount--;
//            }
//            else
//            {
//                context.TableItems.Remove(tableItem);
//            }

//            await context.SaveChangesAsync();
//            return "True";
//        }

//        public async Task<ActionResult> OrderSelectedItems(int tableId)
//        {
//            var tableItems = await this.GetTableItems(tableId);


//        }
//    }
//}
