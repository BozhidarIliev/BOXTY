namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.Models;

    public interface ITableService
    {
        Task<IEnumerable<Table>> GetTables();
    }
}
