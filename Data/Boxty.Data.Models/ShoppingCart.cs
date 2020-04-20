namespace Boxty.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Boxty.Models;

    public class ShoppingCart : IOrderable 
    {
        public ShoppingCart()
        {
            this.Items = new HashSet<OrderItem>();
        }

        public IEnumerable<OrderItem> Items { get; set; }

        public double Total => Items.Sum(x => x.Subtotal);

        public string Status { get; set; }

        public string Destination { get; set; }

        public string Delivery { get; set; }

    }
}
