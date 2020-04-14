namespace Boxty.ViewModels
{
    using System.Collections.Generic;

    public class TableItemOutputModel
    {
        public int TableId { get; set; }

        public IEnumerable<TableItemViewModel> TableItems { get; set; }
    }
}
