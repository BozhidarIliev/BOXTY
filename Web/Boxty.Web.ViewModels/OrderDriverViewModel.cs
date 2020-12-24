namespace Boxty.Web.ViewModels
{
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;
    using System.Collections.Generic;

    public class OrderDriverViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public List<OrderItemOutputModel> OrderItems { get; set; }

        public string Destination{ get; set; }

        public string Status { get; set; }
    }
}
