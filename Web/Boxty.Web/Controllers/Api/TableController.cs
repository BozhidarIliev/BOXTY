namespace Boxty.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.Data;
    using Boxty.Models;
    using Boxty.Services;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly BoxtyDbContext context;
        private readonly IUserService userService;
        private readonly ITableService tableService;

        public TableController(BoxtyDbContext context, IUserService userService, ITableService tableService)
        {
            this.userService = userService;
            this.tableService = tableService;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TableViewModel>> GetTables()
        {
            return await this.tableService.GetTables<TableViewModel>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int tableId)
        {
            var tables = await context.Tables.FindAsync(tableId);

            if (tables == null)
            {
                return NotFound();
            }

            return tables;
        }

    }
}
