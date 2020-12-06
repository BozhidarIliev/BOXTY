namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Data.Common.Repositories;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;

    public class TableService : ITableService
    {
        private readonly IRepository<Table> tableRepository;

        public TableService(IRepository<Table> tableRepositrory)
        {
            this.tableRepository = tableRepositrory;
        }

        public IEnumerable<Table> GetAllTables()
        {
            return tableRepository.All();
        }

        public IEnumerable<Table> GetTablesByNumberOfSeats(int seats)
        {
            return tableRepository.All().Where(x => x.Seats >= seats);
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
            var tables = tableRepository.All();
            var table = tables.FirstOrDefault(x => x.Id == tableId);
            if (table.Available == true)
            {
                table.Available = false;
            }
            else
            {
                table.Available = true;
            }
            await tableRepository.SaveChangesAsync();
        }

        public async Task DeleteTable(int tableId)
        {
            var table = this.GetTableById(tableId);
            this.tableRepository.Delete(table);
            await this.tableRepository.SaveChangesAsync();

        }

        public async Task CreateTable(Table table)
        {
            await this.tableRepository.AddAsync(table);
            await this.tableRepository.SaveChangesAsync();
        }
    }
}
