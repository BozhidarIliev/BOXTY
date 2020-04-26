namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly IRepository<Table> tableRepository;

        public TableService(IRepository<Table> tableRepositrory)
        {
            this.tableRepository = tableRepositrory;
        }

        public IEnumerable<T> GetTables<T>()
        {
            var tables = tableRepository.All();
            return tables.To<T>();
        }

        public Table GetTableById(int tableId)
        {
            var tables = tableRepository.All();
            return tables.FirstOrDefault(x => x.Id == tableId);
        }

        public async Task ChangeTableStatus(int tableId)
        {
            var tables = await tableRepository.AllAsync();
            var table = tables.FirstOrDefault(x => x.Id == tableId);
            if (table.Available == true)
            {
                table.Available = false;
            }
            else
            {
                table.Available = true;
            }
        }

        public void DeleteTable(int tableId)
        {
            var table = this.GetTableById(tableId);
            this.tableRepository.Delete(table);
        }

        public async Task CreateTable(int id)
        {
            var table = new Table
            {
                Id = id,
            };
            await this.tableRepository.AddAsync(table);
        }
    }
}
