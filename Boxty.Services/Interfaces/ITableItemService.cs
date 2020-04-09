using Boxty.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services.Interfaces
{
    public interface ITableItemService
    {
        Task<TableItemOutputModel> GetTableItems(int tableId);

        Task<string> SelectTableItem(TableItemInputModel model);
        Task<string> DeleteSelectedItem(int tableItemId);
    }
}
