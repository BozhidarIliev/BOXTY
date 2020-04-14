namespace Boxty.Services.Interfaces
{
    using System.Threading.Tasks;

    using Boxty.ViewModels;

    public interface ITableItemService
    {
        Task<TableItemViewModel[]> GetTableItems(int tableId);

        Task<string> SelectTableItem(TableItemInputModel model);

        Task<string> DeleteSelectedItem(int tableItemId);

        void OrderSelectedItems(int tableId);
    }
}
