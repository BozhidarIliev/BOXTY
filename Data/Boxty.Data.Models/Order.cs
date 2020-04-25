namespace Boxty.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Boxty.Data.Common.Models;
    using Boxty.Models;

    public class Order : BaseDeletableModel<int>
    {
        [NotMapped]
        public IEnumerable<OrderItem> Items { get; set; }

        public string Status { get; set; }

        public double Total => Items.Sum(x => x.Subtotal);

        public string Destination { get; set; }

        public bool Delivery { get; set; }
    }
}
