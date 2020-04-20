namespace Boxty.Controllers.Api
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Data;
    using Boxty.Services;
    using Boxty.Services.Interfaces;
    using Boxty.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TableItemController : Controller
    {
        private readonly BoxtyDbContext context;
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ITableItemService tableItemService;

        public TableItemController(IOrderService orderService, IUserService userService, IProductService productService, IMapper mapper, ITableItemService tableItemService)
        {
            this.userService = userService;
            this.productService = productService;
            this.mapper = mapper;
            this.tableItemService = tableItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableItemViewModel[]>> GetTableItems(int tableId)
        {
            return await tableItemService.GetTableItems(tableId);
        }

        [HttpPost]
        public async Task<JsonResult> SelectTableItem(TableItemInputModel model)
        {
            var a = new { Success = await tableItemService.SelectTableItem(model) };
            return Json(a);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSelectedItem(int tableItemId)
        {
            return Json(await tableItemService.DeleteSelectedItem(tableItemId));
        }

        [HttpPost("{id}")]
        public void OrderSelectedItems(int tableId)
        {
            tableItemService.OrderSelectedItems(tableId);
        }
    }
}
