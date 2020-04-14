namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

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
