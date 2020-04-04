using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetTables();
    }
}
