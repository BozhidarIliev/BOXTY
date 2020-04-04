using Boxty.Data;
using Boxty.Models;
using Boxty.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly BoxtyDbContext context;
        private readonly IUserService userService;

        public TableController(BoxtyDbContext context, IUserService userService)
        {
            this.userService = userService;
            this.context = context;
        }

        // GET: api/SalesOrderLine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTables(int tableId)
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
