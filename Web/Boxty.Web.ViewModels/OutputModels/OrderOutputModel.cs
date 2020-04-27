namespace Boxty.Web.ViewModels
{
    using System;

    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class OrderOutputModel : IMapFrom<OrderItem>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Amount { get; set; }

        public string Comment { get; set; }

        public Product Product { get; set; }

        public bool IsDeleted { get; set; }

        public string Status { get; set; }
    }
}
