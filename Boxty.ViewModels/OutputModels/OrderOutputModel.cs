using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels.OutputModels
{
    public class OrderOutputModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public Product Product{ get; set; }
    }
}
