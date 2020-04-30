namespace Boxty.Web.ViewModels
{
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class TableItemViewModel : IMapFrom<OrderItem>, IMapTo<OrderItem>, IMapTo<TableItemViewModel>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string Destination { get; set; }

        public int Amount { get; set; }

        public decimal Subtotal => ProductPrice * Amount;

        public string Comment { get; set; } = string.Empty;

        public string Status { get; set; }
    }
}
