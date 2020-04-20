namespace Boxty.ViewModels
{
    using System.Collections.Generic;

    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class ShoppingCartViewModel
    {
        public IEnumerable<OrderItem> Items { get; set; }

        public double Total { get; set; }
    }
}
