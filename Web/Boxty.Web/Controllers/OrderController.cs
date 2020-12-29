namespace Boxty.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ITableService tableService;

        public OrderController(IOrderService orderService, ITableService tableService)
        {
            this.orderService = orderService;
            this.tableService = tableService;
        }


        public IActionResult Index(string filter)
        {
            var items = orderService.GetAllOrdersWithDeleted();
            switch (filter)
            {
                case GlobalConstants.All:
                    return this.View(items);
                case GlobalConstants.Sent:
                    return this.View(items.Where(x => x.Status == GlobalConstants.Sent));
                case GlobalConstants.Open:
                    return this.View(items.Where(x => x.Status == GlobalConstants.Open));
                case GlobalConstants.Delivering:
                    return this.View(items.Where(x => x.Status == GlobalConstants.Delivering));
                case GlobalConstants.Delivered:
                    return this.View(items.Where(x => x.Status == GlobalConstants.Delivered));
                case GlobalConstants.Completed:
                    return this.View(items.Where(x => x.Status == GlobalConstants.Completed));
                case GlobalConstants.Deleted:
                    return this.View(items.Where(x => x.IsDeleted == true));
                default: return this.View(items);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await orderService.DeleteOrder(id);
                var table = tableService.GetTableByOrderId(id);
                if (table != null)
                {
                    tableService.MakeAvailable(table.Id);
                }

            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
