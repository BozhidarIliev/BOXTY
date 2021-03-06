﻿namespace Boxty.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Boxty.Models;

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Items = new HashSet<OrderItemOutputModel>();
        }

        public IEnumerable<OrderItemOutputModel> Items { get; set; }

        public decimal Total => Items.Sum(x => x.Subtotal);

        public string Status { get; set; }

        public string Destination { get; set; }

        public string Delivery { get; set; }

    }
}
