namespace Boxty.Web.ViewModels
{
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;

    public class OrderOutputModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public decimal Total { get; set; }

        public string Destination { get; set; }

        public bool Delivery { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
