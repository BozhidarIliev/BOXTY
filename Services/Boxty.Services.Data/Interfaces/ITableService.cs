﻿namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.Models;

    public interface ITableService
    {
        IEnumerable<Table> GetAllTables();
        IEnumerable<T> GetTables<T>();

        IEnumerable<Table> GetTablesByNumberOfSeats(int seats);

        Table GetTableById(int tableId);

        Task ChangeTableStatus(int tableId);

        Task CreateTable(Table table);

        Task DeleteTable(int tableId);
    }
}
