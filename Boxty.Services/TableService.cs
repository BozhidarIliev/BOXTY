using Boxty.Data;
using Boxty.Models;
using Boxty.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services
{
    public class TableService : ITableService
    {
        private readonly BoxtyDbContext context;

        public TableService(BoxtyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Table>> GetTables() 
        {
            return await context.Tables.ToListAsync();
        }
    }

}
