namespace Boxty.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Boxty.Data.Common.Models;
    using Boxty.Models;

    public class Order : BaseDeletableModel<int>, IOrderable
    {
        public IEnumerable<OrderItem> Items { get; set; }

        public string Status { get; set; }

        public double Total => Items.Sum(x => x.Subtotal);

        public string Destination { get; set; }

        public string Delivery { get; set; }
    }
}
