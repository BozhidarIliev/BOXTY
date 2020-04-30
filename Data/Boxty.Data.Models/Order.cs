namespace Boxty.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Boxty.Data.Common.Models;
    using Boxty.Models;

    public class Order : BaseDeletableModel<int>, ICreatorInfo
    {
        [NotMapped]
        public IEnumerable<OrderItem> Items { get; set; }

        public string Status { get; set; }

        public decimal Total => Items.Sum(x => x.Subtotal);

        public string Destination { get; set; }

        public bool Delivery { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
