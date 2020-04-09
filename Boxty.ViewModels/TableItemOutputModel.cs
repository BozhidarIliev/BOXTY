using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels
{
    public class TableItemOutputModel
    {
        public int TableId { get; set; }
        public IEnumerable<TableItemViewModel> TableItems { get; set; }
    }
}
