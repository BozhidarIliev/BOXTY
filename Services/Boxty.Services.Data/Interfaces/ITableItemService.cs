namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Boxty.ViewModels;

    public interface ITableItemService
    {
        Task<IEnumerable<T>> GetPendingItems<T>(int tableId);

        Task AddPendingItem(int tableId, int productId);

        Task RemovePendingItem(int tableId, int productId);

        Task ClearPendingItems(int tableId);
    }
}
