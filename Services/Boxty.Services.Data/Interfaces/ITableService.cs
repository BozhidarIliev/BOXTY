namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.Models;

    public interface ITableService
    {
        IEnumerable<T> GetTables<T>();

        Table GetTableById(int tableId);

        Task ChangeTableStatus(int tableId);

        Task CreateTable(int tableId);

        void DeleteTable(int tableId);
    }
}
