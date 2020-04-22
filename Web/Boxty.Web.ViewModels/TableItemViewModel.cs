using Boxty.Data.Models;
using Boxty.Models;
using Boxty.Services.Mapping;

namespace Boxty.ViewModels
{
    public class TableItemViewModel : IMapFrom<OrderItem>, IMapTo<OrderItem>, IMapTo<TableItemViewModel>
    {
        public Product Product { get; set; }

        public string Destination { get; set; }

        public int Amount { get; set; }

        public double Subtotal => Product.Price * Amount;

        public string Comment { get; set; } = string.Empty;
    }
}
