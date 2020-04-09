using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels
{
    public class TableItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }
}
