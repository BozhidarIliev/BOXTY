namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Common.Repositories;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TableService : ITableService
    {
        private readonly IDeletableEntityRepository<Table> tableRepositrory;

        public TableService(IDeletableEntityRepository<Table> tableRepositrory)
        {
            this.tableRepositrory = tableRepositrory;
        }

        public async Task<IEnumerable<Table>> GetTables()
        {
            return await tableRepositrory.All().ToListAsync();
        }
    }
}
