namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Data.Common.Repositories;
    using Boxty.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;

    public class TableService : ITableService
    {
        private readonly IRepository<Table> tableRepository;
        private readonly IOrderService orderService;

        public TableService(IRepository<Table> tableRepositrory, IOrderService orderService)
        {
            this.tableRepository = tableRepositrory;
            this.orderService = orderService;
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

        public Table GetTableByOrderId(int orderId)
        {
            var table = GetTableByOrderId(orderId);
            if (table != null)
            {
                return table;
            }
            return null;
        }

        public async Task MakeUnavailable(int tableId)
        {
            var table = GetTableById(tableId); 
            
            if (table.Available == true)
            {
                table.Available = false;
            }
            await tableRepository.SaveChangesAsync();
        }
        public async Task MakeAvailable(int tableId)
        {
            var table = GetTableById(tableId);

            if (table.Available == false)
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
