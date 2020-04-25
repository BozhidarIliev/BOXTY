using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Mapping;

namespace Boxty.ViewModels
{
    public class TableItemViewModel : IMapFrom<OrderItem>, IMapTo<OrderItem>, IMapTo<TableItemViewModel>
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string Destination { get; set; }

        public int Amount { get; set; }

        public double Subtotal => ProductPrice * Amount;

        public string Comment { get; set; } = string.Empty;
    }
}
