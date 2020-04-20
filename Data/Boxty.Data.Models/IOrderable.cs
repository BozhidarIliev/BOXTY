using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Data.Models
{
    public interface IOrderable
    {
        IEnumerable<OrderItem> Items { get; set; }

        public double Total { get; }

        string Status { get; set; }

        string Destination { get; set; }

        string Delivery { get; set; }
    }
}
