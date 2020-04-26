﻿namespace Boxty.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;

    public class OrderItem : BaseDeletableModel<int>
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Amount { get; set; }

        public double Subtotal => this.Product.Price * this.Amount;

        public string Comment { get; set; } = string.Empty;

        public string Status { get; set; }

    }
}
