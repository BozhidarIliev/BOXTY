namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Common.Repositories;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class TableService : ITableService
    {
        private readonly IDeletableEntityRepository<Table> tableRepositrory;

        public TableService(IDeletableEntityRepository<Table> tableRepositrory)
        {
            this.tableRepositrory = tableRepositrory;
        }

        public async Task<IEnumerable<T>> GetTables<T>()
        {
            var tables = await tableRepositrory.AllAsync();
            return tables.To<T>();
        }

        public async Task ChangeTableStatus(int tableId)
        {
            var tables = await tableRepositrory.AllAsync();
            var table = await tables.FirstAsync(x => x.Id == tableId);
            if (table.Status == GlobalConstants.TableAvailable)
            {
                table.Status = GlobalConstants.TableAvailable;
            }
            else
            {
                table.Status = GlobalConstants.TableUnavailable;
            }
        }
    }
}
