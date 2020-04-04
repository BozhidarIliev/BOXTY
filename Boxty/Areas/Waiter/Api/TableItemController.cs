using Boxty.Data;
using Boxty.Models;
using Boxty.Services;
using Boxty.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableItemController : Controller
    {
        private readonly BoxtyDbContext context;
        private readonly IUserService userService;
        private readonly IProductService productService;

        public TableItemController(BoxtyDbContext context, IUserService userService, IProductService productService)
        {
            this.context = context;
            this.userService = userService;
            this.productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableItem>>> GetAllTableItems()
        {
            return await context.TableItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TableItem>>> GetTableItems(int tableId)
        {
            var tableItems = await context.TableItems.Where(x=> x.TableId == tableId).Include(x => x.Product).ToListAsync();

            return tableItems;
        }

        [HttpPost]
        public async Task<JsonResult> SelectTableItem(TableItemInputModel model)
        {
            var tableId = model.TableId;
            var productId = model.ProductId;
            var list = context.TableItems.Where(x => x.TableId == tableId).Where(x => x.Product.Id == productId);
            if (list.Any())
            {
                context.TableItems.Where(x => x.TableId == tableId).Where(x => x.Product.Id == productId).First().Amount++;
            }
            else 
            {
                var tableItem = new TableItem
                {
                    Product = productService.GetProductById(model.ProductId),
                    Amount = 1,
                    Comment = model.Comment,
                    TableId = model.TableId,
                    WaiterId = userService.GetCurrentUser().Id
                };
                context.TableItems.Add(tableItem);
            }

            await context.SaveChangesAsync();
            var result = new { Success = "True"};
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSelectedItem(int tableItemId)
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
            var result = new { Success = "True" };
            return Json(result);
        }
    }
}
