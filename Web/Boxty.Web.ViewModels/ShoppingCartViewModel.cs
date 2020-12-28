namespace Boxty.Web.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartViewModel
    {
        public IEnumerable<OrderItemOutputModel> Items { get; set; }

        public decimal Total => Items.Sum(x => x.Subtotal);
    }
}
