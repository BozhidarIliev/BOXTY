namespace Boxty.Web.ViewModels
{
    using System.Collections.Generic;

    using Boxty.Models;

    public class ShoppingCartViewModel
    {
        public IEnumerable<OrderItem> Items { get; set; }

        public decimal Total { get; set; }
    }
}
